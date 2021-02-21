using System.Text;

namespace FileScout.Interfaces
{
    /// <summary>
    /// 偵察の手掛かり。
    /// </summary>
    public interface IScoutingClue
    {
        /// <summary>
        /// ファイルのパス。
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// ファイルのエンコード。
        /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// バイナリファイルであるかどうかを示す。
        /// </summary>
        bool IsBinary { get; }
    }
}
