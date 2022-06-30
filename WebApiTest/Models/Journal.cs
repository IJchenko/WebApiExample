namespace WebApiTest.Models
{
    public class Journal
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public DateTime Date { get; set; }
        public bool Attendance { get; set; }
    }
}
