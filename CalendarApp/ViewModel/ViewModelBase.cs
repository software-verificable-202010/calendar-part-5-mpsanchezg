using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.ViewModel
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		protected void NotifyPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		}

		public event PropertyChangedEventHandler PropertyChanged;
    }
}
