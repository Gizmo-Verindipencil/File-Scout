using System.Collections.Generic;

namespace FileScout.Interfaces
{
    /// <summary>
    /// 調査結果を表します。
    /// </summary>
    public interface IScoutingResult
    {
        /// <summary>
        /// 調査項目を取得します。
        /// </summary>
        List<string> Columns { get; }

        /// <summary>
        /// 調査項目の内容を取得します。
        /// </summary>
        List<List<string>> Values { get; }
    }
}
