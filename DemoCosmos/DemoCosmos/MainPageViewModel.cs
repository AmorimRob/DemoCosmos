using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoCosmos
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly string URL = "https://rsamorim.documents.azure.com:443/";
        private readonly string READ_ONLY_KEY = "f4RFVaWaQe1oLp0APYxt0Kkq1pynXSycJ6BNEHEhCQoUZUFcQ5bZtC61oP44ovXqQyXiSVhsplN4oKoKkUDI9Q==";
        private readonly string READ_WRITE_KEY = "AIMIFMulSenv9H3tHQfdqa3JY4FK48RgW06C44hHrSBLxEsq9LN60sjYP74mboP3nUG3uB13BBw4uiNoK8yFrA==";
        private readonly Uri DOCUMENT_COLLETCTION_URI;

        public readonly DocumentClient _documentClient;

        private ObservableRangeCollection<Club> clubs;

        public ObservableRangeCollection<Club> ClubsList
        {
            get { return clubs; }
            set { clubs = value; OnPropertyChanged(); }
        }

        public ICommand AddNewClubCmd { get; set; }

        public MainPageViewModel()
        {
            DOCUMENT_COLLETCTION_URI = UriFactory.CreateDocumentCollectionUri("SampleCosmos", "Clubs");
            _documentClient = new DocumentClient(new Uri(URL), READ_ONLY_KEY);

            AddNewClubCmd = new Command(() =>
            {
                App.Current.MainPage.Navigation.PushAsync(new ClubPage(new Club()));
            });

            GetClubs();
        }

        public void GetClubs()
        {
            ClubsList = new ObservableRangeCollection<Club>();
            var clubs = _documentClient.CreateDocumentQuery<Club>(DOCUMENT_COLLETCTION_URI).Where(x => x.Name != string.Empty).ToList();
            ClubsList.AddRange(clubs);
        }
    }
}
