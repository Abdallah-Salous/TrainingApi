using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TrainingApiDAL.Models;

[Table("Post")]
public partial class Post
{
    [Key]
    public long Id { get; set; }

    public string Body { get; set; } = null!;

    [StringLength(250)]
    public string Title { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DatePosted { get; set; }

    public long? UserId { get; set; }

    [InverseProperty("Post")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [ForeignKey("UserId")]
    [InverseProperty("Posts")]
    [JsonIgnore]
    public virtual AppUser? User { get; set; }
}
