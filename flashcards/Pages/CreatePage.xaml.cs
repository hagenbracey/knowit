namespace flashcards;

public partial class CreatePage : ContentPage
{
    List<Flashcard> deck = new List<Flashcard>(new Flashcard[1]);
    Flashcard card;
    string front = "";
    string back = "";
    int frontOrBack = 0;
    int index = 0;

    public CreatePage()
    {
        InitializeComponent();
        card = new Flashcard();
        frontOrBack = 0;
    }

    private void Flip(object sender, EventArgs e)
    {
        if (frontOrBack == 0)
        {
            front = textEditor.Text;
            frontOrBack = 1;
            textEditor.Text = card.Back;
        }
        else
        {
            back = textEditor.Text;
            frontOrBack = 0;
            textEditor.Text = card.Front;
        }
    }

    private void OnHome(object sender, EventArgs e)
    {
        App.Current.MainPage = new MainMenuPage();
    }

    private void OnNext(object sender, EventArgs e)
    {
        card.Front = front;
        card.Back = back;

        card = null;

        index++;
        try
        {
            card = deck[index];
        }
        catch
        {
            card = new Flashcard();
            deck.AddRange(new Flashcard[deck.Capacity - deck.Count]);
            textEditor.Text = "";
        }
        frontOrBack = 0;
    }

    private void OnPrev(object sender, EventArgs e)
    {
        if (index > 0)
        {
            card.Front = front;
            card.Back = back;

            try
            {
                deck[index] = card;
            }
            catch
            {
                deck.Add(card);
            }

            card = null;
            index = index - 1;
            card = deck[index];
            textEditor.Text = card.Front;
            frontOrBack = 0;
        }
    }

    private void Editor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (frontOrBack == 0) card.Front = e.NewTextValue;
        else card.Back = e.NewTextValue;
    }
}