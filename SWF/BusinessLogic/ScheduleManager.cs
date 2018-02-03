using SWF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWF.BusinessLogic
{
    public static class ScheduleManager
    {
        /// <summary>
        /// assume 10 engineers
        /// </summary>
        /// <returns></returns>
        public static List<EngineerShift> InitEngineersShift(int numOfEng = 10)
        {
            var list = new List<EngineerShift>();
            for (int i = 1; i <= numOfEng; i++)
            {
                list.Add(new EngineerShift() { Name = "Engineer" + i, Id = i, WorkDays = new List<DateTime>() });
            }
            return list;
        }


        /// <summary>
        /// Assume always has monday as first day
        /// </summary>
        /// <param name="beginDate"></param>
        //todo: need to validate or accept any date 
        public static void Set2WeekSchedule(List<EngineerShift> engineers, DateTime beginDate)
        {
            var endDate = beginDate.AddDays(14);
            var random = new Random();
            //loop throught all days(2 weeks)
            for (var i = 0; i < 14; i++)
            {
                var date = beginDate.AddDays(i);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    //half day for each, loop twice/day to randomly set shift
                    for (var j = 0; j < 2; j++)
                    {
                        //pick unsigned first, then second round to avoid dead end
                        var subset = engineers.Where(e => !e.WorkDays.Any());
                        if (!subset.Any())
                        {
                          subset = engineers.Where(e=> !e.IsWorkloadFull && e.WorkDays.Any(d => d < date.AddDays(-1)));
                        }
                        var rnd = random.Next(1, subset.Count() + 1);
                        var shift = subset.Take(rnd).Last();
                        shift.WorkDays.Add(date);
                    }
                }
            }
        }
    }
}