using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// ファイル容量の調査を提供します。
    /// </summary>
    public class VolumeScout : BaseScout
    {
        /// <summary>
        /// <see cref="VolumeScout"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public VolumeScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "バイト長", value: new ByteLengthScoutingMethod());
        }
    }
}
