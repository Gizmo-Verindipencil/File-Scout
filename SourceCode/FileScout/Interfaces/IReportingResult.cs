namespace FileScout.Interfaces
{
    /// <summary>
    /// 結果出力の出力結果を表します。
    /// </summary>
    public interface IReportingResult
    {
        /// <summary>
        /// 出力先を取得します。
        /// </summary>
        string OutputLocation { get; }
    }
}
