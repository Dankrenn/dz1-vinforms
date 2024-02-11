using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dz1_vinforms
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Открываем диалог сохранения файла PDF
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.Title = "Save PDF File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Создаем файл PDF и записываем в него содержимое из TextBox
                using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(textBox1.Text);
                    }
                }

                // Печать PDF через принтер
                PrintDocument printDoc = new PrintDocument();
                printDoc.DocumentName = saveFileDialog.FileName;
                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
                printDoc.Print();
            }
        }

        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Открываем файл PDF и печатаем его содержимое
            string filePath = ((PrintDocument)sender).DocumentName;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string text = sr.ReadToEnd();
                    e.Graphics.DrawString(text, textBox1.Font, Brushes.Black, e.MarginBounds);
                }
            }
        }
    }

}
