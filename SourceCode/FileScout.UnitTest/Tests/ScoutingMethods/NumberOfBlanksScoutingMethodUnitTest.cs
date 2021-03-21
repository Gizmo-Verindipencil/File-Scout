using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// ファイルの空白文字数の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class NumberOfBlanksScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(NumberOfBlanksScoutingMethodUnitTest));
                
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
        /// バイナリファイルの場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsZero_WhenFileIsBinary()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            using (var stream = File.Create(filePath))
            {
                stream.Write(new byte[] { 0x0 }, 0, 1);
                stream.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfBlanksScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = null,

            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// 空白文字が存在しない場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileDoesNotHaveAnyBlank()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine("123");
                writer.WriteLine("abc");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfBlanksScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// 半角の空白文字が存在する場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileHasOnlyHalfBlanks()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine(" 23");
                writer.WriteLine("a c");
                writer.WriteLine("xy ");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfBlanksScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("3", actual);
        }

        /// <summary>
        /// 全角の空白文字が存在する場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileHasOnlyFullWidthBlanks()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine("　23");
                writer.WriteLine("a　c");
                writer.WriteLine("xy　");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfBlanksScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("3", actual);
        }

        /// <summary>
        /// タブ文字が存在する場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileHasOnlyTabs()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine("\t23");
                writer.WriteLine("a\tc");
                writer.WriteLine("xy\t");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfBlanksScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("3", actual);
        }

        /// <summary>
        /// 半角/全角の空白文字とタブ文字が混在する場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileHasHalfBlankAndFullWidthBlankAndTabs()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine(" 23");
                writer.WriteLine("a c");
                writer.WriteLine("xy ");
                writer.WriteLine("　23");
                writer.WriteLine("a　c");
                writer.WriteLine("xy　");
                writer.WriteLine("\t23");
                writer.WriteLine("a\tc");
                writer.WriteLine("xy\t");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfBlanksScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("9", actual);
        }
    }
}
