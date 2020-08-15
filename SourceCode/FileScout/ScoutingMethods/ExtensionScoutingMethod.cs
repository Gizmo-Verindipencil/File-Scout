using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// 拡張子の偵察手段。
    /// </summary>
    public class ExtensionScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.Extension;
        }
    }
}
