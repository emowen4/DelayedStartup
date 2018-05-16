using log4net;
using log4net.Config;
using System;
using System.IO;
using System.IO.Compression;

namespace Monicais.DelayedStartup
{
    public static class Config
    {
        internal static readonly string ProgramFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        internal static readonly string DefaultConfigFile = Path.Combine(ProgramFolder, "config.default.gz");
        internal static readonly string ConfigFolder = Path.Combine(ProgramFolder, "Configs");
        internal static readonly string StartupConfigFile = Path.Combine(ConfigFolder, "startup.config");
        internal static readonly string LogConfigFile = Path.Combine(ConfigFolder, "log.config");
        internal static readonly string LogFolder = Path.Combine(ProgramFolder, "Logs");

        internal static readonly bool IsAdminstrator = Privilege.IsAdminstrator();

        internal static ILog StartupLog;

        static Config () {
            Directory.SetCurrentDirectory(ProgramFolder);
        }

        public static void Load()
        {
            // Create config files is they do not exist
            Directory.CreateDirectory(ConfigFolder);
            if (!File.Exists(StartupConfigFile))
            {
                using (var c = File.CreateText(StartupConfigFile))
                {
                    c.Write(Properties.Settings.Default.DefaultStartupConfig);
                }
            }
            if (!File.Exists(LogConfigFile))
            {
                using (var c = File.CreateText(LogConfigFile))
                {
                    c.Write(Properties.Settings.Default.DefaultLogConfig);
                }
            }
            // Create log folder
            Directory.CreateDirectory(LogFolder);
            // Init log
            GlobalContext.Properties["LogFolder"] = LogFolder;
            GlobalContext.Properties["ProcessType"] = IsAdminstrator ? "admin" : "user";
            XmlConfigurator.ConfigureAndWatch(new FileInfo(LogConfigFile));
            StartupLog = LogManager.GetLogger("Startup Log");
        }
    }
}
