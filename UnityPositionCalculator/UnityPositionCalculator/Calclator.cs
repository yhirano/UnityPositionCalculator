using System;

namespace UnityPositionCalculator
{
    class Calculator
    {
        public enum PivotHol
        {
            Left, Mid, Right
        }

        public enum PivotVar
        {
            Top, Mid, Bottom
        }

        public float ScreenWidth { get; set; }

        public float ScreenHeight { get; set; }

        public float ImageWidth { get; set; }

        public float ImageHeight { get; set; }

        public float ImageX { get; set; }

        public float ImageY { get; set; }

        public PivotHol ImagePivotHol { get; set; }

        public PivotVar ImagePivotVar { get; set; }

        public float UnityPositionX
        {
            get
            {
                float result = ImageX - (ScreenWidth / 2f);
                switch (ImagePivotHol)
                {
                    case PivotHol.Left:
                        break;
                    case PivotHol.Mid:
                    default:
                        result += ImageWidth / 2f;
                        break;
                    case PivotHol.Right:
                        result += ImageWidth;
                        break;
                }
                return result;
            }
        }

        public float UnityPositionY
        {
            get
            {
                float result = ImageY - (ScreenHeight / 2f);
                switch (ImagePivotVar)
                {
                    case PivotVar.Top:
                        break;
                    case PivotVar.Mid:
                    default:
                        result += ImageHeight / 2f;
                        break;
                    case PivotVar.Bottom:
                        result += ImageHeight;
                        break;
                }
                result *= -1;
                return result;
            }
        }

        public Calculator()
        {
            ScreenWidth = 1920;
            ScreenHeight = 1128;
            ImageWidth = 100;
            ImageHeight = 100;
            ImageX = 0;
            ImageY = 0;
            ImagePivotHol = PivotHol.Mid;
            ImagePivotVar = PivotVar.Mid;
        }
    }
}
