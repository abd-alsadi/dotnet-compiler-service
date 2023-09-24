using KmnlkCompilerDll.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KmnlkCommon.Shareds.LoggerManagement;

namespace MyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string code = @"
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    namespace Main
    {
        public class Program
        {
            public static string Main()
            {
                " +
                "return\"Hello, world!\";" +
                "Console.WriteLine(\"Hello, world!\"); "
                + @"
            }
        }
    }
";
            //ILog logger = new FileLogger(@"E:\my projects\xxxxxxxxx\compiler\");
            //cSharpCompilerManagement ma = new cSharpCompilerManagement(logger);
            //string xxxx=ma.getResultFileCode(new KmnlkCompilerDll.Models.clsFileCode(code));
            //Console.WriteLine(xxxx);
            //Console.ReadLine();
        }
    }
}
