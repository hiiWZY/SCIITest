using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Text.RegularExpressions;

namespace SCII_Test
{
    public partial class SCII : Form
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        SaveFileDialog savefileDialog = new SaveFileDialog();
        
        public SCII()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileDialog.DefaultExt = ".txt";
            try
            {
                if ((fileDialog.ShowDialog() == DialogResult.OK))
                {
                    String SpaceMark = Interaction.InputBox("请输入间隔符(Tab分隔符请直接键入TAB)", "间隔符", "", 100, 100);
                    if (SpaceMark.ToUpper()=="TAB" | SpaceMark=="")
                    {
                        SpaceMark="\t";
                    }
                    StreamReader sr = new StreamReader(fileDialog.FileName, Encoding.Default);
                    String line;
                    dataGridView1.Columns.Clear();
                    string[] strLine;
                    while ((line = sr.ReadLine()) != null)
                    {
                        strLine = Regex.Split(line, SpaceMark);
                        //add columns
                        if (dataGridView1.Columns.Count<strLine.Length)
                        {
                            int j = dataGridView1.Columns.Count;
                            for (int i = j; i < strLine.Length- j; i++)
                            {
                                dataGridView1.Columns.Add("C" + i.ToString(), "未命名");
                            }
                        }
                        //insert data
                        dataGridView1.Rows.Insert(0, 1);
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            dataGridView1[i, 0].Value = strLine[i];
                        }
                    }
                    MessageBox.Show("导入成功！");
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            savefileDialog.DefaultExt = ".txt";
            try
            {
                String SpaceMark = Interaction.InputBox("请输入间隔符(Tab分隔符请直接键入TAB),为空则默认tab", "间隔符", "", 100, 100);
                if (SpaceMark.ToUpper() == "TAB" | SpaceMark.ToUpper() == "")
                {
                    SpaceMark = "\t";
                }
                string line = "";
                if (savefileDialog.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(savefileDialog.FileName, true, Encoding.Default);

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        for (int i2 = 0; i2 < dataGridView1.Columns.Count; i2++)
                        {
                            line = line + SpaceMark + dataGridView1[i2, i].Value.ToString();
                        }
                        line = line.Remove(0, SpaceMark.Length);
                        sw.WriteLine(line);
                        line = "";
                    }
                    sw.Close();
                    MessageBox.Show("导出成功！");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
