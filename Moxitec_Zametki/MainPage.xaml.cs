using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Moxitec_Zametki;

public partial class MainPage : ContentPage
{
	ObservableCollection<NoteContentElement> notes;
    string FILEPATH = (FileSystem.AppDataDirectory + "/Notes.json");
	public MainPage()
	{
		InitializeComponent();
        notes = new ObservableCollection<NoteContentElement>(); //RECREATE TO LOAD NOTE
        notes.CollectionChanged += Notes_CollectionChangedAsync;
        if (File.Exists(FILEPATH)) { }
        else { File.Create(FILEPATH); }
        var t = LoadNotes();
        if (t != null)
        {
            foreach (var item in t)
            {
                notes.Add(new NoteContentElement(item));
            }
        }
	}

    private void Notes_CollectionChangedAsync(object sender, NotifyCollectionChangedEventArgs e)
    {
       if (e.Action == NotifyCollectionChangedAction.Add)
       {
            foreach (var i in e.NewItems)
            {
                VerticalPanel.Children.Add((NoteContentElement)i);
            }
            SaveNotes(FeatureFunctions.NoteContentElementToArray(notes));
       }

       if (e.Action == NotifyCollectionChangedAction.Remove) 
       {
            foreach (var i in e.OldItems)
            {
                VerticalPanel.Children.Remove((NoteContentElement)i);
            }
            SaveNotes(FeatureFunctions.NoteContentElementToArray(notes));
       }
    }

    private async void CreateNote(object sender, EventArgs e)
	{
        var text = await DisplayPromptAsync("Текст заметки", "Введите текст: ", "Ок", "Отмена");
        if (text == null || text == " ") { }
        else
        {
            NoteContentElement local_note = new NoteContentElement(text);
            notes.Add(local_note);
        }
    }

    private Note[] LoadNotes()
    {
        Note[] data;
        string unparsedNotes = File.ReadAllText(FILEPATH);


        if (unparsedNotes.Length > 0)
        {
           data = JsonConvert.DeserializeObject<Note[]>(unparsedNotes);
        }
        else { data = null; }
        return data;
    }

    private async void SaveNotes(Note[] notes)
    {
        if (File.Exists(FILEPATH)) { }
        else { File.Create(FILEPATH); }
        string NoteStrToSave =  JsonConvert.SerializeObject(notes);
        await File.WriteAllTextAsync(FILEPATH, NoteStrToSave);
    }

    private void OpenSettingPage(object sender, EventArgs e)
    {
        Setting setting = new();
        Application.Current.MainPage = setting;
    }

    private void DeleteCheckedNote(object sender, EventArgs e)
    {
        for (int i = 0; i < notes.Count(); i++)
        {
            if (notes[i].getNote().isOK)
            {
                notes.RemoveAt(i);
            }
        }
    }

    private void TimerSet(object sender, EventArgs e)
    {

    }
}