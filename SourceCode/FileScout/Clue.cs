using System.Text;

namespace FileScout
{
    /// <summary>
    /// 偵察の手掛かり。
    /// </summary>
    public class Clue
    {
        /// <summary>
        /// ファイルのパス。
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// ファイルのエンコード。
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 手掛かりを生成する。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <returns>手掛かり。</returns>
        public static Clue Generate(string filePath)
        {
            return
                new Clue
                {
                    FilePath = filePath,
                    // ToDo : ファイルエンコーディングの自動判定
                    Encoding = Encoding.GetEncoding("Shift_JIS")
                };
        }
    }
}
