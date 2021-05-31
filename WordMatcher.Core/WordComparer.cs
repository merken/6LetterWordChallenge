using System.Collections.Generic;

namespace WordMatcher.Core
{
    public class WordComparer : IEqualityComparer<Word>
    {
        public bool Equals(Word x, Word y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Text == y.Text;
        }

        public int GetHashCode(Word obj)
        {
            return (obj.Text != null ? obj.Text.GetHashCode() : 0);
        }
    }
}