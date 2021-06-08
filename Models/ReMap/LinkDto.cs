using Entities;

namespace Models.ReMap
{
    public class LinkDto
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int LinkIndex { get; set; }
        public LinkType LinkType { get; set; }
    }
}