using FileScout.Interfaces;
using System.IO;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// タブとスペース混在の調査手段を提供します。
    /// </summary>
    public class UsingBothTabsAndSpacesScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var usingTab = false;
            var usingSpace = false;

            using (var stream = new FileStream(clue.FilePath, FileMode.Open))
            using (var reader = new StreamReader(stream, encoding: clue.Encoding))
            {
                var line = reader.ReadLine();

                // タブ・スペース使用の反映
                if (!usingTab) usingTab = line.IndexOf('\t') > -1;
                if (!usingSpace) usingSpace = line.IndexOf(' ') > -1;
                
                // タブ・スペース混在の場合は1を返す
                if (usingTab && usingSpace)
                {
                    return "1";
                }
            }

            // 見つからなければ0を返す
            return "0";
        }
    }
}
