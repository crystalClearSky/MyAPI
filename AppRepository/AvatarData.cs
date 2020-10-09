using System.Threading;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using MyAppAPI.Models;
using MyAppAPI.Services;
using MyAppAPI.Models.GalleryModel;

namespace MyAppAPI.AppRepository
{
    
    public class AvatarData : IAvatarData
    {
        public static AvatarData CurrentAvatar { get; } = new AvatarData();
        public List<Avatar> Avatars { get; set; }
        public AvatarData()
        {
            Avatars = new List<Avatar>()
            {
                new Avatar()
                {
                    Id = 1,
                    CurrentIP = "192.168.101",
                    // Likes = new List<Like>()
                    // {
                    //     new Like()
                    //     {
                    //         Id = 1,
                    //         HasLiked = true
                    //     },
                    //     new Like()
                    //     {
                    //         Id = 5,
                    //         HasLiked = true
                    //     },

                        
                    // },
                    // UpVotes = new List<Vote>()
                    // {
                    //     new Vote()
                    //     {
                    //         Id = 1,
                    //         UpVote = true,
                    //     },
                        
                    //     new Vote()
                    //     {
                    //         Id = 3,
                    //         UpVote = true,
                    //     }
                    // }
                },
                new Avatar()
                {
                    Id = 2,
                    CurrentIP = "192.168.101",
                    // Likes = new List<Like>()
                    // {
                        
                    //     new Like()
                    //     {
                    //         Id = 3,
                    //         HasLiked = true
                    //     }
                    // },
                    // UpVotes = new List<Vote>()
                    // {
                    //     new Vote()
                    //     {
                    //         Id = 1,
                    //         UpVote = true,
                    //     },
                    //     new Vote()
                    //     {
                    //         Id = 2,
                    //         UpVote = true,
                    //     },
                        
                    // }
                },
                new Avatar()
                {
                    Id = 3,
                    CurrentIP = "192.168.101",
                    // Likes = new List<Like>()
                    // {
                    //     new Like()
                    //     {
                    //         Id = 4,
                    //         HasLiked = true,
                    //     },
                    //     new Like()
                    //     {
                    //         Id = 2,
                    //         HasLiked = true
                    //     },
                    //     new Like()
                    //     {
                    //         Id = 6,
                    //         HasLiked = true
                    //     }
                    // },
                    // UpVotes = new List<Vote>()
                    // {
                        
                    //     new Vote()
                    //     {
                    //         Id = 2,
                    //         UpVote = true,
                    //     }
                   
                    // }
                }

            };
        }


        // public int GetAllVotesForCard(int id)
        // {
        //     var avatars = GetAllAvatars();
        //     int count = 0;
        //     var avatarVotes = avatars.Select(x => x.UpVotes);


        //     foreach (var likeGroups in avatarVotes)
        //     {
        //         var likeGroup = likeGroups.FindAll(x => x.Id == id);
        //         foreach (var likeObject in likeGroup)
        //         {
        //             if (likeObject.UpVote)
        //             {
        //                 count++;
        //             }
        //         }
        //     }
        //     Console.WriteLine("VOTES {0}", count);
        //     var result = count;


        //     return result;
        // }

        public IEnumerable<Avatar> GetAllAvatars()
        {
            return Avatars.OrderBy(x => x.Id);
        }

        public Avatar GetAvatarById(int id)
        {
            return Avatars.FirstOrDefault(x => x.Id == id);
        }

        // public int GetAllLikesForCard(int id)
        // {
        //     var avatars = GetAllAvatars();
        //     int count = 0;
        //     var avatarLikes = avatars.Select(x => x.Likes);
            


        //     foreach (var likeGroups in avatarLikes)
        //     {
        //         var likeGroup = likeGroups.FindAll(x => x.Id == id);
        //         foreach (var likeObject in likeGroup)
        //         {
        //             if (likeObject.HasLiked)
        //             {
        //                 count++;
        //             }
        //         }
        //     }
        //     Console.WriteLine("LIKES {0}", count);
        //     var result = count;


        //     return result;
        // }

        public void AddNewAvatar(string newIp)
        {
            if((!string.IsNullOrEmpty(newIp)) && newIp.Contains("168"))
            {
                if (!AvatarData.CurrentAvatar.Avatars.Any(a => a.CurrentIP == newIp))
                {
                var maxId = AvatarData.CurrentAvatar.Avatars.Max(a => a.Id) + 1;
                Avatar avatar = new Avatar()
                {
                    Id = maxId,
                    CurrentIP = newIp,
                    JoinedOn = DateTime.Now
                };
                AvatarData.CurrentAvatar.Avatars.Add(avatar);
                }
            }
        }

        public void DeleteAvatar(int id)
        {
            var avatarToDelete = AvatarData.CurrentAvatar.GetAvatarById(id);
            if (avatarToDelete != null)
            {
                AvatarData.CurrentAvatar.Avatars.Remove(avatarToDelete);
            }
        }
    }
}