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
using static System.Net.Mime.MediaTypeNames;
using ScintillaNET;

namespace dz1_vinforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ofd.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            comboBox1.Items.AddRange(["Обычный","С#"]);
            scintilla = new ScintillaNET.Scintilla();
            scintilla.Dock = DockStyle.Fill;
            Controls.Add(scintilla);

            // Настройте ScintillaNET для подсветки синтаксиса C#
            scintilla.Lexer = Lexer.Cpp;
        }

        OpenFileDialog ofd = new OpenFileDialog();
        SaveFileDialog open = new SaveFileDialog();
        private ScintillaNET.Scintilla scintilla;


        private void button1_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = ofd.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);
            textBox1.Text = fileText;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font, FontStyle.Underline);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            open.ShowDialog();
            // присваниваем строке путь из открытого нами окна
            string path = open.FileName;

            try
            {
                // создаем файл используя конструкцию using
                using (FileStream fs = File.Create(path))
                {

                    byte[] info = new UTF8Encoding(true).GetBytes(textBox1.Text);
                    // производим запись байтов в файл
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
           textBox1.Font = new Font(textBox1.Font, FontStyle.Bold);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "C#")
            {
                // Включите подсветку синтаксиса C#
                scintilla.Lexer = Lexer.Cpp;
            }
            else
            {
                // Выключите подсветку для других языков (по умолчанию)
                scintilla.Lexer = Lexer.Null;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
           new Form2().Show();
        }
    }
}
