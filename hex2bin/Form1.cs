using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hex2bin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.Filter = "BIN2HEX1.exe|BIN2HEX1.exe";
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Select BIN2HEX1.exe File";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName.Length > 0)
            {
                textBox1.Text = openFileDialog1.FileName;
                // AvrdudeInfo();  
            }
        }

        private void hex2bin()
        {
            int i;
            string line;

            System.Diagnostics.Process prc = avrdude("-c "); 
            if (prc == null) return;

            //ProgrammerComboBox.Items.Clear();
        }

        private System.Diagnostics.Process avrdude(string arg)
        {

            if (textBox1.Text.Length == 0) return null;

            System.Diagnostics.Process prc;
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.FileName = textBox1.Text;
                psi.RedirectStandardError = true;
                psi.RedirectStandardOutput = true;
                psi.CreateNoWindow = true;         
                psi.UseShellExecute = false;        
                psi.Arguments = arg;              
                prc = System.Diagnostics.Process.Start(psi);
            }
            catch
            {
                prc = null;
            }
            return prc;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    StringBuilder sb = new StringBuilder();
                    System.Diagnostics.Process prc = avrdude(" " + textBox2.Text);

                    while (!prc.StandardOutput.EndOfStream)
                    {
                        int c;
                        c = prc.StandardOutput.Read();


                        sb.Append((char)c);
                    }


                    richTextBox1.Text = sb.ToString();
                }
                else
                {
                    MessageBox.Show("salah satu file .exe dan .bin belum di masukkan");
                }
                }
            catch
            {
                MessageBox.Show("belum ada exe dan bin yang di masukkan");
            };
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            openFileDialog2.CheckFileExists = true;
            openFileDialog2.Filter = "*.bin|*.bin";
            openFileDialog2.FileName = "";
            openFileDialog2.Title = "Select .bin File";
            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileName.Length > 0)
            {
                textBox2.Text = openFileDialog2.FileName;
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @textBox1.Text; //directori awal save data
            saveFileDialog1.Title = "Save text Files";
            //saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "hex"; //simpan format exel
            saveFileDialog1.Filter = "Hex file (*.hex)|*.hex|All files (.hex)|*.hex*"; //selain format .xls jangan beri pilihan
            saveFileDialog1.FilterIndex = 2; //index yang di filter 
            saveFileDialog1.RestoreDirectory = true; //tampilkan dialog save

            if (saveFileDialog1.ShowDialog() == DialogResult.OK) // jendela dialog save di buka =ok
            {

                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText); // simpan ke exel
                MessageBox.Show("Data tersimpan pada Directory: " + saveFileDialog1.FileName); // tempan penyimpanan dan nama file yang di save
            }
        }


    }

}
