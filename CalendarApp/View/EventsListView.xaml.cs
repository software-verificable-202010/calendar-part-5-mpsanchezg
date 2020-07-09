using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalendarApp.View
{
	/// <summary>
	/// Lógica de interacción para EventsListView.xaml
	/// </summary>
	public partial class EventsListView : UserControl
	{
		public EventsListView()
		{
			InitializeComponent();
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

			// No cargue datos en tiempo de diseño.
			// if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			// {
			// 	//Cargue los datos aquí y asigne el resultado a CollectionViewSource.
			// 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
			// 	myCollectionViewSource.Source = your data
			// }
			// No cargue datos en tiempo de diseño.
			// if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
			// {
			// 	//Cargue los datos aquí y asigne el resultado a CollectionViewSource.
			// 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
			// 	myCollectionViewSource.Source = your data
			// }
		}
	}
}
