using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    [Serializable]
    public class Deck
    {
        public string name;
        public List<Flashcard> Cards { get; set; }

        public Deck()
        {
            Cards = new List<Flashcard>();
        }

        public int GetSize()
        {
            return Cards.Count;
        }

        public void AddCard(Flashcard _card, int _index)
        {
            if (Cards.ElementAtOrDefault(_index) == null)
            {
                Cards.Add(_card);
            }
            else
            {
                Cards.Insert(_index, _card);
            }
        }

        public void UpdateCard(string _front, string _back, int _index)
        {
            Flashcard card = new Flashcard();
            card.Front = _front;
            card.Back = _back;
            if (CardAt(_index) != null)
            {
                Cards[_index] = card;
            }
            else
            {
                Cards.Insert(_index, card);
            }
        }

        public Flashcard CardAt(int _index)
        {
            if (Cards.ElementAtOrDefault(_index) == null)
                return null;
            else
                return Cards.ElementAtOrDefault(_index);
        }

        public void DeleteCard(int _index)
        {
            if (Cards.ElementAtOrDefault(_index) != null)
            {
                Cards.RemoveAt(_index);
            }
        }
    }

}
