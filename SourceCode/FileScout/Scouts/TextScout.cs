using FileScout.ScoutsingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// テキストファイルを調査する斥候。
    /// </summary>
    public class TextScout : BaseScout
    {
        public TextScout()
        {
            /// 調査項目の定義
            Methods.Add(key: "文字数",       value: new TextLengthScoutingMethod());
            Methods.Add(key: "文字数(空白)", value: new NumberOfBlanksScoutingMethod());
            Methods.Add(key: "行数",         value: new NumberOfRowsScoutingMethod());
            Methods.Add(key: "行数(空)",     value: new NumberOfEmptyRowsScoutingMethod());
        }
    }
}
