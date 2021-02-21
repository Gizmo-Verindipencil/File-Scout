using System.Collections.Generic;

namespace FileScout.Interfaces
{
    /// <summary>
    /// 斥候。
    /// </summary>
    interface IScout
    {
        /// <summary>
        /// 調査手段。
        /// </summary>
        Dictionary<string, IScoutingMethod> ScoutingMethod { get; }

        /// <summary>
        /// 偵察する。
        /// </summary>
        /// <param name="path">ディレクトリのパス。</param>
        /// <returns>偵察結果。</returns>
        string Scout(string path);
    }
}
