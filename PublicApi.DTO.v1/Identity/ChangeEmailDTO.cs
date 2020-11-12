namespace PublicApi.DTO.v1.Identity
{
    public class ChangeEmailDTO
    {
        public string Email { get; set; } = default!;
        
        public string NewEmail { get; set; } = default!;
    }
}