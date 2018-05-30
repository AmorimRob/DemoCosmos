using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoCosmos
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = new MainPageViewModel();
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var club = e.SelectedItem as Club;
            App.Current.MainPage.Navigation.PushAsync(new ClubPage(club));
        }
    }
}
