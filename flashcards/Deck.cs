using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class Deck
    {
        private string name;
        private List<Flashcard> cards;

        public Deck()
        {
            name = "";
            cards = new List<Flashcard>();
        }

        public void AddCard(Flashcard _card, int _index)
        {
            if (cards.ElementAtOrDefault(_index) == null)
            {
                cards.Add(_card);
            }
            else
            {
                cards.Insert(_index, _card);
            }
        }

        public Flashcard GetNext(int _index)
        {
            if (cards.ElementAtOrDefault(_index + 1) == null)
            {
                return null;
            }
            else
            {
                return cards.ElementAtOrDefault(_index + 1);
            }
        }

        public Flashcard GetPrev(int _index)
        {
            if (cards.ElementAtOrDefault(_index - 1) == null)
            {
                return null;
            }
            else
            {
                return cards.ElementAtOrDefault(_index - 1);
            }
        }

        public void DeleteCard(int _index)
        {
            if (cards.ElementAtOrDefault(_index) != null)
            {
                cards.RemoveAt(_index);
            }
        }
    }
}
