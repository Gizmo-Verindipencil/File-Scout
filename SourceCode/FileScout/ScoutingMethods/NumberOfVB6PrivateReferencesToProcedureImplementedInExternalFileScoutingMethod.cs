using System.IO;
using FileScout.Interfaces;

namespace FileScout.ScoutsingMethods
{
    /// <summary>
    /// VB6の外部dll参照のプロシージャ（Private）数の偵察手段。
    /// </summary>
    public class NumberOfVB6PrivateReferencesToProcedureImplementedInExternalFileScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = File.ReadLines(clue.FilePath, clue.Encoding);

            int count = 0;
            foreach (var row in text)
            {
                var targets = new string[] { "private declare " };
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
