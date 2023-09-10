namespace WebApplication1.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
        public Status Status { get; set;}
        public Project? Project { get; set; }
    }
    
}

namespace WebApplication1.Models
{
    public enum Status
    {
        Open=1,
        Pending=2,
        Complete=3
    }
}
