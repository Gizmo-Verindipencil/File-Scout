using FileScout.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;

namespace FileScout.ScoutingMethods
{
    /// <summary>
    /// ファイルハッシュ(SHA-256)の偵察手段。
    /// </summary>
    public class HashSHA256ScoutingMethod : IScoutingMethod
    {
        /// <inheritdoc/>
        public string Do(IScoutingClue clue)
        {
            using (var stream = new FileStream(clue.FilePath, FileMode.Open, FileAccess.Read))
            {
                var sha256 = SHA256.Create();
                var hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}
