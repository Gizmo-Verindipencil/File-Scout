using System.Text;

namespace FileScout.Interfaces
{
    /// <summary>
    /// 文字エンコードの検出。
    /// </summary>
    public interface IEncodingDetector
    {
        /// <summary>
        /// 文字エンコードを検出する。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <returns>文字エンコード。</returns>
        Encoding Detect(string filePath);
    }
}