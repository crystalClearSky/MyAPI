using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using MyAppAPI.AppRepository;
using MyAppAPI.Models;
using MyAppAPI.Models.GalleryModel;
using MyAppAPI.Services;
using Xunit;
using Xunit.Abstractions;
using System;
using MyAppAPI.Context;
using MyAppAPI.Entities;
using Microsoft.EntityFrameworkCore;

public class TectClass
{
    private readonly ITestOutputHelper output;
    // private readonly IGalleryContextData ctxr;

    public TectClass(ITestOutputHelper output)
    {
        this.output = output;
        // this.ctxr = ctxr ?? throw new ArgumentNullException(nameof(ctxr));
    }
    // [Fact]
    // public void GalleryCardCount_Test()
    // {
    //     //Given
    //     IAvatarData db = new AvatarData();
    //     int likes = db.GetAllVotesForCard(2);
    //     //When
    //     output.WriteLine("Number {0}", likes);
    //     //Then
    //     // Assert.Equal(1, likes);
    // }
    [Fact]
    public void GalleryCard_Test()
    {
        //Given

        // GalleryCard gal = new GalleryCard();
        // int i = gal.GetVotes();

        IGalleryData db = new GalleryData();
        var gallery = db.getCardById(1);
        //When

        // Assert.Equal("Revenge of Koria", gallery.Title);
        Assert.Equal(3, gallery.UpVotesCount);

        //Then
    }
    [Fact]
    public void TestName()
    {
        //Given
        ICommentData commentData = new CommentData();
        Comment userComment = new Comment() { Id = 8, Message = "Just replying" };
        Comment userComment1 = new Comment() { Id = 9, Message = "Just replying" };
        Comment userComment2 = new Comment() { Id = 10, Message = "Just replying" };

        var comment = commentData.GetCommentById(1);

        comment.Replies.Append(userComment).ToList();
        comment.Replies.Append(userComment1).ToList();
        comment.Replies.Append(userComment2).ToList();

        //When
        //Then
        Assert.Equal(2, comment.Replies.Count());
    }
    [Fact]
    public void TestAddingEnum()
    {
        //Given
        IAvatarData avatarDb = new AvatarData();
        ICommentData commentData = new CommentData();
        //When


        var finalComment = new Comment()
        {
            Id = 12,
            AvatarId = 1,
            Message = "HELLO IS THIS WORKING?",
            PostedOn = DateTime.UtcNow,
        };
        // comments = comments.Concat(new List<Comment> {finalComment});
        CommentData.CurrentComments.Comments.Add(finalComment);
        // commentData.AddComment(finalComment);
        var comments = commentData.GetAllComment();
        var avatar = avatarDb.GetAvatarById(1);
        // avatar.Comments.ToList().Add(finalComment);
        foreach (var item in comments)
        {
            output.WriteLine("{0}, ID: {1}, CommentId: {2}", item.Message, item.AvatarId, item.Id);
        }
        var result = avatar.Comments.FirstOrDefault(c => c.Id == 12);
        if (result == null)
        {
            output.WriteLine("Nothing was found!!");
        }
        else
        {
            foreach (var item in avatar.Comments)
            {
                output.WriteLine("From Avatar: {0} ID:{1}", item.Message, item.Id);
            }

        }
        //Then
        Assert.Equal(4, avatar.TotalComments);
    }
    [Fact]
    public void GetCardsByTag_Test()
    {
        //Given
        Gallery gallery = new Gallery();
        IGalleryData galleryDb = new GalleryData();
        var tags = new List<Tag>()
        {
            new Tag() { TagItem = "drawings" },
            new Tag() { TagItem = "lady" }

        };
        var result = galleryDb.GetGalleryCardsByTags(tags);
        //When
        // var g = gallery.GetGalleryByTag();

        foreach (var item in result)
        {
            output.WriteLine("ID: {0}, Title: {1}", item.Id, item.Title);
        }
        //Then
        Assert.Equal(2, result.Count());
    }
    [Fact]
    public void AddCardToGallery_Test()
    {
        //Given
        IGalleryData galleryDb = new GalleryData();
        var galleryCard = new GalleryCard()
        {
            Id = 4,
            Title = "The Return Of The Thing",
            Descritpion = "A film about the return of the thing."
        };
        GalleryData.Current.Cards.Add(galleryCard);
        var cards = GalleryData.Current.getAllCards();

        //When
        foreach (var card in cards)
        {
            output.WriteLine("ID:{0}", card.Id);
        }
        //Then
    }
    [Fact]
    public void DeletAvatar_Test()
    {
        //Given
        var avatarToDelete = AvatarData.CurrentAvatar.GetAvatarById(1);
        AvatarData.CurrentAvatar.Avatars.Remove(avatarToDelete);
        var avatars = AvatarData.CurrentAvatar.GetAllAvatars();
        //When
        foreach (var avatar in avatars)
        {
            output.WriteLine("ID:{0}", avatar.Id);
        }
        //Then

        Assert.Equal(2, avatars.Count());
    }
    [Fact]
    public void AddAvatar_Test()
    {
        //Given
        AvatarData.CurrentAvatar.AddNewAvatar("192.168.105");
        var avatars = AvatarData.CurrentAvatar.GetAllAvatars();
        //When
        foreach (var avatar in avatars)
        {
            output.WriteLine("ID: {0}", avatar.Id);
        }
        //Then
        Assert.Equal(4, avatars.Count());
    }
    [Fact]
    public void GetLikesByAvatar_Test()
    {
        //Given

        var likes = CommentData.CurrentComments.GetLikesByAvatar(3); //GetLikesForAvatarById
                                                                     //When
        foreach (var like in likes)
        {
            output.WriteLine("ID: {0} LikedBy: {1}", like.Id, like.LikedById);
        }
        //Then
        Assert.Equal(2, likes.Count());
    }
    [Fact]
    public void AddLikeToConent_Test()
    {
        //Given
        var avatar = AvatarData.CurrentAvatar.GetAvatarById(3);
        var done = CommentData.CurrentComments.AddLikeToContent(avatar, 9);
        // Check for result
        var comment = CommentData.CurrentComments.GetCommentById(9);
        //When
        output.WriteLine("{0}", done);
        foreach (var like in comment.Likes)
        {
            output.WriteLine("Comment:{0} Id:{1}, Liked By:{2}", comment.Id, like.Id, like.LikedById);
        }
        //Then
        Assert.Equal(2, comment.Likes.Count);
    }

