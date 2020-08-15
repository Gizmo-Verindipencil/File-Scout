﻿using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// VB6のコメント行数の偵察手段。
    /// </summary>
    public class NumberOfVB6CommentRowsScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);

            int count = 0;
            foreach (var row in text)
            {
                var targets = new string[] { "'", "rem " };
                var blanks = new char[] { ' ', '　' };
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
