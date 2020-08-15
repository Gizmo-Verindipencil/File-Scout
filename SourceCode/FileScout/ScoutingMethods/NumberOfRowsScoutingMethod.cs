using System.IO;
using System.Linq;
using FileScout.Interfaces;


namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// 行数の偵察手段。
    /// </summary>
    public class NumberOfRowsScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            return text
                .Count().ToString();
        }
    }
}
