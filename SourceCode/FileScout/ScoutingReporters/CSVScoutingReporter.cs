using FileScout.Extensions;
using FileScout.Interfaces;
using FileScout.ReportingResults;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FileScout.ScoutingReporters
{
    /// <summary>
    /// 調査結果のCSV出力を提供します。
    /// </summary>
    public class CSVScoutingReporter : IScoutingReporter
    {
        /// <inheritdoc/>
        public IReportingResult Report(IScoutingResult result)
        {
            // 報告内容の作成
            var report = new StringBuilder();

            if (result.ErrorOccured)
            {
                // エラーの出力
                report.Append(result.ErrorMessage);
            }
            else
            {
                // CSVの出力
                report.AppendLine(string.Join(",", result.Columns.Select(x => x.Enclose("\""))));
                foreach (var value in result.Values)
                {
                    report.AppendLine(string.Join(",", value.Select(x => x.Enclose("\""))));
                }
            }

            // ファイルの出力
            var fileName = $"result_{DateTime.Now:yyyyMMddhhmmss}.csv";
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            File.WriteAllText(Path.Combine(path, fileName), report.ToString(), Encoding.UTF8);

            // 出力結果を返す
            return new ReportingResult()
            {
                OutputLocation = path
            };
        }
    }
}
