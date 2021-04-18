using FileScout.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileScout.UnitTest.Tests.Extensions
{
    /// <summary>
    /// <see cref="string"/>の拡張メソッドのテストを提供します。
    /// </summary>
    [TestClass]
    public class StringExtensionsUnitTest
    {
        /// <summary>
        /// 指定表現カウント処理を検証します。
        /// </summary>
        [TestMethod]
        public void Test_CountString()
        {
            // テスト対象の処理を実行
            var actual1 = "abcdefghijklmnopqrstuxyz".CountString("efg");
            var actual2 = "abcdefghijklmnopqrstuxyz".CountString("nothing");
            var actual3 = "abcabcabcefgefgefghijhij".CountString("efg");

            // テスト結果を検証
            Assert.AreEqual(1, actual1);
            Assert.AreEqual(0, actual2);
            Assert.AreEqual(3, actual3);
        }

        /// <summary>
        /// 指定表現削除処理を検証します。
        /// </summary>
        [TestMethod]
        public void Test_RemoveString()
        {
            // テスト対象の処理を実行
            var actual1 = "abcdefghijklmnopqrstuxyz".RemoveString("efg");
            var actual2 = "abcdefghijklmnopqrstuxyz".RemoveString("nothing");
            var actual3 = "abcabcabcefgefgefghijhij".RemoveString("efg");

            // テスト結果を検証
            Assert.AreEqual("abcdhijklmnopqrstuxyz", actual1);
            Assert.AreEqual("abcdefghijklmnopqrstuxyz", actual2);
            Assert.AreEqual("abcabcabchijhij", actual3);
        }

        /// <summary>
        /// 表現の囲い処理を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Enclose()
        {
            // テスト対象の処理を実行
            var actual = "contents".Enclose("|-|");

            // テスト結果を検証
            Assert.AreEqual("|-|contents|-|", actual);
        }
    }
}
