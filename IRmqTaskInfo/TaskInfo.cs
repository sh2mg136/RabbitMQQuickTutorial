namespace IRmqTaskInfo
{
    public interface IRmqTaskInfo
    {
        int Id { get; set; }
        string Message { get; set; }
    }

    public class TaskInfo : IRmqTaskInfo
    {
        public TaskInfo(int id, string message)
        {
            Id = id;
            Message = message;
        }

        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}