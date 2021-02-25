using FileScout.Extensions;
using FileScout.Interfaces;
using System.IO;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイルの空行数の調査手段を提供します。
    /// </summary>
    public class NumberOfEmptyRowsScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            var blanks = new string[] { " ", "　", "\t" };
            return text
                .Where(x => x.RemoveString(blanks).Length == 0)
                .Count().ToString();
        }
    }
}
