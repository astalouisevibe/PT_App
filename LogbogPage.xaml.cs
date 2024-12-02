using System.Collections.ObjectModel;

namespace PT_App;

public partial class LogbogPage : ContentPage
{
    private ObservableCollection<string> previousNotes = new ObservableCollection<string>();

    public LogbogPage()
	{
		InitializeComponent();
        NotesListView.ItemsSource = previousNotes;

    }
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
    private void OnSaveCommentCompleted(object sender, EventArgs e)
    {
        OnSaveCommentClicked(sender, e);
    }

    private async void OnReturnButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}