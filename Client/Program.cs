using Client.Utils;

using static System.Console;

using static Client.Models.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            UI.Banner();

            TeamServerAddr = "http://127.0.0.1:5000";

            while (running) {
                if (CurrentImplant != null && !(Prompt.Contains($"{CurrentImplant}"))) { Prompt = $"[{CurrentImplant}] > "; }
                else if (CurrentImplant is null) { Prompt = "> "; }

                Write(Prompt);
                var _out = ReadLine();

                UI.Action(_out);
            }

        }
    }
}
