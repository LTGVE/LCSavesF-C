using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using static LCSaveFC.LanguageTexts;

namespace LCSaveFC
{
    public partial class Form1 : Form
    {
        public static string SavePath = $"C:/Users/{Environment.UserName}/AppData/LocalLow/ZeekerssRBLX/Lethal Company";
        public List<string> saves=new List<string>();
        public Form1()
        {
            LanguageTexts.init();
            InitializeComponent();
            refreshFile();
            this.DragEnter += new DragEventHandler(Form_DragEnter);
            this.DragDrop += new DragEventHandler(FileDrag);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Saves.SelectedItem == null)
            {
                MessageBox.Show(get("nc"), "TIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedFile = SavePath + $"/{Saves.SelectedItem}";
            Clipboard.SetFileDropList(new StringCollection() { selectedFile });
            MessageBox.Show(get("cpC"), "TIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
            public void FileDrag(Object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length>1) {
                MessageBox.Show(get("noM"),"Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string file = files[0];
            if (Saves.SelectedItem==null) {
                var addfile = SavePath+"/LCSaveFile"+(Saves.Items.Count+1);
                MessageBox.Show(get("addC"), "TIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.WriteAllBytes(addfile, File.ReadAllBytes(file));
                refreshFile();
                return;
            }
            var selectedFile = SavePath + $"/{Saves.SelectedItem}";
            File.Delete(selectedFile);
            File.WriteAllBytes(selectedFile, File.ReadAllBytes(file));
            MessageBox.Show(get("rpC"),"TIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshFile();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Saves.SelectedItem == null)
            {
                MessageBox.Show(get("nc"), "TIP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var selectedFile = SavePath + $"/{Saves.SelectedItem}";
            File.Delete(selectedFile);
            MessageBox.Show(get("delc"), "TIP", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        static bool isShowLanguageWarning = false;
        private void button3_Click(object sender, EventArgs e)
        {
            LanguageTexts.isChinese=!LanguageTexts.isChinese;
            button1.Text = get("copy");
            button2.Text = get("del");
            button3.Text = get("sw");
            if (!LanguageTexts.isChinese&&!isShowLanguageWarning) {
                MessageBox.Show("Using machine translation for English may not be accurate.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isShowLanguageWarning = true;
            }
            label2.Text = get("tip");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refreshFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
