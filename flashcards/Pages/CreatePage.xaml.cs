using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace flashcards;

public partial class CreatePage : ContentPage
{
    Deck deck;
    int frontOrBack;
    int index;
    string front, back, title;

    public CreatePage(KnowitThemes theme)
    {
        InitializeComponent();
        deck = new Deck();
        frontOrBack = 0;
        index = 0;
        front = "";
        back = "";
        title = "";
        textEditor.FontSize = GetFontSize();
        UpdateNumberLabel();

        //themes
        bgGrid.BackgroundColor = theme.primary;
        plusBtn.BackgroundColor = theme.primary;
        trashBtn.BackgroundColor = theme.primary;
        titleBar.BackgroundColor = theme.primaryDark;
        cardNumLbl.TextColor = theme.secondary;
        rectangleOne.BackgroundColor = theme.secondary;
        rectangleTwo.BackgroundColor = theme.secondary;
        topText.TextColor = theme.secondary;
        titleBar.TextColor = theme.secondary;
        bottomBar.BackgroundColor = theme.tertiary;
        homeBtn.BackgroundColor = theme.tertiary;
        prevBtn.BackgroundColor = theme.tertiary;
        flipBtn.BackgroundColor = theme.tertiary;
        nextBtn.BackgroundColor = theme.tertiary;
        saveBtn.BackgroundColor = theme.tertiary;
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

    private int GetFontSize()
    {
        string path = "C:\\Users\\Public\\Documents\\knowit\\fontsize.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "45");
        }
        return Int32.Parse(File.ReadAllText(path));
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

    private string GetFolder()
    {
        try
        {
            string path = "C:\\Users\\Public\\Documents\\knowit\\path.txt";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "C:\\Users\\Public\\Documents\\knowit");
            }

            return File.ReadAllText(path);
        }
        catch (Exception e)
        {
            Debug.Write(e.Message);
            return e.Message;
        }
    }

    private void OnSave(object sender, EventArgs e)
    {
        string fileName;
        title = titleBar.Text;
        XmlSerializer xs = new XmlSerializer(typeof(Deck));
        string pathOne = GetFolder();
        Debug.WriteLine(pathOne);
        string pathTwo = pathOne + "\\Decks";
        Debug.WriteLine(pathTwo);
        if (!Directory.Exists(pathOne))
        {
            DirectoryInfo di = Directory.CreateDirectory(pathOne);
            di = Directory.CreateDirectory(pathTwo);
        }
        if (!Directory.Exists(pathTwo))
        {
            DirectoryInfo di = Directory.CreateDirectory(pathTwo);
        }
        fileName = pathTwo + "\\" + title + ".xml";

        deck.name = title;

        using (StreamWriter file = new StreamWriter(fileName))
        {
            xs.Serialize(file, deck);
        }
        Debug.WriteLine(fileName);
        App.Current.MainPage = new MainMenuPage();
    }


    private void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (frontOrBack == 0) front = e.NewTextValue;
        else back = e.NewTextValue;
        deck.UpdateCard(front, back, index);
    }
}