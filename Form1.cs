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
        String DriveName = @"E:";
        String BasicPath = @"E:\\_Emul_\\";
        String SnapShotFolderName;
        String AssetsFolderName;

        public Form1()
        {
           
            InitializeComponent();

            /* variables 할당 */
            SnapShotFolderName = BasicPath + @"__super_sonico_snap\\";
            AssetsFolderName = BasicPath + @"__super_sonico_assets\\";

            /* 이미지 리스트 세팅 */
            listViewFile.Items.Clear();
            listViewFile.LargeImageList = myImageListLarge;
            myImageListLarge.ImageSize = new Size(255, 149);
            listViewFile.View = View.LargeIcon;

            /* XML 로드 */
            string xmlprt = ""; 
            XmlDocument xml = new XmlDocument(); 
            xml.Load(AssetsFolderName + @"arcade.xml");
            xmlList = xml.SelectNodes("/games/game");

            foreach (XmlNode xnl in xmlList) {
                xmlprt += xnl["name"].InnerText;
                xmlprt += xnl["snap"].InnerText;
                xmlprt += xnl["file"].InnerText;
                xmlprt += xnl["emul"].InnerText;
                xmlprt += xnl["roms"].InnerText;
                xmlprt += xnl["exec"].InnerText;

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
            pictureBox1.Image = Bitmap.FromFile(AssetsFolderName + @"shortcut.png");
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }


        private void listViewFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /* 선택된 아이템의 경로를 얻는다 */
            ListView.SelectedListViewItemCollection items = listViewFile.SelectedItems;
            ListViewItem lvItem = items[0];
            //MessageBox.Show(lvItem.SubItems[0].Text);
            String FullRunPath = BasicPath + xmlList[lvItem.Index]["emul"].InnerText + @"\\" + xmlList[lvItem.Index]["roms"].InnerText + @"\\" + xmlList[lvItem.Index]["file"].InnerText;
            //MessageBox.Show(FullRunPath);

            String FileNameOnly = xmlList[lvItem.Index]["file"].InnerText.Replace(".zip","");

            String runcmd = AssetsFolderName + @"run_arcade.bat " + DriveName + 
                @" " + BasicPath + xmlList[lvItem.Index]["emul"].InnerText + @"/ " + xmlList[lvItem.Index]["exec"].InnerText + 
                @" " + FileNameOnly;

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
            //Console.WriteLine(resultValue);

        }

    }
}
