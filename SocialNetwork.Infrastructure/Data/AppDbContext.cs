using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Infrastructure.Data;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comment__3213E83FCEFB6287");

            entity.ToTable("comment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Comment)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKs1slvnkuemjsq2kj4h3vhx7i1");

            entity.HasOne(d => d.User).WithMany(p => p.Comment)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKoo5phijy22unidgkw0sipcs74");
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__friend_r__3213E83F6410B4C3");

            entity.ToTable("friend_request");

            entity.HasIndex(e => new { e.SenderId, e.ReceiverId }, "UK1o6k35asg93qa1wjg8chjd5rf").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Receiver).WithMany(p => p.FriendRequestReceiver)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK28iulg0paqbykkbq2dtgnn0a0");

            entity.HasOne(d => d.Sender).WithMany(p => p.FriendRequestSender)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK5w70rgoa5dp1nn2g21b3u0umm");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__post__3213E83F45240BBE");

            entity.ToTable("post");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.ImageId)
                .HasMaxLength(100)
                .HasColumnName("image_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Post)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKrh90w2rgxa8jj5r0kjlaims1y");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rating__3213E83FA366B7BD");

            entity.ToTable("rating");

            entity.HasIndex(e => new { e.UserId, e.PostId }, "UKcxrkpte3mtt4fnj1p803sp1sh").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.Stars).HasColumnName("stars");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Rating)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKq23oorcfn21b1owhgcqle69oq");

            entity.HasOne(d => d.User).WithMany(p => p.Rating)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK740eouanmwdsi22ljt2tnwri8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK___user__3213E83FB4988D3F");

            entity.ToTable("_user");

            entity.HasIndex(e => e.Email, "UKk11y3pdtsrjgy8w9b6q4bjwrx").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfileImageId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_image_id");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");

            entity.HasMany(d => d.Friend).WithMany(p => p.UserNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFriends",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK6cnqwvq7lstrjobcyr73yxnyu"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK4uhy0dj0lyfha2thsiapm07m4"),
                    j =>
                    {
                        j.HasKey("UserId", "FriendId").HasName("PK__user_fri__FA44291A245DC703");
                        j.ToTable("user_friends");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("FriendId").HasColumnName("friend_id");
                    });

            entity.HasMany(d => d.UserNavigation).WithMany(p => p.Friend)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFriends",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK4uhy0dj0lyfha2thsiapm07m4"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK6cnqwvq7lstrjobcyr73yxnyu"),
                    j =>
                    {
                        j.HasKey("UserId", "FriendId").HasName("PK__user_fri__FA44291A245DC703");
                        j.ToTable("user_friends");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<long>("FriendId").HasColumnName("friend_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
