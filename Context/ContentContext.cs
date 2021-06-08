using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Data.Common;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyAppAPI.Entities;
using Microsoft.Extensions.Configuration;
using Entities;

namespace MyAppAPI.Context
{
    public class ContentContext : DbContext
    {
        public ContentContext(DbContextOptions<ContentContext> opt) : base(opt)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            // Database.EnsureCreated();
        }
        public ContentContext()
        {

        }

        public DbSet<CardEntity> CardEntity { get; set; }
        public DbSet<AvatarEntity> AvatarEntity { get; set; }
        public DbSet<CommentEntity> CommentEntity { get; set; }
        public DbSet<TagEntity> TagEntity { get; set; }
        public DbSet<ReplyEntity> ReplyEntity { get; set; }
        public DbSet<ImageEntity> ImageEntity { get; set; }
        public DbSet<FruitItemsEntity> FruitItemsEntity { get; set; }
        public DbSet<GuestEntity> GuestEntity { get; set; }
        public DbSet<UnregisteredGuestEnitity> UnregisteredGuestEnitity { get; set; }
        public DbSet<ViewEntity> ViewEntity { get; set; }
        public DbSet<PageEntity> PageEntity { get; set; }
        public DbSet<UniqueVisitEntity> UniqueVisitEntity { get; set; }
        public DbSet<AnonymousEntity> AnonymousEntity { get; set; }
        public DbSet<EnableOptionsEntity> EnableOptionsEntity { get; set; }
        public DbSet<ContactEntity> ContactEntity { get; set; }
        public DbSet<LinkEntity> LinkEntity { get; set; }
        public DbSet<LikeEntity> LikeEntity { get; set; }
        public DbSet<VoteEntity> VoteEntity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = "Server=(localdb)\\mssqllocaldb;Database=ContentContextDB;Trusted_Connection=True;";
                optionsBuilder/*.UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging()*/
                    .UseSqlServer(connectionString);
            }
        }

        // public DbSet<LikeEntity> LikeEntity { get; set; }
        // public DbSet<VoteEntity> VoteEntity { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserEntity>()
            .HasData(
                new UserEntity()
                {
                    Id = true,
                    Name = "Justice",
                    AvatarImgUrl = "Watermelon.svg"
                }
            );

            // Card
            modelBuilder.Entity<CardEntity>()
                .HasData(
                    new CardEntity()
                    {
                        Id = 1,
                        Title = "Return of Golia",
                        //ImageUrl = "https://cdna.artstation.com/p/assets/images/images/004/404/208/medium/justice-headbush-ladywip.jpg?1483463051",
                        CreatedOn = new DateTime(2020, 11, 10, 19, 23, 5),
                        UpdatedOn = DateTime.Now,
                        Description = "A story about a princess you travels the world on a mission to find dragons.",
                        IsSuperUser = true,
                        PostType = PostType.Gallery
                    },
                    new CardEntity()
                    {
                        Id = 2,
                        Title = "Thunder & Lighting",
                        //ImageUrl = "https://cdna.artstation.com/p/assets/images/images/000/445/632/medium/justice-headbush-5fe06d791b271f6d0d1b1cd14d44c4c8.jpg?1422789505",
                        CreatedOn = new DateTime(2020, 7, 19, 16, 20, 8),
                        UpdatedOn = DateTime.Now,
                        Description = "A storm is approaching and this is just the beginning of something big.",
                        IsSuperUser = true,
                        PostType = PostType.Gallery
                    },
                    new CardEntity()
                    {
                        Id = 3,
                        Title = "Return of Repunzal",
                        //ImageUrl = "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508",
                        CreatedOn = new DateTime(2020, 7, 19, 10, 18, 15),
                        UpdatedOn = DateTime.Now,
                        Description = "A storm is approaching and this is just the beginning of something big.",
                        IsSuperUser = true,
                        PostType = PostType.Gallery
                    },
                    new CardEntity()
                    {
                        Id = 4,
                        Title = "Discovering of Amazing 3D Works From ArtStation",
                        //ImageUrl = "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508",
                        CreatedOn = new DateTime(2021, 2, 19, 15, 35, 45),
                        UpdatedOn = DateTime.Now,
                        Description = "This is a blog discussing works found on artstation.",
                        Content = "Here are some works I found that I like from http://www.artstation.com https://cdna.artstation.com/p/assets/images/images/034/796/486/large/daniel-kho-lux-wip13.jpg?1613253409 And a little more text https://cdna.artstation.com/p/assets/images/images/035/653/308/large/qing-xu-.jpg?1615523280",
                        IsSuperUser = true,
                        PostType = PostType.Blog
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
                    ByAvatarId = 2,
                },
                new TagEntity()
                {
                    Id = 6,
                    CardId = 1,
                    TagItem = "3dworks",
                    ByAvatarId = 2,
                },
                new TagEntity()
                {
                    Id = 7,
                    CardId = 2,
                    TagItem = "3dworks",
                    ByAvatarId = 2,
                },
                new TagEntity()
                {
                    Id = 8,
                    CardId = 3,
                    TagItem = "3dworks",
                    ByAvatarId = 2,
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
                    Name = "Apple",
                    CurrentIP = "192.168.0.101",
                    GuestId = 1,
                    IsCheckedIn = true,
                    LastCheckedIn = new DateTime(2020, 06, 28),
                    AvatarImgUrl = "Apple.svg",
                    JoinedOn = new DateTime(2020, 1, 12)
                },
                new AvatarEntity()
                {
                    Id = 2,
                    Name = "Avocado",
                    CurrentIP = "192.168.0.102",
                    IsCheckedIn = false,
                    LastCheckedIn = new DateTime(2020, 06, 18),
                    AvatarImgUrl = "Avocado.svg",
                    JoinedOn = new DateTime(2020, 4, 26)
                },
                new AvatarEntity()
                {
                    Id = 3,
                    Name = "Banana",
                    CurrentIP = "192.168.0.108",
                    IsCheckedIn = false,
                    LastCheckedIn = new DateTime(2020, 06, 04),
                    AvatarImgUrl = "Banana.svg",
                    JoinedOn = new DateTime(2020, 5, 16)
                }
            );

            modelBuilder.Entity<EnableOptionsEntity>()
            .HasData(
                new EnableOptionsEntity()
                {
                    Id = 1,
                    IpAddress = "192.168.0.102",
                    EnableGuest = false,
                    IsMember = true
                },
                new EnableOptionsEntity()
                {
                    Id = 2,
                    IpAddress = "192.168.0.101",
                    EnableGuest = false,
                    IsMember = true
                },
                new EnableOptionsEntity()
                {
                    Id = 3,
                    IpAddress = "192.168.0.108",
                    EnableGuest = false,
                    IsMember = true
                }
            );

            modelBuilder.Entity<AnonymousEntity>()
            .HasData(
                new AnonymousEntity()
                {
                    Id = 1,
                    FirstSeen = new DateTime(2021, 01, 01),
                    LastSeen = new DateTime(2021, 01, 02)
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

            modelBuilder.Entity<GuestEntity>()
            .HasData(
                new GuestEntity()
                {
                    Id = 1,
                    IPAddress = "192.168.0.101",
                    Region = "Tokyo",
                    Country = "Japan",
                    FirstVisit = new DateTime(2021, 01, 02),
                    LastVisit = new DateTime(2021, 01, 03)
                }
            );

            modelBuilder.Entity<PageEntity>()
            .HasData(
                new PageEntity()
                {
                    Id = 1,
                    PageName = "Content"
                }
            );

            modelBuilder.Entity<UniqueVisitEntity>()
            .HasData(
                new UniqueVisitEntity()
                {
                    Id = 1,
                    PageId = 1,
                    AvatarVisited = 1,
                    TimeVisited = new DateTime(2021, 01, 02)
                },
                new UniqueVisitEntity()
                {
                    Id = 2,
                    PageId = 1,
                    AvatarVisited = 1,
                    TimeVisited = new DateTime(2021, 01, 03)
                }
            );

            modelBuilder.Entity<ReplyEntity>()
            .HasKey(r => new { r.AvatarId, r.CommentId });

            // modelBuilder.Entity<ReplyEntity>()
            // .HasOne(r => r.ResponseComment)
            // .WithMany(c => c.Response);

            modelBuilder.Entity<CommentEntity>()
            .HasMany(r => r.RepliesTo).WithOne(r => r.ReplyTo)

            .IsRequired(false).OnDelete(DeleteBehavior.ClientCascade);

            // Comments

            modelBuilder.Entity<CommentEntity>()
            .HasData(
                new CommentEntity()
                {
                    Id = 1, // if avatar2 comment = responseid then collect these for avatar2
                    CardEntityId = 1,
                    AvatarId = 2,
                    Message = "This is great work!!",
                    FlaggedCommentMessages = "Spam"
                },
                new CommentEntity()
                {
                    Id = 2, // if avatar2 comment = responseid then collect these for avatar2
                    CardEntityId = 1,
                    AvatarId = 2,
                    Message = "You deserve a prize!! &img[0] and this &img[1]"
                },
                new CommentEntity()
                {
                    Id = 3,
                    CardEntityId = 1,
                    AvatarId = 3,
                    Message = "I Totally agree!",
                    IsReply = true,
                    ReplyToCommentId = 2,
                    ResponseToAvatarId = 2
                },
                new CommentEntity()
                {
                    Id = 4,
                    CardEntityId = 1,
                    AvatarId = 1,
                    Message = "Nice lady character!"
                },
                new CommentEntity()
                {
                    Id = 5,
                    CardEntityId = 1,
                    AvatarId = 2,
                    Message = "Ya, Pretty cool lady",
                    IsReply = true,
                    ReplyToCommentId = 4,
                    ResponseToAvatarId = 1

                },
                new CommentEntity()
                {
                    Id = 6,
                    CardEntityId = 1,
                    AvatarId = 3,
                    Message = "This 3D lady is awesome!!",
                    IsReply = true,
                    ReplyToCommentId = 5,
                    ResponseToAvatarId = 2
                }


            // new CommentEntity()
            // {
            //     Id = 7,
            //     CardEntityId = 1,
            //     Message = "SuperUser - Thanks for the compliments!",
            //     IsSuperUser = true,
            //     ReplyToCommentId = 1,
            //     IsReply = true
            // }

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

            modelBuilder.Entity<LinkEntity>()
            .HasData(
                new LinkEntity()
                {
                    Id = 1,
                    Link = "https://media.giphy.com/media/mYiBd0ttjosz6/giphy.gif",
                    LinkIndex = "img[0]",
                    CommentEntityId = 2,
                    LinkType = LinkType.Gif,
                    AddedOn = new DateTime(2021, 02, 01)
                },
            new LinkEntity()
            {
                Id = 2,
                Link = "https://media.giphy.com/media/HoM8C39iqS9H2/giphy.gif",
                LinkIndex = "img[1]",
                CommentEntityId = 2,
                LinkType = LinkType.Gif,
                AddedOn = new DateTime(2021, 02, 01)
            }
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

            modelBuilder.Entity<ImageEntity>()
            .HasData(
                new ImageEntity()
                {
                    Id = 1,
                    ImageUrl = "https://cdna.artstation.com/p/assets/images/images/004/404/208/medium/justice-headbush-ladywip.jpg?1483463051",
                    ThumbnailUrl = "https://cdna.artstation.com/p/assets/images/images/004/404/208/medium/justice-headbush-ladywip.jpg?1483463051",
                    QuickDescription = "Practise practise practise.",
                    CardEntityId = 1,
                    UploadedOn = new DateTime(2020, 12, 23),
                    UpdatedOn = new DateTime(2020, 12, 24),
                    Catergory = Catergory.Image
                },
                new ImageEntity()
                {
                    Id = 2,
                    ImageUrl = "https://cdna.artstation.com/p/assets/images/images/000/445/632/medium/justice-headbush-5fe06d791b271f6d0d1b1cd14d44c4c8.jpg?1422789505",
                    ThumbnailUrl = "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508",
                    QuickDescription = "Lots of work but great outcome.",
                    CardEntityId = 2,
                    UploadedOn = new DateTime(2020, 12, 20),
                    UpdatedOn = new DateTime(2020, 12, 22),
                    Catergory = Catergory.Image
                },
                new ImageEntity()
                {
                    Id = 3,
                    ImageUrl = "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508",
                    ThumbnailUrl = "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508",
                    QuickDescription = "A model which i created during my spare time.",
                    CardEntityId = 3,
                    UploadedOn = new DateTime(2020, 11, 28),
                    UpdatedOn = new DateTime(2020, 12, 12),
                    Catergory = Catergory.Image
                },
                new ImageEntity()
                {
                    Id = 4,
                    ImageUrl = "https://cdna.artstation.com/p/assets/images/images/000/445/652/large/justice-headbush-t.jpg?1422790444",
                    ThumbnailUrl = "https://cdna.artstation.com/p/assets/images/images/000/445/652/large/justice-headbush-t.jpg?1422790444",
                    QuickDescription = "More zbrush practise work.",
                    CardEntityId = 1,
                    UploadedOn = new DateTime(2020, 11, 29),
                    UpdatedOn = new DateTime(2020, 12, 13),
                    Catergory = Catergory.Image
                },
                new ImageEntity()
                {
                    Id = 5,
                    ImageUrl = "https://vimeo.com/83443121",
                    ThumbnailUrl = "https://i.vimeocdn.com/video/459920343_640.jpg",
                    QuickDescription = "Entity Framework Best Practices - Should EFCore Be Your Data Access of Choice?",
                    CardEntityId = 2,
                    UploadedOn = new DateTime(2021, 01, 09),
                    UpdatedOn = new DateTime(2021, 02, 10),
                    Catergory = Catergory.VimeoVideo
                },
                new ImageEntity()
                {
                    Id = 6,
                    ImageUrl = "",
                    ThumbnailUrl = "https://cdna.artstation.com/p/assets/images/images/034/796/486/large/daniel-kho-lux-wip13.jpg?1613253409",
                    QuickDescription = "",
                    CardEntityId = 4,
                    UploadedOn = new DateTime(2021, 02, 08),
                    UpdatedOn = new DateTime(2021, 02, 12),
                    Catergory = Catergory.Image
                }
            );

            // Votes

            modelBuilder.Entity<VoteEntity>()
            .HasData(
                new VoteEntity()
                {
                    Id = 1,
                    VoteById = 2, // To ensure we know which user(avatar) has already voted.
                    CardId = 1
                }
            );

            // modelBuilder.Entity<ReplyEntity>()
            // .HasData(
            //     new ReplyEntity()
            //     {
            //         AvatarId = 3,
            //         CommentId = 3,
            //         // ReplyOnId = 1,
            //         ResponseToCommentId = 2,
            //     },
            //     new ReplyEntity()
            //     {
            //         AvatarId = 2,
            //         CommentId = 5,
            //         // ReplyOnId = 2,
            //         ResponseToCommentId = 4,
            //     },
            //     new ReplyEntity()
            //     {
            //         AvatarId = 3,
            //         CommentId = 6,
            //         // ReplyOnId = 3,
            //         ResponseToCommentId = 5,
            //     },
            //     new ReplyEntity()
            //     {
            //         AvatarId = 0,
            //         IsSuperUser = true,
            //         CommentId = 7,
            //         // ReplyOnId = 4,
            //         ResponseToCommentId = 1,
            //     }
            // );

            modelBuilder.Entity<FlagEntity>()
            .HasData(
                new FlagEntity()
                {
                    id = 1,
                    ReasonText = "Just because I like it so much!",
                    AvatarId = 1,
                    CommentEntityId = 1
                }
            );

            modelBuilder.Entity<FruitItemsEntity>()
            .HasData(
                new FruitItemsEntity() { Id = 1, FruitName = "Broccoli", FruitImg = "Broccoli.svg" },
                new FruitItemsEntity() { Id = 2, FruitName = "Cactus", FruitImg = "Cactus.svg" },
                new FruitItemsEntity() { Id = 3, FruitName = "Citrus", FruitImg = "Citrus.svg" },
                new FruitItemsEntity() { Id = 4, FruitName = "Grapes", FruitImg = "Grapes.svg" },
                new FruitItemsEntity() { Id = 5, FruitName = "Hazelnut", FruitImg = "Hazelnut.svg" },
                new FruitItemsEntity() { Id = 6, FruitName = "Melon", FruitImg = "Melon.svg" },
                new FruitItemsEntity() { Id = 7, FruitName = "Nut", FruitImg = "Nut.svg" },
                new FruitItemsEntity() { Id = 8, FruitName = "Pear", FruitImg = "Pear.svg" },
                new FruitItemsEntity() { Id = 9, FruitName = "Plum", FruitImg = "Plum.svg" },
                new FruitItemsEntity() { Id = 10, FruitName = "Pomegranate", FruitImg = "Pomegranate.svg" },
                new FruitItemsEntity() { Id = 11, FruitName = "Raspberry", FruitImg = "Raspberry.svg" }
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