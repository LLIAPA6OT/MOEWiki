using MOEWiki.DBMySQL.Enums;
using MOEWiki.DBMySQL.Gateways;
using MOEWiki.DBMySQL.Models;
using MOEWiki.Parser3.EditForms;
using MOEWiki.Parser3.Helpers;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MOEWiki.Parser3
{
    public partial class MainForm : Form
    {
        private Bitmap? image;
        private Color borderColor = Color.FromArgb(20, 14, 14);
        private int defWidth = 533;
        private ModeEnum selectedMode = ModeEnum.ItemPlusRecipe;
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

        bool CompareColors(Color c1, Color c2)
        {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B) <= colorVariation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tmrMain.Enabled)
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


        private void GetScreenshot()
        {
            using (Graphics g = Graphics.FromImage(ss))
            {
                g.CopyFromScreen(0, 0, 0, 0, new Size(ss.Width, ss.Height));
            }
        }


        #region GetBorders
        Point GetTopBorder(Point mousePos)
        {
            var rnd = new Random();
            var y = mousePos.Y;
            var x = mousePos.X + 290 + rnd.Next(0, 20);
            if (x < ss.Width)
                while (y >= 0 && y < ss.Height - 1)
                {
                    if (CompareColors(ss.GetPixel(x, y), borderColor))
                    {
                        return new Point(x, y);
                    }
                    y--;
                }
            y = mousePos.Y;
            x = mousePos.X - 300;
            if (x > 0)
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
        #endregion

        private async void tmrMain_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(102) != 0 || GetAsyncKeyState(110) != 0) //from both - num6 or [.]
            {
                lWarn.Text = "";
                GetScreenshot();
                var mousePos = Cursor.Position;
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
                    image = ss.Clone(new Rectangle(x + 16, y + 16, 118, 118), ss.PixelFormat);
                }
                if (selectedMode == ModeEnum.ItemPlusRecipe)
                {
                    var leftBorderForRecipe = mousePos.X < x ? GetLeftBorder(new Point(x + w + 30, y)) : GetLeftBorder(new Point(x - 30, y));
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
            else if (GetAsyncKeyState(101) != 0 || GetAsyncKeyState(104) != 0 || GetAsyncKeyState(96) != 0)//parse - num 5 or 8 or 0
            {
                if (chbOffline.Checked)
                {
                    if (lWarn.Text != "" || pictureBox1.Image == null || pictureBox1.Width < 150 || (pictureBox2.Visible && pictureBox1.Width < 150)) return;
                    string p = $"E:\\MOE\\auto";
                    var autoParse = new AutoParse() { Mode = selectedMode, ImageName = $"{p}\\{Guid.NewGuid()}.jpg" };
                    if (cbAddStr1.Visible)
                    {
                        autoParse.AdditionalString1 = cbAddStr1.Text;
                    }
                    pictureBox1.Image.Save(autoParse.ImageName, ImageFormat.Jpeg);
                    if (selectedMode == ModeEnum.ItemPlusRecipe)
                    {
                        autoParse.Mode = ModeEnum.Item;
                        var autoParse2 = new AutoParse() { Mode = ModeEnum.Recipe, ImageName = $"{p}\\{Guid.NewGuid()}.jpg" };
                        pictureBox2.Image.Save(autoParse2.ImageName, ImageFormat.Jpeg);
                        try
                        {
                            GatewaysFacade.AutoParseGateway.Add(autoParse2);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    try
                    {
                        GatewaysFacade.AutoParseGateway.Add(autoParse);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    pictureBox2.Image = pictureBox1.Image = null;
                }
                else
                {
                    if (lWarn.Text != "" || pictureBox1.Width < 150 || (pictureBox2.Visible && pictureBox1.Width < 150)) return;
                    tmrMain.Enabled = false;
                    string path = $"E:\\MOE\\img\\{Guid.NewGuid()}.jpg";
                    if (Directory.Exists("E:\\MOE\\img"))
                        pictureBox1.Image.Save(path, ImageFormat.Jpeg);
                    txtResult1.Text = "";
                    txtResult2.Text = "";
                    try
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.Timeout = new TimeSpan(1, 1, 1);


                        MultipartFormDataContent form = new MultipartFormDataContent();
                        form.Add(new StringContent("K82017420088957"), "apikey"); //Added api key in form data
                        //form.Add(new StringContent("K83082752188957"), "apikey"); //Added api key in form data

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
                        path = $"E:\\MOE\\img\\{Guid.NewGuid()}.jpg";
                        if (Directory.Exists("E:\\MOE\\img"))
                            pictureBox2.Image.Save(path, ImageFormat.Jpeg);
                        try
                        {
                            HttpClient httpClient = new HttpClient();
                            httpClient.Timeout = new TimeSpan(1, 1, 1);


                            MultipartFormDataContent form = new MultipartFormDataContent();
                            form.Add(new StringContent("K87429841688957"), "apikey"); //Added api key in form data
                            //form.Add(new StringContent("K81570705388957"), "apikey"); //Added api key in form data

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
                    tmrMain.Enabled = true;
                    parseToolStripMenuItem_Click(sender, e);
                }
            }
            else if (GetAsyncKeyState(98) != 0)//open parse form - num 2
            {
                parseToolStripMenuItem_Click(sender, e);
            }
            else if (GetAsyncKeyState(99) != 0)//stop num 3
            {
                bStart.BackColor = Color.Red;
                tmrMain.Enabled = false;
            }
            else if (GetAsyncKeyState(103) != 0)//decrease sensitivity - num 7
            {
                nudColorVariation.Value -= 5;
            }
            else if (GetAsyncKeyState(105) != 0)//increase sensitivity - num 9
            {
                nudColorVariation.Value += 5;
            }
        }


        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyValue.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbMode.DataSource = Enum.GetValues(typeof(ModeEnum));
            this.cbMode.SelectedItem = ModeEnum.ItemPlusRecipe;
            if (selectedMode == ModeEnum.ItemPlusRecipe)
            {
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
            }
            tmrMain.Enabled = true;
            bStart.BackColor = Color.Green;
            bStart.Text = "Stop";
        }

        private void cbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newMode = selectedMode;
            Enum.TryParse<ModeEnum>(cbMode.SelectedValue.ToString(), out newMode);

            if (selectedMode != newMode)
            {
                selectedMode = newMode;
                pictureBox2.Visible = false;
                cbAddStr1.Visible = false;
                switch (selectedMode)
                {
                    case ModeEnum.ItemPlusRecipe:
                        pictureBox2.Visible = true;
                        break;
                    case ModeEnum.Perk:
                        cbAddStr1.Visible = true;
                        cbAddStr1.DataSource = Enum.GetValues(typeof(SkillEnum));
                        this.cbAddStr1.SelectedItem = SkillEnum.OneHanded;
                        break;
                    case ModeEnum.Research:
                        cbAddStr1.Visible = true;
                        cbAddStr1.DataSource = Enum.GetValues(typeof(ResearchBranchEnum));
                        this.cbAddStr1.SelectedItem = ResearchBranchEnum.Workbench;
                        break;
                }
            }
        }

        private void nudColorVariation_ValueChanged(object sender, EventArgs e)
        {
            colorVariation = (int)nudColorVariation.Value;
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmrMain.Enabled = false;
            bStart.BackColor = Color.Red;
            bStart.Text = "Start";
            var editForm = new EditCategory();
            editForm.ShowDialog();
        }

        private void subcategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmrMain.Enabled = false;
            bStart.BackColor = Color.Red;
            bStart.Text = "Start";
            var editForm = new EditSubcategory();
            editForm.ShowDialog();
        }

        private void parseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var parseForm = new ParserForm();
            parseForm.mode = selectedMode;
            parseForm.result1 = txtResult1.Text;
            parseForm.result2 = txtResult2.Text;
            parseForm.image = image;
            tmrMain.Enabled = false;
            parseForm.ShowDialog();
            tmrMain.Enabled = true;
        }

        private void propertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmrMain.Enabled = false;
            bStart.BackColor = Color.Red;
            bStart.Text = "Start";
            var editForm = new EditProperty();
            editForm.ShowDialog();
        }

        private void checkImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var counterDel = 0;
            var counterSkip = 0;
            var path = $"E:\\MOE\\icon";
            if (Directory.Exists(path))
            {
                foreach (var file in new DirectoryInfo(path).GetFiles())
                {
                    if (!GatewaysFacade.ItemGateway.CheckImageName(file.Name))
                    {
                        file.Delete();
                        counterDel++;
                    }
                    else
                    {
                        counterSkip++;
                    }
                }
                MessageBox.Show($"Deleted:{counterDel}. Skiped:{counterSkip}");
            }
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmrMain.Enabled = false;
            bStart.BackColor = Color.Red;
            bStart.Text = "Start";
            var editForm = new EditItem();
            editForm.ShowDialog();
        }

        private void autoParserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmrMain.Enabled = false;
            bStart.BackColor = Color.Red;
            bStart.Text = "Start";
            var f = new AutoParserForm();
            f.ShowDialog();
        }

        private void mapMarkerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmrMain.Enabled = false;
            bStart.BackColor = Color.Red;
            bStart.Text = "Start";
            var f = new EditMapMarker();
            f.ShowDialog();
        }

        private void chbOffline_CheckedChanged(object sender, EventArgs e)
        {
            txtResult1.Visible = txtResult2.Visible = !chbOffline.Checked;
        }
    }
}