using FileScout.ScoutsingMethods;

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
            Methods.Add(key: "行数(VBコメント)",       value: new NumberOfVB6CommentRowsScoutingMethod());
            Methods.Add(key: "関数(Private/Sub)",      value: new NumberOfVB6PrivateSubProceduresScoutingMethod());
            Methods.Add(key: "関数(Private/Function)", value: new NumberOfVB6PrivateFunctionProceduresScoutingMethod());
            Methods.Add(key: "関数(Public/Sub)",       value: new NumberOfVB6PublicSubProceduresScoutingMethod());
            Methods.Add(key: "関数(Public/Function)",  value: new NumberOfVB6PublicFunctionProceduresScoutingMethod());
            Methods.Add(key: "関数(外部dll/Private)",  value: new NumberOfVB6PrivateReferencesToProcedureImplementedInExternalFileScoutingMethod());
            Methods.Add(key: "関数(外部dll/Public)",   value: new NumberOfVB6PublicReferencesToProcedureImplementedInExternalFileScoutingMethod());
        }
    }
}
