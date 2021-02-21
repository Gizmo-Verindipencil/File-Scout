using System.Collections.Generic;

namespace FileScout.Interfaces
{
    /// <summary>
    /// 偵察結果。
    /// </summary>
    public interface IScoutingResult
    {
        /// <summary>
        /// 報告項目。
        /// </summary>
        List<string> Columns { get; }

        /// <summary>
        /// 偵察結果。
        /// </summary>
        List<List<string>> Values { get; }
    }
}
