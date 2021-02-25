using System.Text;

namespace FileScout.Interfaces
{
    /// <summary>
    /// 文字エンコードの検出を表します。
    /// </summary>
    public interface IEncodingDetector
    {
        /// <summary>
        /// 文字エンコードを検出します。
        /// </summary>
        /// <param name="filePath">ファイルパス。</param>
        /// <returns>
        /// 文字エンコードを返します。
        /// </returns>
        Encoding Detect(string filePath);
    }
}