using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// 最低限の調査を提供します。
    /// </summary>
    public class SimplestScout : TextScout
    {
        /// <summary>
        /// <see cref="SimplestScout"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public SimplestScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Clear();
            ScoutingMethod.Add(key: "ディレクトリ", value: new ParentDirectoryScoutingMethod());
            ScoutingMethod.Add(key: "ファイル", value: new NameScoutingMethod());
        }
    }
}
