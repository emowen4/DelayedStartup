# DelayedStartup
A utility which can start the startups after specific amount of time.

# Usage
Under `Config` folder, modify `startup.config` file in `json` style.
The automatically created `startup.config` includes the instruction.
```json
{
	"Startups": [
		{
			"ProgramPath": "",
			"//ProgramPath": "Must be an escaped string with valid path",
			"Delay": "1.02:03:04.0050000",
			"//Delay": "[Day.]Hour:Minute:Second[.Millisecond]",
			"StartIn": "",
			"//StartIn": "Default will be the parent directory of {ProgramPath}",
			"CreateNoWindow": true,
			"//CreateNoWindow": "Whether create a window or not",
			"UseShellExecute": false,
			"//UseShellExecute": "True if executing command with cmd.exe; otherwise, starting directly",
			"Priority": 32,
			"//Priority": "Default=Normal; Normal=32; Idle=64; High=128; RealTime=256; BelowNormal=16384; AboveNormal=32768",
			"WindowStyle": 2,
			"//WindowStyle": "Default=Minimized; Normal=0; Hidden=1; Minimized=2; Maximized=3",
			"AsAdminstrator": false,
			"//AsAdminstrator": "Default=false"
		}
	]
}
```

# Lib Used
 - dot4net
 - Newtonsoft.Json
