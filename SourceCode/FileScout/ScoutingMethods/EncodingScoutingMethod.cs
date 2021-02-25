using FileScout.Interfaces;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイル文字エンコードの調査手段を提供します。
    /// </summary>
    public class ExtensionScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            if (clue.Encoding == null) return "unknown";
            return clue.Encoding.WebName;
        }
    }
}
