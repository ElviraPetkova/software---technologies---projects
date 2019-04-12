namespace ProjectRider.App.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public long Budget { get; set; }
    }
}
