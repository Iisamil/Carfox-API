namespace Carfox.Controllers
{
    public class UpdateUserDataDtos
    {
        public string Image { get; set; } = string.Empty;
        public string FName { get; set; }
        public string LName { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public virtual string PhoneNumber { get; set; }

    }
}