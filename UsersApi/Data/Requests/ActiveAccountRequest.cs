using System.ComponentModel.DataAnnotations;

namespace UsersApi.Data.Requests
{
    public class ActiveAccountRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ActivationCode { get; set; }

    }
}
