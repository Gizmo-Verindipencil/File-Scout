using FileScout.Interfaces;
using System.IO;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイルバイト長の調査手段を提供します。
    /// </summary>
    public class ByteLengthScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.Length.ToString();
        }
    }
}
