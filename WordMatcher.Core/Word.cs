using System;
using System.Diagnostics;
using System.Linq;

namespace WordMatcher.Core
{
    [DebuggerDisplay("{Text}")]
    public class Word : IComparable
    {
        public string Text { get; }

        public Word(string text)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentException($"Expected a non-null, non-empty string for {nameof(text)}");

            this.Text = text;
        }

        public Word CombineWith(Word other, int amountOfCharactersForMatches)
        {
            if (!CanCombine(other, amountOfCharactersForMatches))
                return null;

            return new Word($"{this.Text}{other.Text.Substring(1)}");
        }

        private bool CanCombine(Word other, int amountOfCharactersForMatches)
        {
            if (String.IsNullOrEmpty(this.Text) || String.IsNullOrEmpty(other.Text))
                return false; // Cannot combine empty or null words

            var lastChar = this.Text[^1];
            if (!this.Text[^1].Equals(other.Text[0]))
                return false;

            return (this.Text.Length + other.Text.Length - 1) == amountOfCharactersForMatches;
        }

        public int CompareTo(object? obj)
            => this.Text.CompareTo((obj as Word)?.Text);

        public override string ToString()
            => this.Text;
    }
}