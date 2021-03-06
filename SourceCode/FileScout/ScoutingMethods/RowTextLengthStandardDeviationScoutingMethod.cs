﻿using FileScout.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// 行文字数の標準偏差の調査手段を提供します。
    /// </summary>
    public class RowTextLengthStandardDeviationScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            if (text.Count() == 0) return "0";

            // 文字数の平均を算出
            var average = text
                .Select(x => x.Length)
                .Average();

            // 各行の文字数 - 平均文字数の二乗を合計
            var varianceSum = text
                .Select(x => Math.Pow(x.Length - average, 2))
                .Sum();

            // 分散を算出
            var varianceAverage = varianceSum / text.Count();

            // 分散の平方根を返す
            return Math.Sqrt(varianceAverage).ToString();
        }
    }
}