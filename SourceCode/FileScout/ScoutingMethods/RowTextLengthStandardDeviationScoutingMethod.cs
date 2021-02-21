using FileScout.Interfaces;
using System.IO;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// 行毎の平均文字数の偵察手段。
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