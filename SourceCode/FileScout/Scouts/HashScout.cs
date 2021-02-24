using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// ファイルハッシュを調査する斥候。
    /// </summary>
    public class HashScout : BaseScout
    {
        /// <summary>
        /// <see cref="HashScout"/> クラスの新しいインスタンスを作成する。
        /// </summary>
        public HashScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "ハッシュ(SHA-256)", value: new HashSHA256ScoutingMethod());
        }
    }
}
