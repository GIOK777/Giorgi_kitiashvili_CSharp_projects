using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using static System.Formats.Asn1.AsnWriter;

namespace Hangman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> words = new List<string> { "house", "ball", "stone", "door", "banana" };

            Random random = new Random();
            int index = random.Next(words.Count);
            string randomWord = words[index];
            //Console.WriteLine($"Random word:  {randomWord}");


            List<char> underscores = new List<char>(new char[randomWord.Length]);
            for (int i = 0; i < randomWord.Length; i++)
            {
                underscores[i] = '_';
            }


            Console.WriteLine($"Word includes {randomWord.Length} caracters");
            Console.WriteLine("How many try you need to guess word ?");
            int.TryParse(Console.ReadLine(), out int numberOfTries);

            int maxNumberOfTries = numberOfTries;
            int RemainingTries = maxNumberOfTries;

            List<char> incorrectLetters = new List<char>();


            while (RemainingTries > 0)
            {
                Console.WriteLine($"\n Word: {string.Join(" ", underscores)}");
                Console.WriteLine($"Incorrect letters: {string.Join(", ", incorrectLetters)}");
                Console.WriteLine($"Remaining tries: {RemainingTries}");

                Console.Write("Guess a letter: ");
                char guessLetter = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (!Char.IsLetter(guessLetter))
                {
                    Console.WriteLine("Enter valid letter");
                    continue;
                }
                guessLetter = Char.ToLower(guessLetter);

                if (incorrectLetters.Contains(guessLetter) || underscores.Contains(guessLetter))
                {
                    Console.WriteLine("it's have been - try another letter");
                }


                bool correctLetter = false;
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (randomWord[i] == guessLetter)
                    {
                        underscores[i] = guessLetter;
                        correctLetter = true;
                    }
                }

                if (!correctLetter)
                {
                    incorrectLetters.Add(guessLetter);
                    RemainingTries--;
                }

                if (!underscores.Contains('_'))
                {
                    Console.WriteLine($"\n You guessed the word : {randomWord}");
                    break;
                }

                if (RemainingTries == 0) { Console.WriteLine($"\nGame Over! The word was: {randomWord}"); }
            }
        }
    }
}

