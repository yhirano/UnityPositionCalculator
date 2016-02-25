﻿using System;
using System.Globalization;
using System.Windows.Forms;

namespace UnityPositionCalculator
{
    // See https://msdn.microsoft.com/ja-jp/library/ms229644(v=vs.80).aspx
    public class NumericTextBox : TextBox
    {
        bool allowSpace = false;

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            //if (Char.IsDigit(e.KeyChar))
            if ('0' <= e.KeyChar && e.KeyChar <= '9')
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) || keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            //else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            //{
            //    // Let the edit control handle control and alt key combinations
            //}
            else if (this.allowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                //MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                int result;
                if (Int32.TryParse(this.Text, out result))
                {
                    return result;
                } else
                {
                    return 0;
                }
            }
        }

        public decimal DecimalValue
        {
            get
            {
                decimal result;
                if (Decimal.TryParse(this.Text, out result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }
    }
}
