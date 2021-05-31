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
        [Option("-i")] public string Input { get; } = "input.txt";
        
        async Task OnExecute()
        {
            Console.WriteLine("****** 6LetterWordChallenge ******");
            Console.WriteLine("**********************************");

            Console.WriteLine("Loading input.txt");
            if (!File.Exists(Input))
                throw new ArgumentException($"Expected input.txt at {Input}");
            
            var input = await File.ReadAllLinesAsync(Input);

            var wordMatcher = new WordMatcher.Domain.WordMatcher();
            
            Console.WriteLine("Matching words for configuration ");
            var options = new WordMatcherOptions(this.AmountOfCharactersForMatches);
            var words = input.Select(i => new Word(i));
            var collection = new WordCollection(words);
            var matches = await wordMatcher.FindMatches(options, collection);
            
            Console.WriteLine($"Found {matches.Words.Count()} {this.AmountOfCharactersForMatches} letter matches");

            foreach (var match in matches.Words)
                Console.WriteLine(match);

            Console.WriteLine("END OF PROGRAM");
        }
    }
}
