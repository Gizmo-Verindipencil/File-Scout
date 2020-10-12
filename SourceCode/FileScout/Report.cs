using FileScout.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScout
{
    /// <summary>
    /// 偵察報告。
    /// </summary>
    public class Report
    {
        /// <summary>
        /// 報告項目。
        /// </summary>
        public List<string> Columns { get; set; }

        /// <summary>
        /// 偵察結果。
        /// </summary>
        public List<List<string>> Rows { get; set; } = new List<List<string>>();

        /// <summary>
        /// CSV形式に変換する。
        /// </summary>
        /// <returns>CSV形式の文字列。</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Join(",", Columns.Select(x => x.Enclose("\""))));
            foreach (var row in Rows)
            {
                builder.AppendLine(string.Join(",", row.Select(x => x.Enclose("\""))));
            }
            return builder.ToString();
        }
    }
}