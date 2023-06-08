using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.CustomExcepitons
{
    public class InvalidPasswordException:Exception
    {
        public string code = "InvalidPasswordException";
        public InvalidPasswordException(string message) : base(message)
        {

        }
    }
}
