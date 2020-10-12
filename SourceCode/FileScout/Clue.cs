using FileScout.EncodingDetectors;
using FileScout.Interfaces;
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
        /// バイナリファイルであるかどうかを示す。
        /// </summary>
        public bool IsBinary { get => Encoding == null; }

        /// <summary>
        /// 手掛かりを生成する。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <param name="encodingDetector">文字エンコード検出。</param>
        /// <returns>手掛かり。</returns>
        public static Clue Generate(string filePath, IEncodingDetector detector = null)
        {
            return
                new Clue
                {
                    FilePath = filePath,
                    Encoding = detector.Detect(filePath)
                };
        }
    }
}