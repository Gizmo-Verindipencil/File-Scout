using System.IO;
using System.Linq;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// 文字数の偵察手段。
    /// </summary>
    public class TextLengthScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            return text
                .Select(x => x.Length)
                .Sum().ToString();
        }
    }
}
