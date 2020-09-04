using System.IO;
using System.Linq;
using FileScout.Extensions;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// 空行数の偵察手段。
    /// </summary>
    public class NumberOfEmptyRowsScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            var blanks = new string[] { " ", "　", "\t" };
            return text
                .Where(x => x.RemoveString(blanks).Length == 0)
                .Count().ToString();
        }
    }
}
