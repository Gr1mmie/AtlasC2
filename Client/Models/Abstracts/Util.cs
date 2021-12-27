namespace Client.Models
{
    public abstract class Util
    {
        public abstract string UtilName {get;}
        public abstract string Desc { get; }
        public abstract string UtilExecute(string[] opts);
    }
}