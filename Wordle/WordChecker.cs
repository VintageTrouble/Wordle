using System;
using System.IO;
using System.Net.Http;

namespace Wordle
{
	public class WordChecker
	{
		private const string FILE_NAME = "WordList.txt";
		private readonly string _solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
		private readonly SpellChecker _spellChecker;

		public int KeyWordLength => KeyWord.Length;
		public string KeyWord { get; }

		public WordChecker()
		{
			_spellChecker = new SpellChecker();

			KeyWord = GetRandomWord();
		}

		public WordHandler CheckWord(string word)
		{
			word = word.ToLower();

			if (word.Length != KeyWord.Length)
				return new WordHandler(HandlerStatus.WordError, "Words length are not equal");

			bool isWordExists;

			try
			{
				isWordExists = _spellChecker.IsWordExists(word);
			}
			catch (HttpRequestException e)
			{
				return new WordHandler(HandlerStatus.WordError, $"{e.StatusCode} - {e.Message}");
			}

			return !isWordExists
				? new WordHandler(HandlerStatus.WordError, "Word does not exists")
				: new WordHandler(
					HandlerStatus.Ok,
					null,
					GetLettersStatuses(word));
		}

		private LetterStatus[] GetLettersStatuses(string word)
		{
			var result = new LetterStatus[word.Length];

			for (int i = 0; i < word.Length; i++)
			{
				if (word[i] == KeyWord[i])
				{
					result[i] = LetterStatus.RightPlace;
				}
				else if (KeyWord.Contains(word[i]))
				{
					result[i] = LetterStatus.Exists;
				}
				else
				{
					result[i] = LetterStatus.NotExists;
				}
			}

			return result;
		}

		private string GetRandomWord()
		{
			var rnd = new Random();
			var words = File.ReadAllLines(Path.Combine(_solutionPath, FILE_NAME));

			var word = words[rnd.Next(0, words.Length)];

			return string.IsNullOrEmpty(word)
				? throw new ArgumentNullException(nameof(word))
				: word.ToLower();
		}
	}

}