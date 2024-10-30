using System.ComponentModel.DataAnnotations;

namespace LikeButtonApp.Models {
    public class LikeButton
    {
        [Key]
        public int Id { get; set; }
        public int Likes { get; set; } = 0;
    }

    public class LikesResponse
    {
        public int Likes { get; set; }
    }

}
