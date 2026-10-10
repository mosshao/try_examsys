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
    public partial class Form1 : Form
    {
        /// <summary>
        /// 每道题的分值。
        /// </summary>
        private const int ScorePerQuestion = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 「重新作答」按钮：清空全部作答内容。
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAllAnswers();
            lblScore.Text = "得分：--";   // 重新作答时把上一次的得分一起清掉
        }

        /// <summary>
        /// 清空所有题目的作答：单选题和多选题取消勾选，填空题清空文本。
        /// 右上角的「考试时间」文本框不属于作答内容，因此不清空。
        /// </summary>
        private void ClearAllAnswers()
        {
            // 单选题：第 1 题
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            // 单选题：第 2 题
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;

            // 单选题：第 3 题
            radioButton9.Checked = false;
            radioButton10.Checked = false;
            radioButton11.Checked = false;
            radioButton12.Checked = false;

            // 多选题：第 1 题
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;

            // 多选题：第 2 题（groupBox2）
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;

            // 填空题
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        /// <summary>
        /// 「提交」按钮：先检查双选题是否都选了两个选项，再判分并显示得分。
        /// </summary>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // 双选题必须恰好选择两个选项，否则提示后不判分
            if (!CheckMultiChoiceCount())
            {
                return;
            }

            // 逐题判断作答是否正确
            bool q1 = radioButton1.Checked;                                       // 单选 1：答案 A（class）
            bool q2 = radioButton7.Checked;                                       // 单选 2：答案 A（string）
            bool q3 = radioButton10.Checked;                                      // 单选 3：答案 B（const）
            bool q4 = checkBox1.Checked && checkBox3.Checked;                     // 双选 1：答案 AC（public、protected）
            bool q5 = checkBox12.Checked && checkBox10.Checked;                   // 双选 2：答案 AC（方法名相同、参数列表不同）
            bool q6 = (IsSame(textBox2.Text, "int") && IsSame(textBox3.Text, "long"))
                   || (IsSame(textBox2.Text, "long") && IsSame(textBox3.Text, "int"));  // 填空 1：答案 int、long（两个空顺序不限）
            bool q7 = IsSame(textBox5.Text, ":") && IsSame(textBox4.Text, "base");       // 填空 2：答案 :、base

            bool[] results = { q1, q2, q3, q4, q5, q6, q7 };
            string[] names = { "单选题 1", "单选题 2", "单选题 3", "双选题 1", "双选题 2", "填空题 1", "填空题 2" };

            // 统计得分，同时拼出每道题的对错明细
            int score = 0;
            StringBuilder detail = new StringBuilder();
            for (int i = 0; i < results.Length; i++)
            {
                if (results[i])
                {
                    score += ScorePerQuestion;
                }
                detail.AppendLine(names[i] + "：" + (results[i] ? "正确" : "错误"));
            }

            int total = results.Length * ScorePerQuestion;
            lblScore.Text = "得分：" + score + " / " + total;

            MessageBox.Show(
                "每题 " + ScorePerQuestion + " 分，满分 " + total + " 分。\r\n" +
                "你的得分：" + score + " 分\r\n\r\n" +
                detail.ToString(),
                "考试结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 检查两道双选题是否都恰好选择了两个选项，不合格时提示并返回 false。
        /// </summary>
        private bool CheckMultiChoiceCount()
        {
            string tips = "";
            if (CountChecked(checkBox1, checkBox2, checkBox3, checkBox4) != 2)
            {
                tips += "· 双选题 1：以下哪些是C#的访问修饰符？\r\n";
            }
            if (CountChecked(checkBox9, checkBox10, checkBox11, checkBox12) != 2)
            {
                tips += "· 双选题 2：以下哪些是C#中方法重载的条件？\r\n";
            }

            if (tips != "")
            {
                MessageBox.Show("双选题需要选择两个选项，请检查：\r\n\r\n" + tips,
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 统计一组复选框中已经勾选的个数。
        /// </summary>
        private int CountChecked(params CheckBox[] boxes)
        {
            int count = 0;
            foreach (CheckBox box in boxes)
            {
                if (box.Checked)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 比较填空答案是否一致：忽略首尾空白和大小写，全角冒号按半角冒号处理。
        /// </summary>
        private bool IsSame(string input, string answer)
        {
            string text = input.Trim().Replace("：", ":");
            return string.Equals(text, answer, StringComparison.OrdinalIgnoreCase);
        }
    }
}
