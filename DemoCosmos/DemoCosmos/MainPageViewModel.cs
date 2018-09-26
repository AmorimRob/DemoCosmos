using Microsoft.Azure.Documents.Client;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoCosmos
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableRangeCollection<Club> clubs;

        public ObservableRangeCollection<Club> ClubsList
        {
            get { return clubs; }
            set { clubs = value; OnPropertyChanged(); }
        }

        public ICommand AddNewClubCmd { get; set; }

        public MainPageViewModel()
        {
            AddNewClubCmd = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new ClubPage(new Club()));
            });

            GetClubs();
        }

        public void GetClubs()
        {
            ClubsList = new ObservableRangeCollection<Club>();
        }
    }
}
