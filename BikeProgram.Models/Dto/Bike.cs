namespace BikeProgram.Models.Dto
{
    public class Bike
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public Guid ManufacturerId { get; set; }
    }
}