using FileScout.Interfaces;

namespace FileScout.ReportingResults
{
    /// <summary>
    /// 結果の出力結果を提供します。
    /// </summary>
    public class ReportingResult : IReportingResult
    {
        /// <summary>
        /// 出力先を取得または設定します。
        /// </summary>
        public string OutputLocation { get; set; }
    }
}
