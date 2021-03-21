using FileScout.DataObjects;
using FileScout.ScoutingMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingMethods
{
    /// <summary>
    /// ファイル文字エンコードの調査手段のテストを提供します。
    /// </summary>
    [TestClass]
    public class EncodingScoutingMethodUnitTest
    {
        /// <summary>
        /// ファイル文字エンコードが判明している場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsEncoding()
        {
            // インスタンス生成
            var method = new EncodingScoutingMethod();

            // テスト対象の処理を実行
            var actual1 = method.Do(new ScoutingClue() { Encoding = Encoding.UTF8});
            var actual2 = method.Do(new ScoutingClue() { Encoding = Encoding.Unicode });
            var actual3 = method.Do(new ScoutingClue() { Encoding = Encoding.ASCII });

            // テスト結果を検証
            Assert.AreEqual(Encoding.UTF8.WebName, actual1);
            Assert.AreEqual(Encoding.Unicode.WebName, actual2);
            Assert.AreEqual(Encoding.ASCII.WebName, actual3);
        }

        /// <summary>
        /// バイナリファイルの場合の調査結果を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Do_ReturnsUnknown_WhenFileIsBinary()
        {
            // インスタンス生成
            var method = new EncodingScoutingMethod();

            // テスト対象の処理を実行
            var actual = method.Do(new ScoutingClue() { Encoding = null });

            // テスト結果を検証
            Assert.AreEqual("unknown", actual);
        }
    }
}
