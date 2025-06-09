namespace SmokeQuit.Repositories.DuongLNT.ModelExtensions
{
	public class SearchRequest
	{
		public int CurrentPage { get; set; } = 1;

		public int PageSize { get; set; } = 10;
	}

	public class SearchLeaderboardsRequest : SearchRequest
	{
		public string? Note { get; set; } = "";

		public double? Money { get; set; } = 0;

		public string? Reason { get; set; } = "";

	}
}
