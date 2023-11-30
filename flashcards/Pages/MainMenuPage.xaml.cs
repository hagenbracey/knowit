using System.Text;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace flashcards
{
    public partial class MainMenuPage : ContentPage
    {
        Deck deck;

        int count = 0;

        public MainMenuPage()
        {
            InitializeComponent();
            deck = new Deck();
        }

        private void OnCreate(object sender, EventArgs e)
        {
            App.Current.MainPage = new CreatePage();
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
                        App.Current.MainPage = new Pages.LearnPage(deck);
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