using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// VB6のコメント行数の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class NumberOfVB6CommentRowsScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(NumberOfVB6CommentRowsScoutingMethodUnitTest));
                
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
            var method = new NumberOfVB6CommentRowsScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = null,
                
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// VB6のコメント行数の調査結果を検証します。
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
' out of procedure1
rem out of procedure2

Sub a()
    ' in sub procedure1
    rem in sub procedure2

    Dim obj ' this is comment but not comment row

End Sub

Public Sub b()
    ' in sub procedure3
    rem in sub procedure4
End Sub

Private Sub c()
    ' in sub procedure5
    rem in sub procedure6
End Sub

Function d() As Object
    ' in function procedure1
    rem in function procedure2
End Function

Public Function e() As Object
    ' in function procedure3
    rem in function procedure4
End Function

Private Function f() As Object
    ' in function procedure5
    rem in function procedure6
End Function
";

                writer.Write(script);
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfVB6CommentRowsScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("14", actual);
        }
    }
}
