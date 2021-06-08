using System.Collections.Generic;

namespace MyAppAPI.ReMap
{
    public class TagDto
    {
        public int Id { get; set; }
        public string TagItem { get; set; }
        public int CardId { get; set; }
        public int ByAvatarId { get; set; }
        public bool? IsSuperUser { get; set; }
        public bool IsActive { get; set; }
    }
}