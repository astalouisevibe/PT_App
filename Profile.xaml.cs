using System.Xml;

namespace PT_App
{
    public partial class Profile : ContentPage
    {
        private PatientData _patientData;

        public Profile(string cprNumber)
        {
            InitializeComponent();
            _patientData = new PatientData { CPR = cprNumber }; // Initialiserer _patientData
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadPatientData(); 
        }
        private async Task LoadPatientData()
        {
            _patientData = await App.Database.GetPatientByCPRAsync(_patientData.CPR);
            await LoadProfile(_patientData.CPR); // Indlæs profildata også

        }
  

        private async Task LoadProfile(string cprNumber)
        {
            _patientData = await App.Database.GetPatientByCPRAsync(cprNumber);
            if (_patientData != null)
            {
                GenderPicker.SelectedItem = _patientData.Køn;
                HeightPicker.SelectedItem = _patientData.Højde;
                WeightPicker.SelectedItem = _patientData.Vægt;
                EthnicityPicker.SelectedItem = _patientData.Etnicitet;

                GenderPicker.IsEnabled = false;
                HeightPicker.IsEnabled = false;
                WeightPicker.IsEnabled = false;
                EthnicityPicker.IsEnabled = false;
            }
            else
            {
                await DisplayAlert("Fejl", "Patientdata blev ikke fundet.", "OK");
            }
        }

        private async void SaveProfile()
        {
            _patientData.Køn = GenderPicker.SelectedItem?.ToString();
            _patientData.Højde = HeightPicker.SelectedItem?.ToString();
            _patientData.Vægt = WeightPicker.SelectedItem?.ToString();
            _patientData.Etnicitet = EthnicityPicker.SelectedItem?.ToString();

            await App.Database.UpdatePatientAsync(_patientData);
           
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            GenderPicker.IsEnabled = true;
            HeightPicker.IsEnabled = true;
            WeightPicker.IsEnabled = true;
            EthnicityPicker.IsEnabled = true;

            Ændre.IsVisible = false;
            Gem.IsVisible = true;
        }

        private async void Gem_Clicked(object sender, EventArgs e)
        {
            SaveProfile(); // Sørg for, at gemme metoden er asynkron

            GenderPicker.IsEnabled = false;
            HeightPicker.IsEnabled = false;
            WeightPicker.IsEnabled = false;
            EthnicityPicker.IsEnabled = false;

            Ændre.IsVisible = true;
            Gem.IsVisible = false;

            await DisplayAlert("Success", "Profil gemt!", "OK");
        }

        private async void OnReturnButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
