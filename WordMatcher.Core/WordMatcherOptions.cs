namespace WordMatcher.Core
{
    public class WordMatcherOptions
    {
        public WordMatcherOptions(int amountOfCharactersForMatches, int amountOfWordPairs)
        {
            this.AmountOfCharactersForMatches = amountOfCharactersForMatches;
            this.AmountOfWordPairs = amountOfWordPairs;
        }

        public int AmountOfCharactersForMatches { get; private set; }
        public int AmountOfWordPairs { get; }
    }
}