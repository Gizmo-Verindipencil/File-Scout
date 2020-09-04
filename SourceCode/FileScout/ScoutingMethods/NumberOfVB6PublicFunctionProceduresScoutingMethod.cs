using System;
using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// VB6のFunctionプロシージャ―（Public）数の偵察手段。
    /// </summary>
    public class NumberOfVB6PublicFunctionProceduresScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            var newLine = new string[] { Environment.NewLine };

            int count = 0;
            foreach (var row in text)
            {
                var targets = new string[] { "public function ", "function " };
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
