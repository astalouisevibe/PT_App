namespace PT_App;

public partial class LogbogPage : ContentPage
{
	public LogbogPage()
	{
		InitializeComponent();
	}
    private List<string> previousNotes = new List<string>();
    private void OnCommentEditorTextChanged(object sender, TextChangedEventArgs e)
    {
        // Vis knappen, hvis der er tekst i editoren; skjul den ellers
        GemNote.IsVisible = !string.IsNullOrWhiteSpace(commentEditor.Text);
    }
    private void OnSaveCommentClicked(object sender, EventArgs e)
    {
        string comment = commentEditor.Text;
        if (!string.IsNullOrWhiteSpace(comment))
        {
            // Tilføj kommentaren til listen
            previousNotes.Add(comment);

            // Ryd tekstfeltet
            commentEditor.Text = string.Empty;
            GemNote.IsVisible = false;

        }
    }

        private async void OnReturnButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}