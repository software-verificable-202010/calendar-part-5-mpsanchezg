using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CalendarApp.Model
{
	public class CalendarDayModel
	{
		#region Private Variables

		private DateTime date;
		private string color;

		#endregion
		
		public CalendarDayModel(DateTime date, string color)
		{
			Date = date;
			Color = color;
		}

		#region Properties
		
		public DateTime Date
		{
			get => date;
			set => date = value;
		}
		public string Color
		{
			get => color;
			set => color = value;
		}

		#endregion

		#region Public Methods

		public override bool Equals(object obj)
		{
			return Equals(obj as CalendarDayModel);
		}
		public override int GetHashCode()
		{
			return 0;
		}

		public bool Equals(CalendarDayModel calendarDay)
		{
			if (calendarDay == null)
			{
				return false;
			}
			return Date.Date == calendarDay.Date.Date && Color == calendarDay.color;
		}

		#endregion

	}
}
