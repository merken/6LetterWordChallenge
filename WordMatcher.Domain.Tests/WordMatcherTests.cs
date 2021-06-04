using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMatcher.Core;
using Xunit;

namespace WordMatcher.Domain.Tests
{
    public class WordMatcherTests
    {
        /// <summary>
        /// A piece of functionality is only proven if there are tests...
        /// </summary>
        /// <param name="amountOfCharactersForMatches"></param>
        /// <param name="amountOfWordPairs"></param>
        /// <param name="input"></param>
        /// <param name="expectedMatches"></param>
        [Theory]
        [InlineData(6, 2, new string[]
            {
                "foo", "obar", "obaz", "opar", "omar", "baz", "oobar"
            },
            new string[]
            {
                "foobar", "foobaz", "foopar", "foomar",
            })]
        public async Task FooBar6Letters(int amountOfCharactersForMatches, int amountOfWordPairs, string[] input,
            string[] expectedMatches)
        {
            // Arrange
            var sut = CreateSut();
            var amountOfLetters = amountOfCharactersForMatches;
            var options = new WordMatcherOptions(amountOfLetters, amountOfWordPairs);
            var words = input.Select(i => new Word(i));
            var wordCollection = new WordCollection(words);
            var expected = new WordCollection(expectedMatches.Select(e => new Word(e)));

            // Act
            var result = await sut.FindMatches(options, wordCollection);

            // Assert
            Assert.Equal(expected.Words, result.Words);
        }

        [Fact]
        public async Task Word_CTOR_ThrowsArgumentExceptions()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new Word(null));
            Assert.Throws<ArgumentException>(() => new Word(String.Empty));
        }

        [Fact]
        public async Task WordCollection_CTOR_ThrowsArgumentExceptions()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new WordCollection(null));
            Assert.Throws<ArgumentException>(() => new WordCollection(new List<Word>()));
        }

        private IWordMatcher CreateSut()
            => new WordMatcher();
    }
}