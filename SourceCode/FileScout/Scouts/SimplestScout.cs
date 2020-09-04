using FileScout.ScoutsingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// 最も簡素な斥候。
    /// </summary>
    public class SimplestScout : TextScout
    {
        public SimplestScout()
        {
            /// 調査項目の定義
            Methods.Clear();
            Methods.Add(key: "ディレクトリ", value: new ParentDirectoryScoutingMethod());
            Methods.Add(key: "ファイル", value: new NameScoutingMethod());
        }
    }
}
