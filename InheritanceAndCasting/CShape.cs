using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceAndCasting
{
    internal enum ShapeType
    {
        None,
        Point,
        Line,
        Circle,
        Rect,
        Arc
    }

    internal interface IShape
    {
        ShapeType ShapeType { get; }
    }

    internal interface IShapeColor
    {
        int R { get; set; }
        int G { get; set; }
        int B { get; set; }
        byte Alpha { get; set; }
    }

    public class ShapeColor : IShapeColor
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public byte Alpha { get; set; }

        public ShapeColor(int r, int g, int b, byte alpha)
        {
            R = r;
            G = g;
            B = b;
            Alpha = alpha;
        }
    }

    internal abstract class CShape
    {
        public abstract ShapeType ShapeType { get; }

        public Graphics graphics;

        public ShapeColor shapeColor;

        public int Id { get; private set; }

        public Point Origin { get; set; }

        public int LineWidth { get; set; } = 1;

        public static readonly Random Rnd = new Random();

        public CShape(Graphics graphics, ShapeColor shapeColor, int id)
        {
            this.graphics = graphics;
            this.shapeColor = shapeColor;
            Id = id;
        }

        public abstract void Draw(SKCanvas canvas);

        public abstract void Save();
    }
}