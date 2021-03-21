using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// ファイル名の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class NameScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(NameScoutingMethodUnitTest));
                
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
        /// 拡張子が存在しない場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsName_WhenFileDoesNotHaveExtension()
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
            var method = new NameScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath
            });

            // テスト結果を検証
            Assert.AreEqual(fileName, actual);
        }

        /// <summary>
        /// 拡張子が存在する場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsOnlyName_WhenFileHasExtension()
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
            var method = new NameScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath
            });

            // テスト結果を検証
            var expected = fileName + fileExtension;
            Assert.AreEqual(expected, actual);
        }
    }
}
