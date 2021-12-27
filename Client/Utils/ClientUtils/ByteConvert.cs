using System;
using Client.Models;

using static System.Console;

namespace Client.Utils.ClientUtils
{
    class ByteConvert : Models.Util
    {
        private bool isRemote {get;set;}
        private string File { get; set; }
        private int Timeout { get; set; } = 3;
        private int retryCount { get; set; } = 2;

        public override string UtilName => "ByteConvert";

        public override string Desc => "Fetches a local or remote file and converts into a byte array";

        public override string UtilExecute(string[] opts)
        {
            try
            {
                if (opts is null) { throw new AtlasException($"[*] Usage: ByteConvert [isRemote] [filePath] <timeout> <retryCount>\n"); }
                if (opts.Length < 3) { throw new AtlasException($"[*] Usage: ByteConvert [isRemote] [filePath] <timeout> <retryCount>\n"); }

                if (opts[1].ToLower() is "true" || opts[1] is "false") {
                    if (opts[1].ToLower() is "true") { 
                        isRemote = true; 
                        File = opts[2]; 
                    }
                    if (opts[1].ToLower() is "false") { 
                        isRemote = false; 
                        File = opts[2]; 
                    }
                } else { throw new AtlasException($"[*] Usage: ByteConvert [isRemote] [filePath] <timeout> <retryCount>\n"); }

                try {
                if (opts.Length > 3) { Timeout = Int32.Parse(opts[3]); }
                if (opts.Length > 3 && opts.Length <= 5) { retryCount = Int32.Parse(opts[4]); }
                } catch (FormatException) { throw new AtlasException($"[*] Usage: ByteConvert [isRemote] [filePath] <timeout> <retryCount>\n"); }

                if (!isRemote) {
                    WriteLine($"{nameof(isRemote)}: {isRemote}\n" +
                        $"{nameof(File)}: {File}\n" );
                } else {
                    WriteLine($"{nameof(isRemote)}: {isRemote}\n" +
                    $"{nameof(File)}: {File}\n" +
                    $"{nameof(Timeout)}: {Timeout}\n" +
                    $"{nameof(retryCount)}: {retryCount}\n"
                    );
                }

                if (isRemote) {
                    TaskOps.SmraatFetch(File, Timeout, retryCount);
                } else { TaskOps.SmraatFetch(File); }

                Client.Models.TaskOptions.assemBytes.SetPropertyValue("Value", Client.Models.Client.assemBytes);

                return $"[*] Converted {File} to byte array and stored in assemBytes\n";

            } catch (AtlasException e) { return e.Message; }
        }
    }
}