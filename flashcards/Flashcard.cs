using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class Flashcard
    {
        private string? front = null;
        private string? back = null;

        public Flashcard()
        {
            front = back = "";
        }

        public Flashcard(string _front, string _back)
        {
            if (_front != null && _back != null)
            {
                front = _front;
                back = _back;
            }
        }

        public string Front
        {
            get
            {
                if (front != null) return front;
                else return "";
            }
            set
            {
                front = value;
            }
        }

        public string Back
        {
            get
            {
                if (back != null) return back;
                else return "";
            }

            set
            {
                back = value;
            }
        }

        public bool IsComplete
        {
            get
            {
                return (front != null && back != null);
            }
        }
    }
}
