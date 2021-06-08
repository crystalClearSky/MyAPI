using System.ComponentModel.DataAnnotations;

namespace MyAppAPI.Entities
{
    public class FruitItemsEntity
    {
        [Key]
        public int Id { get; set; }
        public string FruitName { get; set; }
        public string FruitImg { get; set; }
    }
}