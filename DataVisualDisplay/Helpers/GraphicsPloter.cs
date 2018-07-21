using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DataVisualDisplay.Helpers
{
    public class GraphicsPloter
    {
        /// <summary>
        /// 画直线段
        /// </summary>
        /// <param name="points"></param>
        /// <param name="clr"></param>
        /// <returns></returns>
        public static Path DrawLine(List<Point> points, Brush brush)
        {
            PathSegmentCollection segments = new PathSegmentCollection();
            for (int i = 1; i < points.Count; i++)
            {
                segments.Add(new LineSegment(points[i], true));
            }

            Path segment = new Path()
            {
                StrokeLineJoin = PenLineJoin.Round,
                Stroke = new SolidColorBrush(Colors.Transparent),
                Fill = brush,
                Opacity = 0.05,
                StrokeThickness = 0,
                Data = new PathGeometry()
                {
                    Figures = new PathFigureCollection()
                    {
                        new PathFigure(){IsClosed = true, IsFilled = true, StartPoint = points[0], Segments = segments}
                    }
                }
            };
            return segment;
        }

        /// <summary>
        /// 画圆点
        /// </summary>
        /// <param name="p"></param>
        /// <param name="radius"></param>
        /// <param name="clr"></param>
        /// <returns></returns>
        public static Path DrawEllipse(Point p, double radius, Brush brush)
        {
            Path segment = new Path()
            {
                StrokeLineJoin = PenLineJoin.Round,
                Stroke = brush,

                Fill = brush,
                Opacity = 0.05,
                StrokeThickness = 0.25,
                Data = new EllipseGeometry(p, radius, radius)
            };

            return segment;
        }

    }
}
