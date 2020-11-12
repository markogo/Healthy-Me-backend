namespace PublicApi.DTO.v1.Identity
{
    public class RegisterDTO
    {
        public string Email { get; set; } = default!;
        
        public string Password { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        
        public string LastName { get; set; } = default!;
        
        public string PhoneNumber { get; set; } = default!;
        
    }
}