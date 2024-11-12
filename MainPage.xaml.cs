using SQLite;

namespace PT_App
{
    public partial class MainPage : ContentPage
    {
        private PatientService _patientService;
        private CPRValidator _validator;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string cprNumber = CPRNumberEntry.Text;

            // CPR-validering
            if (!_validator.IsValid(cprNumber))
            {
                await DisplayAlert("Ugyldigt CPR", "CPR-nummeret skal være 10 cifre.", "OK");
                return;
            }

            // Søgning efter patient i databasen
            var patientData = await _patientService.FindPatientInDatabase(cprNumber);

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
    }
}
