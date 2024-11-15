namespace PT_App.TESTEN;

public partial class IgangLFU : ContentPage
{
    private string _currentCPR;
    private int _testCount;
    private PatientData _patientData;
    private int MaxTests = 2;
    public IgangLFU(string cprNumber, int testCount)
    {
        InitializeComponent();
       _currentCPR = cprNumber;
        _testCount = testCount;
        _patientData = new PatientData { CPR = cprNumber };
        SetFixedProgressValues();
        CheckTestCount();
    }
    private async void SetFixedProgressValues()
    {
        Random random = new Random();
        double fvcProcessValue = Math.Round(3 + random.NextDouble() * (6 - 3), 2);
        double fev1ProcessValue = Math.Round(2.5 + random.NextDouble() * (5 - 2.5 ), 2);

       
        // Opdater progressbar og label for FVC
        FVCProgressBar.Progress = fvcProcessValue / 6.0;
        FVCLabel.Text = $"{fvcProcessValue } L";

        // Opdater progressbar og label for FEV1
        FEV1ProgressBar.Progress = fev1ProcessValue/ 5.0;
        FEV1Label.Text = $"{fev1ProcessValue } L";

        double ratio = Math.Round((fev1ProcessValue / fvcProcessValue)*100,2);
        RatioProgressBar.Progress = ratio /100;
        RatioLabel.Text = $"{ratio:F2} %";

        await SaveLungFunctionValues(_currentCPR, fvcProcessValue, fev1ProcessValue, ratio);
    }
    public async Task SaveLungFunctionValues(string cprNumber, double fev1ProcessValue, double fcvProcessValue, double ratio)
    {
        var maaling = new PatientMålinger()
        {
            CPR = cprNumber,
            Dato = DateTime.Now.ToString(),
            FCV = fcvProcessValue.ToString(),
            FEV1 = fev1ProcessValue.ToString(),
            Ratio = ratio.ToString()
        };
        await App.Database.UpdateMålingerAsync(maaling);

    }

    private  void CheckTestCount()
    {

        if (_testCount < MaxTests)
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
        // Naviger tilbage til startsiden
        await Navigation.PushAsync(new Startside(_patientData));
    }

    private async void OnNextTestButtonClicked(object sender, EventArgs e)
    {
        // Navigate to the next test
        _testCount++;
        await Navigation.PushAsync(new TestStart(_patientData,_testCount));
    }

}