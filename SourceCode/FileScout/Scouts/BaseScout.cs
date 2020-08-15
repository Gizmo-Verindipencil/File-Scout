using FileScout.Interfaces;
using FileScout.ScoutsingMethods;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileScout.Scouts
{
    /// <summary>
    /// 基本的な斥候。
    /// </summary>
    public abstract class BaseScout
    {
        /// <summary>
        /// 調査手段。
        /// </summary>
        protected Dictionary<string, IScoutingMethod> Methods = new Dictionary<string, IScoutingMethod>();

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public BaseScout()
        {
            /// 調査項目の定義
            Methods.Add(key: "ディレクトリ", value: new ParentDirectoryScoutingMethod());
            Methods.Add(key: "ファイル",     value: new NameScoutingMethod());
            Methods.Add(key: "拡張子",       value: new ExtensionScoutingMethod());
        }

        /// <summary>
        /// ディレクトリを偵察する。
        /// </summary>
        /// <param name="path">ディレクトリのパス。</param>
        /// <returns>偵察結果。</returns>
        public string Scout(string path)
        {
            // ファイル毎の調査
            var report = new Report() {
                Columns = Methods.Keys.ToList<string>()
            };
            AppendToReport(path, report);

            // 偵察結果を通知
            return report.ToString();
        }

        /// <summary>
        /// ファイルまたはディレクトリを偵察する。
        /// </summary>
        /// <param name="path">ファイルまたはディレクトリのパス。</param>
        /// <param name="report">偵察結果。</param>
        private void AppendToReport(string path, Report report)
        {
            // ファイルの調査
            if (File.Exists(path))
            {
                if (path == Assembly.GetEntryAssembly().Location)
                {
                    return;
                }

                var clue = Clue.Generate(path);
                var results = new List<string>();

                foreach (var column in report.Columns)
                {
                    var result = Methods[column].Do(clue);
                    results.Add(result);
                }
                report.Rows.Add(results);
                return;
            }

            // ディレクトリ内のファイルを偵察
            foreach (var file in Directory.GetFiles(path))
            {
                AppendToReport(file, report);
            }

            // ディレクトリ内のディレクトリの偵察
            foreach (var dir in Directory.GetDirectories(path))
            {
                AppendToReport(dir, report);
            }
        }
    }
}
