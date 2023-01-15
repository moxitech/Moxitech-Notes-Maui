using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Moxitec_Zametki;

public partial class MainPage : ContentPage
{
    #region Variable
    ObservableCollection<NoteContentElement> notes;
    string FILEPATH = (FileSystem.AppDataDirectory + "/Notes.json");
    bool isOpenedToolBar = true;
    #endregion
    public MainPage()
	{
		InitializeComponent();
        #region Collection with note view element [Setup]
        notes = new ObservableCollection<NoteContentElement>();
        notes.CollectionChanged += Notes_CollectionChangedAsync;
        #endregion
        #region Check file with note exists
        if (File.Exists(FILEPATH)) { }
        else { File.Create(FILEPATH); }
        #endregion
        #region load and set note 
        var loadNotes = LoadNotes();
        if (loadNotes != null)
        {
            foreach (var item in loadNotes)
            {
                notes.Add(new NoteContentElement(item));
            } 
        }
        #endregion
        #region preAnimation
        #endregion
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
        if (text == null || text == " ") {  }
        else
        {
            text = text.TrimStart();
            NoteContentElement local_note = new NoteContentElement(text);
            notes.Add(local_note);
        }
    }

    private Note[] LoadNotes()
    {
        Note[] data;
        string unparsedNotes = File.ReadAllText(FILEPATH); //EXCEPTION L ACCESS
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
        //TODO: Timer for notify
    }
    #region НУЖДАЕТЬСЯ В ФИКСЕЕЕЕЕ
    private void AnimateOfContextButtonOpen(object sender, EventArgs e)
    {// TODO : var flag (true: + else: -)
        /*Анимация открытия заметок
        var translate = ExpandButton.TranslationY;        
        if (!isOpenedToolBar)
        {
            SettingBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
            TimerBtn.IsEnabled = false;
            DeleteBtn.TranslateTo(DeleteBtn.TranslationX, DeleteBtn.TranslationY - 60, 500, Easing.BounceOut);
            SettingBtn.TranslateTo(SettingBtn.TranslationX, SettingBtn.TranslationY - 120, 500, Easing.BounceOut);
            TimerBtn.TranslateTo(TimerBtn.TranslationX, TimerBtn.TranslationY - 180, 500, Easing.BounceOut);
            isOpenedToolBar = true;
        }
        else
        {
            TimerBtn.Opacity = 100;
            DeleteBtn.Opacity = 100;
            SettingBtn.Opacity = 100;
            DeleteBtn.IsEnabled = true;
            SettingBtn.IsEnabled = true;
            TimerBtn.IsEnabled = true;
            DeleteBtn.TranslateTo(DeleteBtn.TranslationX, DeleteBtn.TranslationY + 60, 500, Easing.BounceOut);
            SettingBtn.TranslateTo(SettingBtn.TranslationX, SettingBtn.TranslationY + 120, 500, Easing.BounceOut);
            TimerBtn.TranslateTo(TimerBtn.TranslationX, TimerBtn.TranslationY + 180, 500, Easing.BounceOut);
            
            isOpenedToolBar = false;
        }
        //SettingBtn.X;
        //DeleteBtn.X;
        //TimerBtn;
    }*/
        if (isOpenedToolBar)
        {
            SettingBtn.IsVisible = true;
            DeleteBtn.IsVisible = true;
            TimerBtn.IsVisible = true;
            isOpenedToolBar= false;
        }
        else
        {
            SettingBtn.IsVisible = false;
            DeleteBtn.IsVisible = false;
            TimerBtn.IsVisible = false;
            isOpenedToolBar = true;
        }



    }
    #endregion
}