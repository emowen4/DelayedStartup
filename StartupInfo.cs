using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Monicais.DelayedStartup
{
    public class StartupInfo
    {

        public string ProgramPath { get; set; }

        public TimeSpan Delay { get; set; } = new TimeSpan(0);

        public string Arguments { get; set; } = "";

        public string StartIn { get; set; } = "";

        public bool CreateNoWindow { get; set; } = false;

        public bool UseShellExecute { get; set; } = false;

        public ProcessWindowStyle WindowStyle { get; set; } = ProcessWindowStyle.Minimized;

        public ProcessPriorityClass Priority { get; set; } = ProcessPriorityClass.Normal;

        public bool AsAdminstrator { get; set; } = false;

        public void Start()
        {
            if (!string.IsNullOrWhiteSpace(ProgramPath) && AsAdminstrator == Config.IsAdminstrator)
            {
                new Thread(() =>
                {
                    Thread.Sleep(Delay);
                    try
                    {
                        var info = new ProcessStartInfo()
                        {
                            FileName = ProgramPath,
                            Arguments = Arguments,
                            WorkingDirectory = string.IsNullOrWhiteSpace(StartIn) ? Path.GetDirectoryName(ProgramPath) : StartIn,
                            CreateNoWindow = CreateNoWindow,
                            UseShellExecute = UseShellExecute,
                            WindowStyle = WindowStyle,

                        };
                        Config.StartupLog.Info(ToString());
                        var process = Process.Start(info);
                        process.PriorityClass = Priority;
                    }
                    catch (Exception ex)
                    {
                        Config.StartupLog.Error($"{ToString()}\n{ex.ToString()}");
                    }
                }).Start();
            }
        }

        public JObject ToJObject()
        {
            return JObject.FromObject(this);
        }

        public string ToJson(Formatting format = Formatting.Indented)
        {
            return ToJObject().ToString(format);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;
            return obj is StartupInfo info
                && ProgramPath == info.ProgramPath
                && Delay == info.Delay
                && Arguments == info.Arguments
                && StartIn == info.StartIn
                && CreateNoWindow == info.CreateNoWindow
                && UseShellExecute == info.UseShellExecute;
        }

        public override int GetHashCode()
        {
            return ProgramPath.GetHashCode()
                + Delay.GetHashCode()
                + Arguments.GetHashCode()
                + StartIn.GetHashCode()
                + (CreateNoWindow ? 7 : 13)
                + (UseShellExecute ? 9 : 27);
        }

        public override string ToString()
        {
            return $"{GetType().Name}[ ProgramPath=\"{ProgramPath}\" Delay={Delay} Arguments=\"{Arguments}\" " +
                $"StartIn=\"{StartIn}\" CreateNoWindow={CreateNoWindow} UseShellExecute={UseShellExecute} ]";
        }
    }

    public static class StartupExtension
    {

        public static string ToJson(this List<StartupInfo> infos)
        {
            return new JObject(new JProperty("Startups", new JArray(from info in infos select info.ToJObject()))).ToString();
        }

        public static List<StartupInfo> ReadAsStartupInfos(this string text)
        {
            Console.WriteLine(text);
            var list = new List<StartupInfo>();
            try
            {
                using (var reader = new JsonTextReader(new StringReader(text)))
                {
                    var obj = JObject.Load(reader);
                    if (obj.ContainsKey("Startups"))
                    {
                        if (obj["Startups"] is JArray infos)
                        {
                            foreach (var info in infos)
                            {
                                list.Add(info.ToObject<StartupInfo>());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Config.StartupLog.Error($"Error while parsing StartupInfos.\n{ex.ToString()}");
            }
            return list;
        }
    }
}
