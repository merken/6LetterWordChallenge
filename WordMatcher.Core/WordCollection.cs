using System;
using System.Collections.Generic;
using System.Linq;

namespace WordMatcher.Core
{
    public class WordCollection
    {
        public ICollection<Word> Words { get; }

        public WordCollection(IEnumerable<Word> words)
        {
            if (words == null || !words.Any())
                throw new ArgumentException($"{nameof(words)} must be a non-null, non-empty list.");

            this.Words = new HashSet<Word>(new WordComparer());
            foreach (var word in words)
                this.Words.Add(word); // skip duplicates on failure
        }
    }
}