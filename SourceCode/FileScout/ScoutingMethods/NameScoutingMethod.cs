using FileScout.Interfaces;
using System.IO;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイル名の偵察手段。
    /// </summary>
    public class NameScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.Name;
        }
    }
}
