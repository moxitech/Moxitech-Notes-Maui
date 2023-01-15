namespace Moxitec_Zametki;

public partial class Setting : ContentPage
{
	public Setting()
	{
		InitializeComponent();
		//TODO: x:Name for button
	}

    private void BackButtonClicked(object sender, EventArgs e)
    {
		//TODO: save and recomplete the setting
		App.Current.MainPage = new MainPage();
    }

    private async void ClearAllButtonClicked(object sender, EventArgs e)
    {
        var res = await DisplayActionSheet("Очистка заметок", "Да", "Нет");
        if (res == "Да")
        {
            string FILEPATH = (FileSystem.AppDataDirectory + "/Notes.json");
            File.Delete(FILEPATH);
            File.Create(FILEPATH);
        }

    }
}