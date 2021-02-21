namespace FileScout.Interfaces
{
    /// <summary>
    /// 調査手段。
    /// </summary>
    public interface IScoutingMethod
    {
        /// <summary>
        /// 偵察する。
        /// </summary>
        /// <param name="clue">手掛かり。</param>
        /// <returns>偵察結果。</returns>
        string Do(IScoutingClue clue);
    }
}
