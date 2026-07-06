namespace EventReservationMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
    }
}