using System.ComponentModel.Design;
using System.Diagnostics;
using Plugin.Maui.Audio;
using PT_App.Server;
namespace PT_App.TESTEN;

public partial class IgangLFU : ContentPage
{

    private int _testCount;
    private PatientData _patientData;
    private int _maxTest = 2;
    private RunClient runClient;
 


    public IgangLFU()
    {

    }

    public IgangLFU(string cprNumber, int testCount)
    {
        BindingContext = this;
        InitializeComponent();
        OnAppearing();
        _testCount = testCount;
        _patientData = new PatientData { CPR = cprNumber };
        LoadAndDisplayData(cprNumber);
        CheckTestCount();

    }
    private async void LoadAndDisplayData(string cprNumber)
    {
        try
        {
            // Hent data fra databasen
            var m�linger = await App.Database.GetPatientM�lingerByCPRAsync(cprNumber);

            // Hent den nyeste m�ling
            var nyesteM�ling = m�linger
                .Where(m => m.CPR == cprNumber)
                .OrderByDescending(m => DateTime.Parse(m.Dato))
                .FirstOrDefault();

            if (nyesteM�ling != null)
            {
                // Parse v�rdier
                double fvc = double.Parse(nyesteM�ling.FCV);
                double fev1 = double.Parse(nyesteM�ling.FEV1);
                double ratio = (fev1 / fvc) * 100.0;

                // Opdater FVC
                FVCProgressBar.Progress = fvc / 6.0; // Antag maksimal v�rdi som 6.0
                FVCLabel.Text = $"{fvc:F2} L";

                // Opdater FEV1
                FEV1ProgressBar.Progress = fev1 / 5.0; // Antag maksimal v�rdi som 5.0
                FEV1Label.Text = $"{fev1:F2} L";

                // Opdater Ratio
                RatioProgressBar.Progress = ratio / 100.0; // Ratio er i procent
                RatioLabel.Text = $"{ratio:F2} %";
            }
            else
            {
                await DisplayAlert("Ingen data", "Ingen m�linger fundet for dette CPR-nummer.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fejl", $"Kunne ikke hente data: {ex.Message}", "OK");
        }
    }

    private void CheckTestCount()
    {

        if (_testCount < _maxTest)
        {
            NextTestButton.IsVisible = true;
        }
        else
        {
            AfslutButton.IsVisible = true;
        }
    }

    private async void OnAfslutButtonClicked(object sender, EventArgs e)
    {

        // if test valid --> startside
       
        // Naviger tilbage til startsiden
        await Navigation.PushAsync(new Startside(_patientData));

        // if test invalid --> testcount--, new teststart
    }

    private async void OnNextTestButtonClicked(object sender, EventArgs e)
    {

        _testCount++;
        await Navigation.PushAsync(new TestStart(_patientData,_testCount));
    }

}