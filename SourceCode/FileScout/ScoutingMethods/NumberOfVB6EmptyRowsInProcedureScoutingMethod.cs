using FileScout.Extensions;
using FileScout.Interfaces;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// VB6のSUB/FUNCTIONプロシージャーに含まれる空行数の調査手段を提供します。
    /// </summary>
    public class NumberOfVB6EmptyRowsInProcedureScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            var text = NumberOfVB6ProcedureRowsScoutingMethod.GetProcedureRows(clue);
            var blanks = new string[] { " ", "　" };
            return text
                .Where(x => x.RemoveString(blanks).Length == 0)
                .Count().ToString();
        }
    }
}