using System.Collections.Generic;

namespace FileScout.Interfaces
{
    /// <summary>
    /// ファイルの調査を表します。
    /// </summary>
    interface IScout
    {
        /// <summary>
        /// 調査項目を取得します。
        /// </summary>
        Dictionary<string, IScoutingMethod> ScoutingMethod { get; }

        /// <summary>
        /// ファイルを調査します。
        /// </summary>
        /// <param name="path">ディレクトリのパス。</param>
        /// <returns>
        /// ファイルの調査結果を返します。
        /// </returns>
        string Scout(string path);
    }
}
