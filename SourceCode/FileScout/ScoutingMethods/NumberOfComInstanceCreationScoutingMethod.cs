using System.IO;
using System.Text.RegularExpressions;
using FileScout.Interfaces;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// COMオブジェクトのインスタンス生成回数の偵察手段。
    /// </summary>
    public class NumberOfComInstanceCreationScoutingMethod : IScoutingMethod
    {
        /// <summary>
        /// 偵察対象のCOMオブジェクトの名前。
        /// </summary>
        public string ComObjectName { get; set; }

        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            return Do(clue, ComObjectName);
        }
        
        /// <summary>
        /// コーディングにおける指定されたCOMオブジェクトのインスタンス生成を偵察する。
        /// </summary>
        /// <param name="clue">手掛かり。</param>
        /// <param name="comObjectName">COMオブジェクト名。</param>
        /// <returns>偵察結果。</returns>
        protected string Do(IScoutingClue clue, string comObjectName)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            const string patternHead = @"\s*=\s*(WScript\.){0,1}CreateObject\(\s*""";
            const string patternTail = @"""\s*\)";
            var pattern = patternHead + Regex.Escape(ComObjectName) + patternTail;

            var rows = File.ReadLines(clue.FilePath, clue.Encoding);
            int count = 0;
            foreach (var row in rows)
            {
                var matches = Regex.Matches(row, pattern, RegexOptions.IgnoreCase);

                foreach (var match in matches)
                {
                    var commentHead1 = row.ToLower().IndexOf("rem ");
                    var commentHead2 = row.ToLower().IndexOf("'");
                    var matchIndex = row.IndexOf(match.ToString());
                    if ((commentHead1 != -1 && matchIndex > commentHead1)
                    || (commentHead2 != -1 && matchIndex > commentHead2))
                    {
                        continue;
                    }
                    count++;
                }
            }
            return count.ToString();
        }
    }
}
