namespace LearningXamarin.Models.Enums
{
	public enum OrderByEnum
	{
		/// <summary>
		/// It's the default order when you call the api.
		/// </summary>
		Default,

		/// <summary>
		/// The items are sorted by the lower to the highest price.
		/// </summary>
		LowToHigh,

		/// <summary>
		/// The items are sorted by the highest to the lower price.
		/// </summary>
		HighToLow,

		/// <summary>
		/// The items are sorted by the top rated opinions.
		/// </summary>
		TopRated,
	}
}

