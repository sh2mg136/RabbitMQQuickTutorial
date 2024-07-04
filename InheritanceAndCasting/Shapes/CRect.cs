using InheritanceAndCasting.Shapes;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndCasting
{
    internal class CRect : CShape
    {
        public override ShapeType ShapeType { get => ShapeType.Rect; }
        public int Width { get; set; }
        public int Height { get; set; }

        public CRect(Graphics graphics,
            ShapeColor shapeColor,
            int lineWidth,
            int id,
            int left,
            int top,
            int width,
            int height) : base(graphics, shapeColor, id)
        {
            Origin = new Point(left, top);
            Width = width;
            Height = height;
            LineWidth = lineWidth;
        }

        public override void Draw(SKCanvas canvas)
        {
            using (SKPaint paint = new())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = LineWidth;
                paint.Color = SKColor.FromHsl(shapeColor.R, shapeColor.G, shapeColor.B, shapeColor.Alpha);
                paint.IsAntialias = true;
                SKRect rect = new SKRect(Origin.X, Origin.Y, Width + Origin.X, Height + Origin.Y);
                canvas.DrawRect(rect, paint);
            }
        }

        public override void Save()
        {
        }
    }
}