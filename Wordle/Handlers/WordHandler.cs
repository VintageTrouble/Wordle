namespace Wordle
{

	public class WordHandler
	{
		public HandlerStatus Status { get; }
		public string Message { get; }
		public LetterStatus[] LetterStatuses { get; }

		public WordHandler(
			HandlerStatus status = HandlerStatus.Ok, 
			string message = null, 
			LetterStatus[] letterStatuses = null)
		{
			Status = status;
			Message = message;
			LetterStatuses = letterStatuses;
		}
	}

}