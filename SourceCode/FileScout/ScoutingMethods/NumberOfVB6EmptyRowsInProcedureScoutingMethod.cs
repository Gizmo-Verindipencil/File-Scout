using FileScout.Extensions;
using FileScout.Interfaces;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// VB6のプロシージャ空行数の偵察手段。
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