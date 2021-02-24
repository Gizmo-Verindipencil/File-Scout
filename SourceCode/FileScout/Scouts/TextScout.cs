using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// テキストファイルを調査する斥候。
    /// </summary>
    public class TextScout : BaseScout
    {
        /// <summary>
        /// <see cref="TextScout"/> クラスの新しいインスタンスを作成する。
        /// </summary>
        public TextScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "文字エンコード", value: new EncodingScoutingMethod());
            ScoutingMethod.Add(key: "文字数", value: new TextLengthScoutingMethod());
            ScoutingMethod.Add(key: "文字数(空白)", value: new NumberOfBlanksScoutingMethod());
            ScoutingMethod.Add(key: "行数", value: new NumberOfRowsScoutingMethod());
            ScoutingMethod.Add(key: "行数(空)", value: new NumberOfEmptyRowsScoutingMethod());
            ScoutingMethod.Add(key: "平均(行文字数)", value: new RowTextLengthAverageScoutingMethod());
            ScoutingMethod.Add(key: "標準偏差(行文字数)", value: new RowTextLengthStandardDeviationScoutingMethod());
        }
    }
}