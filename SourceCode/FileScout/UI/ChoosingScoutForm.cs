using FileScout.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace FileScout.UI
{
    /// <summary>
    /// 偵察選択画面。
    /// </summary>
    public partial class ChoosingScoutForm : Form
    {
        private readonly Dictionary<string, IScout> _scout =
            Assembly.GetExecutingAssembly().GetTypes()
            .Where(x =>
                string.Equals(x.Namespace, "FileScout.Scouts", StringComparison.Ordinal) &&
                x.GetInterface("IScout") == typeof(IScout) &&
                !x.IsAbstract
            )
            .ToDictionary(x => x.Name, x => (IScout)Activator.CreateInstance(x));

        internal Dictionary<string, IScout> Scout => _scout;

        public ChoosingScoutForm()
        {
            InitializeComponent();

            var source = new BindingSource() { DataSource = _scout.Keys.Select(x => new { Name = x }) };
            ScoutDataGridView.DataSource = source;
        }

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

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(TargetDirectoryTextBox.Text))
            {
                var message = $"対象ディレクトリが存在しません。\n{TargetDirectoryTextBox.Text}";
                SaveResult(message);
                return;
            }
                
            dynamic obj = ScoutDataGridView.SelectedRows[0].DataBoundItem;

            string result;
            try
            {
                result = Scout[obj.Name].Scout(TargetDirectoryTextBox.Text);
            }
            catch(PathTooLongException ex)
            {
                SaveResult(ex.Message);
                return;
            }
            catch(Exception ex)
            {
                var builder = new StringBuilder();
                builder.AppendLine(ex.Message);
                builder.AppendLine();
                builder.AppendLine(ex.StackTrace);
                SaveResult(builder.ToString());
                return;
            }

            SaveResult(result);
        }

        private void SaveResult(string content)
        {
            var fileName = $"result_{DateTime.Now:yyyyMMddhhmmss}.csv";
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            File.WriteAllText(Path.Combine(path, fileName), content, Encoding.UTF8);
            MessageBox.Show($"{fileName} に結果を作成しました。");
        }
    }
}