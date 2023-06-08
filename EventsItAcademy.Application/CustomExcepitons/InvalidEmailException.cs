using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsItAcademy.Application.CustomExcepitons
{
    public class InvalidEmailException:Exception
    {
        public string code = "InvalidEmailException";
        public InvalidEmailException(string message) : base(message)
        {

        }
    }
}
