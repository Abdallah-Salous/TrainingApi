using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainingApiDAL.Models;

public partial class TrainingTestDbContext : DbContext
{
    public TrainingTestDbContext()
    {
    }

    public TrainingTestDbContext(DbContextOptions<TrainingTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Post).WithMany(p => p.Comments).HasConstraintName("FK_Comment_Post");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Posts).HasConstraintName("FK_Post_AppUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
