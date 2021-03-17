using FileScout.Interfaces;
using System.IO;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイル拡張子の調査手段を提供します。
    /// </summary>
    public class ExtensionScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.Extension;
        }
    }
}