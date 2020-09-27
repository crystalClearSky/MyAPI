using System.Collections.Generic;

namespace MyAppAPI.Models.GalleryModel.UpdateModel
{
    public class UpdateAvatar
    {
        public bool IsCheckedIn { get; set; }
        public List<Like> Likes { get; set; }
        public List<Vote> UpVotes { get; set; }
        public List<Comment> Comments { get; set; }
    }
}