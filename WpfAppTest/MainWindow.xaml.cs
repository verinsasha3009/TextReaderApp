using IronOcr;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;


//using static System.Net.Mime.MediaTypeNames;

namespace WpfAppTest
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {      
            InitializeComponent();
        }
        [Obsolete]
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {             
                string text = text1.Text;
                var Ocr = new IronTesseract(); // nothing to configure
                Ocr.Language = OcrLanguage.Russian;
                Ocr.Configuration.TesseractVersion = TesseractVersion.Tesseract5;
                using (var Input = new OcrInput())
                {
                    Input.AddImage(text);

                    try
                    {                                      
                       var Result = Ocr.Read(Input).Text;                                           
                        textBox.Text = Result;
                    }
                    catch(Exception ex) { textBox.Text =(ex.Message + "\n для работающей версии нужна лицензия на пакет IronOcr \n (В отладчике VS работает без лицензии :D  ) "); }
                }
            }
            catch( Exception ex) 
            {
                MessageBox.Show("Необходимо вставить путь к файлу(.png,.json)");
            }            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            try
            {
                bool? open2 = open.ShowDialog();
                text1.Text = open.FileName;
                // Create source
                BitmapImage myBitmapImage = new BitmapImage();
                // BitmapImage.UriSource must be in a BeginInit/EndInit block
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(open.FileName);
                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();
                //set image source
                mainImage.Source = myBitmapImage;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }         
        }
        public string Text(string text)
        {
            return  text1.Text = text;
        }       
    }
}
