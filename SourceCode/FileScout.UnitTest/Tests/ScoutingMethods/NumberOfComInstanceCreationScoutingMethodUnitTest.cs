using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// COMオブジェクトのインスタンス生成回数の調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class NumberOfComInstanceCreationScoutingMethodUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(NumberOfComInstanceCreationScoutingMethodUnitTest));
                
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
            var method = new NumberOfComInstanceCreationScoutingMethod();
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = null,
                
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// COMインスタンス生成が存在しない場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileDoesNotHaveComInstanceCreation()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.GetEncoding("shift-jis");
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                var script = @"
MsgBox ""Hello World""
";

                writer.Write(script);
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfComInstanceCreationScoutingMethod() { ComObjectName = "ComClass1" };
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("0", actual);
        }

        /// <summary>
        /// COMインスタンス生成が存在する場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsNumber_WhenFileHasComInstanceCreation()
        {
            // テスト用のファイルを用意
            var fileName = MethodBase.GetCurrentMethod().Name;
            var filePath = Path.Combine(this.DirectoryPath, fileName);
            var fileEncoding = Encoding.GetEncoding("shift-jis");
            using (var stream = File.Create(filePath))
            using (var writer = new StreamWriter(stream, fileEncoding))
            {
                var script = @"
Dim a As Object
Dim b As Object
Dim c As Object

Set a = CreateObject(""ComClass1"") ' カウント対象1
Set b = CreateObject(""ComClass2"")
Set c = CreateObject(""ComClass3"")

Dim d As Object
Dim e As Object
Dim f As Object

Set d = CreateObject(""cOMcLASS1"") ' カウント対象2
Set e = CreateObject(""cOMcLASS2"")
Set f = CreateObject(""cOMcLASS3"")

Dim h As Object
Dim i As Object
Dim j As Object

Set h = CreateObject(""comclass1"") ' カウント対象3
Set i = CreateObject(""comclass2"")
Set j = CreateObject(""comclass3"")

Dim k As Object
Dim l As Object
Dim m As Object

Set k = CreateObject(""COMCLASS1"") ' カウント対象4
Set l = CreateObject(""COMCLASS2"")
Set m = CreateObject(""COMCLASS3"")

Dim o As Object
Dim p As Object
Dim q As Object

Set o=CreateObject(""ComClass1"") ' カウント対象5
Set p=CreateObject(""ComClass2"")
Set q=CreateObject(""ComClass3"")

'Dim r As Object
'Dim s As Object
'Dim t As Object

'Set r = CreateObject(""ComClass1"") ' 非カウント対象
'Set s = CreateObject(""ComClass2"")
'Set t = CreateObject(""ComClass3"")

Dim u As Object
Dim v As Object
Dim w As Object

Set u = WScript.CreateObject(""ComClass1"") ' カウント対象6
Set v = WScript.CreateObject(""ComClass2"")
Set w = WScript.CreateObject(""ComClass3"")
";

                writer.Write(script);
                writer.Flush();
            }

            // テスト対象の処理を実行
            var method = new NumberOfComInstanceCreationScoutingMethod() { ComObjectName = "ComClass1"};
            var actual = method.Do(new ScoutingClue()
            {
                FilePath = filePath,
                Encoding = fileEncoding
            });

            // テスト結果を検証
            Assert.AreEqual("6", actual);
        }
    }
}
