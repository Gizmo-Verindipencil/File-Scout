using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// VB6のPUBLIC/SUBプロシージャー数の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class NumberOfVB6PublicSubProceduresScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(NumberOfVB6PublicSubProceduresScoutingMethodUnitTest));
                
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
            var method = new NumberOfVB6PublicSubProceduresScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = null,
                
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// VB6のPRIVATE/SUBプロシージャー数の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.GetEncoding("shift-jis");
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                var script = @"

Sub a()
    Dim a1 As Object
End Sub

Public Sub b()
    Dim b1 As Object
End Sub

Private Sub c()
    Dim c1 As Object
End Sub

Function d() As Object
    Dim d1 As Object
End Function

Public Function e() As Object
    Dim e1 As Object
End Function

Private Function f() As Object
    Dim f1 As Object
End Function

";

                writer.Write(script);
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfVB6PublicSubProceduresScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("2", actual);
        }
    }
}
