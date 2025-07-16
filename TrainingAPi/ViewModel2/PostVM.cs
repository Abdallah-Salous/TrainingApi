using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrainingAPi.ViewModel2
{
    public class PostVM
    {
        public long Id { get; set; }

        public string Body { get; set; } = null!;

        public string Title { get; set; } = null!;

        public DateTime DatePosted { get; set; }

        public long? UserId { get; set; }
    }
}
