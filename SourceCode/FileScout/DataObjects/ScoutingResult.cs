using FileScout.Interfaces;
using System.Collections.Generic;

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

        /// <summary>
        /// 調査過程のエラー発生有無を取得または設定します。
        /// </summary>
        public bool ErrorOccurred { get; set; }

        /// <summary>
        /// 調査過程のエラー情報を取得または設定します。
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}