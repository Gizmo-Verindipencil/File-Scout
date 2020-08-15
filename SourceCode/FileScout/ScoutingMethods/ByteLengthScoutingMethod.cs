using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// バイト長の偵察手段。
    /// </summary>
    public class ByteLengthScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var info = new FileInfo(clue.FilePath);
            return info.Length.ToString();
        }
    }
}
