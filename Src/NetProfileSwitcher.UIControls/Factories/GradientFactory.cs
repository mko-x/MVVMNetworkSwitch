using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NetProfileSwitcher.UIControls
{
    public enum LinearBaseDirection{
        HorizontalLeftToRight,
        VerticalTopToBottom,
        DiagonalUpperLeftToLowerRight
    }

    public class GradientFactory
    {
        private GradientFactory()
        {
        }

        #region - public detail creators
        /// <summary>
        /// Creates simple two color blackAndWhite gradients
        /// </summary>
        /// <param name="startColor"></param>
        /// <param name="endColor"></param>
        /// <param name="direction"></param>
        /// <param name="invert"></param>
        /// <returns></returns>
        public static LinearGradientBrush CreateLinear(Color startColor, Color endColor, LinearBaseDirection direction = LinearBaseDirection.HorizontalLeftToRight, bool invert = false)
        {
            GradientFactory innerFactory = new GradientFactory();

            if (startColor == null)
            {
                startColor = Colors.White;
            }

            if (endColor == null)
            {
                endColor = Colors.Gray;
            }

            if (invert)
            {
                innerFactory.swapColors(ref startColor, ref endColor);
            }

            double angle = innerFactory.createAngleFromLinearBaseDirection(direction);

            return innerFactory.doCreateLinear(startColor, endColor, angle);
        }

        /// <summary>
        /// Create a blackAndWhite gradient with a set of GradientStops
        /// </summary>
        /// <param name="gradientStopCollection"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static LinearGradientBrush CreateLinear(GradientStopCollection gradientStopCollection, LinearBaseDirection direction = LinearBaseDirection.HorizontalLeftToRight)
        {
            GradientFactory innerFactory = new GradientFactory();

            double angle = innerFactory.createAngleFromLinearBaseDirection(direction);
            LinearGradientBrush result = new LinearGradientBrush(gradientStopCollection,angle);

            return result;
        }
        #endregion

        #region - public predefined creators

        public static LinearGradientBrush PredefinedElegantBlack
        {
            get
            {
                LinearGradientBrush elegantBlack = new LinearGradientBrush();
                elegantBlack.StartPoint = new Point(0, 0);
                elegantBlack.EndPoint = new Point(1, 1);
                elegantBlack.SpreadMethod = GradientSpreadMethod.Pad;
                elegantBlack.ColorInterpolationMode = ColorInterpolationMode.ScRgbLinearInterpolation;
                elegantBlack.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x65, 0x65, 0x64), 0));
                elegantBlack.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x00, 0x00, 0x00), 1));
                return elegantBlack;
            }
        }

        public static LinearGradientBrush PredefinedBlackAndWhite
        {
            get
            {
                LinearGradientBrush blackAndWhite = new LinearGradientBrush();
                blackAndWhite.StartPoint = new Point(0, 0);
                blackAndWhite.EndPoint = new Point(1, 1);
                blackAndWhite.SpreadMethod = GradientSpreadMethod.Pad;
                blackAndWhite.ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;
                blackAndWhite.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF), 0));
                blackAndWhite.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x00, 0x00, 0x00), 1));
                return blackAndWhite;
            }
        }

        #endregion

        #region - internal methods
        private LinearGradientBrush doCreateLinear(Color startColor, Color endColor, double angle = 0.0)
        {
            LinearGradientBrush brushResult = new LinearGradientBrush(startColor, endColor, angle);

            return brushResult;
        }

        private void swapColors(ref Color startColor, ref Color endColor)
        {
            Color swapContainer = endColor;
            endColor = startColor;
            startColor = swapContainer;
        }

        private double createAngleFromLinearBaseDirection(LinearBaseDirection direction)
        {
            //the base value is LinearBaseDirection.HorizontalLeftToRight
            double result = angleHorizontalLeftToRight;
            switch (direction)
            {
                case LinearBaseDirection.VerticalTopToBottom: result = angleVerticalTopToBottom;
                    break;
                case LinearBaseDirection.DiagonalUpperLeftToLowerRight: result = angleDiagonalUpperLeftToLowerRight;
                    break;                
            }

            return result;
        }
        #endregion

        #region - internal LinearBaseDirection to angle 

        private double angleHorizontalLeftToRight = 0.0;
        private double angleVerticalTopToBottom = 90.0;
        private double angleDiagonalUpperLeftToLowerRight = 45.0;

        #endregion
    }
}
