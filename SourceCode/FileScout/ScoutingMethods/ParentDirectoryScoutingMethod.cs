using FileScout.Interfaces;
using System.IO;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイルの親ディレクトリの調査手段を提供します。
    /// </summary>
    public class ParentDirectoryScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.DirectoryName;
        }
    }
}
