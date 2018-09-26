using Microsoft.Azure.Documents.Client;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoCosmos
{
    public class ClubPageViewModel : BaseViewModel
    {
        private readonly string URL = "https://rsamorim.documents.azure.com:443/";
        private readonly string READ_WRITE_KEY = "gVfE2Gc4AznzfUBUArDdSmSwoevyzemGbIHyhJZe48BQobgagNITDcQ3Te1KycAkk8MC6UR4tAsi9VLKdjcekA==";
        private readonly string READ_ONLY_KEY = "6EgWliigrdQWj0Fx29z6XEWlbFbHukIcieCYiFYz1ZcfAXWxBC5VetwUP9WSvKARSjKs1jVPfCy3kI4adw9VaA==";

        private readonly Uri DOCUMENT_COLLECTION_URI;
        private DocumentClient _documentClient;


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

            _documentClient = new DocumentClient(new Uri(URL), READ_WRITE_KEY);
            DOCUMENT_COLLECTION_URI = UriFactory.CreateDocumentCollectionUri("SampleCosmos", "Clubs");

            IsAdding = string.IsNullOrEmpty(club.id);
            IsUpdating = !string.IsNullOrEmpty(club.id);

            SaveCmd = new Command(async () =>
            {
                Club.Name = await TransformClubName(Club.Name);
                var response = await _documentClient.CreateDocumentAsync(DOCUMENT_COLLECTION_URI, Club);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    await App.Current.MainPage.DisplayAlert("SFCI2018", "Save success", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("SFCI2018", "Save fail", "OK");
            });

            UpdateCmd = new Command(async () =>
            {
                var response = await _documentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri("SampleCosmos", "Clubs", club.id), Club);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    await App.Current.MainPage.DisplayAlert("SFCI2018", "Update success", "OK");
                }
                else
                    await App.Current.MainPage.DisplayAlert("SFCI2018", "Update fail", "OK");
            });
        }

        public async Task<string> TransformClubName(string clubName)
        {
            var url = $"https://scfifunction.azurewebsites.net/" +
                $"api/SCIFunction?code=FCpza1aE5BalrqnTl7vvXsOM0vEt2AM7rtOZSs8IbrlAbHea3KXVlA==&name={clubName}";

            var client = new HttpClient();
            try
            {
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<string>(result);
            }
            catch (Exception)
            {
                return "Error";
            }
        }
    }
}
