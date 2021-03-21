using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// ファイルハッシュ(SHA-256)の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class HashSHA256ScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(HashSHA256ScoutingMethodUnitTest));
                
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
        /// ファイルハッシュ(SHA-256)の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsHash()
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
            var method = new HashSHA256ScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath
            });

            // 予想結果を生成
            string expected = default;
            using (var stream = File.OpenRead(filePath))
            {
                var sha256 = SHA256.Create();
                var hash = sha256.ComputeHash(stream);
                expected = BitConverter.ToString(hash).Replace("-", string.Empty);
            }

            // テスト結果を検証
            Assert.AreEqual(expected, actual);
        }
    }
}
