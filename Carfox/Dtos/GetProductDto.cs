namespace Carfox.Dtos
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string Area { get; set; }
        public bool IsNew { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int zipCode { get; set; }
        public string FuelType { get; set; }
        public bool IsAutomatic { get; set; }
        public string BodyShape { get; set; }
        public int EngineCapacity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }

    }
}
