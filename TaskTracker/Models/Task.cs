using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskTracker.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public int Project_id { get; set; }
        public int Priority { get; set; }

        [ForeignKey("Project_id")]
        public Project Project { get; set; }
    }
}
