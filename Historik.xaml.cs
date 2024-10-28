using System.Runtime.CompilerServices;

namespace PT_App;

public partial class Historik : ContentPage
{
	public Historik(string cprNumber)
	{
		InitializeComponent();
        LoadPatientMålinger(cprNumber);


    }
    private async void LoadPatientMålinger(string cprNumber)
    {
        // Hent målinger fra databasen for den valgte patient
        var målinger = await App.Database.GetPatientMålingerByCPRAsync(cprNumber);

        if (målinger != null)
        {
            var målingStrings = målinger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1}, FCV: {m.FCV}").ToList();
            MålingerListView.ItemsSource = målingStrings;
        }
        else
        {
            // Hvis ingen målinger findes, vis besked
            await DisplayAlert("Ingen målinger", "Ingen målinger fundet for denne patient.", "OK");
        }
    }

    private async void OnReturnButtonClicked(object sender, EventArgs e) 
    {
        await Navigation.PopAsync();
    }
}