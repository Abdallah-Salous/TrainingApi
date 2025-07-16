using TrainingApiDAL.Models;

namespace TrainingAPi.ViewModel2
{
    public class AppUserVM
    {
        public string Email { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<PostVM> Posts { get; set; } = new List<PostVM>();
    }
}
