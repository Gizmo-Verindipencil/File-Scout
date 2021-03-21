using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// ファイルの空行数の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class NumberOfEmptyRowsScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(NumberOfEmptyRowsScoutingMethodUnitTest));
                
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
            var method = new NumberOfEmptyRowsScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = null,

            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// 空行が存在しない場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileDoesNotHaveEmptyRow()
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
            var method = new NumberOfEmptyRowsScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// 空行が存在する場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileHasEmptyRows()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                // カウント対象
                writer.WriteLine(new string(' ', 15));
                writer.WriteLine(new string('　', 15));
                writer.WriteLine(new string('\t', 15));
                writer.WriteLine($"{' '}{'　'}{'\t'}");

                // 非カウント対象
                writer.WriteLine("       a       ");

                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfEmptyRowsScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("4", actual);
        }
    }
}
