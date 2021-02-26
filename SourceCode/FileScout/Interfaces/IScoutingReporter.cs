namespace FileScout.Interfaces
{
    /// <summary>
    /// 偵察結果の出力を表します。
    /// </summary>
    public interface IScoutingReporter
    {
        /// <summary>
        /// 偵察結果を出力します。
        /// </summary>
        /// <param name="result">偵察結果。</param>
        void Report(IScoutingResult result);
    }
}
