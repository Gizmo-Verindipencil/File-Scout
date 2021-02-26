using FileScout.Extensions;
using FileScout.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FileScout.ScoutingReporters
{
    /// <summary>
    /// 偵察結果のCSV出力を提供します。
    /// </summary>
    public class CsvScoutingReporter : IScoutingReporter
    {
        /// <inheritdoc/>
        public void Report(IScoutingResult result)
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
        }
    }
}
