using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using WordMatcher.Core;

namespace WordMatcher.App
{
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option("-a")] public int AmountOfCharactersForMatches { get; } = 6;
        [Option("-p")] public int AmountOfWordPairs { get; } = 2;
        [Option("-i")] public string Input { get; } = "input.txt";

        async Task OnExecute()
        {
            Console.WriteLine($"****** 6LetterWordChallenge ******");
            Console.WriteLine($"AmountOfCharactersForMatches: {AmountOfCharactersForMatches}");
            Console.WriteLine($"AmountOfWordPairs: {AmountOfWordPairs}");
            Console.WriteLine($"Input: {Input}");
            Console.WriteLine($"*********************************");
            var start = DateTime.Now;
            Console.WriteLine(start.ToLongTimeString());

            Console.WriteLine("Loading input.txt");
            if (!File.Exists(Input))
                throw new ArgumentException($"Expected input.txt at {Input}");

            var input = await File.ReadAllLinesAsync(Input);

            var wordMatcher = new WordMatcher.Domain.WordMatcher();

            Console.WriteLine("Matching words for configuration ");
            var options = new WordMatcherOptions(this.AmountOfCharactersForMatches, this.AmountOfWordPairs);
            var words = input.Select(i => new Word(i));
            var collection = new WordCollection(words);
            var matches = await wordMatcher.FindMatches(options, collection);

            var orderedResults = matches.Words
                .OrderBy(w => w.Text)
                .Select(w => w.Text)
                .ToArray();

            foreach (var match in orderedResults)
                Console.WriteLine(match);

            Console.WriteLine($"Found {matches.Words.Count} {this.AmountOfCharactersForMatches} letter matches");
            Console.WriteLine("Writing to disk");

            await File.WriteAllLinesAsync("output.txt", orderedResults);
            
            var end = DateTime.Now;
            Console.WriteLine(end.ToLongTimeString());
            var elapsed = end - start;
            Console.WriteLine($"This took: {elapsed.Minutes} minutes and {elapsed.Seconds} seconds ");

            Console.WriteLine("END OF PROGRAM");
        }
    }
}