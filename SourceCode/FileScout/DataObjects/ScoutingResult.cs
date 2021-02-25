using FileScout.Extensions;
using FileScout.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileScout.DataObjects
{
    /// <summary>
    /// 調査結果を提供します。
    /// </summary>
    public class ScoutingResult : IScoutingResult
    {
        /// <summary>
        /// 調査項目を取得または設定します。
        /// </summary>
        public List<string> Columns { get; set; }

        /// <summary>
        /// 調査項目の内容を取得または設定します。
        /// </summary>
        public List<List<string>> Values { get; set; } = new List<List<string>>();

        /// <inheritdoc/>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Join(",", Columns.Select(x => x.Enclose("\""))));
            foreach (var value in Values)
            {
                builder.AppendLine(string.Join(",", value.Select(x => x.Enclose("\""))));
            }
            return builder.ToString();
        }
    }
}