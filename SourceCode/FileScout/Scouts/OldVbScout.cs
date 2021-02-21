using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// 古い時代のVisual Basicを調査する斥候。
    /// </summary>
    public class OldVbScout : TextScout
    {
        public OldVbScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "行数(VBコメント)", value: new NumberOfVB6CommentRowsScoutingMethod());
            ScoutingMethod.Add(key: "関数(Private/Sub)", value: new NumberOfVB6PrivateSubProceduresScoutingMethod());
            ScoutingMethod.Add(key: "関数(Private/Function)", value: new NumberOfVB6PrivateFunctionProceduresScoutingMethod());
            ScoutingMethod.Add(key: "関数(Public/Sub)", value: new NumberOfVB6PublicSubProceduresScoutingMethod());
            ScoutingMethod.Add(key: "関数(Public/Function)", value: new NumberOfVB6PublicFunctionProceduresScoutingMethod());
            ScoutingMethod.Add(key: "関数(外部dll/Private)", value: new NumberOfVB6PrivateReferencesToProcedureImplementedInExternalFileScoutingMethod());
            ScoutingMethod.Add(key: "関数(外部dll/Public)", value: new NumberOfVB6PublicReferencesToProcedureImplementedInExternalFileScoutingMethod());
            ScoutingMethod.Add(key: "関数(行数)", value: new NumberOfVB6ProcedureRowsScoutingMethod());
            ScoutingMethod.Add(key: "関数(空行数)", value: new NumberOfVB6EmptyRowsInProcedureScoutingMethod());
            ScoutingMethod.Add(key: "関数(コメント行)", value: new NumberOfVB6CommentRowsInProcedureScoutingMethod());
        }
    }
}
