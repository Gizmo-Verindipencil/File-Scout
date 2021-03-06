﻿using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// タブとスペース混在有無の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class UsingBothTabsAndSpacesScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(UsingBothTabsAndSpacesScoutingMethodUnitTest));
                
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
            var method = new UsingBothTabsAndSpacesScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// スペースのみが含まれる場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsZero_WhenFileContainsOnlySpaces()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine(new string(' ', 5) + "a");
                writer.WriteLine(new string(' ', 5) + "b");
                writer.WriteLine(new string(' ', 5) + "c");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new UsingBothTabsAndSpacesScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// タブのみが含まれる場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsZero_WhenFileContainsOnlyTabs()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine(new string('\t', 5) + "a");
                writer.WriteLine(new string('\t', 5) + "b");
                writer.WriteLine(new string('\t', 5) + "c");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new UsingBothTabsAndSpacesScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// タブのみが含まれる場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsOne_WhenFileContainsBothTabsAndSpaces()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.UTF8;
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                writer.WriteLine(new string('\t', 5) + "a");
                writer.WriteLine(new string('\t', 5) + "b");
                writer.WriteLine(new string('\t', 5) + "c");
                writer.WriteLine(new string(' ', 5) + "d");
                writer.WriteLine(new string(' ', 5) + "e");
                writer.WriteLine(new string(' ', 5) + "f");
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new UsingBothTabsAndSpacesScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("1", actual);
        }
    }
}
