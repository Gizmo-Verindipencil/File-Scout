namespace FileScout.Interfaces
{
    /// <summary>
    /// 調査結果の出力を表します。
    /// </summary>
    public interface IScoutingReporter
    {
        /// <summary>
        /// 調査結果を出力します。
        /// </summary>
        /// <param name="clue">調査結果。</param>
        /// <returns>
        /// 出力結果を返します。
        /// </returns>
        IReportingResult Report(IReportingClue clue);
    }
}
