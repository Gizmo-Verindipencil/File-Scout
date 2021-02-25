using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// ファイルハッシュを調査を提供します。
    /// </summary>
    public class HashScout : BaseScout
    {
        /// <summary>
        /// <see cref="HashScout"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public HashScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "ハッシュ(SHA-256)", value: new HashSHA256ScoutingMethod());
        }
    }
}
