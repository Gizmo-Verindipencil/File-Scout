using FileScout.Interfaces;
using Hnx8.ReadJEnc;
using System.IO;
using System.Text;

namespace FileScout.EncodingDetectors
{
    /// <summary>
    /// <see cref="ReadJEnc"/>を利用した文字エンコード検出クラス。
    /// </summary>
    public class ReadJEncEncodingDetector : IEncodingDetector
    {
        /// <inheritdoc/>
        public Encoding Detect(string filePath)
        {
            var info = new FileInfo(filePath);
            using (var reader = new FileReader(info))
            {
                var result = reader.Read(info);
                return result.GetEncoding();
            }
        }
    }
}