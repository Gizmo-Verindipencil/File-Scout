﻿using System.IO;
using System.Linq;
using FileScout.Interfaces;


namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// 行数の偵察手段。
    /// </summary>
    public class NumberOfRowsScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            return text
                .Count().ToString();
        }
    }
}
