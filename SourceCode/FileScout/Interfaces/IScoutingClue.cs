using System.Text;

namespace FileScout.Interfaces
{
    /// <summary>
    /// ファイル調査の入力内容を表します。
    /// </summary>
    public interface IScoutingClue
    {
        /// <summary>
        /// ファイルのパスを取得します。
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// ファイルの文字エンコードを取得します。
        /// </summary>
        Encoding Encoding { get; }

        /// <summary>
        /// バイナリファイルの該当フラグを取得します。
        /// </summary>
        bool IsBinary { get; }
    }
}
