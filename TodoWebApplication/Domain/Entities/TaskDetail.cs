namespace TodoWebApplication.Domain.Entities
{
    public class TaskDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public int EmployeeId { get; set; }
    }
}
