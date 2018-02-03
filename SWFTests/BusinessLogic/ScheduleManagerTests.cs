using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWF.BusinessLogic;
using SWF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWF.BusinessLogic.Tests
{
    [TestClass()]
    public class ScheduleManagerTests
    {
        [TestMethod()]
        public void Set2WeekScheduleTest()
        {
            var shifts = ScheduleManager.InitEngineersShift();
            ScheduleManager.Set2WeekSchedule(shifts, new DateTime(2018, 02, 05));

            Assert.IsTrue(WorkloadCheck(shifts));
            Assert.IsTrue(NonConsecutiveWorkload(shifts));
        }

        private bool NonConsecutiveWorkload(List<EngineerShift> shifts)
        {
            bool allGood = true;
            foreach (var shift in shifts)
            {
                int days = shift.WorkDays.Count();
                for (var i = 0; i < days; i++)
                {
                    if (i >= days - 1) break;
                    TimeSpan diff = shift.WorkDays[i + 1] - shift.WorkDays[i];
                    if (diff.Days <= 1) //also check if work in same day(diff = 0)
                    {
                        allGood = false;
                        break;
                    }
                }
                if (!allGood) break;

            }

            return allGood;
        }

        /// <summary>
        /// Check if everyone has one full day work
        /// </summary>
        /// <param name="shifts"></param>
        /// <returns></returns>
        private bool WorkloadCheck(List<EngineerShift> shifts)
        {
            bool allGood = true;
            foreach (var shift in shifts)
            {
                if (shift.WorkDays.Count() < 2)
                {
                    allGood = false;
                    break;
                }
            }

            return allGood;
        }


    }
}