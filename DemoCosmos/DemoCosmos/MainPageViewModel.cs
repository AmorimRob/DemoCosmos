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
        private readonly string URL = "https://rsamorim.documents.azure.com:443/";
        private readonly string READ_WRITE_KEY = "gVfE2Gc4AznzfUBUArDdSmSwoevyzemGbIHyhJZe48BQobgagNITDcQ3Te1KycAkk8MC6UR4tAsi9VLKdjcekA==";
        private readonly string READ_ONLY_KEY = "6EgWliigrdQWj0Fx29z6XEWlbFbHukIcieCYiFYz1ZcfAXWxBC5VetwUP9WSvKARSjKs1jVPfCy3kI4adw9VaA==";

        private readonly Uri DOCUMENT_COLLECTION_URI;
        private DocumentClient _documentClient;

        private ObservableRangeCollection<Club> clubs;

        public ObservableRangeCollection<Club> ClubsList
        {
            get { return clubs; }
            set { clubs = value; OnPropertyChanged(); }
        }

        public ICommand AddNewClubCmd { get; set; }

        public MainPageViewModel()
        {
            _documentClient = new DocumentClient(new Uri(URL), READ_ONLY_KEY);
            DOCUMENT_COLLECTION_URI = UriFactory.CreateDocumentCollectionUri("SampleCosmos", "Clubs");

            AddNewClubCmd = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new ClubPage(new Club()));
            });

            GetClubs();
        }

        public void GetClubs()
        {
            ClubsList = new ObservableRangeCollection<Club>();
            var clubs = _documentClient.CreateDocumentQuery<Club>(DOCUMENT_COLLECTION_URI).Where(c => c.Name != string.Empty).ToList();
            ClubsList.AddRange(clubs);
        }
    }
}
