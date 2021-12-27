using System;

using Client.Models;

namespace Client.Utils
{
    class SetOpt : Models.Util
    {
        string OptName { get; set; }
        string OptVal { get; set; }

        public override string UtilName => "SetOpt";
        public override string Desc => "Set an option";
        public override string UtilExecute(string[] opts)
        {
            try
            {
                if (opts is null) { throw new AtlasException($"[-] No parameters passed\nUsage: SetOpt [optionName] [optionValue]"); }

                OptName = opts[1];
                OptVal = opts[2];

                if (OptName == null) { throw new AtlasException($"[-] Invalid parameters passed\nUsage: SetOpt [optionName] [optionValue]"); }
                if (OptVal == null) { throw new AtlasException($"[-] Invalid parameters passed\nUsage: SetOpt [optionName] [optionValue]"); }



                return $"{OptName} set to {OptVal}";
                
            } catch (AtlasException e) { return e.Message; }

            throw new NotImplementedException();
        }
    }
}
