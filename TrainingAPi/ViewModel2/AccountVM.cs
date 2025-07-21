using System.ComponentModel.DataAnnotations;

namespace TrainingAPi.ViewModel2
{
    public class TokenVM
    {
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
