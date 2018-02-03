using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWF.Models
{
    public class Engineer
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class EngineerShift : Engineer
    {
        public List<DateTime> WorkDays { get; set; }

        public bool IsWorkloadFull
        {
            get
            {
                return WorkDays !=null && WorkDays.Count >= 2;
            }
        }
    }
}