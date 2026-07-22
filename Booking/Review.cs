namespace Booking;

public class Review
{
    public int Rating { get; set; }
    public string Date { get; set; }
    public string Country { get; set; }
    public string Room { get; set; }
    public string Title { get; set; }
    public string Pros { get; set; }
    public string Cons { get; set; }
    
    public string GetInfo()
    {
        return $"Rating: {Rating}\nDate:{Date}\nCountry: {Country}\nRoom: {Room}\nTitle: {Title}\n✅: {Pros}\n❌: {Cons}";
    }
}