using EventsItAcademy.Application.Roles;
using EventsItAcademy.Domain.Users;
using EventsItAcademy.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace EventsItAcademy.WEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<User> _userManager;

        public AdminController(IRoleService roleService, UserManager<User> userManager)
        {
            _roleService = roleService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {

            XDocument xdoc = XDocument.Load("SettingsSetByAdmin.xml");
            if(xdoc == null)
            {
                return View(new SettingsViewModel());
            }
            XNamespace ns = xdoc.Root.GetDefaultNamespace();

            XElement eventEditLimitDays = xdoc.Descendants(ns + "EventEditLimitDays").FirstOrDefault();
            XElement eventBookingPeriod = xdoc.Descendants(ns + "EventBookingPeriod").FirstOrDefault();

            //var limitDays = xSettings.Document.Element("EventEditLimitDays");

            var settingsViewModel = new SettingsViewModel() { 
                EventEditLimitDays = int.Parse(eventEditLimitDays.Value) ,
                EventBookingPeriod = int.Parse(eventBookingPeriod.Value) 
            };

            return View(settingsViewModel);
            //return View(new SettingsViewModel());
        }
        public async Task<IActionResult> EditRole( string id, CancellationToken cancellationToken = default)
        {
            var role = await _roleService.GetById(cancellationToken, id);

            ViewBag.roleId=role.Id;
            ViewBag.roleName=role.Name;

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            var model = new List<UserRoleViewModel>();
            foreach(var user in usersInRole)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                model.Add(userRoleViewModel);
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult>EditUsersInRole(string roleId, CancellationToken cancellationToken = default)
        {
            ViewBag.roleId = roleId;

            var role = await _roleService.GetById(cancellationToken, roleId);

            var model = new List<UserRoleViewModel>();

            foreach(var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName

                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        public async Task<IActionResult>ListRoles(CancellationToken cancellationToken=default)
        {
            var roles = await _roleService.GetAllAsync(cancellationToken);

            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId, CancellationToken cancellationToken=default)
        {
            var role = await _roleService.GetById(cancellationToken, roleId);

            for(int i = 0; i < model.Count; i++)
            {
               var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { id = roleId });
                }
            }
            return RedirectToAction("EditRole", new {id=roleId});
        }

        [HttpPost]
        public IActionResult SetSettings(SettingsViewModel model)
        {
            XDocument xdoc = XDocument.Load("SettingsSetByAdmin.xml");
            XNamespace ns = xdoc.Root.GetDefaultNamespace();

            XElement eventEditLimitDays = xdoc.Descendants(ns + "EventEditLimitDays").FirstOrDefault();
            eventEditLimitDays.Value = model.EventEditLimitDays.ToString();

            XElement eventBookingPeriod = xdoc.Descendants(ns + "EventBookingPeriod").FirstOrDefault();
            eventBookingPeriod.Value = model.EventBookingPeriod.ToString();
            xdoc.Save("SettingsSetByAdmin.xml");

            return RedirectToAction("index");
        }
    }
}
