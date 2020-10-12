using FileScout.Interfaces;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// VB6のプロシージャ内のコメント行数の偵察手段。
    /// </summary>
    public class NumberOfVB6CommentRowsInProcedureScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            var text = NumberOfVB6ProcedureRowsScoutingMethod.GetProcedureRows(clue);

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