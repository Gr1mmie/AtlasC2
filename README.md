# AtlasC2
C# C2 Framework centered around Stage 1 operations

Atlas is based around gaining a foothold within an environment and further utilizing it to smuggle in C# (currently strictly C#) weaponry utilizing an HTTP based implant. Isn't exactly very OPSEC safe in it's current state...at all. Currently targets only windows environments

## Usage

### Starting TeamServer

After generating an exe of the client, teamserver, and implant, simply execute `Teamserver.exe` to start up the teamserver

### Starting/Managing Listeners
To start a new listener, use the `StartListener` command. This command takes two params: Listener name and the port to run on

<img width="356" alt="image" src="https://user-images.githubusercontent.com/57014148/159146033-692c0db2-b7cd-4f10-adff-b8717ae4e990.png">

The `Listeners` command lists all listeners, `ViewListener` returns data on the specified listener, and `RemoveListener` can be used to remove a listener from the list

<img width="345" alt="image" src="https://user-images.githubusercontent.com/57014148/159146101-61a73e5d-51f4-411f-bd43-f7121b28ec6e.png">

### Interfacing W/ Implants
Listing connected implants can be done using the `Implants` command

<img width="489" alt="image" src="https://user-images.githubusercontent.com/57014148/159146433-3c6858f4-226a-4f9c-ae8d-a50105336880.png">

Connecting to an implant is as simple as `Connect <ImplantId>`. See what I did there?...ok I'll see myself out. Just as the `Connect` command is used to select an implant. `ViewImplant` can be used to view more information on the selected implant. The `Disconnect` command will deselect the currently selected implant as shown below. 


<img width="487" alt="image" src="https://user-images.githubusercontent.com/57014148/159146537-9fa722a9-ac90-42f7-86c1-a7d8502cc876.png">

### Executing Tasks
To use a task, a task must first be selected using `SetTask`. Options can be viewed using `TaskOpts` and set using `SetTaskOpt`. Tasks are executing using `SendTask`

<img width="489" alt="image" src="https://user-images.githubusercontent.com/57014148/159146660-38af02d0-bdb7-4c8e-a1f7-e0b56b27cbcd.png">

### Viewing Previous Tasks
It's posible to view the output of a previously run task using `TaskOut`. `TasksOut` can be used to view all previously run tasks pertaining to the selected implant.

<img width="332" alt="image" src="https://user-images.githubusercontent.com/57014148/159146707-6c9ca507-eb50-413a-a7aa-03bceefe5193.png">

### Shell Execution
Atlas allows operators to execute both PowerShell and Cmd commands using the `PSShell` and `CMDShell` tasks respectively. `PSShell` opens a new runspace and executes the command so even if `powershell.exe` is blacklisted, PowerShell commands can still be executed. This method also bypasses Constrained Language Mode. `CMDShell` opens a `cmd.exe` process and passes the command into the process. Executing a PowerShell command was shown above so that won't be shown here as well. Site note about `CMDShell`, many common commands executed including (but not limited to) whoami, ipconfig, pwd, and cd have been implemented into the implants functionality to avoid the need to execute such commands via a `cmd.exe` process.

<img width="307" alt="image" src="https://user-images.githubusercontent.com/57014148/159146916-8db10341-354d-4ab2-8feb-6931d6bc8681.png">


### Loading C# assemblies into memory

Loading assembies takes a few steps unlike something like CobaltStrike that does everything using `execute-assembly`. First, an operator must use the `ByteConvert` utility (`ByteConvert` must be told whether the file is local or remote) to convert either a locally stored or remote file into a byte array and stores this in the `assemBytes` variable. Once this is done, the `Load` task is used to load the assembly into the implant process.

<img width="580" alt="image" src="https://user-images.githubusercontent.com/57014148/159147169-d24bfb6b-893e-430a-bf52-223257311bfc.png">

### Viewing Loaded Assemblies
To view assemblies loaded into the implant process, operators can use the `AssemQuery` and `AssemMethodQuery` tasks. The former returns all loaded assemblies while the latter returns All public methods pertaining to a loaded assembly

<img width="613" alt="image" src="https://user-images.githubusercontent.com/57014148/159147810-7f0454a8-860e-43b1-a74e-4e496a59d4ce.png">

The screenshot confirms that the `TestAssem` assembly was indeed loaded into the implant's process.

`AssemMethodQuery` can then be used to return information on `TestAssem` an operator can use to return information used to execute public methods 

<img width="475" alt="image" src="https://user-images.githubusercontent.com/57014148/159147821-c5373915-871d-486d-b366-6b96241e1647.png">

### Executing Loaded Assemblies
Atlas offers the option to execute an assembly from its entry point or a specified exposed method. `ExecuteAssem` can be used to execute from the entry point. This task takes only the name of the assembly. `ExecuteAssemMethod` allows for the execution of other methods using information fetched from `AssemMethodQuery`.

<img width="616" alt="image" src="https://user-images.githubusercontent.com/57014148/159148129-0d558363-a4e7-4cfe-a140-acf21c22548f.png">

For a full list of features, swing by the wiki (add link here)

## Compilation
Open .sln and build all 3 components in Release mode

## To-Do
* Authentication
* Encode PowerShell Commands
* Add admin utils: 
    * `cp` 
    * `upload`/`download`
* Keylogger (probs make standalone to load into implant)
* Some barebones persistence commands (idk something like creating a user via ADSI, WMI subscription creation. probs make these standalone assems to load into implant)
* Allow for the changing of the sleep timing on implant and implement jitter
* Allow for operator to change port TeamServer starts on via CLI 
* Encrypted comms (yikes, ik)
* AppDomain Manipulation - Allow for the creation/removal of AppDomains and allow operator the ability to select which AppDomain to load assemblies into
* Better extensability capabilities
* Implement some sort of profiling system
* Automated Compilation:
    * implant
    * droppers/loaders/stagers  
* Shellcode generation via Donut
* BOFs would be cool 

## Disclaimer
Atlas was designed soley for educational/ethical purposes. I do not condone nor am I responsible for actions taken by users of Atlas
