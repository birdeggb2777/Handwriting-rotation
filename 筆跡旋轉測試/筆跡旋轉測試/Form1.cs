using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace 筆跡旋轉測試
{
    public partial class Form1 : Form
    {
        Image image0;
        int X = 0;
        int Y = 0;
        int x2 = 0;
        int y2 = 0;
        bool pencheck = false;
        float[] drawx=new float[1000000];
        float[] drawy = new float[1000000];
        float[] drawx2 = new float[1000000];
        float[] drawy2 = new float[1000000];
        float[] drawx03 = new float[1000];
        float[] drawy03 = new float[1000];
        int drawnumber=0;
        public Form1()
        {
            InitializeComponent();
        }
        private void picturebox1_Mousedown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            X = e.X;
            Y = e.Y;
            Random ran = new Random();          
            pencheck = true;
            drawx[drawnumber] = (X);
            drawy[drawnumber] = (Y);
            drawnumber += 1;
            drawx[drawnumber] = (X);
            drawy[drawnumber] = (Y);
        }
        private void picturebox1_Mousemove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (pencheck == true)
            {               
                x2 = e.X;
                y2 = e.Y;
                drawx2[drawnumber] = (x2);
                drawy2[drawnumber] = (y2);
                try
                {
                    Image pic1s = image0;
                    Bitmap drawImage = new Bitmap(pic1s, pic1s.Width, pic1s.Height);
                    Graphics g = Graphics.FromImage(drawImage);
                    Pen myPen = new Pen(Color.FromArgb(255, 0, 0, 0), 10);
                    myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    g.DrawLine(myPen, X, Y, x2, y2);
                    g.Dispose();
                    pictureBox1.Image = drawImage;
                }
                catch
                {
                    MessageBox.Show("1");
                }
                try
                {
                   float xxx, yyy, xxx2, yyy2;
                     float x41, x42, y41, y42;
                    /*  for (int i = 1; i < drawnumber; i++)
                      {
                          x41 = (drawx[i] + drawx2[i]) / 2;
                          y41 = (drawy[i] + drawy2[i]) / 2;
                          // xxx = (float)(Math.Cos(drawx[i])- (float)(Math.Sin(drawy[i])));
                          // yyy = (float)(Math.Sin(drawx[i])+ (float)(Math.Cos(drawy[i])));
                          // xxx2 = (float)(Math.Cos(drawx2[i]) - (float)(Math.Sin(drawy2[i])));
                          // yyy2 = (float)(Math.Sin(drawx2[i]) + (float)(Math.Cos(drawy2[i])));
                          xxx = drawx[i]*(float)(Math.Cos(60) - drawy[i]*(float)(Math.Sin(60)));
                          yyy = drawx[i] * (float)(Math.Sin(60) + drawy[i] * (float)(Math.Cos(60)));
                          xxx2 = drawx2[i] * (float)(Math.Cos(60) - drawy2[i] * (float)(Math.Sin(60)));
                          yyy2 = drawx2[i] * (float)(Math.Sin(60) + drawy2[i] * (float)(Math.Cos(60)));
                          Image pic1s = pictureBox2.Image;
                      Bitmap drawImage = new Bitmap(pic1s, pic1s.Width, pic1s.Height);
                      Graphics g = Graphics.FromImage(drawImage);
                      Pen myPen = new Pen(Color.FromArgb(255, 0, 0, 0), 10);
                      myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                      g.DrawLine(myPen, xxx+x41, yyy + y41, xxx2 + x41, yyy2+y41);
                          MessageBox.Show(Convert .ToString( xxx));
                      g.Dispose();
                      pictureBox2.Image = drawImage;
                      }*/
                      if (drawnumber >=510) { 
                          Image pic1s = image0;
                        Bitmap drawImage = new Bitmap(pic1s, pic1s.Width, pic1s.Height);
                        Graphics g = Graphics.FromImage(drawImage);
                       
                        for (int i = drawnumber-505; i < drawnumber; i+=3)
                    {
                       Pen myPen = new Pen(Color.FromArgb(127, (drawnumber-i)/2, 0, 0), 10);
                        myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        
                       
                        g.DrawLine(myPen, drawx[i], drawy[i], drawx2[drawnumber-i], drawy2[drawnumber-i]);
                     
                        
                        pictureBox2.Image = drawImage;

                    }
                         g.Dispose();
                    }


                }



                catch
                {
                    MessageBox.Show("2");
                }
                X = e.X;
                Y = e.Y;
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                drawnumber += 1;
                 drawx[drawnumber] = (X);
                 drawy[drawnumber] = (Y);
            }
            GC.Collect();
        }
        private void picturebox1_Mouseup(object sender, System.Windows.Forms.MouseEventArgs e)
        {
         //   MessageBox.Show(Convert.ToString(drawnumber));
            pencheck = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            Bitmap resultBitmap = new Bitmap(714, 700);
            pictureBox1.Image = resultBitmap;
            Bitmap MyNewBmp = (Bitmap)pictureBox1.Image;
            byte[] MyInvertColor = new byte[256];
            for (int n = 0; n < 255; n++)
                MyInvertColor[n] = (byte)(255 - n);
            Rectangle MyRec = new Rectangle(0, 0, MyNewBmp.Width, MyNewBmp.Height);
            BitmapData MyBmpData = MyNewBmp.LockBits(MyRec, ImageLockMode.ReadWrite, MyNewBmp.PixelFormat);
            IntPtr MyPtr = MyBmpData.Scan0;
            int MyByteCount = MyBmpData.Stride * MyNewBmp.Height;
            byte[] MyNewColor = new byte[MyByteCount];
            Marshal.Copy(MyPtr, MyNewColor, 0, MyByteCount);
            for (int n = 0; n < MyByteCount; n += 3)
            {
                MyNewColor[n] = MyInvertColor[MyNewColor[n]];
                MyNewColor[n + 1] = MyInvertColor[MyNewColor[n + 1]];
                MyNewColor[n + 2] = MyInvertColor[MyNewColor[n + 2]];
            }
            Marshal.Copy(MyNewColor, 0, MyPtr, MyByteCount);
            MyNewBmp.UnlockBits(MyBmpData);
            pictureBox1.Image = resultBitmap;
            pictureBox2.Image = pictureBox1.Image;
            image0 = pictureBox1.Image;
            Random ran = new Random(Guid.NewGuid().GetHashCode());
            int rancolor;
            for (int n = 0; n < 990; n += 4)
            {
                rancolor = ran.Next(0, 690 + 1);
                drawx03[n] = 0;
                drawy03[n] = rancolor;
            }
            for (int n = 1; n < 990; n += 4)
            {
                rancolor = ran.Next(0, 690 + 1);
                drawx03[n] = 255;
                drawy03[n] = rancolor;
            }
            for (int n = 2; n < 990; n += 4)
            {
                rancolor = ran.Next(0, 690+ 1);
                drawx03[n] = rancolor;
                drawy03[n] = 0;
            }
            for (int n = 3; n < 990; n += 4)
            {
                rancolor = ran.Next(0, 690 + 1);
                drawx03[n] = rancolor;
                drawy03[n] = 255;
            }
        }
    }
}
