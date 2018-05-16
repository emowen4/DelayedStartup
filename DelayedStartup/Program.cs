using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Win32.TaskScheduler;

namespace Monicais.DelayedStartup
{
    public class Program
    {

        private static readonly string MutexString = Config.IsAdminstrator
            ? "Monicais.Toolsets.DelayedStartup"
            : "Monicais.Toolsets.DelayedStartup.Admin";
        private static readonly Mutex Mutex = new Mutex(true, MutexString);

        static void Main(string[] args)
        {
            Config.Load();
            if (Mutex.WaitOne(0, true))
            {
                if (args.Length == 0)
                {
                    ExecuteStartups();
                }
                else if (args.Length == 1)
                {
                    switch (args[0].ToLower())
                    {
                        case "install":
                            Install();
                            break;
                        case "uninstall":
                            Uninstall();
                            break;
                        case "start":
                            ExecuteStartups();
                            break;
                        default:
                            System.Console.WriteLine($"Unknown command {args[1]}");
                            Config.Log.Info($"Unknown command {args[1]}");
                            break;
                    }
                }
                Mutex.ReleaseMutex();
            }
            else
            {
                Config.Log.Warn("DelayedStartup is running");
            }
        }

        public static void Install()
        {
            Config.Log.Info("DelayedStartup is installing.");
            using (var service = new TaskService())
            {
                {
                    TaskDefinition def = service.NewTask();
                    def.Principal.LogonType = TaskLogonType.InteractiveToken;
                    def.RegistrationInfo.Author = "Monicais Toolsets";
                    def.RegistrationInfo.Description = "A utility which can start the startups after specific amount of time.";
                    LogonTrigger logonTrigger = new LogonTrigger();
                    def.Triggers.Add(logonTrigger);
                    def.Actions.Add(Config.ProgramFile, null, Config.ProgramFolder);
                    var task = service.RootFolder.RegisterTaskDefinition(@"\Monicais Toolsets\DelayedStartup", def);
                    task.ShowEditor();
                    Config.Log.Info("Install new task \"\\Monicais Toolsets\\DelayedStartup\" in TaskScheduler");
                }
                {
                    TaskDefinition def = service.NewTask();
                    def.Principal.LogonType = TaskLogonType.InteractiveToken;
                    def.RegistrationInfo.Author = "Monicais Toolsets";
                    def.RegistrationInfo.Description = "A utility which can start the startups after specific amount of time.";
                    def.Principal.RunLevel = TaskRunLevel.Highest;
                    LogonTrigger logonTrigger = new LogonTrigger();
                    def.Triggers.Add(logonTrigger);
                    def.Actions.Add(Config.ProgramFile, null, Config.ProgramFolder);
                    service.RootFolder.RegisterTaskDefinition(@"\Monicais Toolsets\DelayedStartup (admin)", def);
                    Config.Log.Info("Install new task \"\\Monicais Toolsets\\DelayedStartup (admin)\" in TaskScheduler");
                }
            }
        }

        public static void Uninstall()
        {
            Config.Log.Info("DelayedStartup is uninstalling.");
            using (var service = new TaskService())
            {
                service.RootFolder.DeleteTask(@"\Monicais Toolsets\DelayedStartup");
                service.RootFolder.DeleteTask(@"\Monicais Toolsets\DelayedStartup (admin)");
            }
        }

        private static void ExecuteStartups()
        {
            Config.Log.Info("DelayedStartup executes.");
            List<StartupInfo> startupInfos = File.ReadAllText(Config.StartupConfigFile, Encoding.UTF8).ReadAsStartupInfos();
            Parallel.ForEach(startupInfos, info => info.Start());
        }
    }
}
