using SQLite;

namespace PT_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string cprNumber = CPRNumberEntry.Text;

            // CPR-validering
            if (string.IsNullOrWhiteSpace(cprNumber) || cprNumber.Length != 10 || !long.TryParse(cprNumber, out _))
            {
                await DisplayAlert("Ugyldigt CPR", "CPR-nummeret skal være 10 cifre.", "OK");
                return;
            }

            // Søgning efter patient i databasen
            var patientData = await FindPatientInDatabase(cprNumber);

            if (patientData != null)
            {
                // Navigér til patientdetaljesiden hvis patienten findes
                await Navigation.PushAsync(new Startside(patientData));
            }
            else
            {
                // Vis en besked, hvis patienten ikke findes
                await DisplayAlert("Ikke fundet", "Patienten blev ikke fundet i databasen.", "OK");
            }
        }

        // Metode til at søge efter patient i databasen
        private async Task<PatientData> FindPatientInDatabase(string cprNumber)
        {
            return await App.Database.GetPatientByCPRAsync(cprNumber);
        }
    }

    // PatientData-klassen defineret separat, men kan også placeres i en anden fil
    public class PatientData
    {
        [PrimaryKey]
        public string CPR { get; set; }
        public string Name { get; set; }
        public string Alder { get; set; }
        public string Køn { get; set; }
        public string Højde { get; set; }
        public string Vægt { get; set; }
        public string Etnicitet { get; set; }
        public string Dato { get; set; }
        public string FCV { get; set; }
        public string FEV1 { get; set; }

    }
    public class PatientMålinger
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CPR { get; set; }
        public string Dato { get; set; }
        public string FCV { get; set; }
        public string FEV1 { get; set; }
    }
}
