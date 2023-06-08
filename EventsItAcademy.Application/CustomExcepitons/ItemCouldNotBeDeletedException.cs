using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.CustomExcepitons
{
    public class ItemCouldNotBeDeletedException:Exception
    {
        public string code = "ItemCouldNotBeDeletedException";
        public ItemCouldNotBeDeletedException(string message):base(message)
        {

        }
    }
}
