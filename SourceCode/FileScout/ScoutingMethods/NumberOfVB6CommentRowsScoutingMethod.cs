using FileScout.Interfaces;
using System.IO;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// VB6のコメント行数の調査手段を提供します。
    /// </summary>
    public class NumberOfVB6CommentRowsScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            int count = 0;
            foreach (var row in text)
            {
                var targets = new string[] { "'", "rem " };
                var blanks = new char[] { ' ', '　', '\t' };
                var head = row.TrimStart(blanks);

                foreach (var target in targets)
                {
                    if (head.ToLower().StartsWith(target))
                    {
                        count++;
                    }
                }
            }
            return count.ToString();
        }
    }
}
