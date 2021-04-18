using FileScout.DataObjects;
using FileScout.ScoutingReporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FileScout.UnitTest.Tests.ScoutingReporters
{
    /// <summary>
    /// 調査結果のCSV出力のテストを提供します。
    /// </summary>
    [TestClass]
    public class CSVScoutingReporterUnitTest
    {
        /// <summary>
        /// テスト用ディレクトリのパスを取得します。
        /// </summary>
        private string DirectoryPath
        {
            get
            {
                var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var dir = Path.Combine(root, nameof(CSVScoutingReporterUnitTest));

                return dir;
            }
        }

        /// <summary>
        /// テストで使うリソースを準備します。
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            if (!Directory.Exists(this.DirectoryPath))
            {
                Directory.CreateDirectory(this.DirectoryPath);
            }
        }

        /// <summary>
        /// テストで使ったリソースを開放します。
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            if (Directory.Exists(this.DirectoryPath))
            {
                Directory.Delete(this.DirectoryPath, true);
            }
        }

        /// <summary>
        /// 調査が成功した場合の出力を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Report_WhenScoutingIsSuccessful()
        {
            // テスト前のファイル一覧を取得
            var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filesBeforeReporting = Directory.GetFiles(root);

            // インスタンス生成
            var columns = new string[] { "C1", "C2", "C3" };
            var values = new string[][] {
                new string[] { "V11", "V12", "V13" },
                new string[] { "V21", "V22", "V23" },
                new string[] { "V31", "V32", "V33" },
            };
            var scoutingResult = new ScoutingResult()
            {
                Columns = columns.ToList(),
                Values = values.Select(x => x.ToList()).ToList(),
                ErrorOccurred = false
            };

            // テスト対象の処理を実行
            var reporter = new CSVScoutingReporter();
            var reporterResult = reporter.Report(new ReportingClue()
            {
                ScoutingResult = scoutingResult,
                OutputLocation = Path.Combine(this.DirectoryPath, "unittest.csv")
            });

            // テスト結果を検証
            var expected =
                "\"C1\",\"C2\",\"C3\"\r\n" +
                "\"V11\",\"V12\",\"V13\"\r\n" +
                "\"V21\",\"V22\",\"V23\"\r\n" +
                "\"V31\",\"V32\",\"V33\"\r\n";
            var actual = File.ReadAllText(reporterResult.OutputLocation, Encoding.UTF8);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 調査が失敗した場合の出力を検証します。
        /// </summary>
        [TestMethod]
        public void Test_Report_WhenScoutingIsFailed()
        {
            // インスタンス生成
            var scoutingResult = new ScoutingResult()
            {
                ErrorOccurred = true,
                ErrorMessage = "abc123"
            };

            // テスト対象の処理を実行
            var reporter = new CSVScoutingReporter();
            var reporterResult = reporter.Report(new ReportingClue()
            {
                ScoutingResult = scoutingResult,
                OutputLocation = Path.Combine(this.DirectoryPath, "unittest.csv")
            });

            // テスト結果を検証
            var actual = File.ReadAllText(reporterResult.OutputLocation, Encoding.UTF8);
            Assert.AreEqual("abc123", actual);
        }
    }
}
