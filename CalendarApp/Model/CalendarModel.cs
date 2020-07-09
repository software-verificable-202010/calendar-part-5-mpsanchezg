using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Model
{
	public class CalendarModel
	{
		public CalendarModel(int year, int monthNumber)
		{
			CalendarMonth = new CalendarMonthModel(monthNumber, year);
		}

		#region Properties
		public CalendarMonthModel CalendarMonth
		{
			get; set;
		}

		#endregion

	}
}
