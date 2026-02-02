namespace BikeProgram.Models.Requests
{
    public class AddBikeRequest
    {
        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public Guid ManufacturerId { get; set; }
    }
}