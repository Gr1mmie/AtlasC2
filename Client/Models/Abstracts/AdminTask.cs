namespace Client.Models
{
    abstract class AdminTask
    {
        public abstract string TaskName { get; }
        public abstract string Desc { get; }
        public abstract string AdminUtilExec(string[] opts);
    }
}
