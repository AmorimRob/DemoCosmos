using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoCosmos
{
    public class ClubPageViewModel : BaseViewModel
    {

        private Club _club;

        public Club Club
        {
            get { return _club; }
            set { _club = value; OnPropertyChanged(); }
        }

        private bool _isAdding;

        public bool IsAdding
        {
            get { return _isAdding; }
            set { _isAdding = value; OnPropertyChanged(); }
        }

        private bool _isUpdating;

        public bool IsUpdating
        {
            get { return _isUpdating; }
            set { _isUpdating = value; OnPropertyChanged(); }
        }

        public ICommand SaveCmd { get; set; }
        public ICommand UpdateCmd { get; set; }

        public ClubPageViewModel(Club club)
        {
            Club = club;


            IsAdding = string.IsNullOrEmpty(club.id);
            IsUpdating = !string.IsNullOrEmpty(club.id);

            SaveCmd = new Command(async () =>
            {
                if (true)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    await App.Current.MainPage.DisplayAlert("Xamarin Summit 2018", "Save success", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Xamarin Summit 2018", "Save fail", "OK");
            });

            UpdateCmd = new Command(async () =>
            {
                if (true)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    await App.Current.MainPage.DisplayAlert("Xamarin Summit 2018", "Update success", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Xamarin Summit 2018", "Update fail", "OK");
            });
        }
    }
}
