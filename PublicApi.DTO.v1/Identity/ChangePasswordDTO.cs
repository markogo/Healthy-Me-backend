namespace PublicApi.DTO.v1.Identity
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; } = default!;
        
        public string OldPassword { get; set; } = default!;
        
        public string NewPassword { get; set; } = default!;
    }
}