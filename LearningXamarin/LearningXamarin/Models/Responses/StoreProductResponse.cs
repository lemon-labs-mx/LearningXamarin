namespace LearningXamarin.Models.Responses
{
	public class StoreProductResponse
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public Rating Rating { get; set; }
    }

    public class Rating
    {
        public double Rate { get; set; }
        public int Count { get; set; }
    }
}

