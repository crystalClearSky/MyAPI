using System;

namespace MyAppAPI.Entities.Simple
{
    public class AvatarSimple
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentIP { get; set; }
        // checks if current avatar is online
        public bool IsCheckedIn { get; set; }
        public string AvatarImgUrl { get; set; }
        public DateTime JoinedOn { get; set; }
        public DateTime LastCheckedIn { get; set; }
    }
}