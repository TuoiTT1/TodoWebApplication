namespace TodoWebApplication.Application.Queries.Employees
{
    public class TaskDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return
                $"Id: {Id}, " +
                $"Name: {Name}, " +
                $"Description: {Description}" +
                $"StartDate: {StartDate}" +
                $"EndDate: {EndDate}" +
                $"IsCompleted: {IsCompleted}";
        }
    }
}
