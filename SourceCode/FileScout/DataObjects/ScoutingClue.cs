using FileScout.Interfaces;
using System.Text;

namespace FileScout.DataObjects
{
    /// <summary>
    /// ファイル調査の入力内容を提供します。
    /// </summary>
    public class ScoutingClue : IScoutingClue
    {
        /// <summary>
        /// ファイルのパスを取得または設定します。
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// ファイルの文字エンコードを取得または設定します。
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <inheritdoc/>
        public bool IsBinary { get => Encoding == null; }

        /// <summary>
        /// <see cref="ScoutingClue"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        /// <param name="filePath">ファイルのパス。</param>
        /// <param name="detector">文字エンコード検出。</param>
        /// <returns>手掛かり。
        /// ファイル調査の入力内容を返します。
        /// </returns>
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