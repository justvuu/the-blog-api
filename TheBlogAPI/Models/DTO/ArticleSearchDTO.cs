using TheBlogAPI.Models.Entities;

namespace TheBlogAPI.Models.DTO
{
    public class ArticleSearchDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
    }
}
