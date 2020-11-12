namespace PublicApi.DTO.v1.Identity
{
    public class ChangeNamesDTO
    {
        public string Email { get; set; } = default!;
        
        public string FirstName { get; set; } = default!;
        
        public string LastName { get; set; } = default!;
    }
}