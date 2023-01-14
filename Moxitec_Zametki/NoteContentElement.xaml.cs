using Microsoft.Maui.Devices;

namespace Moxitec_Zametki;

public partial class NoteContentElement : ContentView
{
	Note localNote;
	public NoteContentElement(string TextOfNote)
	{
		InitializeComponent();
		localNote= new Note();
		localNote.Text = TextOfNote;
		localNote.timeOfCreate = DateTime.Now;
		localNote.isOK = false;
		Text.Text = localNote.Text;
		Text.FontSize = 18;
		DateTimeLabel.Text = localNote.timeOfCreate.ToString();
		DateTimeLabel.FontSize = 12;
		Text.HorizontalOptions = LayoutOptions.Center;
		Text.Margin = new Thickness(5);
	}
	public NoteContentElement(Note note) 
	{
		InitializeComponent();
        localNote = note;
        Text.Text = localNote.Text;
        Text.FontSize = 18;
        DateTimeLabel.Text = localNote.timeOfCreate.ToString();
        DateTimeLabel.FontSize = 12;
        Text.HorizontalOptions = LayoutOptions.Center;
        Text.Margin = new Thickness(5);
    }

	public Note getNote() { 
		if (checkBox.IsChecked) { localNote.isOK =true; }
        return localNote; }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		if (e.Value)
		{
			localNote.isOK = true;
			Text.TextDecorations = TextDecorations.Strikethrough;
			Text.TextColor = Color.FromArgb("DFD8F7");
		}
		else { localNote.isOK = false; Text.TextDecorations = TextDecorations.None; Text.TextColor = Color.Parse("White"); }
    }
}