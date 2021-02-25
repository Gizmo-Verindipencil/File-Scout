using FileScout.Interfaces;
using System.IO;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// 行文字数の平均の調査手段を提供します。
    /// </summary>
    public class RowTextLengthAverageScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            if (text.Count() == 0) return "0";
            return text
                .Select(x => x.Length)
                .Average().ToString();
        }
    }
}