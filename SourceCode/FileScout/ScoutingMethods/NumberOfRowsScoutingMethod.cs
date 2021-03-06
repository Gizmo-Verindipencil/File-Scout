﻿using FileScout.Interfaces;
using System.IO;
using System.Linq;


namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイルの行数の調査手段を提供します。
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
