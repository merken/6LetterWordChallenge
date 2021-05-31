using System;
using System.Threading.Tasks;

namespace WordMatcher.Core
{
    public interface IWordMatcher
    {
        ValueTask<WordCollection> FindMatches(WordMatcherOptions options, WordCollection collection); 
    }
}