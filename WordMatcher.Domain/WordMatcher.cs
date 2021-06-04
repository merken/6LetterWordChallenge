using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMatcher.Core;

namespace WordMatcher.Domain
{
    public class WordMatcher : IWordMatcher
    {
        public async ValueTask<WordCollection> FindMatches(WordMatcherOptions options, WordCollection collection)
        {
            var originalCollection = collection.Words;
            var amountOfCharactersForMatches = options.AmountOfCharactersForMatches;
            var amountOfWordPairs = options.AmountOfWordPairs;

            var results = new List<Word>();
            var candidates = FindCandidates(originalCollection, originalCollection, amountOfCharactersForMatches);
            var concatenations = 1;
            while (candidates.Any() && concatenations < amountOfWordPairs)
            {
                results.AddRange(candidates);
                concatenations++;
                candidates = FindCandidates(results, originalCollection, amountOfCharactersForMatches);
            }

            return new WordCollection(results);
        }

        private List<Word> FindCandidates(ICollection<Word> currentCollection, ICollection<Word> originalCollection,
            int amountOfCharactersForMatches)
        {
            var candidates = new List<Word>();

            foreach (var word in currentCollection)
            {
                foreach (var other in originalCollection)
                {
                    var candidate = word.CombineWith(other, amountOfCharactersForMatches);
                    if (candidate != null)
                        candidates.Add(candidate);
                }
            }

            return candidates;
        }
    }
}