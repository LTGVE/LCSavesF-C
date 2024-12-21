using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCSaveFC
{
    internal class LanguageTexts
    {
        public static bool isChinese = true;
        public static Dictionary<string,string> chinese=new Dictionary<string,string>();
        public static Dictionary<string,string> english=new Dictionary<string,string>();
        public static void init() {
            {
                chinese.Add("copy","复制");
                chinese.Add("del","删除");
                chinese.Add("nc","未选择存档");
                chinese.Add("cpC","已复制到粘贴板");
                chinese.Add("addC","已添加存档");
                chinese.Add("delc","已删除");
                chinese.Add("rpC","已替换存档");
                chinese.Add("noM","请不要拖动多个文件(懒得写)");
                chinese.Add("sw","Switch to English");
                chinese.Add("tip", "把存档文件拖进来可以添加/替换存档");
            }
            {
                english.Add("copy", "Copy");
                english.Add("del", "Delete");
                english.Add("nc", "You haven't selected any archive file");
                english.Add("cpC", "Copied to clipboard");
                english.Add("addC", "Added successfully");
                english.Add("delc", "Delected successfully");
                english.Add("rpC", "Replaced successfully");
                english.Add("noM", "Please don't drag multiple files(i can't be bothered to write this code)");
                english.Add("sw", "让我们说中文");
                english.Add("tip", "Drag the archive file in to add/replace the archive file");
                
            }
        }
        public static string get(string key)
        {
            if (isChinese)
            {
                return chinese[key];
            }
            else
            {
                return english[key];
            }
        }    
    }
}
