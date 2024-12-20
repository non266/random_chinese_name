using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace 随机名字生成器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string[] ReadFileLinesToArray(string filePath)
        {
            string[] lines = new string[0];
            using (StreamReader reader = new StreamReader(filePath))
            {
                lines = reader.ReadToEnd().Replace("\r", "").Split('\n', (char)StringSplitOptions.RemoveEmptyEntries);
            }
            return lines;
        }
        string[] firstNames;
        string[] lastNames;
        void loadname()
        {
            firstNames = new string[] { "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯", "陈", "褚", "卫", "蒋", "沈", "韩", "杨", "朱", "秦", "尤", "许", "何", "吕", "施", "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻", "柏", "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎", "鲁", "韦", "昌", "马", "苗", "凤", "花", "方", "俞", "任", "袁", "柳", "酆", "鲍", "史", "唐", "费", "廉", "岑", "薛", "雷", "贺", "倪", "汤", "滕", "殷", "罗", "毕", "郝", "邬", "安", "常", "乐", "于", "时", "傅", "皮", "卞", "齐", "康", "伍", "余", "元", "卜", "顾", "孟", "平", "黄", "和", "穆", "萧", "尹", "姚", "邵", "湛", "汪", "祁", "毛", "禹", "狄", "米", "贝", "明", "臧", "计", "伏", "成", "戴", "谈", "宋", "茅", "庞", "熊", "纪", "舒", "屈", "项", "祝", "董", "梁", "杜", "阮", "蓝", "闵", "席", "季", "麻", "强", "贾", "路", "娄", "危", "江", "童", "颜", "郭", "梅", "盛", "林", "刁", "钟", "徐", "邱", "骆", "高", "夏", "蔡", "田", "樊", "胡", "凌", "霍", "虞" };
            string characters = "涛昌进林有坚和彪博诚先敬震振壮会群豪心邦承乐绍功松善厚庆磊民友裕河哲江超浩亮政谦亨奇固之轮翰朗伯宏言若鸣朋斌梁栋维启克伦翔旭鹏泽晨辰士以建家致树炎德行时泰盛雄琛钧冠策腾伟刚勇毅俊峰强军平保东文辉力明永健世广志义兴良海山仁波宁贵福生龙元全国胜学祥才发成康星光天达安岩中茂武新利清飞彬富顺信子杰楠榕风航弘嘉琼桂娣叶璧璐娅琦晶妍茜秋珊莎锦黛青倩婷姣婉娴瑾颖露瑶怡婵雁蓓纨仪荷丹蓉眉君琴蕊薇菁梦岚苑婕馨瑗琰韵融园艺咏卿聪澜纯毓悦昭冰爽琬茗羽希宁欣飘育滢馥筠柔竹霭凝晓欢霄枫芸菲寒伊亚宜可姬舒影荔枝思丽秀娟英华慧巧美娜静淑惠珠翠雅芝玉萍红娥玲芬芳燕彩春菊勤珍贞莉兰凤洁梅琳素云莲真环雪荣爱妹霞香月莺媛艳瑞凡佳";
            lastNames = characters.ToCharArray().Select(c => c.ToString()).ToArray();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                if (File.Exists("姓.txt") && File.Exists("名.txt"))
                {
                    firstNames = ReadFileLinesToArray("姓.txt");
                    lastNames = ReadFileLinesToArray("名.txt");
                }
                else
                {
                    MessageBox.Show("没有找到txt文件,使用默认生成","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                loadname();
            }
            Random random = new Random();
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                if (random.Next(0,2) == 0)
                {
                    listBox1.Items.Add(firstNames[random.Next(0, firstNames.Length)] + lastNames[random.Next(0, lastNames.Length)]);
                }
                else
                {
                    listBox1.Items.Add(firstNames[random.Next(0, firstNames.Length)] + lastNames[random.Next(0, lastNames.Length)] + lastNames[random.Next(0, lastNames.Length)]);
                }
            }
            contextMenuStrip1.Enabled = true;
            int count = listBox1.Items.Count;
            listBox1.SelectedIndex = count - 1;
            label4.Text = count.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            contextMenuStrip1.Enabled = false;
            numericUpDown1.Minimum = 1;
            numericUpDown1.Value = 10;
            loadname();
        }

        private void 复制选中的名字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             Clipboard.SetText(listBox1.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件|*.txt";
            saveFileDialog1.FileName = "随机名字.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    sw.WriteLine(listBox1.Items[i].ToString());
                }
                sw.Flush();
                sw.Close();
            }
        }
        void out_default_name()
        {
            try
            {
                StreamWriter sw = new StreamWriter("姓.txt");
                for (int i = 0; i < firstNames.Length; i++)
                {
                    sw.Write(firstNames[i].ToString() + "\n");
                }
                sw.Flush();
                sw.Close();
                StreamWriter sw1 = new StreamWriter("名.txt");
                for (int i = 0; i < lastNames.Length; i++)
                {
                    sw1.Write(lastNames[i].ToString() + "\n");
                }
                sw1.Flush();
                sw1.Close();
                MessageBox.Show("导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists("姓.txt") && File.Exists("名.txt"))
            {
                if (MessageBox.Show("已经存在txt文件,是否覆盖?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    out_default_name();
                }
            }
            else
            {
                out_default_name();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("点击确定会清空列表", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                listBox1.Items.Clear();
                contextMenuStrip1.Enabled = false;
            }
        }
    }
}