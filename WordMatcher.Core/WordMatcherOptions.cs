namespace WordMatcher.Core
{
    public class WordMatcherOptions
    {
        public WordMatcherOptions(int amountOfCharactersForMatches)
        {
            this.AmountOfCharactersForMatches = amountOfCharactersForMatches;
        }
        public int AmountOfCharactersForMatches { get; private set; }
    }
}