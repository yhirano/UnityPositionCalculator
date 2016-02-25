using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace UnityPositionCalculator
{
    public partial class Form1 : Form
    {
        private readonly Calculator calculator = new Calculator();

        private bool calcWait = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Program.AppName + " Ver." + Application.ProductVersion;

            calcWait = true;
            {
                // Load properties data
                {
                    calculator.ScreenWidth = Properties.Settings.Default.ValueDataScreenWidth;
                    calculator.ScreenHeight = Properties.Settings.Default.ValueDataScreenHeight;
                    calculator.ImageWidth = Properties.Settings.Default.ValueDataImageWidth;
                    calculator.ImageHeight = Properties.Settings.Default.ValueDataImageHeight;
                    calculator.ImageX = Properties.Settings.Default.ValueDataImageX;
                    calculator.ImageY = Properties.Settings.Default.ValueDataImageY;
                    switch (Properties.Settings.Default.ValueDataImagePivotHol)
                    {
                        case 0:
                            calculator.ImagePivotHol = Calculator.PivotHol.Left;
                            break;
                        case 1:
                        default:
                            calculator.ImagePivotHol = Calculator.PivotHol.Mid;
                            break;
                        case 2:
                            calculator.ImagePivotHol = Calculator.PivotHol.Right;
                            break;
                    }
                    switch (Properties.Settings.Default.ValueDataImagePivotVar)
                    {
                        case 0:
                            calculator.ImagePivotVar = Calculator.PivotVar.Top;
                            break;
                        case 1:
                        default:
                            calculator.ImagePivotVar = Calculator.PivotVar.Mid;
                            break;
                        case 2:
                            calculator.ImagePivotVar = Calculator.PivotVar.Bottom;
                            break;
                    }
                }

                // Effect form
                {
                    screenWidthNumericTextBox.Text = calculator.ScreenWidth.ToString();
                    screenHeightNumericTextBox.Text = calculator.ScreenHeight.ToString();
                    imageWidthNumericTextBox.Text = calculator.ImageWidth.ToString();
                    imageHeightNumericTextBox.Text = calculator.ImageHeight.ToString();
                    imageXNumericTextBox.Text = calculator.ImageX.ToString();
                    imageYNumericTextBox.Text = calculator.ImageY.ToString();
                    switch (calculator.ImagePivotHol)
                    {
                        case Calculator.PivotHol.Left:
                            imagePiviotLeftRadioButton.Checked = true;
                            break;
                        case Calculator.PivotHol.Mid:
                        default:
                            imagePiviotHolMidRadioButton.Checked = true;
                            break;
                        case Calculator.PivotHol.Right:
                            imagePiviotRightRadioButton.Checked = true;
                            break;
                    }
                    switch (calculator.ImagePivotVar)
                    {
                        case Calculator.PivotVar.Top:
                            imagePiviotTopRadioButton.Checked = true;
                            break;
                        case Calculator.PivotVar.Mid:
                        default:
                            imagePiviotVarMidRadioButton.Checked = true;
                            break;
                        case Calculator.PivotVar.Bottom:
                            imagePiviotBottomRadioButton.Checked = true;
                            break;
                    }
                }
            }
            calcWait = false;
            Calculate();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save properties data
            {
                Properties.Settings.Default.ValueDataScreenWidth = calculator.ScreenWidth;
                Properties.Settings.Default.ValueDataScreenHeight = calculator.ScreenHeight;
                Properties.Settings.Default.ValueDataImageWidth = calculator.ImageWidth;
                Properties.Settings.Default.ValueDataImageHeight = calculator.ImageHeight;
                Properties.Settings.Default.ValueDataImageX = calculator.ImageX;
                Properties.Settings.Default.ValueDataImageY = calculator.ImageY;
                switch (calculator.ImagePivotHol)
                {
                    case Calculator.PivotHol.Left:
                        Properties.Settings.Default.ValueDataImagePivotHol = 0;
                        break;
                    case Calculator.PivotHol.Mid:
                    default:
                        Properties.Settings.Default.ValueDataImagePivotHol = 1;
                        break;
                    case Calculator.PivotHol.Right:
                        Properties.Settings.Default.ValueDataImagePivotHol = 2;
                        break;
                }
                switch (calculator.ImagePivotVar)
                {
                    case Calculator.PivotVar.Top:
                        Properties.Settings.Default.ValueDataImagePivotVar = 0;
                        break;
                    case Calculator.PivotVar.Mid:
                    default:
                        Properties.Settings.Default.ValueDataImagePivotVar = 1;
                        break;
                    case Calculator.PivotVar.Bottom:
                        Properties.Settings.Default.ValueDataImagePivotVar = 2;
                        break;
                }
                Properties.Settings.Default.Save();
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (drags.Length == 0 || !File.Exists(drags[0])) { return; }
            e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0 || !File.Exists(files[0])) { return; }

            calcWait = true;
            {
                Bitmap img;
                try
                {
                    img = new Bitmap(files[0]);
                }
                catch
                {
                    // 例外は発生したら中止
                    goto erroredBitmap;
                }
                imageWidthNumericTextBox.Text = img.Width.ToString();
                imageHeightNumericTextBox.Text = img.Height.ToString();
            }
        erroredBitmap:
            calcWait = false;
            Calculate();
        }

        private void screenWidthNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            calculator.ScreenWidth = screenWidthNumericTextBox.IntValue;
            Calculate();
        }

        private void screenHeightNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            calculator.ScreenHeight = screenHeightNumericTextBox.IntValue;
            Calculate();
        }

        private void imageWidthNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            calculator.ImageWidth = imageWidthNumericTextBox.IntValue;
            Calculate();
        }

        private void imageHeightNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            calculator.ImageHeight = imageHeightNumericTextBox.IntValue;
            Calculate();
        }

        private void imageXNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            calculator.ImageX = imageXNumericTextBox.IntValue;
            Calculate();
        }

        private void imageYNumericTextBox_TextChanged(object sender, EventArgs e)
        {
            calculator.ImageY = imageYNumericTextBox.IntValue;
            Calculate();
        }

        private void imagePiviotLeftRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            calculator.ImagePivotHol = Calculator.PivotHol.Left;
            Calculate();
        }

        private void imagePiviotHolMidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            calculator.ImagePivotHol = Calculator.PivotHol.Mid;
            Calculate();
        }

        private void imagePiviotRightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            calculator.ImagePivotHol = Calculator.PivotHol.Right;
            Calculate();
        }

        private void imagePiviotTopRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            calculator.ImagePivotVar = Calculator.PivotVar.Top;
            Calculate();
        }

        private void imagePiviotVarMidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            calculator.ImagePivotVar = Calculator.PivotVar.Mid;
            Calculate();
        }

        private void imagePiviotBottomRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            calculator.ImagePivotVar = Calculator.PivotVar.Bottom;
            Calculate();
        }

        private void loadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image File(*.png;*.jpg)|*.png;*.jpg|All File(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                calcWait = true;
                {
                    Bitmap img;
                    try
                    {
                        img = new Bitmap(ofd.FileName);
                    }
                    catch
                    {
                        // 例外は発生したら中止
                        goto erroredBitmap;
                    }
                    imageWidthNumericTextBox.Text = img.Width.ToString();
                    imageHeightNumericTextBox.Text = img.Height.ToString();
                }
            erroredBitmap:
                calcWait = false;
                Calculate();
            }
        }

        private void unityPositionXCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(unityPositionXNumericTextBox.Text);
        }

        private void unityPositionYCopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(unityPositionYNumericTextBox.Text);
        }

        private void Calculate()
        {
            if (calcWait) { return; }

            unityPositionXNumericTextBox.Text = calculator.UnityPositionX.ToString();
            unityPositionYNumericTextBox.Text = calculator.UnityPositionY.ToString();
        }
    }
}
