using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 考试系统
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 文件 - 退出(X)：退出整个程序。
        /// </summary>
        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 题型选择 - 初级：打开试题界面（Form1）。
        /// 先收起欢迎界面，交卷关闭试题界面后自动回到欢迎界面。
        /// </summary>
        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            using (Form1 exam = new Form1())
            {
                exam.ShowDialog();
            }
            Show();
        }
    }
}
