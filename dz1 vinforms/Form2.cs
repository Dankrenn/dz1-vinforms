using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dz1_vinforms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Имя", typeof(string));
            dataTable.Columns.Add("Возраст", typeof(int));
            dataTable.Rows.Add("Алексей", 25);
            dataTable.Rows.Add("Елена", 30);
            dataTable.Rows.Add("Иван", 22);

            dataGridView1.DataSource = dataTable;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExportToTextFile();
        }

        private void ExportToTextFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            string line = "";
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                // Проверяем, не является ли значение ячейки null
                                if (cell.Value != null)
                                {
                                    line += cell.Value.ToString() + "\t"; // табуляция между значениями
                                }
                                else
                                {
                                    // Если значение ячейки null, добавляем пустую строку с табуляцией
                                    line += "\t";
                                }
                            }
                            sw.WriteLine(line.TrimEnd('\t'));
                        }
                   
                }

                MessageBox.Show("Данные успешно выгружены в текстовый файл.", "Выгрузка завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExportToCsvFile();
        }

        private void ExportToCsvFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var values = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value != null ? cell.Value.ToString() : "");
                        string line = string.Join(",", values);
                        sw.WriteLine(line);
                    }
                }

                MessageBox.Show("Данные успешно выгружены в CSV файл.", "Выгрузка завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
