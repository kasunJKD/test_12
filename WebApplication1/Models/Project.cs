namespace WebApplication1.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
        public ICollection<User>? Users { get; set; }
        public Customer? Customer { get; set; }  
    }
}
