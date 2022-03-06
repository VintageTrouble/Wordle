using System.Net;
using System.Net.Http;

namespace Wordle
{
	public class SpellChecker
	{
		public bool IsWordExists(string word)
		{
			using var client = new HttpClient();
			
			var response = client.GetAsync(@$"https://api.dictionaryapi.dev/api/v2/entries/en/{word}").Result;

			if (response.IsSuccessStatusCode)
			{
				return !response.Content
					.ReadAsStringAsync()
					.Result
					.Contains("No Definitions Found");
			}
			else
			{
				if (response.StatusCode == HttpStatusCode.NotFound)
					return false;

				throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
			}
		}
	}
}
