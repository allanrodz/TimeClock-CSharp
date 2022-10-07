using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public class dateGrabbing
    {
        public string dateGrabber()
        {

            string todayDate = DateTime.UtcNow.ToShortDateString();

            return todayDate;


        }
    }
}
