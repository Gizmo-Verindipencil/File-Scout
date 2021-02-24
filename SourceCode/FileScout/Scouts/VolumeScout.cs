using FileScout.ScoutingMethods;

namespace FileScout.Scouts
{
    /// <summary>
    /// ファイル容量を調査する斥候。
    /// </summary>
    public class VolumeScout : BaseScout
    {
        /// <summary>
        /// <see cref="VolumeScout"/> クラスの新しいインスタンスを作成する。
        /// </summary>
        public VolumeScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "バイト長", value: new ByteLengthScoutingMethod());
        }
    }
}
