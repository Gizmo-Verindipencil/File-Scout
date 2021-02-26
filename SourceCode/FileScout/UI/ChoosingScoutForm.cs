using FileScout.Interfaces;
using FileScout.ScoutingReporters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace FileScout.UI
{
    /// <summary>
    /// 調査選択の画面を提供します。
    /// </summary>
    public partial class ChoosingScoutForm : Form
    {
        // 調査
        private readonly Dictionary<string, IScout> _scout =
            Assembly.GetExecutingAssembly().GetTypes()
            .Where(x =>
                string.Equals(x.Namespace, "FileScout.Scouts", StringComparison.Ordinal) &&
                x.GetInterface("IScout") == typeof(IScout) &&
                !x.IsAbstract
            )
            .ToDictionary(x => x.Name, x => (IScout)Activator.CreateInstance(x));

        /// <summary>
        /// 利用可能な調査を取得または設定します。
        /// </summary>
        private Dictionary<string, IScout> Scout => _scout;

        /// <summary>
        /// <see cref="ChoosingScoutForm"/> クラスの新しいインスタンスを作成します。
        /// </summary>
        public ChoosingScoutForm()
        {
            InitializeComponent();

            var source = new BindingSource() { DataSource = _scout.Keys.Select(x => new { Name = x }) };
            ScoutDataGridView.DataSource = source;
        }

        /// <summary>
        /// 調査の選択が変更された際に、調査項目の一覧を更新します。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベント引数。</param>
        private void ScoutDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (_scout.Count() == 0 ||
                ScoutDataGridView.SelectedRows.Count == 0
                )
            {
                ExecuteButton.Enabled = false;
                return;
            }
            ExecuteButton.Enabled = true;

            dynamic item = new { Name = "" };
            item = ScoutDataGridView.SelectedRows[0].DataBoundItem;

            var scoutingMethod = (Dictionary<string, IScoutingMethod>)Scout[item.Name].ScoutingMethod;
            NumberOfMethodsLabel.Text = scoutingMethod.Count.ToString("(0)");
            var source = new BindingSource() { DataSource = scoutingMethod.Keys.Select(x => new { Name = x }) };
            MethodDataGridView.DataSource = source;
        }

        /// <summary>
        /// フォルダの選択ボタンが押下された場合の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベント引数。</param>
        private void SelectButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog()
            {
                CheckFileExists = false,
                FileName = "Folder Selection",
                Filter = "Folder|."
            }) {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                TargetDirectoryTextBox.Text = Path.GetDirectoryName(dialog.FileName);
            }
        }

        /// <summary>
        /// 調査の実行ボタンが押下された場合の処理を行います。
        /// </summary>
        /// <param name="sender">イベント発生元。</param>
        /// <param name="e">イベント引数。</param>
        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            dynamic obj = ScoutDataGridView.SelectedRows[0].DataBoundItem;
            var scoutingResult = Scout[obj.Name].Scout(TargetDirectoryTextBox.Text);
            var reporter = new CSVScoutingReporter();
            IReportingResult reportingResult = reporter.Report(scoutingResult);

            MessageBox.Show($"結果を保存しました。\r\n保存先：{reportingResult.OutputLocation}");
        }
    }
}