using FileScout.DataObjects;
using FileScout.Extensions;
using FileScout.Interfaces;
using System.IO;
using System.Linq;
using System.Text;

namespace FileScout.ScoutingReporters
{
    /// <summary>
    /// 調査結果のCSV出力を提供します。
    /// </summary>
    public class CSVScoutingReporter : IScoutingReporter
    {
        /// <inheritdoc/>
        public IReportingResult Report(IReportingClue clue)
        {
            // 報告内容の作成
            var report = new StringBuilder();

            if (clue.ScoutingResult.ErrorOccurred)
            {
                // エラーの出力
                report.Append(clue.ScoutingResult.ErrorMessage);
            }
            else
            {
                // CSVの出力
                report.AppendLine(string.Join(",", clue.ScoutingResult.Columns.Select(x => x.Enclose("\""))));
                foreach (var value in clue.ScoutingResult.Values)
                {
                    report.AppendLine(string.Join(",", value.Select(x => x.Enclose("\""))));
                }
            }

            // ファイルの出力
            
            File.WriteAllText(clue.OutputLocation, report.ToString(), Encoding.UTF8);

            // 出力結果を返す
            return new ReportingResult()
            {
                OutputLocation = clue.OutputLocation
            };
        }
    }
}
