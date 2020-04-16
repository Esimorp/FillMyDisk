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

namespace FillMyDisk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.Filter = "所有文件|*.*";
            saveFile1.FilterIndex = 1;

            Random random = new Random();

            long num = Convert.ToInt64(1024 * 1024 * 1024 + random.Next(1024));
            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                FileStream fs = new FileStream(saveFile1.FileName, FileMode.Create, FileAccess.Write, FileShare.Write);
                try
                {
                    if (fs.Length < num)
                    {
                        long byteSize = num - fs.Length;
                        byte[] byteArray = new byte[byteSize];
                        fs.Write(byteArray, 0, byteArray.Length);
                    }
                }
                catch
                {
                    MessageBox.Show("生成失败");
                }
                finally
                {
                    fs.Close();
                }
                MessageBox.Show("生成成功");
            }
        }

        long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "所有文件|*.*";
            openFileDialog.FilterIndex = 1;

            Random random = new Random();


            if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName.Length > 0)
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                try
                {
                    var times = random.Next(1024);
                    byte[] buffer = new byte[] { 0 };
                    for (var i = 0; i < times; i++)
                    {
                        fs.Seek(LongRandom(0, fs.Length, random), SeekOrigin.Begin);
                        random.NextBytes(buffer);
                        fs.WriteByte(buffer[0]);
                    }

                }
                catch
                {
                    MessageBox.Show("修改失败");
                }
                finally
                {
                    fs.Close();
                }
                MessageBox.Show("修改成功");
            }
        }
    }
}
