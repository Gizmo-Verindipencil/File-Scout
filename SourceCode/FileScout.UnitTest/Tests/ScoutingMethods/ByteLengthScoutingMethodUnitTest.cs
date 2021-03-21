﻿using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// ファイルバイト長の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class ByteLengthScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(ByteLengthScoutingMethodUnitTest));
                
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
        /// ファイルバイト長の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsByteLength()
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
            var method = new ByteLengthScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath
            });

            // テスト結果を検証
            Assert.AreEqual("6", actual);
        }
    }
}
