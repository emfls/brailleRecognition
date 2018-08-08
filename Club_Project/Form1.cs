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
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.XImgproc;
using System.Collections;
using System.Diagnostics;
using Emgu.CV.Cvb;

namespace Club_Project
{
    public partial class MainForm : Form
    {
        Image<Bgr, Byte> openImg;
        Hashtable hashtable = new Hashtable();
        Hashtable b2t = new Hashtable();


        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            startPanel.Visible = true;
            b2t_Panel.Visible = false;
            t2b_Panel.Visible = false;
            setHashTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e) // 홈 버튼 클릭시
        {
            b2t_Panel.Visible = false;
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void startPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void b2t_Icon_Click(object sender, EventArgs e)
        {
            b2t_Panel.Visible = true;
            t2b_Panel.Visible = false;
        }

        private void t2b_Icon_Click(object sender, EventArgs e)
        {
            b2t_Panel.Visible = true;
            t2b_Panel.Visible = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void b2t_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void b2t_openButton_Click_1(object sender, EventArgs e) //파일 열기 버튼 구현
        {
            string openstrFilename;

            ImageOpen.Title = "점자 파일 열기";
            ImageOpen.Filter = "JPEG File(*.jpg)|*.jpg|Bitmap File(*.bmp)|*.bmp";

            // 다이얼로그의 확인 처리
            if (ImageOpen.ShowDialog() == DialogResult.OK)
            {
                openstrFilename = ImageOpen.FileName;
                openImg = new Image<Bgr, byte>(openstrFilename);
                b2t_openImageBox.Image = openImg;
                //b2t_openImageBox.Visible = true;
            }
        }

        private void b2t_openImageBox_Click(object sender, EventArgs e)
        {

        }

        private void ComputeProjections(Image<Bgr, byte> inputImage)
        {
            Image<Gray, Byte> inputGrayImage = inputImage.Convert<Gray, Byte>();
            Matrix<float> imgMatHorizontal = new Matrix<float>(inputGrayImage.Height, 1, 1);
            Matrix<float> imgMatVertical = new Matrix<float>(1, inputGrayImage.Width, 1);

            inputGrayImage.Reduce<float>(imgMatHorizontal, ReduceDimension.SingleCol, ReduceType.ReduceAvg);
            inputGrayImage.Reduce<float>(imgMatVertical, ReduceDimension.SingleRow, ReduceType.ReduceAvg);
            double minH, maxH, minV, maxV;
            Point minLocH, maxLocH, minLocV, maxLocV;
            imgMatHorizontal.MinMax(out minH, out maxH, out minLocH, out maxLocH);
            imgMatVertical.MinMax(out minV, out maxV, out minLocV, out maxLocV);

            Image<Gray, Byte> maskProvaH = new Image<Gray, byte>(new Size((int)(maxH - minH + 1), imgMatHorizontal.Rows));
            Image<Gray, Byte> maskProvaV = new Image<Gray, byte>(new Size(imgMatVertical.Cols, (int)(maxV - minV + 1)));

            for (int i = 0; i < imgMatHorizontal.Rows; i++)
                maskProvaH.Draw(new CircleF(new PointF((float)(imgMatHorizontal[i, 0] - minH), i), 1f), new Gray(255), 1);

            for (int i = 0; i < imgMatVertical.Cols; i++)
                maskProvaV.Draw(new CircleF(new PointF(i, (float)(imgMatVertical[0, i] - minV)), 1f), new Gray(255), 1);

            inputImage.Draw(new CircleF(new PointF(minLocV.X, minLocH.Y), 2), new Bgr(Color.Green), 1);
            

        }

        private void Proses(Image<Bgr, Byte> image)
        {
            Image<Gray, Byte> gray = image.Convert<Gray, Byte>();
            Image<Gray, float> sobel = new Image<Gray, float>(gray.Size);
            Image<Gray, Byte> thres = new Image<Gray, Byte>(gray.Size);
            //CvInvoke.(gray, gray, Emgu.CV.CvEnum.SMOOTH_TYPE.CV_MEDIAN, 5, 5, 9, 9);
            CvInvoke.Sobel(gray, sobel,DepthType.Default, 1, 0);
            gray = sobel.Convert<Gray, Byte>();
            Matrix<float> imgMatH = new Matrix<float>(gray.Height, 1, 1);
            Matrix<float> imgMatV = new Matrix<float>(1, gray.Width, 1);
            Matrix<Byte> imgMat = new Matrix<Byte>(gray.Size);
            imgMat.CopyTo(gray);
            CvInvoke.Reduce(imgMat, imgMatH, ReduceDimension.SingleCol, ReduceType.ReduceSum);
        }



        private void b2t_ConversionButton_Click(object sender, EventArgs e)
        {
            b2t_resultBox.Clear();
            if (b2t_openImageBox.Image != null)
            {
                Image<Gray, Byte> gray = openImg.Convert<Gray, Byte>();//원본을 그레이 이미지로
                CvInvoke.Threshold(gray, gray, 0, 255, ThresholdType.Otsu);//이진화

                CvInvoke.BitwiseNot(gray, gray);//흑백 전환


                CvBlobDetector blobDetector = new CvBlobDetector();
                CvBlobs blobs = new CvBlobs();
                blobDetector.Detect(gray, blobs);
                List<CvBlob> blobList = new List<CvBlob>();

                for (int i = 0; i < blobs.Count; i++)
                {
                    CvBlob blob = blobs.Values.ElementAt(i);
                    int j;
                    for (j = 0; j < blobs.Count; j++)
                    {
                        CvBlob otherBlob = blobs.Values.ElementAt(j);
                        if (blob.Label == otherBlob.Label) continue;
                        if (otherBlob.BoundingBox.Contains(blob.BoundingBox)) break;
                    }
                    if (j == blobs.Count)
                        blobList.Add(blob);
                }
                
                int width = openImg.Width;
                int height = openImg.Height;

                Image<Gray, Byte> hor = new Image<Gray, byte>(new Size(width, height));
                Image<Gray, Byte> ver = new Image<Gray, byte>(new Size(height, height));
                CvInvoke.Blur(gray, gray, new Size(9, 9), new Point(5, 5));

                int ysize = gray.Rows;
                int xsize = gray.Cols;

                byte white = 255;

                for (int x = 0; x < xsize; x++)
                {
                    int z = 0;
                    for (int y = 0; y < ysize; y++)
                    {
                        if (gray.Data[y, x, 0] > 100)
                        {
                            if (z < ysize)
                            {
                                hor.Data[z++, x, 0] = white;
                            }
                        }
                    }
                }

                for (int y = 0; y < ysize; y++)
                {
                    int z = 0;
                    for (int x = 0; x < xsize; x++)
                    {
                        if (gray.Data[y, x, 0] > 200)
                        {
                            if (z < ver.Width)
                            {
                                ver.Data[y, z++, 0] = white;
                            }
                        }
                    }
                }

                Image<Gray, Byte> black = new Image<Gray, byte>(new Size(gray.Width, gray.Height));
                
                //------------Drawing areas---------------
                //--Vertical
                for (int y = 0; y < ysize; y++)
                {
                    if (ver.Data[y, 0, 0] != white)
                    {
                        for (int i = 0; i < openImg.Cols; i++)
                        {
                            black.Data[y, i, 0] = 255;
                        }
                    }
                }


                //--Horizontal
                for (int x = 0; x < xsize; x++)
                {
                    if (hor.Data[0, x, 0] != white)
                    {
                        for (int i = 0; i < openImg.Rows; i++)
                        {
                            black.Data[i, x, 0] = 255;
                        }
                    }
                }

                CvInvoke.BitwiseNot(black, black);//흑백 전환

                CvBlobDetector blackBlobDetector = new CvBlobDetector();
                CvBlobs blackBlobs = new CvBlobs();
                blackBlobDetector.Detect(black, blackBlobs);
                List<CvBlob> blackBlobList = new List<CvBlob>();
                for (int i = 0; i < blackBlobs.Count; i++)
                {
                    CvBlob blackBlob = blackBlobs.Values.ElementAt(i);
                    int j;
                    for (j = 0; j < blackBlobs.Count; j++)
                    {
                        CvBlob blackOtherBlob = blackBlobs.Values.ElementAt(j);
                        if (blackBlob.Label == blackOtherBlob.Label) continue;
                        if (blackOtherBlob.BoundingBox.Contains(blackBlob.BoundingBox)) break;
                    }
                    if (j == blackBlobs.Count)
                        blackBlobList.Add(blackBlob);
                }
                
                b2t_openImageBox.Image = openImg;
                
                //각각의 축의 개수를 추출하기 위한 변수
                int brailleBase_Count_X = 1;
                int brailleBase_Count_Y = 1;

                //첫번째 blob을 기준으로 각각의 축의 개수를 추출
                int brailleBase_X = blackBlobList.ElementAt(0).BoundingBox.X;
                int brailleBase_Y = blackBlobList.ElementAt(0).BoundingBox.Y;

                for (int i = 1; i < blackBlobs.Count; i++)
                    //x축의 점자 베이스 개수, y축의 점자 베이스 개수를 추출하는 루프
                {
                    int blackX = blackBlobList.ElementAt(i).BoundingBox.X;
                    int blackY = blackBlobList.ElementAt(i).BoundingBox.Y;

                    if (blackX == brailleBase_X) brailleBase_Count_X++;
                    if (blackY == brailleBase_Y) brailleBase_Count_Y++;

                }


                int[] arr = new int[brailleBase_Count_Y - 1];
                int[] brr = new int[brailleBase_Count_Y - 1];

                for (int i = 0; i < brailleBase_Count_Y - 1; i++)
                {
                    arr[i] = blackBlobList.ElementAt(i + 1).BoundingBox.X - blackBlobList.ElementAt(i).BoundingBox.X;
                    brr[i] = blackBlobList.ElementAt(i + 1).BoundingBox.X - blackBlobList.ElementAt(i).BoundingBox.X;
                }

                for (int i = 0; i < arr.Length; i++)
                {
                    for (int j = 0; j < arr.Length - 1; j++)
                    {
                        if (arr[j + 1] < arr[j])
                        {
                            int temp = arr[j + 1];
                            arr[j + 1] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }

                int A = arr[1];
                int B = 0; int b;
                int max = arr[arr.Length - 1];
                for (b = 0; b < arr.Length; b++)
                {
                    if (arr[b] > A + 4 && arr[b] < max - 4)
                    {
                        B = arr[b];
                        break;
                    }

                }
                A += 1;
                B += 1;
                
                Image<Gray, Byte> Cblack = new Image<Gray, byte>(new Size(gray.Width, gray.Height));

                int px = blackBlobList.ElementAt(0).BoundingBox.X;
                int py = blackBlobList.ElementAt(0).BoundingBox.Y;
                int c = 0;
                while (c < brr.Length && px < Cblack.Width)
                {
                    Cblack.Draw(new Rectangle(new Point(px, py), blackBlobList.ElementAt(0).BoundingBox.Size), new Gray(255), 1);
                    Cblack.Draw(new Rectangle(new Point(px, py + A), blackBlobList.ElementAt(0).BoundingBox.Size), new Gray(255), 1);
                    Cblack.Draw(new Rectangle(new Point(px, py + A * 2), blackBlobList.ElementAt(0).BoundingBox.Size), new Gray(255), 1);
                    
                    px += A;
                    Cblack.Draw(new Rectangle(new Point(px, py), blackBlobList.ElementAt(0).BoundingBox.Size), new Gray(255), 1);
                    Cblack.Draw(new Rectangle(new Point(px, py + A), blackBlobList.ElementAt(0).BoundingBox.Size), new Gray(255), 1);
                    Cblack.Draw(new Rectangle(new Point(px, py + A * 2), blackBlobList.ElementAt(0).BoundingBox.Size), new Gray(255), 1);
                    
                    c++;

                    if (c < brr.Length && brr[c] > B - 4 && brr[c] < B + 4) 
                    {
                        px += B;
                        c++;
                    }

                    else if (c < brr.Length && brr[c] > (int)(A * 2.5) - 4 && brr[c] < (int)(A * 2.5) + 4)
                    {
                        px += B;
                    }

                    else if (c < brr.Length && brr[c] > (int)(A * 4.2) - 4 && brr[c] < (int)(A * 4.2) + 4)
                    {
                        px += (int)(A * 3.0);
                        px += (int)(A / 2);
                    }

                    else
                    {
                        px += (int)(A * 3.3);
                        c++;
                    }

                }
                
                CvBlobDetector CblackBlobDetector = new CvBlobDetector();
                CvBlobs CblackBlobs = new CvBlobs();
                CblackBlobDetector.Detect(Cblack, CblackBlobs);
                List<CvBlob> CblackBlobList = new List<CvBlob>();
                for (int i = 0; i < CblackBlobs.Count; i++)
                {
                    CvBlob CblackBlob = CblackBlobs.Values.ElementAt(i);
                    int j;
                    for (j = 0; j < CblackBlobs.Count; j++)
                    {
                        CvBlob CblackOtherBlob = CblackBlobs.Values.ElementAt(j);
                        if (CblackBlob.Label == CblackOtherBlob.Label) continue;
                        if (CblackOtherBlob.BoundingBox.Contains(CblackBlob.BoundingBox)) break;
                    }
                    if (j == CblackBlobs.Count)
                        CblackBlobList.Add(CblackBlob);
                }
                
                /////////////////////////////////////////////////////////////////
                //각각의 축의 개수를 추출하기 위한 변수
                int CbrailleBase_Count_X = 1;
                int CbrailleBase_Count_Y = 1;

                //첫번째 blob을 기준으로 각각의 축의 개수를 추출
                int CbrailleBase_X = CblackBlobList.ElementAt(0).BoundingBox.X;
                int CbrailleBase_Y = CblackBlobList.ElementAt(0).BoundingBox.Y;

                for (int i = 1; i < CblackBlobList.Count; i++)//x축의 점자 베이스 개수, y축의 점자 베이스 개수를 추출하는 루프
                {
                    int CblackX = CblackBlobList.ElementAt(i).BoundingBox.X;
                    int CblackY = CblackBlobList.ElementAt(i).BoundingBox.Y;

                    if (CblackX == CbrailleBase_X) CbrailleBase_Count_X++;
                    if (CblackY == CbrailleBase_Y) CbrailleBase_Count_Y++;

                }
                
                int[,] CbrailleArray = new int[CbrailleBase_Count_Y, CbrailleBase_Count_X]; //점자를 위한 배열

                for (int j = 0; j < CbrailleBase_Count_X; j++)  //배열 초기화
                {
                    for (int i = 0; i < CbrailleBase_Count_Y; i++)
                    {
                        CbrailleArray[i, j] = 0;//초기값은 0으로
                    }
                }


                for (int j = 0; j < CbrailleBase_Count_X; j++)
                {
                    for (int i = 0; i < CbrailleBase_Count_Y; i++)
                    {
                        int cnt = (CbrailleBase_Count_Y * j) + i;    //점자 베이스의 위치를 위한 변수

                        int bx = CblackBlobList.ElementAt(cnt).BoundingBox.X;    //점자 베이스 시작 x위치
                        int by = CblackBlobList.ElementAt(cnt).BoundingBox.Y;    //점자 베이스 시작 y위치
                        int bendX = CblackBlobList.ElementAt(cnt).BoundingBox.X + CblackBlobList.ElementAt(cnt).BoundingBox.Width;   //점자 베이스 끝 x위치
                        int bendY = CblackBlobList.ElementAt(cnt).BoundingBox.Y + CblackBlobList.ElementAt(cnt).BoundingBox.Height;  //점자 베이스 끝 Y위치

                        for (int k = 0; k < blobList.Count; k++)
                        {
                            int x = blobList.ElementAt(k).BoundingBox.X;    //점자 시작 x위치
                            int y = blobList.ElementAt(k).BoundingBox.Y;    //점자 시작 y위치
                            int endX = blobList.ElementAt(k).BoundingBox.X + blobList.ElementAt(k).BoundingBox.Width;   //점자 끝 x위치
                            int endY = blobList.ElementAt(k).BoundingBox.Y + blobList.ElementAt(k).BoundingBox.Height;  //점자 끝 Y위치


                            if (x < bendX && endX > bx && y < bendY && endY > by)    //사각형이 겹치는지 체크
                            {
                                CbrailleArray[i, j] = 1; //겹치면 점자가 있는 것으로 간주하고 1을 넣는다.
                                break;
                            }
                        }
                    }
                }
                
                //---------------------------------------------------------
                double code = 0;
                int cnti = 1;
                int zed = 1;
                for (int j = 0; j < CbrailleBase_Count_Y; j++)
                {
                    for (int i = 0; i < 3 * zed && i < CbrailleBase_Count_X; i++)
                    {
                        if (cnti > 6)
                        {
                            exceptionCode((int)code);
                            code = 0;
                            cnti = 1;
                        }
                        if (CbrailleArray[j, i] == 1) code += Math.Pow(2, cnti);
                        cnti++;
                    }

                }

                exceptionCode((int)code);
                
                ArrayList cho = new ArrayList(new char[]{ 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' });
                ArrayList jung = new ArrayList(new char[] { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ', 'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' });
                ArrayList jong = new ArrayList(new char[] { ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ', 'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' });

                int c1 = -1;
                int c2 = -1;
                int c3 = -1;

                string hanString = b2t_resultBox.Text;
                char[] hanChar = hanString.ToCharArray();
                
                b2t_resultBox.Clear();

                int c4 = -1;
                
                for(int i=0; i<hanChar.Length; i++)
                {
                    if (cho.Contains(hanChar[i]))  //자음일때 초성 종성
                    {
                        if (i+1<hanChar.Length && jung.Contains(hanChar[i + 1]))  //자음 다음이 모음일때   초성
                        {
                            if (i< hanChar.Length && jung.Contains(hanChar[i + 1]) && c1!=-1)
                            {
                                c3 = 0;
                                c4 = 44032 + c1 + c2 + c3;
                                b2t_resultBox.AppendText(((char)c4).ToString());

                                c1 = -1;
                                c2 = -1;
                                c3 = -1;
                            }
                            
                            //초성+중성
                            c1 = cho.IndexOf(hanChar[i]);
                            c2 = jung.IndexOf(hanChar[i + 1]);
                            c1 *= 588;
                            c2 *= 28;

                            //중성(모음)을 추가했으니 다음 배열 인덱스로~
                            i++;
                            if (i + 2 >= hanChar.Length)
                            {
                                if (cho.Contains(hanChar[hanChar.Length - 1]))  //끝에 자음일 경우 종성이기에
                                    c3 = jong.IndexOf(hanChar[hanChar.Length - 1]);
                                else//아니면 종성없음
                                    c3 = 0;
                                c4 = 44032 + c1 + c2 + c3;
                                b2t_resultBox.AppendText(((char)c4).ToString());
                            }
                        }
                        else if(i+1< hanChar.Length && cho.Contains(hanChar[i+1]))//자음 다음 자음일 때   종성
                        {
                            if (i+3< hanChar.Length && cho.Contains(hanChar[i + 2]) && cho.Contains(hanChar[i + 3]))//종성뒤에 자음이 있을경우 두 자음 모두 받침
                            {
                                /////없음
                            }
                            else//없을 때 그냥
                            {
                                c3 = jong.IndexOf(hanChar[i]);
                                c4 = 44032 + c1 + c2 + c3;
                                b2t_resultBox.AppendText(((char)c4).ToString());

                                c1 = -1;
                                c2 = -1;
                                c3 = -1;
                            }
                        }
                        
                    }
                    else if (i < hanChar.Length && jung.Contains(hanChar[i]))     //모음일때  중성
                    {
                            //앞에 ㅇ을 붙인다.
                            c1 =cho.IndexOf('ㅇ');
                            c2 = jung.IndexOf(hanChar[i]);
                            c1 *= 588;
                            c2 *= 28;
                            if (i+1== hanChar.Length)
                            {
                                c3 = 0;
                                c4 = 44032 + c1 + c2 + c3;
                                b2t_resultBox.AppendText(((char)c4).ToString());

                                c1 = -1;
                                c2 = -1;
                                c3 = -1;
                            }
                    }
                }

            }

        }
        
        int code2 = 0;
        ArrayList excArr = new ArrayList(new[] { 18, 20, 34, 48, 80, 22, 50, 52 });
        ArrayList excBrr = new ArrayList(new[] { 70, 56, 28, 98, 74, 88, 26, 82, 84, 42, 46, 58, 122, 78, 30, 116, 24, 46, 114, 124, 60, 102, 90, 110, 126, 54, 94, 106, 92, 62, 66 });
        private void exceptionCode(int code)
        {
            if(code==32 && code2==0)
            {
                code2 = code;
            }
            else if(code2!=0 && code==92)
            {
                code2 = 0;
                b2t_resultBox.AppendText("ㄹㅡㄹ");
            }
            else if(excArr.Contains(code))
            {
                code2 = code;
            }
            else if(code2!=0 && excBrr.Contains(code))
            {
                b2t_resultBox.AppendText(Convert.ToString(b2t[code2]) + Convert.ToString(b2t[(int)code]));
                code2 = 0;
            }
            else if(code2 != 0 && !(excBrr.Contains(code)))
            {
                b2t_resultBox.AppendText(Convert.ToString(b2t[code2]) + "ㅏ" + Convert.ToString(b2t[code]));
                code2 = 0;
            }
            else if (code2 != 0)
            {
                b2t_resultBox.AppendText(Convert.ToString(b2t[code2]) + Convert.ToString(b2t[(int)code]));
                code2 = 0;
            }
            else
            {
                b2t_resultBox.AppendText(Convert.ToString(b2t[(int)code]));
            }
        }

        private void t2b_Button_Click(object sender, EventArgs e)
        {
            if (t2b_ResultPanel.Controls.Count > 0)
            {
                for (int i = (t2b_ResultPanel.Controls.Count - 1); i >= 0; i--)
                {
                    Control c = t2b_ResultPanel.Controls[i];
                    c.Dispose();
                }
                GC.Collect();
            }
            String text = t2b_TextBox.Text;
            string imagePath = @"..\..\Resources\braille\";
            string pathname = " ";
            PictureBox[] pbName = new PictureBox[text.Length];

            int charState = 0;  // 1->num, 2->eng, 3->hang
            bool korean = false;
            List<string> abrrs = new List<string>();

            string[] words = text.Split(' ', '\n', '\r', ',', '.', '-', '?');
            for (int abrPos = 0; abrPos < words.Length; abrPos++)
            {
                if (isAbb(words[abrPos]))
                {
                    abrrs.Add(words[abrPos]);
                }
            }

            int abrsPos = 0;
            for (int i = 0; i < pbName.Length; i++)
            {
                int j = i, ja = 0;

                while (j < text.Length
                    && abrsPos < abrrs.Count()
                    && ja < abrrs[abrsPos].Length
                    && text.Substring(j, 1).Equals(abrrs[abrsPos].Substring(ja, 1))
                    && text.Substring(j, 1) != " ")
                {
                    j++;
                    ja++;
                }

                if (abrsPos < abrrs.Count() && ja == abrrs[abrsPos].Length)
                {
                    putHangul(abrrs[abrsPos]);
                    abrsPos++;
                    i = j - 1;
                    continue;
                }

                string key = text.Substring(i, 1).ToLower();
                if (isHangul(key.ElementAt(0)))
                {
                    string[] s = splitHangul(key.ElementAt(0));
                    int k = 0;
                    while (k < s.Length && s[k] != "final_")
                    {
                        putHangul(s[k]);
                        k++;
                    }
                    korean = true;
                }
                if (!korean)
                    pathname = (string)hashtable[key];

                if (charState != checkCharSet(text.Substring(i, 1).ToLower()) && text.Substring(i, 1) != " " && !korean)
                {
                    charState = checkCharSet(text.Substring(i, 1).ToLower());
                    string path = null;
                    if (charState == 1)
                        path = "n_start.png";
                    else if (charState == 2)
                        path = "eng_start.png";
                    if (charState != 0)
                    {
                        Image img3 = Image.FromFile(imagePath + path);
                        PictureBox charStateImg = new PictureBox();
                        charStateImg.Size = new Size(img3.Size.Width, img3.Size.Height);
                        charStateImg.BackgroundImage = img3;
                        charStateImg.BackgroundImageLayout = ImageLayout.Stretch;
                        charStateImg.Image = img3;
                        charStateImg.Anchor = AnchorStyles.Left;
                        charStateImg.Visible = true;
                        t2b_ResultPanel.Visible = true;
                        charStateImg.Location = new Point(0, 0);
                        t2b_ResultPanel.Controls.Add(charStateImg);
                    }
                }

                if (!korean)
                {
                    Image img2 = Image.FromFile(imagePath + pathname);
                    pbName[i] = new PictureBox();
                    pbName[i].Size = new Size(img2.Size.Width, img2.Size.Height);
                    pbName[i].BackgroundImage = img2;
                    pbName[i].BackgroundImageLayout = ImageLayout.Stretch;
                    pbName[i].Image = img2;
                    pbName[i].Anchor = AnchorStyles.Left;
                    pbName[i].Visible = true;
                    t2b_ResultPanel.Visible = true;
                    pbName[i].Location = new Point(0, 0);
                    t2b_ResultPanel.Controls.Add(pbName[i]);
                }
                korean = false;
            }
        }

        private bool isHangul(char c)
        {
            if ((c >= 0x3130 && c <= 0x318f) || (c >= 0xac00 && c <= 0xd7af))
                return true;
            return false;
        }

        private int checkCharSet(string set)
        {
            char charSet = set.ElementAt(0);
            if (charSet >= '0' && charSet <= '9')
                return 1;
            else if (charSet >= 'a' && charSet <= 'z')
                return 2;
            else return 0;
        }

        //initial:초성 vowel:모음 final:종성
        static String[] initial = { "ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
        static String[] vowel = { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" };
        static String[] final = { "", "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };
        static String[] abbreviated = { "가", "사", "억", "옹", "울", "옥", "연", "운", "온", "언", "얼", "열", "인", "영", "을", "은", "것", "그러나", "그러면", "그래서", "그런데", "그러므로", "그리고", "그리하여" };

        private String[] splitHangul(char c)
        {
            String t = "";
            String tmp = "";
            string[] s = { "", "", "" };
            int n, n1, n2, n3;

            n = (int)(c & 0xFFFF);

            if (n >= 0xAC00 && n <= 0xD7A3)
            {
                n = n - 0xAC00;
                n1 = n / (21 * 28);
                n = n % (21 * 28);
                n2 = n / 28;
                n3 = n % 28;
                tmp = initial[n1] + vowel[n2] + final[n3];

                t += tmp;

                //파일명 ex)초성 ㄱ = initial_ㄱ
                //hashtable["initial_" + initial[n1]] 값이 t2b_ResultBox에 찍힘
                s[0] = "initial_" + initial[n1];
                //hashtable["vowel_" + vowel[n2]] 값이 t2b_ResultBox에 찍힘
                s[1] = "vowel_" + vowel[n2];
                //hashtable["final_" + final[n3] 값이 t2b_ResultBox에 찍힘
                s[2] = "final_" + final[n3];
            }
            else
            {
                t += c;
                //hashtable["initial_" + t] 값이 t2b_ResultBox에 찍힘
                s[0] = "initial_" + t;
            }
            return s;
        }

        private bool isAbb(string s)
        {
            for (int i = 0; i < abbreviated.Length; i++)
            {
                if (s == abbreviated[i])
                    return true;
            }
            return false;
        }

        private void putHangul(string path)
        {
            Image img3 = Image.FromFile(@"..\..\Resources\braille\" + (string)hashtable[path]);
            PictureBox charStateImg = new PictureBox();
            charStateImg.Size = new Size(img3.Size.Width, img3.Size.Height);
            charStateImg.BackgroundImage = img3;
            charStateImg.BackgroundImageLayout = ImageLayout.Stretch;
            charStateImg.Image = img3;
            charStateImg.Anchor = AnchorStyles.Left;
            charStateImg.Visible = true;
            t2b_ResultPanel.Visible = true;
            charStateImg.Location = new Point(0, 0);
            t2b_ResultPanel.Controls.Add(charStateImg);
        }



        private void setHashTable()
        {
            // eng & num
            hashtable["a"] = "a.png"; hashtable["1"] = "a.png";
            hashtable["b"] = "b.png"; hashtable["2"] = "b.png";
            hashtable["c"] = "c.png"; hashtable["3"] = "c.png";
            hashtable["d"] = "d.png"; hashtable["4"] = "d.png";
            hashtable["e"] = "e.png"; hashtable["5"] = "e.png";
            hashtable["f"] = "f.png"; hashtable["6"] = "f.png";
            hashtable["g"] = "g.png"; hashtable["7"] = "g.png";
            hashtable["h"] = "h.png"; hashtable["8"] = "h.png";
            hashtable["i"] = "i.png"; hashtable["9"] = "i.png";
            hashtable["j"] = "j.png"; hashtable["0"] = "j.png";
            hashtable["k"] = "k.png";
            hashtable["l"] = "l.png";
            hashtable["m"] = "m.png";
            hashtable["n"] = "n.png";
            hashtable["o"] = "o.png";
            hashtable["p"] = "p.png";
            hashtable["q"] = "q.png";
            hashtable["r"] = "r.png";
            hashtable["s"] = "s.png";
            hashtable["t"] = "t.png";
            hashtable["u"] = "u.png";
            hashtable["v"] = "v.png";
            hashtable["w"] = "w.png";
            hashtable["x"] = "x.png";
            hashtable["y"] = "y.png";
            hashtable["z"] = "z.png";

            // special character
            hashtable["?"] = "zq.png";
            hashtable["!"] = "ze.png";
            hashtable["'"] = "zsq.png";
            hashtable[","] = "zc.png";
            hashtable["-"] = "zh.png";
            hashtable["."] = "zd.png";
            hashtable["#"] = "z#.png";
            hashtable["…"] = "zellipse.png";
            hashtable[":"] = "zcolon.png";
            hashtable[";"] = "zsemicolon.png";
            hashtable["~"] = "zwave.png";
            hashtable["*"] = "zasterisk.png";

            hashtable[" "] = "blank.png";
            hashtable["\n"] = "enter.png";

            //kor
            //initial [초성]
            hashtable["initial_ㄱ"] = "initial_ㄱ.png";
            hashtable["initial_ㄴ"] = "initial_ㄴ.png";
            hashtable["initial_ㄷ"] = "initial_ㄷ.png";
            hashtable["initial_ㄹ"] = "initial_ㄹ.png";
            hashtable["initial_ㅁ"] = "initial_ㅁ.png";
            hashtable["initial_ㅂ"] = "initial_ㅂ.png";
            hashtable["initial_ㅅ"] = "initial_ㅅ.png";
            hashtable["initial_ㅇ"] = "initial_ㅇ.png";
            hashtable["initial_ㅈ"] = "initial_ㅈ.png";
            hashtable["initial_ㅊ"] = "initial_ㅊ.png";
            hashtable["initial_ㅋ"] = "initial_ㅋ.png";
            hashtable["initial_ㅌ"] = "initial_ㅌ.png";
            hashtable["initial_ㅍ"] = "initial_ㅍ.png";
            hashtable["initial_ㅎ"] = "initial_ㅎ.png";
            hashtable["initial_ㄲ"] = "initial_ㄲ.png";
            hashtable["initial_ㄸ"] = "initial_ㄸ.png";
            hashtable["initial_ㅃ"] = "initial_ㅃ.png";
            hashtable["initial_ㅆ"] = "initial_ㅆ.png";
            hashtable["initial_ㅉ"] = "initial_ㅉ.png";

            //vowel [모음]
            hashtable["vowel_ㅏ"] = "vowel_ㅏ.png";
            hashtable["vowel_ㅐ"] = "vowel_ㅐ.png";
            hashtable["vowel_ㅑ"] = "vowel_ㅑ.png";
            hashtable["vowel_ㅒ"] = "vowel_ㅒ.png";
            hashtable["vowel_ㅓ"] = "vowel_ㅓ.png";
            hashtable["vowel_ㅔ"] = "vowel_ㅔ.png";
            hashtable["vowel_ㅕ"] = "vowel_ㅕ.png";
            hashtable["vowel_ㅖ"] = "vowel_ㅖ.png";
            hashtable["vowel_ㅗ"] = "vowel_ㅗ.png";
            hashtable["vowel_ㅘ"] = "vowel_ㅘ.png";
            hashtable["vowel_ㅙ"] = "vowel_ㅙ.png";
            hashtable["vowel_ㅚ"] = "vowel_ㅚ.png";
            hashtable["vowel_ㅛ"] = "vowel_ㅛ.png";
            hashtable["vowel_ㅜ"] = "vowel_ㅜ.png";
            hashtable["vowel_ㅝ"] = "vowel_ㅝ.png";
            hashtable["vowel_ㅞ"] = "vowel_ㅞ.png";
            hashtable["vowel_ㅟ"] = "vowel_ㅟ.png";
            hashtable["vowel_ㅠ"] = "vowel_ㅠ.png";
            hashtable["vowel_ㅡ"] = "vowel_ㅡ.png";
            hashtable["vowel_ㅢ"] = "vowel_ㅢ.png";
            hashtable["vowel_ㅣ"] = "vowel_ㅣ.png";

            //final [종성]
            hashtable["final_ㄱ"] = "final_ㄱ.png";
            hashtable["final_ㄴ"] = "final_ㄴ.png";
            hashtable["final_ㄷ"] = "final_ㄷ.png";
            hashtable["final_ㄹ"] = "final_ㄹ.png";
            hashtable["final_ㅁ"] = "final_ㅁ.png";
            hashtable["final_ㅂ"] = "final_ㅂ.png";
            hashtable["final_ㅅ"] = "final_ㅅ.png";
            hashtable["final_ㅇ"] = "final_ㅇ.png";
            hashtable["final_ㅈ"] = "final_ㅈ.png";
            hashtable["final_ㅊ"] = "final_ㅊ.png";
            hashtable["final_ㅋ"] = "final_ㅋ.png";
            hashtable["final_ㅌ"] = "final_ㅌ.png";
            hashtable["final_ㅍ"] = "final_ㅍ.png";
            hashtable["final_ㅎ"] = "final_ㅎ.png";
            hashtable["final_ㅆ"] = "final_ㅆ.png";
            hashtable["final_ㄲ"] = "final_ㄲ.png";
            hashtable["final_ㄳ"] = "final_ㄳ.png";
            hashtable["final_ㄵ"] = "final_ㄵ.png";
            hashtable["final_ㄶ"] = "final_ㄶ.png";
            hashtable["final_ㄺ"] = "final_ㄺ.png";
            hashtable["final_ㄻ"] = "final_ㄻ.png";
            hashtable["final_ㄼ"] = "final_ㄼ.png";
            hashtable["final_ㄾ"] = "final_ㄾ.png";
            hashtable["final_ㄿ"] = "final_ㄿ.png";
            hashtable["final_ㅀ"] = "final_ㅀ.png";
            hashtable["final_ㅄ"] = "final_ㅄ.png";

            // abbreviations
            hashtable["그리고"] = "그리고.png";
            hashtable["가"] = "가.png";
            hashtable["것"] = "것.png";
            hashtable["그래서"] = "그래서.png";
            hashtable["그러나"] = "그러나.png";
            hashtable["그러면"] = "그러면.png";
            hashtable["그러므로"] = "그러므로.png";
            hashtable["그런데"] = "그런데.png";
            hashtable["그리하여"] = "그리하여.png";
            hashtable["사"] = "사.png";
            hashtable["억"] = "억.png";
            hashtable["언"] = "언.png";
            hashtable["얼"] = "얼.png";
            hashtable["연"] = "연.png";
            hashtable["열"] = "열.png";
            hashtable["영"] = "영.png";
            hashtable["옥"] = "옥.png";
            hashtable["온"] = "온.png";
            hashtable["올"] = "올.png";
            hashtable["옹"] = "옹.png";
            hashtable["운"] = "운.png";
            hashtable["울"] = "울.png";
            hashtable["은"] = "은.png";
            hashtable["인"] = "인.png";
            
            b2t[16] = "ㄱ";
            b2t[18] = "ㄴ";
            b2t[20] = "ㄷ";
            b2t[32] = "ㄹ";
            b2t[34] = "ㅁ";
            b2t[48] = "ㅂ";
            b2t[64] = "(된/ㅅ)";
            b2t[80] = "ㅈ";
            b2t[96] = "ㅊ";
            b2t[22] = "ㅋ";
            b2t[38] = "ㅌ";
            b2t[50] = "ㅍ";
            b2t[52] = "ㅎ";
            //b2t[64] = "<>";
            b2t[2] = "ㄱ";
            b2t[36] = "ㄴ";
            b2t[40] = "ㄷ";
            b2t[4] = "ㄹ";
            b2t[68] = "ㅁ";
            b2t[6] = "ㅂ";
            b2t[8] = "ㅅ";
            b2t[108] = "ㅇ";
            b2t[10] = "ㅈ";
            b2t[12] = "ㅊ";
            b2t[44] = "ㅋ";
            b2t[76] = "ㅌ";
            b2t[100] = "(./ㅍ)";
            b2t[104] = "ㅎ";
            //-------------
            b2t[70] = "ㅏ";
            b2t[56] = "ㅑ";
            b2t[28] = "ㅓ";
            b2t[98] = "ㅕ";
            b2t[74] = "ㅗ";
            b2t[88] = "ㅛ";
            b2t[26] = "ㅜ";
            b2t[82] = "ㅠ";
            b2t[84] = "ㅡ";
            b2t[42] = "ㅣ";
            b2t[46] = "ㅐ";
            b2t[58] = "ㅔ";
            b2t[122] = "ㅚ";
            b2t[78] = "ㅘ";
            b2t[30] = "ㅝ";
            b2t[116] = "ㅢ";
            b2t[24] = "ㅖ";
            b2t[46] = "ㅣ";
            
            b2t[86] = "ㄱㅏ";
            b2t[14] = "ㅅㅏ";
            b2t[114] = "ㅓㄱ";
            b2t[124] = "ㅓㄴ";
            b2t[60] = "ㅓㄹ";
            b2t[102] = "ㅕㅇ";
            b2t[90] = "ㅗㄱ";
            b2t[110] = "ㅗㄴ";
            b2t[126] = "ㅗㅇ";
            b2t[54] = "ㅜㄴ";
            b2t[94] = "ㅜㄹ";
            b2t[106] = "ㅡㄴ";
            b2t[92] = "ㅡㄹ";
            b2t[62] = "ㅣㄴ";
            b2t[66] = "ㅕㄴ";
            
        }
    }
}