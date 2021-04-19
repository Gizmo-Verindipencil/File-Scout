using FileScout.EncodingDetectors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.EncodingDetectors
{
    /// <summary>
    /// 文字エンコード検出のテストを提供します。
    /// </summary>
    [TestClass]
    public class ReadJEncEncodingDetectorUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(ReadJEncEncodingDetectorUnitTest));

                return dir;
            }
        }

        /// <summary>
        /// テストで使うリソースを準備します。
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            if (!Directory.Exists(this.DirectoryPath))
            {
                Directory.CreateDirectory(this.DirectoryPath);
            }
        }

        /// <summary>
        /// テストで使ったリソースを開放します。
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(this.DirectoryPath))
            {
                Directory.Delete(this.DirectoryPath, true);
            }
        }

        /// <summary>
        /// バイナリファイルの場合の文字エンコード検知を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Detect_WhenFileIsBinary()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            using (var stream = File.Create(filePath))
            {
                var data = new byte[] { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6 };
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }

            // テスト対象の処理を実行
            var detector = new ReadJEncEncodingDetector();
            var actual = detector.Detect(filePath);

            // テスト結果を検証
            Assert.IsNull(actual);
        }

        /// <summary>
        /// テキストファイルの場合の文字エンコード検知を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Detect_WhenFileIsText()
        {
            // 指定した文字エンコードのファイルの作成処理
            var i = 0;
            var fileNameBase = MethodBase.GetCurrentMethod().Name;
            string CreateFile(Encoding encoding)
            {
                var fileName = $"{fileNameBase}{i++}";
                var filePath = Path.Combine(this.DirectoryPath, fileName);
                using (var stream = File.Create(filePath))
                using (var writer = new StreamWriter(stream, encoding))
                {
                    writer.WriteLine("abcdefghijklmnopqrstuvwxyz");
                    writer.WriteLine("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
                    writer.WriteLine("0123456789");
                    writer.Flush();
                }
                return filePath;
            }

            // ファイル生成・文字エンコード検出処理
            var detector = new ReadJEncEncodingDetector();
            Encoding ForceDetectorToDetect(Encoding encoding)
            {
                var filePath = CreateFile(encoding);
                return detector.Detect(filePath);
            }

            // テスト対象の実行・検証処理
            void Test(Encoding encoding, Encoding alsoExpected=null)
            {
                var actual = ForceDetectorToDetect(encoding);
                Assert.AreEqual((alsoExpected ?? encoding).WebName, actual.WebName);
            }

            // 検証の実行
            Test(Encoding.ASCII, Encoding.UTF8);    // UTF8はASCIIと互換あり
            Test(Encoding.UTF7, Encoding.UTF8);     // ???
            Test(Encoding.UTF8);
            Test(Encoding.UTF32);
            Test(Encoding.Unicode);
            Test(Encoding.BigEndianUnicode);
        }
    }
}
