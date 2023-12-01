using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace flashcards
{
    public partial class MainMenuPage : ContentPage
    {
        KnowitThemes theme;

        Deck deck;

        int count = 0;

        public MainMenuPage(string themeName = "green")
        {
            InitializeComponent();
            themeName = GetThemeName();
            theme = new KnowitThemes(themeName);
            deck = new Deck();

            //theme
            bgGrid.BackgroundColor = theme.primary;
            rectOne.BackgroundColor = theme.secondary;
            rectTwo.BackgroundColor = theme.secondary;
            studyIt.TextColor = theme.tertiary;
            createBtn.BackgroundColor = theme.secondary;
            createBtn.TextColor = theme.primary;
            learnBtn.BackgroundColor = theme.secondary;
            learnBtn.TextColor = theme.primary;
            settingsBtn.BackgroundColor = theme.secondary;
            settingsBtn.TextColor = theme.primary;
            bottomBar.BackgroundColor = theme.tertiary;

        }

        string GetThemeName()
        {
            string pathOne = "C:\\Users\\Public\\Documents\\knowit";
            string pathTwo = "C:\\Users\\Public\\Documents\\knowit\\prefs.txt";
            if (!File.Exists(pathTwo))
            {
                if (!Directory.Exists(pathOne))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathOne);
                    di = Directory.CreateDirectory(pathOne);
                }
                File.WriteAllText(pathTwo, "green");
            }
            return File.ReadAllText(pathTwo);
        }

        private void OnCreate(object sender, EventArgs e)
        {
            App.Current.MainPage = new CreatePage(theme);
        }
        
        private void OnSettings(object sender, EventArgs e)
        {
            App.Current.MainPage = new Pages.SettingsPage(theme);
        }

        private Deck LoadDeckFromFile(string filePath)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Deck));

                using (StreamReader file = new StreamReader(filePath))
                {
                    return (Deck)xs.Deserialize(file);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during deserialization
                Console.WriteLine($"Error loading deck from file: {ex.Message}");
                return null;
            }
        }

        private async void OnLearn(object sender, EventArgs e)
        {
            OpenFile();
        }

        private async void OpenFile()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.Android, new[] { "application/xml", "text/xml" } },
                        { DevicePlatform.iOS, new[] { "public.xml" } },
                        { DevicePlatform.WinUI, new [] {".xml"} }
                        // Add more platform-specific extensions if needed
                    }),
                });

                if (result != null)
                {
                    string filePath = result.FullPath;
                    deck = LoadDeckFromFile(filePath);

                    if (deck != null)
                    {
                        // Successfully loaded the deck, do something with it
                        App.Current.MainPage = new Pages.LearnPage(deck, theme);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during the file picking process
                Console.WriteLine($"Error picking file: {ex.Message}");
            }
        }

    }
}