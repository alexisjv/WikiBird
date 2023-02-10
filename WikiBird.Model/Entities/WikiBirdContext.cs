using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WikiBird.Model.Entities;

public partial class WikiBirdContext : DbContext
{
    public WikiBirdContext()
    {
    }

    public WikiBirdContext(DbContextOptions<WikiBirdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bird> Birds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=wikibirdserver.database.windows.net;Database=wikibird;User Id=adminsql;Password=W1k1b1rd;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bird>(entity =>
        {
            entity.ToTable("Bird");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
