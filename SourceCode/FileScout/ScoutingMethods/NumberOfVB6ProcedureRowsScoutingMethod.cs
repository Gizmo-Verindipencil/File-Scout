using FileScout.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// VB6のプロシージャ行数の偵察手段。
    /// </summary>
    public class NumberOfVB6ProcedureRowsScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(Clue clue)
        {
            return GetProcedureRows(clue).Count().ToString();
        }

        /// <summary>
        /// Sub・Functionプロシージャの行を取得する。
        /// </summary>
        /// <param name="clue">手掛かり。</param>
        /// <returns>プロシージャの行。</returns>
        public static IEnumerable<string> GetProcedureRows(Clue clue)
        {
            // バイナリファイルの場合は終了
            if (clue.IsBinary) yield break;

            // 対象フラグ
            var diggingSub = false;
            var diggingFnc = false;

            // 始端・終端の検知フラグ
            var detectingSubHead = false;
            var detectingFncHead = false;
            var detectingSubTail = false;
            var detectingFncTail = false;

            // 終端検知時の次行対象外フラグ
            var nextRowIsOutOfSub = false;
            var nextRowIsOutOfFnc = false;

            // カウントを開始する表現
            var subHeads = new string[] { "sub ", "public sub ", "private sub " };
            var fncHeads = new string[] { "function ", "public function ", "private function " };

            // カウントを終了する表現
            const string subTail = "end sub";
            const string fncTail = "end function";

            var text = File.ReadLines(clue.FilePath, clue.Encoding);
            foreach (var row in text)
            {
                // 始端の判定
                bool IsStartingWithThose(string value, string[] heads)
                {
                    foreach (var head in heads)
                    {
                        if (value.ToLower().StartsWith(head)) return true;
                    }
                    return false;
                }

                // 前行が終端ならば次行は対象外
                if (nextRowIsOutOfSub)
                {
                    diggingSub = false;
                    nextRowIsOutOfSub = false;
                }
                if (nextRowIsOutOfFnc)
                {
                    diggingFnc = false;
                    nextRowIsOutOfFnc = false;
                }

                // プロシージャの開始・終了を検知
                var blanks = new char[] { ' ', '　', '\t' };
                var rowHead = row.TrimStart(blanks);
                detectingSubHead = IsStartingWithThose(rowHead, subHeads);
                detectingFncHead = IsStartingWithThose(rowHead, fncHeads);
                detectingSubTail = rowHead.ToLower().StartsWith(subTail);
                detectingFncTail = rowHead.ToLower().StartsWith(fncTail);

                // 始端は対象に含む
                if (!diggingSub && detectingSubHead) diggingSub = true;
                if (!diggingFnc && detectingFncHead) diggingFnc = true;

                // 終端は対象に含む
                if (diggingSub && detectingSubTail) nextRowIsOutOfSub = true;
                if (diggingFnc && detectingFncTail) nextRowIsOutOfFnc = true;

                // 対象ならば行を返す
                if (diggingSub || diggingFnc) yield return row;
            }
        }
    }
}