using FileScout.Interfaces;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// 文字エンコードの偵察手段。
    /// </summary>
    public class EncodingScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            if (clue.Encoding == null) return "unknown";
            return clue.Encoding.WebName;
        }
    }
}