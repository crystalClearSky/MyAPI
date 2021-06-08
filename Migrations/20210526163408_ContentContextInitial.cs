using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAppApi.Migrations
{
    public partial class ContentContextInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnableOptionsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false),
                    IsUnregistered = table.Column<bool>(type: "bit", nullable: false),
                    EnableGuest = table.Column<bool>(type: "bit", nullable: false),
                    IsMember = table.Column<bool>(type: "bit", nullable: false),
                    IsLiving = table.Column<bool>(type: "bit", nullable: false),
                    VisitCount = table.Column<int>(type: "int", nullable: false),
                    FirstSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnableOptionsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FruitItemsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FruitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FruitImg = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FruitItemsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnregisteredGuestEnitity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnableGuest = table.Column<bool>(type: "bit", nullable: false),
                    FirstVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnregisteredGuestEnitity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvatarImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuperUser = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    SearchIndexValue = table.Column<int>(type: "int", nullable: false),
                    TagIsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReplyBoxIsActive = table.Column<bool>(type: "bit", nullable: false),
                    PostType = table.Column<int>(type: "int", nullable: false),
                    PageEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardEntity_PageEntity_PageEntityId",
                        column: x => x.PageEntityId,
                        principalTable: "PageEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardEntity_UserEntity_IsSuperUser",
                        column: x => x.IsSuperUser,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuickDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardEntityId = table.Column<int>(type: "int", nullable: false),
                    UploadedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Catergory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageEntity_CardEntity_CardEntityId",
                        column: x => x.CardEntityId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniqueVisitEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageId = table.Column<int>(type: "int", nullable: false),
                    AvatarVisited = table.Column<int>(type: "int", nullable: true),
                    GuestVisited = table.Column<int>(type: "int", nullable: true),
                    UnregisteredGuestVisited = table.Column<int>(type: "int", nullable: true),
                    AnonymousId = table.Column<int>(type: "int", nullable: true),
                    TimeVisited = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueVisitEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniqueVisitEntity_AnonymousEntity_AnonymousId",
                        column: x => x.AnonymousId,
                        principalTable: "AnonymousEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UniqueVisitEntity_GuestEntity_GuestVisited",
                        column: x => x.GuestVisited,
                        principalTable: "GuestEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UniqueVisitEntity_PageEntity_PageId",
                        column: x => x.PageId,
                        principalTable: "PageEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniqueVisitEntity_UnregisteredGuestEnitity_UnregisteredGuestVisited",
                        column: x => x.UnregisteredGuestVisited,
                        principalTable: "UnregisteredGuestEnitity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ViewEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewedByGuestId = table.Column<int>(type: "int", nullable: true),
                    ViewedByAvatarId = table.Column<int>(type: "int", nullable: true),
                    ViewedByUnregisteredGuest = table.Column<int>(type: "int", nullable: true),
                    AnonymousId = table.Column<int>(type: "int", nullable: true),
                    CardEntityId = table.Column<int>(type: "int", nullable: true),
                    NumberOfTimesSeen = table.Column<int>(type: "int", nullable: false),
                    FirstSeen = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewEntity_AnonymousEntity_AnonymousId",
                        column: x => x.AnonymousId,
                        principalTable: "AnonymousEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewEntity_CardEntity_CardEntityId",
                        column: x => x.CardEntityId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewEntity_GuestEntity_ViewedByGuestId",
                        column: x => x.ViewedByGuestId,
                        principalTable: "GuestEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ViewEntity_UnregisteredGuestEnitity_ViewedByUnregisteredGuest",
                        column: x => x.ViewedByUnregisteredGuest,
                        principalTable: "UnregisteredGuestEnitity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResponseToAvatarId = table.Column<int>(type: "int", nullable: true),
                    ReplyToCommentId = table.Column<int>(type: "int", nullable: true),
                    CardEntityId = table.Column<int>(type: "int", nullable: false),
                    IsReply = table.Column<bool>(type: "bit", nullable: false),
                    SearchIndexValue = table.Column<int>(type: "int", nullable: false),
                    IsSuperUser = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FlaggedCommentMessages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medium = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEntity_CardEntity_CardEntityId",
                        column: x => x.CardEntityId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentEntity_CommentEntity_ReplyToCommentId",
                        column: x => x.ReplyToCommentId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentEntity_UserEntity_IsSuperUser",
                        column: x => x.IsSuperUser,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlagEntity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonText = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AvatarId = table.Column<int>(type: "int", nullable: false),
                    CommentEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlagEntity", x => x.id);
                    table.ForeignKey(
                        name: "FK_FlagEntity_CommentEntity_CommentEntityId",
                        column: x => x.CommentEntityId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkIndex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentEntityId = table.Column<int>(type: "int", nullable: false),
                    LinkType = table.Column<int>(type: "int", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkEntity_CommentEntity_CommentEntityId",
                        column: x => x.CommentEntityId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvatarEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuestId = table.Column<int>(type: "int", nullable: true),
                    IsCheckedIn = table.Column<bool>(type: "bit", nullable: false),
                    AvatarImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastCheckedIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvatarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvatarEntity_FlagEntity_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "FlagEntity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvatarEntity_GuestEntity_GuestId",
                        column: x => x.GuestId,
                        principalTable: "GuestEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LikeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasLiked = table.Column<bool>(type: "bit", nullable: false),
                    LikedById = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeEntity_AvatarEntity_LikedById",
                        column: x => x.LikedById,
                        principalTable: "AvatarEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeEntity_CommentEntity_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplyEntity",
                columns: table => new
                {
                    AvatarId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    ResponseToCommentId = table.Column<int>(type: "int", nullable: true),
                    IsSuperUser = table.Column<bool>(type: "bit", nullable: true),
                    AvatarEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyEntity", x => new { x.AvatarId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_ReplyEntity_AvatarEntity_AvatarEntityId",
                        column: x => x.AvatarEntityId,
                        principalTable: "AvatarEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyEntity_CommentEntity_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReplyEntity_CommentEntity_ResponseToCommentId",
                        column: x => x.ResponseToCommentId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyEntity_UserEntity_IsSuperUser",
                        column: x => x.IsSuperUser,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    ByAvatarId = table.Column<int>(type: "int", nullable: true),
                    IsSuperUser = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CardsWithThisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagEntity_AvatarEntity_ByAvatarId",
                        column: x => x.ByAvatarId,
                        principalTable: "AvatarEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagEntity_CardEntity_CardId",
                        column: x => x.CardId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagEntity_UserEntity_IsSuperUser",
                        column: x => x.IsSuperUser,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoteEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoteById = table.Column<int>(type: "int", nullable: true),
                    VoteByGuest = table.Column<int>(type: "int", nullable: true),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteEntity_AvatarEntity_VoteById",
                        column: x => x.VoteById,
                        principalTable: "AvatarEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoteEntity_CardEntity_CardId",
                        column: x => x.CardId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteEntity_GuestEntity_VoteByGuest",
                        column: x => x.VoteByGuest,
                        principalTable: "GuestEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AnonymousEntity",
                columns: new[] { "Id", "FirstSeen", "LastSeen" },
                values: new object[] { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "AvatarEntity",
                columns: new[] { "Id", "AvatarId", "AvatarImgUrl", "CurrentIP", "GuestId", "IsCheckedIn", "JoinedOn", "LastCheckedIn", "Name" },
                values: new object[,]
                {
                    { 2, null, "Avocado.svg", "192.168.0.102", null, false, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avocado" },
                    { 3, null, "Banana.svg", "192.168.0.108", null, false, new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Banana" }
                });

            migrationBuilder.InsertData(
                table: "EnableOptionsEntity",
                columns: new[] { "Id", "EnableGuest", "FirstSeen", "IpAddress", "IsAnonymous", "IsLiving", "IsMember", "IsUnregistered", "LastSeen", "VisitCount" },
                values: new object[,]
                {
                    { 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.168.0.102", false, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.168.0.101", false, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.168.0.108", false, false, true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "FruitItemsEntity",
                columns: new[] { "Id", "FruitImg", "FruitName" },
                values: new object[,]
                {
                    { 10, "Pomegranate.svg", "Pomegranate" },
                    { 9, "Plum.svg", "Plum" },
                    { 8, "Pear.svg", "Pear" },
                    { 7, "Nut.svg", "Nut" },
                    { 6, "Melon.svg", "Melon" },
                    { 5, "Hazelnut.svg", "Hazelnut" },
                    { 2, "Cactus.svg", "Cactus" },
                    { 3, "Citrus.svg", "Citrus" },
                    { 11, "Raspberry.svg", "Raspberry" },
                    { 1, "Broccoli.svg", "Broccoli" },
                    { 4, "Grapes.svg", "Grapes" }
                });

            migrationBuilder.InsertData(
                table: "GuestEntity",
                columns: new[] { "Id", "Country", "FirstVisit", "IPAddress", "LastVisit", "Region" },
                values: new object[] { 1, "Japan", new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "192.168.0.101", new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tokyo" });

            migrationBuilder.InsertData(
                table: "PageEntity",
                columns: new[] { "Id", "PageName" },
                values: new object[] { 1, "Content" });

            migrationBuilder.InsertData(
                table: "UserEntity",
                columns: new[] { "Id", "AvatarImgUrl", "Name" },
                values: new object[] { true, "Watermelon.svg", "Justice" });

            migrationBuilder.InsertData(
                table: "AvatarEntity",
                columns: new[] { "Id", "AvatarId", "AvatarImgUrl", "CurrentIP", "GuestId", "IsCheckedIn", "JoinedOn", "LastCheckedIn", "Name" },
                values: new object[] { 1, null, "Apple.svg", "192.168.0.101", 1, true, new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple" });

            migrationBuilder.InsertData(
                table: "CardEntity",
                columns: new[] { "Id", "Content", "CreatedOn", "Description", "IsSuperUser", "PageEntityId", "PostType", "ReplyBoxIsActive", "SearchIndexValue", "TagIsActive", "Title", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2020, 11, 10, 19, 23, 5, 0, DateTimeKind.Unspecified), "A story about a princess you travels the world on a mission to find dragons.", true, null, 0, false, 0, false, "Return of Golia", new DateTime(2021, 5, 26, 17, 34, 8, 110, DateTimeKind.Local).AddTicks(4745) },
                    { 2, null, new DateTime(2020, 7, 19, 16, 20, 8, 0, DateTimeKind.Unspecified), "A storm is approaching and this is just the beginning of something big.", true, null, 0, false, 0, false, "Thunder & Lighting", new DateTime(2021, 5, 26, 17, 34, 8, 110, DateTimeKind.Local).AddTicks(5877) },
                    { 3, null, new DateTime(2020, 7, 19, 10, 18, 15, 0, DateTimeKind.Unspecified), "A storm is approaching and this is just the beginning of something big.", true, null, 0, false, 0, false, "Return of Repunzal", new DateTime(2021, 5, 26, 17, 34, 8, 110, DateTimeKind.Local).AddTicks(5923) },
                    { 4, "Here are some works I found that I like from http://www.artstation.com https://cdna.artstation.com/p/assets/images/images/034/796/486/large/daniel-kho-lux-wip13.jpg?1613253409 And a little more text https://cdna.artstation.com/p/assets/images/images/035/653/308/large/qing-xu-.jpg?1615523280", new DateTime(2021, 2, 19, 15, 35, 45, 0, DateTimeKind.Unspecified), "This is a blog discussing works found on artstation.", true, null, 1, false, 0, false, "Discovering of Amazing 3D Works From ArtStation", new DateTime(2021, 5, 26, 17, 34, 8, 110, DateTimeKind.Local).AddTicks(5930) }
                });

            migrationBuilder.InsertData(
                table: "CommentEntity",
                columns: new[] { "Id", "AvatarId", "CardEntityId", "FlaggedCommentMessages", "IsActive", "IsReply", "IsSuperUser", "LastUpdatedOn", "Medium", "Message", "PostedOn", "ReplyToCommentId", "ResponseToAvatarId", "SearchIndexValue" },
                values: new object[,]
                {
                    { 1, 2, 1, "Spam", false, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "This is great work!!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0 },
                    { 2, 2, 1, null, false, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "You deserve a prize!! &img[0] and this &img[1]", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0 },
                    { 4, 1, 1, null, false, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Nice lady character!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "ImageEntity",
                columns: new[] { "Id", "CardEntityId", "Catergory", "ImageUrl", "QuickDescription", "ThumbnailUrl", "UpdatedOn", "UploadedOn" },
                values: new object[,]
                {
                    { 6, 4, 0, "", "", "https://cdna.artstation.com/p/assets/images/images/034/796/486/large/daniel-kho-lux-wip13.jpg?1613253409", new DateTime(2021, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 0, "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508", "A model which i created during my spare time.", "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508", new DateTime(2020, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, 1, 0, "https://cdna.artstation.com/p/assets/images/images/004/404/208/medium/justice-headbush-ladywip.jpg?1483463051", "Practise practise practise.", "https://cdna.artstation.com/p/assets/images/images/004/404/208/medium/justice-headbush-ladywip.jpg?1483463051", new DateTime(2020, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 0, "https://cdna.artstation.com/p/assets/images/images/000/445/652/large/justice-headbush-t.jpg?1422790444", "More zbrush practise work.", "https://cdna.artstation.com/p/assets/images/images/000/445/652/large/justice-headbush-t.jpg?1422790444", new DateTime(2020, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, 2, "https://vimeo.com/83443121", "Entity Framework Best Practices - Should EFCore Be Your Data Access of Choice?", "https://i.vimeocdn.com/video/459920343_640.jpg", new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 0, "https://cdna.artstation.com/p/assets/images/images/000/445/632/medium/justice-headbush-5fe06d791b271f6d0d1b1cd14d44c4c8.jpg?1422789505", "Lots of work but great outcome.", "https://cdnb.artstation.com/p/assets/images/images/000/445/633/large/justice-headbush-9d9f84a6298e17bf294bd02dc901dd95.jpg?1422789508", new DateTime(2020, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TagEntity",
                columns: new[] { "Id", "ByAvatarId", "CardId", "CardsWithThisId", "IsActive", "IsSuperUser", "TagItem" },
                values: new object[,]
                {
                    { 7, 2, 2, 0, false, null, "3dworks" },
                    { 5, 2, 2, 0, false, null, "crystal" },
                    { 3, 2, 1, 0, false, null, "story" },
                    { 4, 1, 1, 0, false, null, "dragon" },
                    { 8, 2, 3, 0, false, null, "3dworks" },
                    { 2, 3, 1, 0, false, null, "zbrush" },
                    { 1, 3, 1, 0, false, null, "golia" },
                    { 6, 2, 1, 0, false, null, "3dworks" }
                });

            migrationBuilder.InsertData(
                table: "UniqueVisitEntity",
                columns: new[] { "Id", "AnonymousId", "AvatarVisited", "GuestVisited", "PageId", "TimeVisited", "UnregisteredGuestVisited" },
                values: new object[,]
                {
                    { 2, null, 1, null, 1, new DateTime(2021, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 1, null, 1, null, 1, new DateTime(2021, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "VoteEntity",
                columns: new[] { "Id", "CardId", "VoteByGuest", "VoteById" },
                values: new object[] { 1, 1, null, 2 });

            migrationBuilder.InsertData(
                table: "CommentEntity",
                columns: new[] { "Id", "AvatarId", "CardEntityId", "FlaggedCommentMessages", "IsActive", "IsReply", "IsSuperUser", "LastUpdatedOn", "Medium", "Message", "PostedOn", "ReplyToCommentId", "ResponseToAvatarId", "SearchIndexValue" },
                values: new object[,]
                {
                    { 3, 3, 1, null, false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "I Totally agree!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 0 },
                    { 5, 2, 1, null, false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Ya, Pretty cool lady", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "FlagEntity",
                columns: new[] { "id", "AvatarId", "CommentEntityId", "ReasonText" },
                values: new object[] { 1, 1, 1, "Just because I like it so much!" });

            migrationBuilder.InsertData(
                table: "LikeEntity",
                columns: new[] { "Id", "CommentId", "HasLiked", "LikedById" },
                values: new object[] { 1, 1, true, 2 });

            migrationBuilder.InsertData(
                table: "LinkEntity",
                columns: new[] { "Id", "AddedOn", "CommentEntityId", "Link", "LinkIndex", "LinkType" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "https://media.giphy.com/media/mYiBd0ttjosz6/giphy.gif", "img[0]", 3 },
                    { 2, new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "https://media.giphy.com/media/HoM8C39iqS9H2/giphy.gif", "img[1]", 3 }
                });

            migrationBuilder.InsertData(
                table: "CommentEntity",
                columns: new[] { "Id", "AvatarId", "CardEntityId", "FlaggedCommentMessages", "IsActive", "IsReply", "IsSuperUser", "LastUpdatedOn", "Medium", "Message", "PostedOn", "ReplyToCommentId", "ResponseToAvatarId", "SearchIndexValue" },
                values: new object[] { 6, 3, 1, null, false, true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "This 3D lady is awesome!!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_AvatarEntity_AvatarId",
                table: "AvatarEntity",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_AvatarEntity_GuestId",
                table: "AvatarEntity",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_CardEntity_IsSuperUser",
                table: "CardEntity",
                column: "IsSuperUser");

            migrationBuilder.CreateIndex(
                name: "IX_CardEntity_PageEntityId",
                table: "CardEntity",
                column: "PageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_AvatarId",
                table: "CommentEntity",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_CardEntityId",
                table: "CommentEntity",
                column: "CardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_IsSuperUser",
                table: "CommentEntity",
                column: "IsSuperUser");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_ReplyToCommentId",
                table: "CommentEntity",
                column: "ReplyToCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_FlagEntity_CommentEntityId",
                table: "FlagEntity",
                column: "CommentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageEntity_CardEntityId",
                table: "ImageEntity",
                column: "CardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntity_CommentId",
                table: "LikeEntity",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntity_LikedById",
                table: "LikeEntity",
                column: "LikedById");

            migrationBuilder.CreateIndex(
                name: "IX_LinkEntity_CommentEntityId",
                table: "LinkEntity",
                column: "CommentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_AvatarEntityId",
                table: "ReplyEntity",
                column: "AvatarEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_CommentId",
                table: "ReplyEntity",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_IsSuperUser",
                table: "ReplyEntity",
                column: "IsSuperUser");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_ResponseToCommentId",
                table: "ReplyEntity",
                column: "ResponseToCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_ByAvatarId",
                table: "TagEntity",
                column: "ByAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_CardId",
                table: "TagEntity",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_IsSuperUser",
                table: "TagEntity",
                column: "IsSuperUser");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueVisitEntity_AnonymousId",
                table: "UniqueVisitEntity",
                column: "AnonymousId");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueVisitEntity_AvatarVisited",
                table: "UniqueVisitEntity",
                column: "AvatarVisited");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueVisitEntity_GuestVisited",
                table: "UniqueVisitEntity",
                column: "GuestVisited");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueVisitEntity_PageId",
                table: "UniqueVisitEntity",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_UniqueVisitEntity_UnregisteredGuestVisited",
                table: "UniqueVisitEntity",
                column: "UnregisteredGuestVisited",
                unique: true,
                filter: "[UnregisteredGuestVisited] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ViewEntity_AnonymousId",
                table: "ViewEntity",
                column: "AnonymousId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewEntity_CardEntityId",
                table: "ViewEntity",
                column: "CardEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewEntity_ViewedByAvatarId",
                table: "ViewEntity",
                column: "ViewedByAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewEntity_ViewedByGuestId",
                table: "ViewEntity",
                column: "ViewedByGuestId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewEntity_ViewedByUnregisteredGuest",
                table: "ViewEntity",
                column: "ViewedByUnregisteredGuest");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntity_CardId",
                table: "VoteEntity",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntity_VoteByGuest",
                table: "VoteEntity",
                column: "VoteByGuest");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntity_VoteById",
                table: "VoteEntity",
                column: "VoteById");

            migrationBuilder.AddForeignKey(
                name: "FK_UniqueVisitEntity_AvatarEntity_AvatarVisited",
                table: "UniqueVisitEntity",
                column: "AvatarVisited",
                principalTable: "AvatarEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewEntity_AvatarEntity_ViewedByAvatarId",
                table: "ViewEntity",
                column: "ViewedByAvatarId",
                principalTable: "AvatarEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentEntity_AvatarEntity_AvatarId",
                table: "CommentEntity",
                column: "AvatarId",
                principalTable: "AvatarEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvatarEntity_FlagEntity_AvatarId",
                table: "AvatarEntity");

            migrationBuilder.DropTable(
                name: "ContactEntity");

            migrationBuilder.DropTable(
                name: "EnableOptionsEntity");

            migrationBuilder.DropTable(
                name: "FruitItemsEntity");

            migrationBuilder.DropTable(
                name: "ImageEntity");

            migrationBuilder.DropTable(
                name: "LikeEntity");

            migrationBuilder.DropTable(
                name: "LinkEntity");

            migrationBuilder.DropTable(
                name: "ReplyEntity");

            migrationBuilder.DropTable(
                name: "TagEntity");

            migrationBuilder.DropTable(
                name: "UniqueVisitEntity");

            migrationBuilder.DropTable(
                name: "ViewEntity");

            migrationBuilder.DropTable(
                name: "VoteEntity");

            migrationBuilder.DropTable(
                name: "AnonymousEntity");

            migrationBuilder.DropTable(
                name: "UnregisteredGuestEnitity");

            migrationBuilder.DropTable(
                name: "FlagEntity");

            migrationBuilder.DropTable(
                name: "CommentEntity");

            migrationBuilder.DropTable(
                name: "AvatarEntity");

            migrationBuilder.DropTable(
                name: "CardEntity");

            migrationBuilder.DropTable(
                name: "GuestEntity");

            migrationBuilder.DropTable(
                name: "PageEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
