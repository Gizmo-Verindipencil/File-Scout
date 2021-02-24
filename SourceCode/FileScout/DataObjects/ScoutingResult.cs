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
        /// <inheritdoc/>
        public List<string> Columns { get; set; }

        /// <inheritdoc/>
        public List<List<string>> Values { get; set; } = new List<List<string>>();

        /// <inheritdoc/>
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