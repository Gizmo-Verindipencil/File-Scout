using FileScout.Scouts;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout
{
    class Program
    {
        static void Main(string[] args)
        {
            var scout = new FullScout();
            var target = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var result = scout.Scout(target);
            File.WriteAllText(Path.Combine(target, "result.csv"), result, Encoding.UTF8);
        }
    }
}
