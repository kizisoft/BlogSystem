namespace BlogSystem.Web.ViewModels.Tag
{
    using System.ComponentModel.DataAnnotations;

    using BlogSystem.Data.Models;
    using BlogSystem.Web.Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}