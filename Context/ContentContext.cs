using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Data.Common;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyAppAPI.Entities;

namespace MyAppAPI.Context
{
    public class ContentContext : DbContext
    {
        public ContentContext(DbContextOptions<ContentContext> opt) : base(opt)
        {
            // Database.EnsureCreated();
        }
        public DbSet<CardEntity> CardEntity { get; set; }
        public DbSet<AvatarEntity> AvatarEntity { get; set; }
        public DbSet<CommentEntity> CommentEntity { get; set; }

        // public DbSet<LikeEntity> LikeEntity { get; set; }
        // public DbSet<VoteEntity> VoteEntity { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserEntity>()
            .HasData(
                new UserEntity()
                {
                    Id = 1,
                    Name = "Justice",
                    IsSuperUser = true
                }
            );

            // Card
            modelBuilder.Entity<CardEntity>()
                .HasData(
                    new CardEntity()
                    {
                        Id = 1,
                        Title = "Return of Golia",
                        ImageUrl = "image.jpg",
                        CreatedOn = new DateTime(2020, 11, 10),
                        UpdatedOn = DateTime.Now,
                        Flag = false,
                        Descritpion = "A story about a princess you travels the world on a mission to find dragons.",
                        UserId = 1
                    },
                    new CardEntity()
                    {
                        Id = 2,
                        Title = "Thunder and Lighting",
                        ImageUrl = "image.jpg",
                        CreatedOn = new DateTime(2020, 7, 19),
                        UpdatedOn = DateTime.Now,
                        Flag = false,
                        Descritpion = "A storm is approaching and this is just the beginning of something big.",
                        UserId = 1
                    }
                );
            modelBuilder.Entity<TagEntity>()
            .HasData(
                new TagEntity()
                {
                    Id = 1,
                    CardId = 1,
                    TagItem = "golia",
                    ByAvatarId = 3
                },
                new TagEntity()
                {
                    Id = 2,
                    CardId = 1,
                    TagItem = "zbrush",
                    ByAvatarId = 3
                },
                new TagEntity()
                {
                    Id = 3,
                    CardId = 1,
                    TagItem = "story",
                    ByAvatarId = 2
                    
                },
                new TagEntity()
                {
                    Id = 4,
                    CardId = 1,
                    TagItem = "dragon",
                    ByAvatarId = 1
                },
                new TagEntity()
                {
                    Id = 5,
                    CardId = 2,
                    TagItem = "crystal",
                    ByAvatarId = 2
                }
            );

            // modelBuilder.Entity<CardEntity>()
            //     .HasMany(c => c.UpVotes)
            //     .WithOne(e => e.CardEntity);

            // modelBuilder.Entity<CardEntity>()
            //     .HasMany(c => c.Comments)
            //     .WithOne(e => e.CardEntity);

            // Avatar
            modelBuilder.Entity<AvatarEntity>()
            .HasData(
                new AvatarEntity()
                {
                    Id = 1,
                    CurrentIP = "192.168.0.101",
                    IsCheckedIn = true,
                    AvatarImgUrl = "avatar.jpg",
                    JoinedOn = new DateTime(2020, 1, 12)
                },
                new AvatarEntity()
                {
                    Id = 2,
                    CurrentIP = "192.168.0.102",
                    IsCheckedIn = false,
                    AvatarImgUrl = "avatar.jpg",
                    JoinedOn = new DateTime(2020, 4, 26)
                },
                new AvatarEntity()
                {
                    Id = 3,
                    CurrentIP = "192.168.0.108",
                    IsCheckedIn = false,
                    AvatarImgUrl = "avatar.jpg",
                    JoinedOn = new DateTime(2020, 5, 16)
                }
            );

            modelBuilder.Entity<AvatarEntity>()
            .HasMany(c => c.Likes)
            .WithOne(e => e.AvatarEntity);

            modelBuilder.Entity<AvatarEntity>()
            .HasMany(c => c.UpVotes)
            .WithOne(e => e.AvatarEntity);

            // modelBuilder.Entity<AvatarEntity>()
            // .HasMany(c => c.Comments)
            // .WithOne(e => e.AvatarEntity);

            // ReplyEntity

            modelBuilder.Entity<ReplyEntity>()
            .HasKey(r => new {r.AvatarId, r.CommentId});
            
