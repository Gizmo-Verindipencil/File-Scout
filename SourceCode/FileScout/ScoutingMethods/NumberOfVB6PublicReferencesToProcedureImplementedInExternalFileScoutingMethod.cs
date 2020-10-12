﻿using System;
using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// VB6の外部dll参照のプロシージャ（Public）数の偵察手段。
    /// </summary>
    public class NumberOfVB6PublicReferencesToProcedureImplementedInExternalFileScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            // バイナリファイルの場合は処理なし
            if (clue.IsBinary) return "0";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            int count = 0;
            foreach (var row in text)
            {
                var targets = new string[] { "public declare ", "declare " };
                var blanks = new char[] { ' ', '　', '\t' };
                var head = row.TrimStart(blanks);

                foreach (var target in targets)
                {
                    if (head.ToLower().StartsWith(target))
                    {
                        count++;
                    }
                }
            }
            return count.ToString();
        }
    }
}
