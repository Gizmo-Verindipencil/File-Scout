using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイル名の偵察手段。
    /// </summary>
    public class NameScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.Name;
        }
    }
}
