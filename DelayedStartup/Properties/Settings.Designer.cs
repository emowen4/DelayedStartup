﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Monicais.DelayedStartup.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<log4net>
    <appender name=""consoleAppender"" type=""log4net.Appender.ConsoleAppender"">
        <layout type=""log4net.Layout.PatternLayout"">
            <conversionPattern value=""%date [%thread] %-5level %logger %ndc%newline%message%newline%newline"" />
        </layout>
    </appender>
	
    <appender name=""fileAppender"" type=""log4net.Appender.RollingFileAppender"">
        <file type=""log4net.Util.PatternString"" value=""%property{LogFolder}/logging(%property{ProcessType}).log"" />
        <appendToFile value=""true"" />
        <maximumFileSize value=""100KB"" />
        <maxSizeRollBackups value=""4"" />
        <preserveLogFileNameExtension value=""true""/>
        <layout type=""log4net.Layout.PatternLayout"">
            <conversionPattern value=""%date [%thread] %-5level %logger {%newline%message%newline}%newline"" />
        </layout>
    </appender>
    
    <root>
        <level value=""INFO"" />
        <appender-ref ref=""consoleAppender"" />
        <appender-ref ref=""fileAppender"" />
    </root>
</log4net>")]
        public string DefaultLogConfig {
            get {
                return ((string)(this["DefaultLogConfig"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"{
	""Startups"": [
		{
			""ProgramPath"": """",
			""//ProgramPath"": ""Must be an escaped string with valid path"",
			""Delay"": ""1.02:03:04.0050000"",
			""//Delay"": ""[Day.]Hour:Minute:Second[.Millisecond]"",
			""StartIn"": """",
			""//StartIn"": ""Default will be the parent directory of {ProgramPath}"",
			""CreateNoWindow"": true,
			""//CreateNoWindow"": ""Whether create a window or not"",
			""UseShellExecute"": false,
			""//UseShellExecute"": ""True if executing command with cmd.exe; otherwise, starting directly"",
			""Priority"": 32,
			""//Priority"": ""Default=Normal; Normal=32; Idle=64; High=128; RealTime=256; BelowNormal=16384; AboveNormal=32768"",
			""WindowStyle"": 2,
			""//WindowStyle"": ""Default=Minimized; Normal=0; Hidden=1; Minimized=2; Maximized=3"",
			""AsAdminstrator"": false,
			""//AsAdminstrator"": ""Default=false""
		}
	]
}")]
        public string DefaultStartupConfig {
            get {
                return ((string)(this["DefaultStartupConfig"]));
            }
        }
    }
}
