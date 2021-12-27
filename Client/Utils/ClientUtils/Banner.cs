using System.Text;

namespace Client.Utils
{
    public class Banner : Models.Util
    {
        
        public override string UtilName => "Banner";
        public override string Desc => "Display banner";
        public override string UtilExecute(string[] opts)
        {
            StringBuilder _out = new StringBuilder();

           _out.AppendLine(
                @"   _____     __   .__                    _________  ________  " + "\n" +
                @"  /  _  \  _/  |_ |  |  _____     ______ \_   ___ \ \_____  \ " + "\n" +
                @" /  /_\  \ \   __\|  |  \__  \   /  ___/ /    \  \/  /  ____/ " + "\n" +
                @"/    |    \ |  |  |  |__ / __ \_ \___ \  \     \____/       \ " + "\n" +
                @"\____|__  / |__|  |____/(____  //____  >  \______  /\_______ \" + "\n" +
                @"        \/                   \/      \/          \/         \/" + "\n" +
                $"\tVer: {Models.Client.Ver} \n\tAuthor: Grimmie\n"
            );

            return _out.ToString();
        }

    }
}
