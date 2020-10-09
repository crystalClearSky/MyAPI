using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyAppApi.Migrations
{
    public partial class ContentContextInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AvatarEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCheckedIn = table.Column<bool>(type: "bit", nullable: false),
                    AvatarImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoinedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvatarEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuperUser = table.Column<bool>(type: "bit", nullable: false)
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
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Flag = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Descritpion = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvatarId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastUpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSuperUser = table.Column<bool>(type: "bit", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentEntity_AvatarEntity_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "AvatarEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentEntity_CardEntity_CardId",
                        column: x => x.CardId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentEntity_UserEntity_UserId",
                        column: x => x.UserId,
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
                    ByAvatarId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagEntity_AvatarEntity_ByAvatarId",
                        column: x => x.ByAvatarId,
                        principalTable: "AvatarEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagEntity_CardEntity_CardId",
                        column: x => x.CardId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagEntity_UserEntity_UserId",
                        column: x => x.UserId,
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
                    UpVote = table.Column<bool>(type: "bit", nullable: false),
                    VoteById = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoteEntity_CardEntity_CardId",
                        column: x => x.CardId,
                        principalTable: "CardEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasLiked = table.Column<bool>(type: "bit", nullable: false),
                    LikedId = table.Column<int>(type: "int", nullable: true),
                    LikedById = table.Column<int>(type: "int", nullable: false),
                    AvatarId = table.Column<int>(type: "int", nullable: false),
                    CommentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeEntity_AvatarEntity_LikedId",
                        column: x => x.LikedId,
                        principalTable: "AvatarEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    AvatarEnityId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ResponseId = table.Column<int>(type: "int", nullable: true),
                    ResponseToCommentId = table.Column<int>(type: "int", nullable: true),
                    HasReplied = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyEntity", x => new { x.AvatarId, x.CommentId });
                    table.ForeignKey(
                        name: "FK_ReplyEntity_AvatarEntity_AvatarEnityId",
                        column: x => x.AvatarEnityId,
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
                        name: "FK_ReplyEntity_CommentEntity_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "CommentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AvatarEntity",
                columns: new[] { "Id", "AvatarImgUrl", "CurrentIP", "IsCheckedIn", "JoinedOn" },
                values: new object[,]
                {
                    { 1, "avatar.jpg", "192.168.0.101", true, new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "avatar.jpg", "192.168.0.102", false, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "avatar.jpg", "192.168.0.108", false, new DateTime(2020, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserEntity",
                columns: new[] { "Id", "IsSuperUser", "Name" },
                values: new object[] { 1, true, "Justice" });

            migrationBuilder.InsertData(
                table: "CardEntity",
                columns: new[] { "Id", "CreatedOn", "Descritpion", "Flag", "ImageUrl", "Title", "UpdatedOn", "UserId" },
                values: new object[] { 1, new DateTime(2020, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "A story about a princess you travels the world on a mission to find dragons.", false, "image.jpg", "Return of Golia", new DateTime(2020, 10, 8, 14, 54, 51, 979, DateTimeKind.Local).AddTicks(6106), 1 });

            migrationBuilder.InsertData(
                table: "CardEntity",
                columns: new[] { "Id", "CreatedOn", "Descritpion", "Flag", "ImageUrl", "Title", "UpdatedOn", "UserId" },
                values: new object[] { 2, new DateTime(2020, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "A storm is approaching and this is just the beginning of something big.", false, "image.jpg", "Thunder and Lighting", new DateTime(2020, 10, 8, 14, 54, 51, 979, DateTimeKind.Local).AddTicks(7188), 1 });

            migrationBuilder.InsertData(
                table: "CommentEntity",
                columns: new[] { "Id", "AvatarId", "CardId", "IsSuperUser", "LastUpdatedOn", "Message", "PostedOn", "UserId" },
                values: new object[,]
                {
                    { 1, 2, 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is great work!!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "You deserve a prize!!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 3, 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "I Totally agree!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 1, 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nice lady character!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 2, 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ya, Pretty cool lady", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, 3, 1, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "This 3D lady is awesome!!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 7, null, 1, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SuperUser - Thanks for the compliments!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "TagEntity",
                columns: new[] { "Id", "ByAvatarId", "CardId", "TagItem", "UserId" },
                values: new object[,]
                {
                    { 1, 3, 1, "golia", null },
                    { 2, 3, 1, "zbrush", null },
                    { 3, 2, 1, "story", null },
                    { 4, 1, 1, "dragon", null },
                    { 5, 2, 2, "crystal", null }
                });

            migrationBuilder.InsertData(
                table: "VoteEntity",
                columns: new[] { "Id", "CardId", "UpVote", "VoteById" },
                values: new object[] { 1, 1, true, 2 });

            migrationBuilder.InsertData(
                table: "LikeEntity",
                columns: new[] { "Id", "AvatarId", "CommentId", "HasLiked", "LikedById", "LikedId" },
                values: new object[] { 1, 0, 1, true, 2, null });

            migrationBuilder.InsertData(
                table: "ReplyEntity",
                columns: new[] { "AvatarId", "CommentId", "AvatarEnityId", "HasReplied", "ResponseId", "ResponseToCommentId", "UserId" },
                values: new object[,]
                {
                    { 3, 2, null, true, null, 2, null },
                    { 2, 5, null, true, null, 5, null },
                    { 3, 6, null, true, null, 5, null },
                    { 0, 7, null, true, null, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardEntity_UserId",
                table: "CardEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_AvatarId",
                table: "CommentEntity",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_CardId",
                table: "CommentEntity",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentEntity_UserId",
                table: "CommentEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntity_CommentId",
                table: "LikeEntity",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeEntity_LikedId",
                table: "LikeEntity",
                column: "LikedId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_AvatarEnityId",
                table: "ReplyEntity",
                column: "AvatarEnityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_CommentId",
                table: "ReplyEntity",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_ResponseId",
                table: "ReplyEntity",
                column: "ResponseId",
                unique: true,
                filter: "[ResponseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyEntity_UserId",
                table: "ReplyEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_ByAvatarId",
                table: "TagEntity",
                column: "ByAvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_CardId",
                table: "TagEntity",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_UserId",
                table: "TagEntity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntity_CardId",
                table: "VoteEntity",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteEntity_VoteById",
                table: "VoteEntity",
                column: "VoteById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeEntity");

            migrationBuilder.DropTable(
                name: "ReplyEntity");

            migrationBuilder.DropTable(
                name: "TagEntity");

            migrationBuilder.DropTable(
                name: "VoteEntity");

            migrationBuilder.DropTable(
                name: "CommentEntity");

            migrationBuilder.DropTable(
                name: "AvatarEntity");

            migrationBuilder.DropTable(
                name: "CardEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
