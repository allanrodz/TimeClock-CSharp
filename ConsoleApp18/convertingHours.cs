using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    public class convertingHours
    {
        public decimal convertHours(string startTime, string endTime)
        {
            decimal totalHours = 0;
            string st = startTime.Replace(':', ',');
            string et = endTime.Replace(':', ',');
            decimal startHour = Convert.ToDecimal(st);
            decimal endHour = Convert.ToDecimal(et);
            totalHours = endHour - startHour;
            return totalHours;
        }
    }
}
