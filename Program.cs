using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PFGA_Membership
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmParent());
        }

        ///<summary>Gets the first week day following a date.</summary>
        ///<param name="date">The date.</param>
        ///<param name="dayOfWeek">The day of week to return.</param>
        ///<returns>The first dayOfWeek day following date, or date if it is on dayOfWeek.</returns>
        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            return date.AddDays((dayOfWeek < date.DayOfWeek ? 7 : 0) + dayOfWeek - date.DayOfWeek);
        }

        public static DateTime GetNthWeekofMonth(DateTime date, int nthWeek, DayOfWeek dayOfWeek)
        {
            return date.Next(dayOfWeek).AddDays((nthWeek - 1) * 7);
        }

        public static DateTime getNextTuesday()
        {
            DateTime retVal = DateTime.Today;
            DateTime startFrom = DateTime.Today;

            if (startFrom.Day >= 7 || startFrom.DayOfWeek > DayOfWeek.Tuesday)
            {
                startFrom = startFrom.AddMonths(1);
                startFrom = DateTime.Parse(string.Format("{0}-{1}-01", startFrom.Year, startFrom.Month));
            }

            retVal = GetNthWeekofMonth(startFrom, 1, DayOfWeek.Tuesday);

            return retVal;

        }
    }
}
