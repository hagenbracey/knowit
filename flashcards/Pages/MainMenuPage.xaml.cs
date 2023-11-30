using System.Text;

namespace flashcards
{
    public partial class MainMenuPage : ContentPage
    {
        int count = 0;

        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void OnCreate(object sender, EventArgs e)
        {
            App.Current.MainPage = new CreatePage();
        }

        private void OnLearn(object sender, EventArgs e)
        {

        }

    }
}