using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndCasting
{
    internal class CLine : CShape
    {
        public override ShapeType ShapeType { get => ShapeType.Line; }

        public Point Finish { get; set; }

        public CLine(Graphics graphics,
            ShapeColor shapeColor,
            int lineWidth,
            int id,
            int left,
            int top,
            int right,
            int bottom) : base(graphics, shapeColor, id)
        {
            LineWidth = lineWidth;
            Origin = new Point(left, top);
            Finish = new Point(right, bottom);
        }

        public override void Draw(SKCanvas canvas)
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = LineWidth;
                // paint.PathEffect = SKPathEffect.CreateDash([1, 1, 0, 0], 1);
                paint.Color = SKColor.FromHsl(shapeColor.R, shapeColor.G, shapeColor.B, shapeColor.Alpha);
                paint.IsAntialias = true;
                canvas.DrawLine(Origin.X, Origin.Y, Finish.X, Finish.Y, paint);
            }
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}