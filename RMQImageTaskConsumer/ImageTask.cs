using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace RMQImageTasks
{
    public class ImageTask
    {
        public int Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public TaskTypes TaskType { get; set; } = TaskTypes.NONE; // e.g., "resize", "watermark"

        public override string ToString() => $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] ({Id}) Processing '{ImagePath}'. TaskType = {TaskType}";
    }

    public enum TaskTypes
    {
        [Description("None")]
        NONE,

        [Description("Resize image")]
        RESIZE,

        [Description("Make Watermark")]
        WATERMARK
    }

    public static class Ext
    {
        internal static Random random = new Random();

        public static T? GetRandomEnumValue<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            Contract.Assert(values != null);
            int randomIndex = random.Next(values.Length);
            Contract.Assume(values != null);
            return (T?)values.GetValue(randomIndex);
        }
    }
}