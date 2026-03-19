namespace TaskQ_webapi.Model
{
    public class Todolist
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public bool IsCompleted { get; set; }

    }
}
