using System;

namespace FileScout.Extensions
{
    /// <summary>
    /// <see cref="string"/>の拡張メソッド。
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 指定した文字列が出現する回数をカウントする。
        /// </summary>
        /// <param name="self">調べる文字列。</param>
        /// <param name="find">数える文字列。</param>
        /// <returns>出現回数。</returns>
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
        /// 指定した文字列を削除した新しい文字列のインスタンスを返す。
        /// </summary>
        /// <param name="self">処理先の文字列。</param>
        /// <param name="remove">削除する文字列。</param>
        /// <returns>削除された文字列。</returns>
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
        /// 指定した文字列で囲われた新しい文字列のインスタンスを返す。
        /// </summary>
        /// <param name="self">囲われる文字列。</param>
        /// <param name="enclosure">囲い文字列。</param>
        /// <returns>囲われた文字列。</returns>
        public static string Enclose(this string self, string enclosure)
        {
            return enclosure + self + enclosure;
        }
    }
}
