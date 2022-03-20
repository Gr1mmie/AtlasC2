rule AtlasImplant_Yara {
    meta:
        last_updated = "3-20-2022"
        author = "Grimmie (@Gr1mmie)"
        description = "Searches for strings present in the Atlas C2 Implant"
        md5 = "7bbb5fce0b18f613674fd09da14e5e45"

    strings:
        // system namespaces
        $systemNamespace1 = "System.Net" ascii
        $systemNamespace2 = "System.Text" ascii
        $systemNamespace3 = "System.Linq" ascii
        $systemNamespace4 = "System.Timers" ascii
        $systemNamespace5 = "System.Runtime" ascii 
        $systemNamespace6 = "System.Reflection" ascii
        $systemNamespace7 = "System.Collections" ascii
        $systemNamespace8 = "System.Diagnostics" ascii
        $systemNamespace9 = "System.Security" ascii
        $systemNamespace10 = "System.Management.Automation" ascii

        // misc stuffs
        $misc1 = "ImplantDataUtils" ascii
        $misc2 = "ImplantTaskUtils" ascii 
        $misc3 = "ImplantOptionUtils" ascii
        $misc4 = "ImplantCommands" ascii
        $misc5 = "ImplantTask" ascii
        $misc6 = "ImplantTaskOptions" ascii
        $misc7 = "ImplantOptions" ascii
        $misc8 = "ImplantCommandsInit" ascii
        $misc9 = "ImplantInit" ascii
        $misc10 = "PollImplant" ascii
        $misc11 = "ImplantTaskOut" ascii

        // comms stuffs
        $comms1 = "+Implant.Models.HTTPComms+<PollImplant>d__16" ascii
        $comms2 = "(Implant.Models.HTTPComms+<PostData>d__18" ascii
        $comms3 = "%Implant.Models.HTTPComms+<Start>d__19" ascii

        // getter stuffs
        $get1 = "get_assemParams" ascii
        $get2 = "get_assemBytes" ascii
        $get3 = "get_targetDir" ascii
        $get4 = "get_procIDLen" ascii
        $get5 = "get_procSessionIDLen" ascii
        $get6 = "get_procNameLen" ascii
        $get7 = "get_fileNameLen" ascii
        $get8 = "get_dirNameLen" ascii
        $get9 = "get_fileSizeLen" ascii
        $get10 = "get_CurrentDomain" ascii
        $get11 = "get_IsCancellationRequested" ascii
        $get12 = "get_Command" ascii
        $get13 = "get_assemMethod" ascii

    condition:
    uint16(0) == 0x5A4D and
    all of $systemNamespace* and
    all of $misc* and 
    all of $comms* and 
    3 of $get*

}
