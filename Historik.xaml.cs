using System.Runtime.CompilerServices;

namespace PT_App;

public partial class Historik : ContentPage
{
	public Historik(string cprNumber)
	{
		InitializeComponent();
        LoadPatientM�linger(cprNumber);
    }
    private async void LoadPatientM�linger(string cprNumber)
    {
        // Hent m�linger fra databasen for den valgte patient
        var m�linger = await App.Database.GetPatientM�lingerByCPRAsync(cprNumber);

        if (m�linger != null)
        {
            var m�lingStrings = m�linger.Select(m => $"Dato: {m.Dato}, FEV1: {m.FEV1:F2}, FCV: {m.FCV:F2}, Ratio: {m.Ratio:F2}").Reverse().ToList();
            M�lingerListView.ItemsSource = m�lingStrings;
        }
        else
        {
            // Hvis ingen m�linger findes, vis besked
            await DisplayAlert("Ingen m�linger", "Ingen m�linger fundet for denne patient.", "OK");
        }
    }

    private async void OnReturnButtonClicked(object sender, EventArgs e) 
    {
        await Navigation.PopAsync();
    }
}