using FileScout.Interfaces;

namespace FileScout.DataObjects
{
    /// <summary>
    /// 結果出力の入力内容を提供します。
    /// </summary>
    public class ReportingClue : IReportingClue
    {
        /// <summary>
        /// 調査結果を取得または設定します。
        /// </summary>
        public IScoutingResult ScoutingResult { get; set; }

        /// <summary>
        /// 調査結果の出力先パスを取得または設定します。
        /// </summary>
        public string OutputLocation { get; set; }
    }
}
