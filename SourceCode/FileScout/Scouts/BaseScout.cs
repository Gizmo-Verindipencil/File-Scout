using FileScout.Interfaces;
using FileScout.ScoutingMethods;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileScout.Scouts
{
    /// <summary>
    /// 斥候。
    /// </summary>
    public abstract class BaseScout
    {
        /// <summary>
        /// 調査手段。
        /// </summary>
        public Dictionary<string, IScoutingMethod> Methods { get; set; } = new Dictionary<string, IScoutingMethod>();

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public BaseScout()
        {
            /// 調査項目の定義
            Methods.Add(key: "ディレクトリ", value: new ParentDirectoryScoutingMethod());
            Methods.Add(key: "ファイル", value: new NameScoutingMethod());
            Methods.Add(key: "拡張子", value: new ExtensionScoutingMethod());
        }

        /// <summary>
        /// ディレクトリを偵察する。
        /// </summary>
        /// <param name="path">ディレクトリのパス。</param>
        /// <returns>偵察結果。</returns>
        public string Scout(string path)
        {
            // ファイル毎の調査
            var report = new Report()
            {
                Columns = Methods.Keys.ToList<string>()
            };
            AppendToReport(path, report);

            // 報告を通知
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

                foreach (var header in report.Columns)
                {
                    var result = Methods[header].Do(clue);
                    results.Add(result);
                }
                report.Rows.Add(results);
                return;
            }

            // 偵察対象パスの取得
            string[] files;
            string[] directories;
            try
            {
                files = Directory.GetFiles(path);
                directories = Directory.GetDirectories(path);
            }
            catch (PathTooLongException)
            {
                throw new PathTooLongException($"長すぎるパスです。\n{path}");
            }

            // ディレクトリ内のファイルを偵察
            foreach (var file in files)
            {
                AppendToReport(file, report);
            }

            // ディレクトリ内のディレクトリの偵察
            foreach (var dir in directories)
            {
                AppendToReport(dir, report);
            }
        }
    }
}