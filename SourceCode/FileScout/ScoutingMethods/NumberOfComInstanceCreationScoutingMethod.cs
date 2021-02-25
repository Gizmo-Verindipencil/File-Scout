using FileScout.Interfaces;
using System.IO;
using System.Text.RegularExpressions;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// COMオブジェクトのインスタンス生成回数の調査手段を提供します。
    /// </summary>
    public class NumberOfComInstanceCreationScoutingMethod : IScoutingMethod
    {
        /// <summary>
        /// 偵察対象のCOMオブジェクトの名前を取得または設定します。
        /// </summary>
        public string ComObjectName { get; set; }

        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            return Do(clue, ComObjectName);
        }
        
        /// <summary>
        /// 指定されたCOMオブジェクトのインスタンス生成回数を調査します。
        /// </summary>
        /// <param name="clue">調査の入力内容。</param>
        /// <param name="comObjectName">COMオブジェクト名。</param>
        /// <returns>
        /// <paramref name="comObjectName"/> で指定したCOMオブジェクトのインスタンス生成回数を返します。
        /// </returns>
        protected string Do(IScoutingClue clue, string comObjectName)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            const string patternHead = @"\s*=\s*(WScript\.){0,1}CreateObject\(\s*""";
            const string patternTail = @"""\s*\)";
            var pattern = patternHead + Regex.Escape(comObjectName) + patternTail;

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
