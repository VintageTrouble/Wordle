using System;
using System.Linq;

namespace Wordle
{
	class Program
	{
		private static WordChecker _wordChecker;
		private static int _tryNumber = 1;

		static void Main(string[] args)
		{
			_wordChecker = new WordChecker();

			Console.WriteLine("Wordle begins!");
			Console.WriteLine($"Word Length is {_wordChecker.KeyWordLength} letters");
			Console.WriteLine("-----");

			StartGame();
		}

		private static void StartGame()
		{
			while (_tryNumber < 6)
			{
				Console.Write($"{_tryNumber}) word: ");
				var word = Console.ReadLine();

				var handler = _wordChecker.CheckWord(word);

				if (handler.Status == HandlerStatus.WordError)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(handler.Message);
					Console.ResetColor();

					continue;
				}

				Handle(word, handler);

				if (handler.LetterStatuses.All(s => s == LetterStatus.RightPlace))
				{
					Console.WriteLine("You won!");
					return;
				}
			}

			Console.WriteLine($"You lose. The word was: {_wordChecker.KeyWord}");
		}

		private static void Handle(string word, WordHandler handler)
		{
			

			for (int i = 0; i < word.Length; i++)
			{
				switch (handler.LetterStatuses[i])
				{
					case LetterStatus.NotExists:
						Console.ForegroundColor = ConsoleColor.DarkGray;
						break;
					case LetterStatus.Exists:
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						break;
					case LetterStatus.RightPlace:
						Console.ForegroundColor = ConsoleColor.Green;
						break;
				}

				Console.Write(word[i]);
				Console.ResetColor();
			}
			Console.Write("\r");
			Console.WriteLine();
			_tryNumber++;
		}
	}
}
