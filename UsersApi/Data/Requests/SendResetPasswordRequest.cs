using System.ComponentModel.DataAnnotations;

namespace UsersApi.Data.Requests
{
    public class SendResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
