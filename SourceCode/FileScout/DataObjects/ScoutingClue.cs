using FileScout.Interfaces;
using System.Text;

namespace FileScout.DataObjects
{
    /// <summary>
    /// 偵察の手掛かり。
    /// </summary>
    public class ScoutingClue : IScoutingClue
    {
        /// <inheritdoc/>
        public string FilePath { get; set; }

        /// <inheritdoc/>
        public Encoding Encoding { get; set; }

        /// <inheritdoc/>
        public bool IsBinary { get => Encoding == null; }

        /// <summary>
        /// <see cref="ScoutingClue"/> クラスの新しいインスタンスを作成する。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <param name="encodingDetector">文字エンコード検出。</param>
        /// <returns>手掛かり。</returns>
        public static ScoutingClue Generate(string filePath, IEncodingDetector detector = null)
        {
            return
                new ScoutingClue
                {
                    FilePath = filePath,
                    Encoding = detector?.Detect(filePath)
                };
        }
    }
}