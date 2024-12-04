using Microsoft.Maui.Controls;
using System.Net.Sockets;
using Plugin.Maui.Audio;
using PT_App.Server;

namespace PT_App.TESTEN;

public partial class TestStart : ContentPage
{
    private PatientData _patientData;
    private int _testCount;
    private Audio _audio;
    private static RunClient runClient;

    public TestStart(PatientData patientData, int testCount) 
    {
        InitializeComponent();
        _patientData = patientData;
        _testCount = testCount; 
        if (runClient == null)
        {
            runClient = new RunClient();
        }

     
    }
    private async void OnPåbegyndLFUButton(object sender, EventArgs e)
    {

        try
        {
            string cprNumber = _patientData.CPR;
           

            runClient.Running(cprNumber);
            // Send "start" besked til serveren        
            await Navigation.PushAsync(new IgangLFU(cprNumber, _testCount));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fejl", $"Kunne ikke sende besked: {ex.Message}", "OK");
        }
    }

    private void Playsound()
    {
        _audio = new Audio(new AudioManager());
        _audio.HandlePlaySound();
    }

  
}
