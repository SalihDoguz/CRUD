namespace CRUD.Models
{
    public class UpdatePersonRequest
    {
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public long? Phone { get; set; }
        public string? Address { get; set; }
    }
}
