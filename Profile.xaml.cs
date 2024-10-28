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
            await LoadProfile(_patientData.CPR); // Indl�s profildata ogs�

        }
  

        private async Task LoadProfile(string cprNumber)
        {
            _patientData = await App.Database.GetPatientByCPRAsync(cprNumber);
            if (_patientData != null)
            {
                GenderPicker.SelectedItem = _patientData.K�n;
                HeightPicker.SelectedItem = _patientData.H�jde;
                WeightPicker.SelectedItem = _patientData.V�gt;
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
            _patientData.K�n = GenderPicker.SelectedItem?.ToString();
            _patientData.H�jde = HeightPicker.SelectedItem?.ToString();
            _patientData.V�gt = WeightPicker.SelectedItem?.ToString();
            _patientData.Etnicitet = EthnicityPicker.SelectedItem?.ToString();

            await App.Database.UpdatePatientAsync(_patientData);
           
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            GenderPicker.IsEnabled = true;
            HeightPicker.IsEnabled = true;
            WeightPicker.IsEnabled = true;
            EthnicityPicker.IsEnabled = true;

            �ndre.IsVisible = false;
            Gem.IsVisible = true;
        }

        private async void Gem_Clicked(object sender, EventArgs e)
        {
            SaveProfile(); // S�rg for, at gemme metoden er asynkron

            GenderPicker.IsEnabled = false;
            HeightPicker.IsEnabled = false;
            WeightPicker.IsEnabled = false;
            EthnicityPicker.IsEnabled = false;

            �ndre.IsVisible = true;
            Gem.IsVisible = false;

            await DisplayAlert("Success", "Profil gemt!", "OK");
        }

        private async void OnReturnButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
