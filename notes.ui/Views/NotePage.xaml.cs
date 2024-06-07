using System.Runtime.InteropServices;

namespace notes.ui.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{

    public NotePage()
    {
        InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    private void LoadNote(string fileName)
    {
        Models.Nota noteModel = new Models.Nota();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }

    public string ItemId
    {
        set { LoadNote(value); }
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Nota nota)
            File.WriteAllText(nota.Filename, txtEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Nota nota)
        {
            if (File.Exists(nota.Filename))
                File.Delete(nota.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }
}