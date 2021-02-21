using FileScout.Extensions;
using FileScout.Interfaces;
using System.IO;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// 空白文字数の偵察手段。
    /// </summary>
    public class NumberOfBlanksScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            var blanks = new string[] { " ", "　", "\t" };
            return text
                .Select(x => x.CountString(blanks))
                .Sum()
                .ToString();
        }
    }
}
