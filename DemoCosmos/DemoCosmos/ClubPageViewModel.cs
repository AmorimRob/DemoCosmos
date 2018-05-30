using Microsoft.Azure.Documents.Client;
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
        private readonly string URL = "https://rsamorim.documents.azure.com:443/";
        private readonly string READ_ONLY_KEY = "f4RFVaWaQe1oLp0APYxt0Kkq1pynXSycJ6BNEHEhCQoUZUFcQ5bZtC61oP44ovXqQyXiSVhsplN4oKoKkUDI9Q==";
        private readonly string READ_WRITE_KEY = "AIMIFMulSenv9H3tHQfdqa3JY4FK48RgW06C44hHrSBLxEsq9LN60sjYP74mboP3nUG3uB13BBw4uiNoK8yFrA==";
        private readonly Uri DOCUMENT_COLLETCTION_URI;

        public readonly DocumentClient _documentClient;

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

            DOCUMENT_COLLETCTION_URI = UriFactory.CreateDocumentCollectionUri("SampleCosmos", "Clubs");
            _documentClient = new DocumentClient(new Uri(URL), READ_WRITE_KEY);

            IsAdding = string.IsNullOrEmpty(club.id);
            IsUpdating = !string.IsNullOrEmpty(club.id);

            SaveCmd = new Command(async () =>
            {
                var response = await _documentClient.CreateDocumentAsync(DOCUMENT_COLLETCTION_URI, Club);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    await App.Current.MainPage.DisplayAlert("Xamarin Summit 2018", "Save success", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Xamarin Summit 2018", "Save fail", "OK");
            });

            UpdateCmd = new Command(async () =>
            {
                var response = await _documentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri("SampleCosmos", "Clubs", club.id), Club);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
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
