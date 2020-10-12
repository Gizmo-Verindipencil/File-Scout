using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// ファイルハッシュを調査する斥候。
    /// </summary>
    public class HashScout : BaseScout
    {
        public HashScout()
        {
            /// 調査項目の定義
            Methods.Add(key: "ハッシュ(SHA-256)", value: new HashSHA256ScoutingMethod());
        }
    }
}
