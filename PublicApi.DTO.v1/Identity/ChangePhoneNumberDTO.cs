namespace PublicApi.DTO.v1.Identity
{
    public class ChangePhoneNumberDTO
    {
        public string Email { get; set; } = default!;
        
        public string PhoneNumber { get; set; } = default!;
        
        public string NewPhoneNumber { get; set; } = default!;
    }
}