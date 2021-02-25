using System;

namespace FileScout.Extensions
{
    /// <summary>
    /// <see cref="string"/>の拡張メソッド。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 指定した表現の出現回数をカウントします。
        /// </summary>
        /// <param name="self">調べる文字列。</param>
        /// <param name="find">数える文字列。</param>
        /// <returns>
        /// <paramref name="self"/> における <paramref name="find"/> の出現回数を返します。
        /// </returns>
        public static int CountString(this string self, params string[] find)
        {
            int count = 0;
            foreach(var value in find)
            {
                var diff = self.Length - self.Replace(value, string.Empty).Length;
                count += diff / value.Length;
            }
            return count;
        }

        /// <summary>
        /// 指定した表現を削除した文字列を生成します。
        /// </summary>
        /// <param name="self">処理先の文字列。</param>
        /// <param name="remove">削除する文字列。</param>
        /// <returns>
        /// <paramref name="self"/> から <paramref name="remove"/> を削除した新しい文字列のインスタンスを返します。
        /// </returns>
        public static string RemoveString(this string self, params string[] remove)
        {
            string result = self;
            foreach (var value in remove)
            {
                result = result.Replace(value, String.Empty);
            }
            return result;
        }

        /// <summary>
        /// 指定した表現で囲われた文字列を生成します。
        /// </summary>
        /// <param name="self">囲われる文字列。</param>
        /// <param name="enclosure">囲い文字列。</param>
        /// <returns>
        /// <paramref name="self"/> の両端に <paramref name="enclosure"/> を付与した新しい文字列のインスタンスを返します。
        /// </returns>
        public static string Enclose(this string self, string enclosure)
        {
            return enclosure + self + enclosure;
        }
    }
}
