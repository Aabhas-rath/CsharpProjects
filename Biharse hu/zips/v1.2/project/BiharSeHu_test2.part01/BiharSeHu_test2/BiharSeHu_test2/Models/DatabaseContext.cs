namespace BiharSeHu_test2.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<LoginInfo> LoginInfoes { get; set; }
        public virtual DbSet<PictureContent> PictureContents { get; set; }
        public virtual DbSet<PictureMetadata> PictureMetadatas { get; set; }
        public virtual DbSet<postContent> postContents { get; set; }
        public virtual DbSet<postmetadata> postmetadatas { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginInfo>()
                .HasMany(e => e.postmetadatas)
                .WithRequired(e => e.LoginInfo)
                .HasForeignKey(e => e.authorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoginInfo>()
                .HasMany(e => e.UserInfoes)
                .WithRequired(e => e.LoginInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PictureContent>()
                .Property(e => e.takenBy)
                .IsUnicode(false);

            modelBuilder.Entity<postmetadata>()
                .HasMany(e => e.PictureMetadatas)
                .WithOptional(e => e.postmetadata)
                .HasForeignKey(e => e.AssociatedPostId);

            modelBuilder.Entity<postmetadata>()
                .HasMany(e => e.postContents)
                .WithRequired(e => e.postmetadata)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.UserInfoes)
                .WithMany(e => e.Tags)
                .Map(m => m.ToTable("UserInfo_Tags").MapLeftKey("tagId").MapRightKey("UserId"));

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }
}
