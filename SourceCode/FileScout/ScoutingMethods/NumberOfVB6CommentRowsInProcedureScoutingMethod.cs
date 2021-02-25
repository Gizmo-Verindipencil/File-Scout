using FileScout.Interfaces;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// VB6のSUB/FUNCTIONプロシージャーに含まれるコメント行数の調査手段を提供します。
    /// </summary>
    public class NumberOfVB6CommentRowsInProcedureScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
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