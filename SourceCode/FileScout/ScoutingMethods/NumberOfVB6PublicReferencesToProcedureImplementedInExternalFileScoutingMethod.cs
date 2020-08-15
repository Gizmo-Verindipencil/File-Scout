using System;
using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// VB6の外部dll参照のプロシージャ（Public）数の偵察手段。
    /// </summary>
    public class NumberOfVB6PublicReferencesToProcedureImplementedInExternalFileScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            var newLine = new string[] { Environment.NewLine };

            int count = 0;
            foreach (var row in text)
            {
                var targets = new string[] { "public declare ", "declare " };
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
