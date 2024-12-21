using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;

namespace LethalCompanySavesFinder
{
    public partial class Form1 : Form
    {
        public static string SavePath = $"C:/Users/{Environment.UserName}/AppData/LocalLow/ZeekerssRBLX/Lethal Company";
        public List<string> saves=new List<string>();
        public Form1()
        {
            InitializeComponent();
            refreshFile();
            this.DragEnter += new DragEventHandler(Form_DragEnter);
            this.DragDrop += new DragEventHandler(FileDrag);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Saves.SelectedItem == null)
            {
                MessageBox.Show("请先选择存档", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedFile = SavePath + $"/{Saves.SelectedItem}";
            Clipboard.SetFileDropList(new StringCollection() { selectedFile });
            MessageBox.Show("已复制到剪贴板","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
            public void FileDrag(Object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length>1) {
                MessageBox.Show("请勿拖动多个文件(懒得写多文件拖动)","警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string file = files[0];
            if (Saves.SelectedItem==null) {
                var addfile = SavePath+"/LCSaveFile"+(Saves.Items.Count+1);
                MessageBox.Show("存档已添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.WriteAllBytes(addfile, File.ReadAllBytes(file));
                refreshFile();
                return;
            }
            var selectedFile = SavePath + $"/{Saves.SelectedItem}";
            File.Delete(selectedFile);
            File.WriteAllBytes(selectedFile, File.ReadAllBytes(file));
            MessageBox.Show("存档已替换","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshFile();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Saves.SelectedItem == null)
            {
                MessageBox.Show("请先选择存档", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedFile = SavePath + $"/{Saves.SelectedItem}";
            File.Delete(selectedFile);
            MessageBox.Show("存档已删除", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshFile();
        }
        public void refreshFile() {
            saves.Clear();
            Saves.Items.Clear();
            DirectoryInfo folder = new DirectoryInfo(SavePath);
            foreach (FileInfo file in folder.GetFiles("*", SearchOption.TopDirectoryOnly))
            {
                if (file.Extension == "" && file.Name.Contains("SaveFile"))
                {
                    saves.Add(file.Name);
                }
            }
            Saves.Items.AddRange(saves.ToArray());
        }
    }
}
