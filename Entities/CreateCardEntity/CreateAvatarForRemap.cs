using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.Entities.CreateCardEntity
{
    public class CreateAvatarForRemap
    {
        [Required]
        public string CurrentIP { get; set; }
        public bool IsCheckedIn { get; set; }
        public string AvatarImgUrl { get; set; } = "defaultImage.jpg";
        public int? GuestId { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime LastCheckedIn { get; set; }
        public List<VoteEntity> Votes { get; set; } = new List<VoteEntity>();
    }
}