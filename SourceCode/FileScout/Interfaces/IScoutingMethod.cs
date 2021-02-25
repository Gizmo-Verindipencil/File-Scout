namespace FileScout.Interfaces
{
    /// <summary>
    /// 調査手段を表します。
    /// </summary>
    public interface IScoutingMethod
    {
        /// <summary>
        /// 調査します。
        /// </summary>
        /// <param name="clue">調査の入力内容。</param>
        /// <returns>
        /// 項目の調査結果を返します。
        /// </returns>
        string Do(IScoutingClue clue);
    }
}
