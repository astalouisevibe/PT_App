using System.Runtime.CompilerServices;
using PT_App.TESTEN;

namespace PT_App;

public partial class LFUstart : ContentPage
{
	private PatientData _patientData;

	public LFUstart(string cprNumber)
	{
		InitializeComponent();
        _patientData = new PatientData { CPR = cprNumber };

    }

    private async void OnReturnButton(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

    private async void OnTestStart(object sender, EventArgs e)
    {
      
        await Navigation.PushAsync(new TestStart(_patientData,0)); // Pass the PatientData object
    }

 
}

