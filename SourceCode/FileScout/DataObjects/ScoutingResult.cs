using FileScout.Extensions;
using FileScout.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScout.DataObjects
{
    /// <summary>
    /// 偵察結果。
    /// </summary>
    public class ScoutingResult : IScoutingResult
    {
        /// <summary>
        /// 報告項目。
        /// </summary>
        public List<string> Columns { get; set; }

        /// <summary>
        /// 偵察結果。
        /// </summary>
        public List<List<string>> Values { get; set; } = new List<List<string>>();

        /// <summary>
        /// CSV形式に変換する。
        /// </summary>
        /// <returns>CSV形式の文字列。</returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Join(",", Columns.Select(x => x.Enclose("\""))));
            foreach (var row in Values)
            {
                builder.AppendLine(string.Join(",", row.Select(x => x.Enclose("\""))));
            }
            return builder.ToString();
        }
    }
}