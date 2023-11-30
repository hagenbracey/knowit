using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace flashcards;

public partial class CreatePage : ContentPage
{
    Deck deck;
    int frontOrBack;
    int index;
    string front, back, title;

    public CreatePage()
    {
        InitializeComponent();
        deck = new Deck();
        frontOrBack = 0;
        index = 0;
        front = "";
        back = "";
        title = "";
        UpdateNumberLabel();
    }

    void UpdateNumberLabel()
    {
        string s = "";
        if (frontOrBack == 0) s = "F";
        else s = "B";
        cardNumLbl.Text = $"{Convert.ToString(index + 1)}\n{s}";
    }

    private void Flip(object sender, EventArgs e)
    {
        if (frontOrBack == 0)
        {
            frontOrBack = 1;
            textEditor.Text = back;
        }
        else
        {
            frontOrBack = 0;
            textEditor.Text = front;
        }
        UpdateNumberLabel();
    }

    private void OnHome(object sender, EventArgs e)
    {
        App.Current.MainPage = new MainMenuPage();
    }

    private void OnNext(object sender, EventArgs e)
    {
        if (index < deck.GetSize() && index + 1 < deck.GetSize())
        {
            deck.UpdateCard(front, back, index);
            index++;
            Flashcard card = deck.CardAt(index);
            front = card.Front;
            back = card.Back;
            frontOrBack = 0;
            textEditor.Text = front;
            UpdateNumberLabel();
        }
    }

    private void OnNew(object sender, EventArgs e)
    {
        deck.UpdateCard(front, back, index);
        index++;
        Flashcard card = new Flashcard();
        deck.AddCard(card, index);
        front = card.Front;
        back = card.Back;
        textEditor.Text = front;
        frontOrBack = 0;
        UpdateNumberLabel();
    }

    private void OnPrev(object sender, EventArgs e)
    {
        if (index > 0)
        {
            deck.UpdateCard(front, back, index);
            index--;
            Flashcard card = deck.CardAt(index);
            front = card.Front;
            back = card.Back;
            frontOrBack = 0;
            textEditor.Text = front;
            UpdateNumberLabel();
        }
    }

    private void OnDelete(object sender, EventArgs e)
    {
        if (index > 0)
        {
            deck.UpdateCard(front, back, index);
            deck.DeleteCard(index);
            index--;
            Flashcard card = deck.CardAt(index);
            front = card.Front;
            back = card.Back;
            frontOrBack = 0;
            textEditor.Text = front;
            UpdateNumberLabel();
        }
        else if (index == 0 && index < deck.GetSize() && index + 1 < deck.GetSize())
        {
            deck.UpdateCard(front, back, index);
            deck.DeleteCard(index);
            Flashcard card = deck.CardAt(index);
            front = card.Front;
            back = card.Back;
            frontOrBack = 0;
            textEditor.Text = front;
            UpdateNumberLabel();
        }
    }

    private void OnSave(object sender, EventArgs e)
    {
        title = titleBar.Text;
        if (title != "")
        {
            XmlSerializer xs = new XmlSerializer(typeof(Deck));
            string pathOne = "C:\\Users\\Public\\Documents\\knowit";
            string pathTwo = "C:\\Users\\Public\\Documents\\knowit\\Decks";
            if (!Directory.Exists(pathOne))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathOne);
                di = Directory.CreateDirectory(pathTwo);
            }
            if (!Directory.Exists(pathTwo))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathTwo);
            }
            string fileName = pathTwo + "\\" + title + ".xml";

            using (StreamWriter file = new StreamWriter(fileName))
            {
                xs.Serialize(file, deck);
            }
        }
        App.Current.MainPage = new MainMenuPage();
    }


    private void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (frontOrBack == 0) front = e.NewTextValue;
        else back = e.NewTextValue;
    }
}