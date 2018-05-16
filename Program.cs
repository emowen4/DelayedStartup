using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Monicais.DelayedStartup
{
    public class Program
    {

        static void Main(string[] args)
        {
            Config.Load();

            List<StartupInfo> startupInfos = File.ReadAllText(Config.StartupConfigFile, Encoding.UTF8).ReadAsStartupInfos();
            Parallel.ForEach(startupInfos, info => info.Start());
        }
    }
}
