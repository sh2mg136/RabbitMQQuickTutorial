using SkiaSharp;

namespace InheritanceAndCasting.Shapes
{
    internal class CArc : CShape
    {
        public override ShapeType ShapeType { get => ShapeType.Arc; }
        public Point Finish { get; set; }
        private float StartAngle { get; set; }
        private float SweepAngle { get; set; }

        public CArc(Graphics graphics,
            ShapeColor shapeColor,
            int lineWidth,
            int id,
            int left,
            int top,
            int right,
            int bottom,
            float startAngle,
            float sweepAngle) : base(graphics, shapeColor, id)
        {
            LineWidth = lineWidth;
            Origin = new Point(left, top);
            Finish = new Point(right, bottom);
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
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
                SKRect rect = new SKRect(Origin.X, Origin.Y, Finish.X, Finish.Y);
                canvas.DrawArc(rect, StartAngle, SweepAngle, false, paint);
            }
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}