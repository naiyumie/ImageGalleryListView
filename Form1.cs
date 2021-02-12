using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;



namespace ImageGalleryListView
{
    public partial class Form1 : Form
    {
        /* variables 초기로딩 */
        System.Windows.Forms.ImageList myImageListLarge = new ImageList();
        int lstvf_add_count = 0;
        XmlNodeList xmlList;
        String AppConfig = @"config";
        String DriveName;
        String EmulatorRoot;
        String SnapShotFolderName;
        String AssetsFolderName;
        String ArcadeXml;
        String ShortCutSheet;



        public Form1()
        {
           
            InitializeComponent();

            // ini
            Console.WriteLine(Cfg.ReadConfig(AppConfig, "setup", "DriveName"));
            Console.WriteLine(Cfg.ReadConfig(AppConfig, "setup", "EmulatorRoot"));
            Console.WriteLine(Cfg.ReadConfig(AppConfig, "setup", "SnapShotFolderName"));
            Console.WriteLine(Cfg.ReadConfig(AppConfig, "setup", "AssetsFolderName"));
            Console.WriteLine(Cfg.ReadConfig(AppConfig, "setup", "ArcadeXml"));
            Console.WriteLine(Cfg.ReadConfig(AppConfig, "setup", "ShortCutSheet"));


            /* variables 할당 */
            AppConfig = @"config";
            DriveName = Cfg.ReadConfig(AppConfig, "setup", "DriveName");
            EmulatorRoot = Cfg.ReadConfig(AppConfig, "setup", "EmulatorRoot");
            SnapShotFolderName = Application.StartupPath + @"\\" + Cfg.ReadConfig(AppConfig, "setup", "SnapShotFolderName");
            AssetsFolderName = Application.StartupPath + @"\\" + Cfg.ReadConfig(AppConfig, "setup", "AssetsFolderName");
            ArcadeXml = Cfg.ReadConfig(AppConfig, "setup", "ArcadeXml");
            ShortCutSheet = Cfg.ReadConfig(AppConfig, "setup", "ShortCutSheet");




            /* 이미지 리스트 세팅 */
            listViewFile.Items.Clear();
            listViewFile.LargeImageList = myImageListLarge;
            myImageListLarge.ImageSize = new Size(255, 149);
            listViewFile.View = View.LargeIcon;

            /* XML 로드 */
            string xmlprt = ""; 
            XmlDocument xml = new XmlDocument(); 
            xml.Load(AssetsFolderName + ArcadeXml);
            xmlList = xml.SelectNodes("/arcade/games/game");

            foreach (XmlNode xnl in xmlList) {
                xmlprt += xnl["name"].InnerText;
                xmlprt += xnl["snap"].InnerText;
                xmlprt += xnl["file"].InnerText;
                xmlprt += xnl["emul"].InnerText;
                xmlprt += xnl["roms"].InnerText;
                xmlprt += xnl["exec"].InnerText;
                xmlprt += xnl["option"].InnerText;

                String fullpathfile = SnapShotFolderName + @"\\" + xnl["snap"].InnerText;
                FileInfo fileinfo = new FileInfo(fullpathfile);
                //Console.WriteLine(fileinfo);

                /* 이미지 할당 및 텍스트 실행경로등 할당 */
                using (FileStream stream = new FileStream(fullpathfile, FileMode.Open, FileAccess.Read))
                {
                    myImageListLarge.Images.Add(Image.FromStream(stream));
                    listViewFile.Items.Add(new ListViewItem
                    {
                        ImageIndex = lstvf_add_count,
                        Text = xnl["name"].InnerText + Environment.NewLine
                             + xnl["file"].InnerText
                    });
                }
                lstvf_add_count++;
            }


            /* 단축키 이미지 */ 
            pictureBox1.Image = Bitmap.FromFile(AssetsFolderName + ShortCutSheet);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void GameRun(object sender)
        {
            /* 선택된 아이템의 경로를 얻는다 */
            ListView.SelectedListViewItemCollection items = listViewFile.SelectedItems;
            ListViewItem lvItem = items[0];
            //MessageBox.Show(lvItem.SubItems[0].Text);
            String FullRunPath = EmulatorRoot + xmlList[lvItem.Index]["emul"].InnerText + @"\\" + xmlList[lvItem.Index]["roms"].InnerText + @"\\" + xmlList[lvItem.Index]["file"].InnerText;
            //MessageBox.Show(FullRunPath);

            String FileNameOnly = xmlList[lvItem.Index]["file"].InnerText.Replace(".zip", "");


            String runcmd = String.Format(
                "{0}run_arcade.bat {1} {2}{3}\\\\ {4} {5} {6}",
                AssetsFolderName,
                DriveName,
                EmulatorRoot,
                xmlList[lvItem.Index]["emul"].InnerText,
                xmlList[lvItem.Index]["exec"].InnerText,
                FileNameOnly,
                xmlList[lvItem.Index]["option"].InnerText
                );

            //MessageBox.Show(runcmd);
            Console.WriteLine(runcmd);

            /* CMD 실행 */
            System.Diagnostics.ProcessStartInfo pri = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process pro = new System.Diagnostics.Process();

            pri.FileName = "cmd.exe";
            pri.CreateNoWindow = true;
            pri.UseShellExecute = false;

            pri.RedirectStandardInput = true;
            pri.RedirectStandardOutput = true;
            pri.RedirectStandardError = true;

            pro.StartInfo = pri;
            pro.Start();

            pro.StandardInput.Write(runcmd + Environment.NewLine);
            pro.StandardInput.Close();
            string resultValue = pro.StandardOutput.ReadToEnd();
            pro.WaitForExit();
            pro.Close();

            //MessageBox.Show(resultValue);
            Console.WriteLine(resultValue);
        }

        private void listViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GameRun(sender);

        }

    }
}
