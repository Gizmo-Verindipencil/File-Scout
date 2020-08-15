using System.IO;
using System.Linq;
using FileScout.Extensions;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// 空白文字数の偵察手段。
    /// </summary>
    public class NumberOfBlanksScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            var blanks = new string[] { " ", "　" };
            return text
                .Select(x => x.CountString(blanks))
                .Sum()
                .ToString();
        }
    }
}
