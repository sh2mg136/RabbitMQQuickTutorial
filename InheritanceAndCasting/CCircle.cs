using SkiaSharp;

namespace InheritanceAndCasting
{
    internal class CCircle : CShape
    {
        public override ShapeType ShapeType { get => ShapeType.Circle; }
        public int Radius { get; set; }
        private Pen _pen = new Pen(Color.Black);

        public CCircle(Graphics graphics, ShapeColor shapeColor, int id) : base(graphics, shapeColor, id)
        {
            Origin = new Point(0, 0);
            Radius = 0;
        }

        public CCircle(Graphics graphics, Point center, ShapeColor shapeColor, int id) : base(graphics, shapeColor, id)
        {
            Origin = center;
            Radius = 0;
        }

        public CCircle(Graphics graphics, ShapeColor shapeColor, Point center, int radius, int id)
            : base(graphics, shapeColor, id)
        {
            Origin = center;
            Radius = radius;
        }

        public override void Draw(SKCanvas canvas)
        {
            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = LineWidth;
                // paint.PathEffect = SKPathEffect.CreateDash([1, 1, 0, 0], 1);
                // paint.Color = SKColor.FromHsl(Rnd.Next(250), Rnd.Next(250), Rnd.Next(250), (byte)Rnd.Next(50, 255));
                paint.Color = SKColor.FromHsl(shapeColor.R, shapeColor.G, shapeColor.B, shapeColor.Alpha);
                paint.IsAntialias = true;
                canvas.DrawCircle(Origin.X, Origin.Y, Radius, paint);
            }

            // graphics.DrawCircle(_pen, Center.X, Center.Y, Radius);
        }

        public override void Save()
        {
        }
    }
}