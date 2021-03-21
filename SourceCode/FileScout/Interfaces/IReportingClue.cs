namespace FileScout.Interfaces
{
    /// <summary>
    /// 結果の入力内容を表します。
    /// </summary>
    public interface IReportingClue
    {
        /// <summary>
        /// 調査結果を取得します。
        /// </summary>
        IScoutingResult ScoutingResult { get; }

        /// <summary>
        /// 調査結果の出力先パスを取得します。
        /// </summary>
        string OutputLocation { get; }
    }
}
