namespace flashcards.Pages;

public partial class LearnPage : ContentPage
{
    int frontOrBack;
    int index;
    string front, back;
    Deck deck;

    public LearnPage(Deck _deck, KnowitThemes theme)
    {
        InitializeComponent();
        deck = _deck;
        index = 0;
        Flashcard card = deck.CardAt(index);
        front = card.Front;
        back = card.Back;
        frontOrBack = 0;
        textEditor.Text = front;
        UpdateNumberLabel();
        textEditor.FontSize = GetFontSize();
        titleBar.Text = deck.name;

        //themes
        bgGrid.BackgroundColor = theme.primary;
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

    private void OnHome(object sender, EventArgs e)
    {
        App.Current.MainPage = new MainMenuPage();
    }

    private void OnPrev(object sender, EventArgs e)
    {
        if (index > 0)
        {
            index--;
            Flashcard card = deck.CardAt(index);
            front = card.Front;
            back = card.Back;
            frontOrBack = 0;
            textEditor.Text = front;
            UpdateNumberLabel();
        }
    }

    private void OnNext(object sender, EventArgs e)
    {
        if (index < deck.GetSize() && index + 1 < deck.GetSize())
        {
            index++;
            Flashcard card = deck.CardAt(index);
            front = card.Front;
            back = card.Back;
            frontOrBack = 0;
            textEditor.Text = front;
            UpdateNumberLabel();
        }
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

    void UpdateNumberLabel()
    {
        string s = "";
        if (frontOrBack == 0) s = "F";
        else s = "B";
        cardNumLbl.Text = $"{Convert.ToString(index + 1)}\n{s}";
    }

}