            modelBuilder.Entity<ReplyEntity>()
            .HasOne(r => r.ResponseComment)
            .WithOne(c =>c.Response);
            // Comments

            modelBuilder.Entity<CommentEntity>()
            .HasData(
                new CommentEntity()
                {
                    Id = 1, // if avatar2 comment = responseid then collect these for avatar2
                    CardId = 1,
                    AvatarId = 2,
                    Message = "This is great work!!"
                },
                new CommentEntity()
                {
                    Id = 2, // if avatar2 comment = responseid then collect these for avatar2
                    CardId = 1,
                    AvatarId = 2,
                    Message = "You deserve a prize!!"
                },
                new CommentEntity()
                {
                    Id = 3,
                    CardId = 1,
                    AvatarId = 3,
                    Message = "I Totally agree!",
                },
                new CommentEntity()
                {
                    Id = 4,
                    CardId = 1,
                    AvatarId = 1,
                    Message = "Nice lady character!",
                },
                new CommentEntity()
                {
                    Id = 5,
                    CardId = 1,
                    AvatarId = 2,
                    Message = "Ya, Pretty cool lady",
                },
                new CommentEntity()
                {
                    Id = 6,
                    CardId = 1,
                    AvatarId = 3,
                    Message = "This 3D lady is awesome!!",
                },
                new CommentEntity()
                {
                    Id = 7,
                    CardId = 1,
                    UserId = 1,
                    Message = "SuperUser - Thanks for the compliments!",
                    IsSuperUser = true
                }
                // new CommentEntity()
                // {
                //     Id = 8,
                //     CardId = 1,
                //     AvatarId = 3,
                //     UserId = 1,
                //     Message = "TEST!",
                //     IsSuperUser = true
                // }
                
            );
            // modelBuilder.Entity<CommentEntity>()
            // .HasMany(c => c.Likes)
            // .WithOne(e => e.CommentEntity);

            // modelBuilder.Entity<CommentEntity>()
            // .HasMany(c => c.Replies);

            base.OnModelCreating(modelBuilder);

            //Likes

            modelBuilder.Entity<LikeEntity>()
            .HasData(
                new LikeEntity()
                {
                    Id = 1,
                    LikedById = 2, // Liked by Avatar 2
                    CommentId = 1, // This like is on CommentID 1
                    HasLiked = true // If current user(avatar) has like this comment then true and the heart will be RED else its GRAY.
                }
            );

            // Votes

            modelBuilder.Entity<VoteEntity>()
            .HasData(
                new VoteEntity()
                {
                    Id = 1,
                    VoteById = 2, // To ensure we know which user(avatar) has already voted.
                    UpVote = true, // If this is true then it will prevent the current user(avatar) from voting more than once.
                    CardId = 1
                }
            );

            modelBuilder.Entity<ReplyEntity>()
            .HasData(
                new ReplyEntity()
                {
                    AvatarId = 3,
                    CommentId = 2,
                    // ReplyOnId = 1,
                    ResponseToCommentId = 2,
                    HasReplied = true
                },
                new ReplyEntity()
                {
                    AvatarId = 2,
                    CommentId = 5,
                    // ReplyOnId = 2,
                    ResponseToCommentId = 5,
                    HasReplied = true
                },
                new ReplyEntity()
                {
                    AvatarId = 3,
                    CommentId = 6,
                    // ReplyOnId = 3,
                    ResponseToCommentId = 5,
                    HasReplied = true
                },
                new ReplyEntity()
                {
                    UserId = 1,
                    CommentId = 7,
                    // ReplyOnId = 4,
                    ResponseToCommentId = 1,
                    HasReplied = true
                }
            );

            // modelBuilder.Entity<ReplyOnEntity>()
            // .HasData(
            //     new ReplyOnEntity()
            //     {
            //         Id = 1,
            //         CommentId = 2
            //     },
            //     new ReplyOnEntity()
            //     {
            //         Id = 2,
            //         CommentId = 5
            //     },
            //     new ReplyOnEntity()
            //     {
            //         Id = 3,
            //         CommentId = 5
            //     },
            //     new ReplyOnEntity()
            //     {
            //         Id = 4,
            //         CommentId = 1,
            //     }
            // );
        }
    }
}