using System.Xml;
using static PT_App.MainPage;

namespace PT_App
{
    public partial class Startside : ContentPage
    {
        private PatientData _patientData;
        private string _cprNumber;

        public Startside(PatientData patientData)
        {
            InitializeComponent();
            _patientData = patientData;
            _cprNumber = patientData.CPR;

            BindPatientData();

        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPatientData(); // Hent patientdata n�r siden vises
            if (string.IsNullOrWhiteSpace(_patientData.K�n) ||
                 string.IsNullOrWhiteSpace(_patientData.H�jde) ||
                 string.IsNullOrWhiteSpace(_patientData.Etnicitet) ||
              string.IsNullOrWhiteSpace(_patientData.V�gt))
            {
                // Vis popup-besked
                await DisplayAlert("Husk", "Husk at opdatere personoplysninger", "OK");
            }
        }

        private async Task LoadPatientData()
        {
            _patientData = await App.Database.GetPatientByCPRAsync(_cprNumber); // CPRNumber skal v�re defineret
            BindPatientData(); // Bind data til UI
        }


    private void BindPatientData()
        {
            CPRLabel.Text = _patientData.CPR;
            NameLabel.Text = _patientData.Name;
            AlderLabel.Text =$"{_patientData.Alder} �r" ;

        }

        private async void OnLFUstartButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LFUstart(_cprNumber));
        }

        private async void OnProfileButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Profile(_patientData.CPR)); 

        }


        private async void OnHistorikButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Historik(_cprNumber));
        }
        private async void OnL�geButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new L�gePage());
        }

        private async void OnLogbogButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LogbogPage());
        }

        
    }
}
