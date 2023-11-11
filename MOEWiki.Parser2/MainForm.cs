using MOEWiki.Parser2;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MOEWiki.Parser2
{
    public partial class MainForm : Form
    {
        private Color borderColor = Color.FromArgb(20, 14, 14);
        private int defWidth = 533;
        private int selectedMode = 0;
        private Dictionary<int, int> modeWidth = new Dictionary<int, int>()
        {
            { 0, 533},
            { 1, 533},
            { 2, 533},
            { 3, 533},
            { 4, 533},
        };
        private Bitmap ss = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        private int colorVariation = 15;

        public MainForm()
        {
            InitializeComponent();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int keys);

        private void SelectedModeChanged(int newMode)
        {
            if (selectedMode != newMode)
            {
                selectedMode = newMode;
                if (selectedMode == 2)
                {
                    pictureBox2.Visible = true;
                }
                else
                {
                    pictureBox2.Visible = false;
                }
            }
        }
        bool CompareColors(Color c1, Color c2)
        {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B) <= colorVariation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tmrMain.Enabled)
            {
                tmrMain.Enabled = false;
                bStart.BackColor = Color.Red;
                bStart.Text = "Start";
            }
            else
            {
                tmrMain.Enabled = true;
                bStart.BackColor = Color.Green;
                bStart.Text = "Stop";
            }

        }

        private void GetImage(PictureBox pb, int sx, int sy)
        {
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(sx, sy, 0, 0, new Size(pb.Width, pb.Height));
            }
            pb.Image = bmp;
        }

        private void GetScreenshot()
        {
            using (Graphics g = Graphics.FromImage(ss))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(ss.Width, ss.Height));
            }
        }

        private int GetDirection(Point point)
        {
            if (point.X > Screen.PrimaryScreen.Bounds.Width - defWidth - 20 - (selectedMode == 2 ? defWidth : 0))
                return -1;
            else
                return 1;
        }

        #region GetBorders
        Point GetTopBorder(Point mousePos)
        {
            var x = mousePos.X + 250;
            var y = mousePos.Y;
            while (y >= 0 && y < ss.Height - 1)
            {
                if (CompareColors(ss.GetPixel(x, y), borderColor))
                {
                    return new Point(x, y);
                }
                y--;
            }
            return new Point(-1, -1);
        }

        Point GetBottomBorder(Point leftAngle)
        {
            var y = leftAngle.Y + 21;
            while (y >= 0 && y < ss.Height - 1)
            {
                if (CompareColors(ss.GetPixel(leftAngle.X, y), borderColor))
                {
                    return new Point(leftAngle.X, y + 19);
                }
                y++;
            }
            return new Point(-1, -1);
        }

        Point GetLeftBorder(Point topBorder)
        {
            if (topBorder.Y < 0)
                return new Point(-1, -1);
            var x = topBorder.X;
            while (x >= 0 && x < ss.Width - 1)
            {
                x--;
                if (!CompareColors(ss.GetPixel(x, topBorder.Y), borderColor))
                    return new Point(x + 1, topBorder.Y);
            }
            return new Point(-1, -1);
        }

        Point GetRightBorder(Point topBorder)
        {
            if (topBorder.Y < 0)
                return new Point(-1, -1);
            var x = topBorder.X;
            while (x >= 0 && x < ss.Width - 1)
            {
                x++;
                if (!CompareColors(ss.GetPixel(x, topBorder.Y), borderColor))
                    return new Point(x - 1, topBorder.Y);
            }
            return new Point(-1, -1);
        }
        Point GetTopBorderRev(Point mousePos)
        {
            var x = mousePos.X - 250;
            var y = mousePos.Y;
            while (y >= 0 && y < ss.Height - 1)
            {
                if (CompareColors(ss.GetPixel(x, y), borderColor))
                {
                    return new Point(x, y);
                }
                y--;
            }
            return new Point(-1, -1);
        }
        #endregion

        private async void tmrMain_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(102) != 0) //getscrin - num6
            {
                lWarn.Text = "";
                GetScreenshot();
                var mousePos = Cursor.Position;
                var direction = GetDirection(mousePos);
                var firstPoint = mousePos;
                var topBorder = GetTopBorder(mousePos);
                var x = 0;
                var y = 0;
                var w = 0;
                var h = 0;
                if (topBorder.Y < 0)
                {
                    lWarn.Text += "Верхняя граница не найдена. ";
                }
                else
                {
                    y = topBorder.Y;
                    var leftBorder = GetLeftBorder(topBorder);
                    if (leftBorder.X < 0)
                    {
                        lWarn.Text += "Левая граница не найдена. ";
                    }
                    else
                    {
                        x = leftBorder.X;
                        var rightBorder = GetRightBorder(topBorder);
                        if (rightBorder.X < 0)
                        {
                            lWarn.Text += "Правая граница не найдена. ";
                        }
                        else
                        {
                            w = rightBorder.X - x + 1;
                        }
                        var bottomBorder = GetBottomBorder(new Point(x, y));
                        if (bottomBorder.Y < 0)
                        {
                            lWarn.Text += "Нижняя граница не найдена. ";
                        }
                        else
                        {
                            h = bottomBorder.Y - y + 1;
                        }
                    }
                }
                if (x != 0 && y != 0 && w != 0 && h != 0)
                {
                    pictureBox1.Width = w;
                    pictureBox1.Height = h;
                    pictureBox1.Image = ss.Clone(new Rectangle(x, y, w, h), ss.PixelFormat);
                }
                if (selectedMode == 2)
                {
                    var leftBorderForRecipe = GetLeftBorder(new Point(x + w + 30, y));
                    if (leftBorderForRecipe.X < 0)
                    {
                        lWarn.Text += "Левая граница рецепта не найдена. ";
                    }
                    else
                    {
                        x = leftBorderForRecipe.X;
                        var rightBorder = GetRightBorder(new Point(x + 30, y));
                        if (rightBorder.X < 0)
                        {
                            lWarn.Text += "Правая граница рецепта не найдена. ";
                        }
                        else
                        {
                            w = rightBorder.X - x + 1;
                        }
                        var bottomBorderForRecipe = GetBottomBorder(new Point(x, y));
                        if (bottomBorderForRecipe.Y < 0)
                        {
                            lWarn.Text += "Нижняя граница рецепта не найдена. ";
                        }
                        else
                        {
                            h = bottomBorderForRecipe.Y - y + 1;
                            if (x != 0 && y != 0 && w != 0 && h != 0)
                            {
                                pictureBox2.Width = w;
                                pictureBox2.Height = h;
                                pictureBox2.Location = new Point(pictureBox1.Location.X + pictureBox1.Width + 5, pictureBox2.Location.Y);
                                pictureBox2.Image = ss.Clone(new Rectangle(x, y, w, h), ss.PixelFormat);
                            }
                        }
                    }
                }
            }
            else
            if (GetAsyncKeyState(100) != 0) //getscrin - num4
            {
                lWarn.Text = "";
                GetScreenshot();
                var mousePos = Cursor.Position;
                var direction = GetDirection(mousePos);
                var firstPoint = mousePos;
                var topBorder = GetTopBorderRev(mousePos);
                var x = 0;
                var y = 0;
                var w = 0;
                var h = 0;
                if (topBorder.Y < 0)
                {
                    lWarn.Text += "Верхняя граница не найдена. ";
                }
                else
                {
                    y = topBorder.Y;
                    var leftBorder = GetLeftBorder(topBorder);
                    if (leftBorder.X < 0)
                    {
                        lWarn.Text += "Левая граница не найдена. ";
                    }
                    else
                    {
                        x = leftBorder.X;
                        var rightBorder = GetRightBorder(topBorder);
                        if (rightBorder.X < 0)
                        {
                            lWarn.Text += "Правая граница не найдена. ";
                        }
                        else
                        {
                            w = rightBorder.X - x + 1;
                        }
                        var bottomBorder = GetBottomBorder(new Point(x, y));
                        if (bottomBorder.Y < 0)
                        {
                            lWarn.Text += "Нижняя граница не найдена. ";
                        }
                        else
                        {
                            h = bottomBorder.Y - y + 1;
                        }
                    }
                }
                if (x != 0 && y != 0 && w != 0 && h != 0)
                {
                    pictureBox1.Width = w;
                    pictureBox1.Height = h;
                    pictureBox1.Image = ss.Clone(new Rectangle(x, y, w, h), ss.PixelFormat);
                }
                if (selectedMode == 2)
                {
                    var leftBorderForRecipe = GetLeftBorder(new Point(x - 30, y));
                    if (leftBorderForRecipe.X < 0)
                    {
                        lWarn.Text += "Левая граница рецепта не найдена. ";
                    }
                    else
                    {
                        x = leftBorderForRecipe.X;
                        var rightBorder = GetRightBorder(new Point(x + 30, y));
                        if (rightBorder.X < 0)
                        {
                            lWarn.Text += "Правая граница рецепта не найдена. ";
                        }
                        else
                        {
                            w = rightBorder.X - x + 1;
                        }
                        var bottomBorderForRecipe = GetBottomBorder(new Point(x, y));
                        if (bottomBorderForRecipe.Y < 0)
                        {
                            lWarn.Text += "Нижняя граница рецепта не найдена. ";
                        }
                        else
                        {
                            h = bottomBorderForRecipe.Y - y + 1;
                            if (x != 0 && y != 0 && w != 0 && h != 0)
                            {
                                pictureBox2.Width = w;
                                pictureBox2.Height = h;
                                pictureBox2.Location = new Point(pictureBox1.Location.X + pictureBox1.Width + 5, pictureBox2.Location.Y);
                                pictureBox2.Image = ss.Clone(new Rectangle(x, y, w, h), ss.PixelFormat);
                            }
                        }
                    }
                }
            }
            else if (GetAsyncKeyState(104) != 0)//parse - num 8
            {
                string path = $"E:\\МОЕ\\img\\{Guid.NewGuid()}.jpg";
                if (Directory.Exists("E:\\МОЕ\\img"))
                    pictureBox1.Image.Save(path, ImageFormat.Jpeg);
                txtResult1.Text = "";
                txtResult2.Text = "";
                try
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.Timeout = new TimeSpan(1, 1, 1);


                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent("K87429841688957"), "apikey"); //Added api key in form data

                    form.Add(new StringContent(cbocrengine.Text), "ocrengine");
                    form.Add(new StringContent("true"), "scale");
                    form.Add(new StringContent("true"), "istable");

                    byte[] imageData = File.ReadAllBytes(path);
                    form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

                    HttpResponseMessage response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);

                    string strContent = await response.Content.ReadAsStringAsync();



                    Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);


                    if (ocrResult.OCRExitCode == 1)
                    {
                        //txtResult2.Text = strContent;
                        for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                        {
                            txtResult1.Text = txtResult1.Text + ocrResult.ParsedResults[i].ParsedText;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR: " + strContent);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Ooops 1");
                }
                if (pictureBox2.Visible)
                {
                    path = $"E:\\МОЕ\\img\\{Guid.NewGuid()}.jpg";
                    if (Directory.Exists("E:\\МОЕ\\img"))
                        pictureBox2.Image.Save(path, ImageFormat.Jpeg);
                    try
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.Timeout = new TimeSpan(1, 1, 1);


                        MultipartFormDataContent form = new MultipartFormDataContent();
                        form.Add(new StringContent("K87429841688957"), "apikey"); //Added api key in form data

                        form.Add(new StringContent(cbocrengine.Text), "ocrengine");
                        form.Add(new StringContent("true"), "scale");
                        form.Add(new StringContent("true"), "istable");

                        byte[] imageData = File.ReadAllBytes(path);
                        form.Add(new ByteArrayContent(imageData, 0, imageData.Length), "image", "image.jpg");

                        HttpResponseMessage response = await httpClient.PostAsync("https://api.ocr.space/Parse/Image", form);

                        string strContent = await response.Content.ReadAsStringAsync();



                        Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);


                        if (ocrResult.OCRExitCode == 1)
                        {
                            for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                            {
                                txtResult2.Text = txtResult2.Text + ocrResult.ParsedResults[i].ParsedText;
                            }
                        }
                        else
                        {
                            MessageBox.Show("ERROR: " + strContent);
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Ooops 2");
                    }
                }
            }
            else if (GetAsyncKeyState(99) != 0)//stop num 3
            {
                bStart.BackColor = Color.Red;
                tmrMain.Enabled = false;
            }
        }


        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyValue.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cbMode.SelectedIndex = selectedMode;
            tmrMain.Enabled = true;
            bStart.BackColor = Color.Green;
            bStart.Text = "Stop";
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedModeChanged(cbMode.SelectedIndex);
        }

        private void nudColorVariation_ValueChanged(object sender, EventArgs e)
        {
            colorVariation = (int)nudColorVariation.Value;
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editCategory = new EditCategory();
            editCategory.ShowDialog();
        }
    }
}