    [Fact]
    public void GetCommentsLikedByAvatar_Test()
    {
        //Given
        var comments = CommentData.CurrentComments.GetCommentsLikedByAvatar(1
        );
        //When
        foreach (var comment in comments)
        {
            output.WriteLine("ID: {0}, Message: {1}", comment.Id, comment.Message);
        }
        //Then
        Assert.Equal(1, comments.Count());
    }
    [Fact]
    public void RemoveVote_Test()
    {
        //Given
        var avatar = AvatarData.CurrentAvatar.GetAvatarById(1);
        GalleryData.Current.RemoveVote(avatar, 1);
        var gCard = GalleryData.Current.getCardById(1);
        //When
        foreach (var item in gCard.UpVotes)
        {
            output.WriteLine("ID: {0}, By: {1}", item.Id, item.VoteById);
        }
        if (gCard.UpVotes == null)
        {
            output.WriteLine("No Vote was found for ID: {0}", gCard.Id);
        }
        //Then
        Assert.Equal(1, gCard.UpVotes.Count);
    }

    [Fact]
    public void GetAvatarsThatLikeThisComment_Test()
    {
        //Given
        var avatars = CommentData.CurrentComments.GetAvatarsThatLikeThisComment(3);
        //When
        foreach (var avatar in avatars)
        {
            output.WriteLine("IP: {0}", avatar.CurrentIP);
        }
        //Then
        Assert.Equal(2, avatars.Count);
    }
    [Fact]
    public void DatabaseTest()
    {
    //Given
    using (var context = new ContentContext())
    {
        // context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var avatar = new AvatarEntity();
        context.AvatarEntity.Add(avatar);
        Debug.WriteLine($"Before save: {avatar.Id}");

        context.SaveChanges();
        Debug.WriteLine($"After save: {avatar.Id}");

        Assert.NotEqual(0, avatar.Id);
    }
    //When
    // var cards = this.ctxr.GetAllCards();
    // //Then
    // Assert.Equal(3, cards.Count());
    }

    [Fact]
    public void TestRetrieveData()
    {
    //Given
    using (var context = new ContentContext())
    {
         var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("TestRetrieveData");
        // context.Database.EnsureDeleted();
        // context.Database.EnsureCreated();
        
        var card = context.CardEntity.Include(c =>c.Comments);
        
        var comments = card.Select(c =>c.Comments).FirstOrDefault();
        foreach (var comment in comments)
        {
           output.WriteLine(comment.Message);
        }
        Assert.NotEqual(0, comments.Count);
    }
    //When
    
    //Then
    }
    
}