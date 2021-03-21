using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// ファイル拡張子の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class ExtensionScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(ExtensionScoutingMethodUnitTest));
                
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
        /// ファイル拡張子がある場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsExtension()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var fileExtension = ".extension";
            var filePath = Path.Combine(this.DirectoryPath, fileName + fileExtension);
            using (var stream = File.Create(filePath))
            {
                stream.Write(new byte[] { 0x0 }, 0, 1);
                stream.Flush();
            }

            // テスト対象の処理を実行
            var method = new ExtensionScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath
            });

            // テスト結果を検証
            Assert.AreEqual(fileExtension, actual);
        }

        /// <summary>
        /// ファイル拡張子がない場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNoExtension()
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
            var method = new ExtensionScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath
            });

            // テスト結果を検証
            Assert.AreEqual(string.Empty, actual);
        }
    }
}
