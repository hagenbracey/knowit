
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;

namespace flashcards.Pages;


public partial class SettingsPage : ContentPage
{

	public SettingsPage(KnowitThemes theme)
	{
		InitializeComponent();
		fontSizeEntry.Text = Convert.ToString(GetFontSize());

		// themes
		bgGrid.BackgroundColor = theme.primary;
		rectOne.BackgroundColor = theme.secondary;
		rectTwo.BackgroundColor = theme.secondary;
		topText.TextColor = theme.secondary;
		fontBtn.BackgroundColor = theme.primary;
		fontBtn.BackgroundColor = theme.secondary;
		themeBtn.TextColor = theme.primary;
		themeBtn.BackgroundColor = theme.secondary;
		folderBtn.TextColor = theme.primary;
		folderBtn.BackgroundColor = theme.secondary;
		bottomGrid.BackgroundColor = theme.tertiary;
		homeBtn.BackgroundColor = theme.tertiary;
		fontLabel.TextColor = theme.primary;

	}

    private int GetFontSize()
    {
        string path = "C:\\Users\\Public\\Documents\\knowit\\fontsize.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "45");
        }

        return Int32.Parse(File.ReadAllText(path));
    }

    private void Entry_TextChanged(object sender, EventArgs e)
	{
        string path = "C:\\Users\\Public\\Documents\\knowit\\fontsize.txt";

		File.WriteAllText(path, string.Empty);
		File.WriteAllText(path, fontSizeEntry.Text);
    }
	
	private void OnTheme(object sender, EventArgs e)
	{
        string path = "C:\\Users\\Public\\Documents\\knowit\\prefs.txt";
        string themeName = GetThemeName();

		if (themeName == "green")
		{
			File.WriteAllText(path, string.Empty);
			File.WriteAllText(path, "red");
		}
		else if (themeName == "red")
		{
			File.WriteAllText(path, string.Empty);
			File.WriteAllText(path, "blue");
		}
		else
		{
            File.WriteAllText(path, string.Empty);
            File.WriteAllText(path, "green");
        }

		App.Current.MainPage = new MainMenuPage(themeName);
    }

    string GetThemeName()
    {
        string path = "C:\\Users\\Public\\Documents\\knowit\\prefs.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "green");
        }
        return File.ReadAllText(path);
    }

    private async void OnFolder(object sender, EventArgs e)
	{
		string folderPath;
		var folder = await FolderPicker.PickAsync(default);
		folderPath = $"{folder.Folder.Path}";
        string path = "C:\\Users\\Public\\Documents\\knowit\\path.txt";
		if (!File.Exists (folderPath))
		{
			File.WriteAllText(path, folderPath);
		}
		else
		{
			File.WriteAllText(path, string.Empty);
			File.WriteAllText(path, folderPath);
		}

    }
    private void OnHome(object sender, EventArgs e)
	{
        App.Current.MainPage = new MainMenuPage();
    }
}