namespace PT_App.TESTEN;

public partial class TestStart : ContentPage
{
    private PatientData _patientData;
    private int _testCount;

    public TestStart(PatientData patientData, int testCount) // Vi modtager nu testCount her
    {
        InitializeComponent();
        _patientData = patientData;
        _testCount = testCount; // Brug den aktuelle værdi af testCount
    }

    private async void OnPåbegyndLFUButton(object sender, EventArgs e)
    {
        string cprNumber = _patientData.CPR; // Hent CPR fra patientdata
        await Navigation.PushAsync(new IgangLFU(cprNumber, _testCount)); // Sender CPR til IgangLFU
    }
}
