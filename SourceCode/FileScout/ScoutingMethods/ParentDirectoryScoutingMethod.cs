using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// 親ディレクトリの偵察手段。
    /// </summary>
    public class ParentDirectoryScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.DirectoryName;
        }
    }
}
