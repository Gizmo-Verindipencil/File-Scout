using FileScout.DataObjects;
using FileScout.EncodingDetectors;
using FileScout.Interfaces;
using FileScout.ScoutingMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileScout.Scouts
{
    /// <summary>
    /// ファイルの調査を提供します。
    /// </summary>
    public abstract class BaseScout : IScout
    {
        /// <summary>
        /// 調査手段を取得または設定します。
        /// </summary>
        public Dictionary<string, IScoutingMethod> ScoutingMethod { get; set; } = new Dictionary<string, IScoutingMethod>();

        /// <summary>
        /// <see cref="BaseScout"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public BaseScout()
        {
            /// 調査項目の定義
            ScoutingMethod.Add(key: "ディレクトリ", value: new ParentDirectoryScoutingMethod());
            ScoutingMethod.Add(key: "ファイル", value: new NameScoutingMethod());
            ScoutingMethod.Add(key: "拡張子", value: new ExtensionScoutingMethod());
        }

        /// <inheritdoc/>
        public IScoutingResult Scout(string path)
        {
            var result = new ScoutingResult()
            {
                Columns = ScoutingMethod.Keys.ToList<string>()
            };

            if (!Directory.Exists(path))
            {
                result.ErrorOccured = true;
                result.ErrorMessage = $"対象ディレクトリが存在しません。\n{path}";
                return result;
            }

            try
            {
                AppendToReport(path, result);

                result.ErrorOccured = false;
            }
            catch (PathTooLongException ex)
            {
                result.ErrorOccured = true;
                result.ErrorMessage = ex.Message;
                return result;
            }
            catch (Exception ex)
            {
                result.ErrorOccured = true;
                result.ErrorMessage = $@"{ex.Message}\r\n{ex.StackTrace}";
                return result;
            }

            return result;
        }

        /// <summary>
        /// ファイルまたはディレクトリを調査します。
        /// </summary>
        /// <param name="path">ファイルまたはディレクトリのパス。</param>
        /// <param name="report">調査結果。</param>
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

            // 調査対象パスの取得
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

            // ディレクトリ内のファイルを調査
            foreach (var file in files)
            {
                AppendToReport(file, report);
            }

            // ディレクトリ内のディレクトリの調査
            foreach (var dir in directories)
            {
                AppendToReport(dir, report);
            }
        }
    }
}