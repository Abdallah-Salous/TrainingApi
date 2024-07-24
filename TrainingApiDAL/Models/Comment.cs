using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TrainingApiDAL.Models;

[Table("Comment")]
public partial class Comment
{
    [Key]
    public long Id { get; set; }

    public string Body { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DatePosted { get; set; }

    public long? UserId { get; set; }

    public long? PostId { get; set; }

    [ForeignKey("PostId")]
    [InverseProperty("Comments")]
    public virtual Post? Post { get; set; }
}
