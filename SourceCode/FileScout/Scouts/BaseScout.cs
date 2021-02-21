using FileScout.DataObjects;
using FileScout.EncodingDetectors;
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
    public abstract class BaseScout : IScout
    {
        /// <summary>
        /// 調査手段。
        /// </summary>
        public Dictionary<string, IScoutingMethod> ScoutingMethod { get; set; } = new Dictionary<string, IScoutingMethod>();

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public BaseScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "ディレクトリ", value: new ParentDirectoryScoutingMethod());
            ScoutingMethod.Add(key: "ファイル", value: new NameScoutingMethod());
            ScoutingMethod.Add(key: "拡張子", value: new ExtensionScoutingMethod());
        }

        /// <inheritdoc/>
        public string Scout(string path)
        {
            // ファイル毎の調査
            var report = new ScoutingResult()
            {
                Columns = ScoutingMethod.Keys.ToList<string>()
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
        private void AppendToReport(string path, IScoutingResult report)
        {
            // ファイルの調査
            if (File.Exists(path))
            {
                if (path == Assembly.GetEntryAssembly().Location)
                {
                    return;
                }

                var clue = ScoutingClue.Generate(path, new ReadJEncEncodingDetector());
                var results = new List<string>();

                foreach (var header in report.Columns)
                {
                    var result = ScoutingMethod[header].Do(clue);
                    results.Add(result);
                }
                report.Values.Add(results);
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