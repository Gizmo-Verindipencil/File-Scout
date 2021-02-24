using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// 最も簡素な斥候。
    /// </summary>
    public class SimplestScout : TextScout
    {
        /// <summary>
        /// <see cref="SimplestScout"/> クラスの新しいインスタンスを作成する。
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
