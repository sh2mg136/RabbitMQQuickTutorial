using InheritanceAndCasting.Shapes;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Diagnostics.Contracts;
using System.Threading;

namespace InheritanceAndCasting
{
    public partial class Form1 : Form
    {
        private SKControl skControl;
        private Graphics graphics;
        private Brush _brush = Brushes.Gray;
        private Pen _pen = new Pen(Color.Gray, 1);
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationTokenSource _cancellationTokenSourceCircles = new CancellationTokenSource();

        private Action<Type>? Completed;

        public Form1()
        {
            InitializeComponent();

            skControl = new SKControl { Dock = DockStyle.Fill };
            this.Controls.Add(skControl);

            panel1.SendToBack();
            Txt_Console.SendToBack();
            skControl.BringToFront();

            graphics = skControl.CreateGraphics();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            skControl.PaintSurface += OnPaintSurface;
            skControl.MouseMove += (object? sender, MouseEventArgs e) =>
            {
                Txt_Coords.Text = $"{e.X} x {e.Y}";
            };

            Txt_Dimensions.Text = $"{skControl.Bounds.Width} x {skControl.Bounds.Height}";
            graphics.DrawRectangle(_pen, 0, 0, skControl.Bounds.Width, skControl.Bounds.Height);
            SetText($"\r\n{DateTime.Now:g}");

            Completed += (Type t) =>
            {
                if (t == typeof(CCircle)) { /* ... */ }

                var dt = Shapes.GroupBy(x => x.Id).Select(x => new { Id = x.Key, Qty = x.Count() }).ToList();

                if (dt.Any(x => x.Qty > 1))
                {
                    SetText("ERROR FOUND");
                }
                else SetText("NO ERRORS FOUND");
            };
        }

        private void SkControl_MouseMove(object? sender, MouseEventArgs e)
        { }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            int width = e.Info.Width;
            int height = e.Info.Height;

            canvas.Clear(SKColors.White);

            using (SKPaint paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = 1;
                paint.PathEffect = SKPathEffect.CreateDash([1, 1, 0, 0, 0, 0, 0, 0], 1);
                paint.Color = SKColors.Gray;
                // paint.IsAntialias = true;
                SKRect rect = new SKRect(10, 10, width - 10, height - 10);
                canvas.DrawRect(rect, paint);
            }

            // DrawLogo(canvas, 100, 100, 50);

            using (SKPaint paint = new SKPaint())
            {
                SKRect rect = new SKRect(10, 10, 300, 300);

                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = 1;
                paint.Color = SKColors.Gray;
                // canvas.DrawRect(rect, paint);
                // paint.StrokeWidth = 2;
                // paint.Color = SKColors.Green;
                // paint.IsAntialias = true;
                // canvas.DrawArc(rect, 0, 90, true, paint);
                // paint.Color = SKColors.Blue;
                // canvas.DrawArc(rect, -10, -45, true, paint);
                // paint.Color = SKColors.Lime;
                // canvas.DrawArc(rect, 90, 90, true, paint);
            }

            _shapes.ToList().ForEach(shape => shape.Draw(canvas));

            using (SKPaint paint = new SKPaint())
            {
                SKRect rect = new SKRect(e.Info.Width / 2 - 100, e.Info.Height / 2 - 100, e.Info.Width / 2 + 100, e.Info.Height / 2 + 100);

                paint.Style = SKPaintStyle.Stroke;
                paint.StrokeWidth = 1;
                paint.Color = SKColors.Gray;
                //canvas.DrawRect(rect, paint);

                //paint.Style = SKPaintStyle.Fill;
                //paint.StrokeWidth = 2;
                //paint.Color = SKColors.Green;
                //paint.IsAntialias = true;
                //canvas.DrawArc(rect, 0, 30, false, paint);

                while (_arcAmount < 10)
                {
                    _arcAmount++;
                    paint.Color = SKColor.FromHsl(_rnd.Next(250), _rnd.Next(250), _rnd.Next(250), (byte)_rnd.Next(250));
                    // canvas.DrawArc(rect, _arcAmount * 30, 30, false, paint);
                    // Thread.Sleep(500);
                }
            }
        }

        private int _arcAmount = 0;

        private Random _rnd = new Random();
        private List<CShape> Shapes = new List<CShape>();

        private const int MAX_SHAPES_AMOUNT = 16;
        private const int DRAW_TIMEOUT = 50;

        private Queue<CShape> _shapes = new Queue<CShape>(MAX_SHAPES_AMOUNT);

        private static int _counter = 0;
        private static readonly object lockObject = new object();

        private async void Btn_RandomShapes_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;
            Btn_RandomShapes.Enabled = false;
            Txt_Status.Text = "Processing random shapes...";
            try
            {
                await CreateShapes(ShapeType.None, token);

                await Task.Delay(10);

                Completed?.Invoke(typeof(CShape));
            }
            catch (OperationCanceledException)
            {
                Txt_Status.Text = "Operation canceled!";
            }
            finally
            {
                Txt_Status.Text = "Random shape tasks completed";
                await RemoveShapes();
                Btn_RandomShapes.Enabled = true;
            }
            skControl.Refresh();
        }

        private async void Btn_Circle_Click(object sender, EventArgs e)
        {
            Btn_Circle.Enabled = false;
            Txt_Status.Text = "Processing circles...";
            _cancellationTokenSourceCircles = new CancellationTokenSource();

            try
            {
                await CreateCircles(_cancellationTokenSourceCircles.Token);
                await Task.Delay(10);
                await RemoveShapes();
                Completed?.Invoke(typeof(CCircle));
                Txt_Status.Text = "Circles tasks completed";
            }
            catch (OperationCanceledException ex)
            {
                Txt_Status.Text = "Circle opration canceled";
            }
            finally
            {
                await RemoveShapes();
                skControl.Refresh();
                Btn_Circle.Enabled = true;
            }
        }

        private async void Btn_Rectangle_Click(object sender, EventArgs e)
        {
            Btn_Rectangle.Enabled = false;
            Txt_Status.Text = "Processing rectangles...";
            await CreateRects();
            await Task.Delay(10);
            await RemoveShapes();
            skControl.Refresh();
            Txt_Status.Text = "Rect tasks completed";
            Btn_Rectangle.Enabled = true;
            Completed?.Invoke(typeof(CRect));
        }

        private async void Btn_Lines_Click(object sender, EventArgs e)
        {
            Btn_Lines.Enabled = false;
            Txt_Status.Text = "Processing lines...";
            await CreateLines();
            await Task.Delay(10);
            await RemoveShapes();
            skControl.Refresh();
            Txt_Status.Text = "Lines tasks completed";
            Btn_Lines.Enabled = true;
            Completed?.Invoke(typeof(CLine));
        }

        private async void Btn_Arc_Click(object sender, EventArgs e)
        {
            Btn_Arc.Enabled = false;
            Txt_Status.Text = "Processing arcs...";
            await CreateArcs();
            await Task.Delay(10);
            await RemoveShapes();
            skControl.Refresh();
            Txt_Status.Text = "Arc tasks completed";
            Btn_Arc.Enabled = true;
            Completed?.Invoke(typeof(CLine));
        }

        private void SafeCanvasRefresh()
        {
            if (InvokeRequired)
            {
                Invoke(SafeCanvasRefresh);
                return;
            }

            skControl.Refresh();
        }

        private async Task RemoveShapes()
        {
            await Task.Run(() =>
            {
                while (_shapes.Count > 0)
                {
                    var shape = _shapes.Dequeue();
                    Thread.Sleep(DRAW_TIMEOUT);
                    SafeCanvasRefresh();
                }
            });
        }

        private delegate Task CreateShapeDelegate();

        private async Task CreateShapes(ShapeType shapeType, CancellationToken token)
        {
            await Task.Run(() =>
            {
                List<Task> tasks = new List<Task>();
                // Func<int[], int, Task<int>> taskFunction = (array, index) => { return Task.Run(() => array[index]); };
                CreateShapeDelegate? shapeCreator = null;
                Func<Task>? taskFunction = null;

                Enumerable.Range(1, (int)Num_TotalAmountOfParticularShape.Value).ToList().ForEach(x =>
                        {
                            if (token.IsCancellationRequested)
                            {
                                token.ThrowIfCancellationRequested();
                            }

                            switch (shapeType)
                            {
                                case ShapeType.Circle:
                                    shapeCreator = new CreateShapeDelegate(() => CreateCircle());
                                    taskFunction = () => CreateCircle();
                                    break;

                                case ShapeType.Rect:
                                    shapeCreator = new CreateShapeDelegate(() => CreateRect());
                                    taskFunction = () => CreateRect();
                                    break;

                                case ShapeType.Line:
                                    shapeCreator = new CreateShapeDelegate(() => CreateLine());
                                    taskFunction = () => CreateLine();
                                    break;

                                case ShapeType.Arc:
                                    shapeCreator = new CreateShapeDelegate(() => CreateArc());
                                    taskFunction = () => CreateArc();
                                    break;

                                case ShapeType.None:
                                default:
                                    int r = _rnd.Next(100);
                                    if (r > 30)
                                        shapeCreator = new CreateShapeDelegate(() => CreateArc());
                                    else if (r > 20)
                                        shapeCreator = new CreateShapeDelegate(() => CreateCircle());
                                    else if (r > 10)
                                        shapeCreator = new CreateShapeDelegate(() => CreateRect());
                                    else
                                        shapeCreator = new CreateShapeDelegate(() => CreateLine());
                                    break;
                            }

                            Contract.Assert(shapeCreator != null);
                            var task = shapeCreator();
                            Contract.Assert(task != null);
                            tasks.Add(task);
                            Thread.Sleep(DRAW_TIMEOUT);
                        });

                int finalCounterValue = Interlocked.CompareExchange(ref _counter, 0, 0);
                Console.WriteLine($"Final Counter Value: {finalCounterValue}");
                SetText($"All circles added. Last ID: {finalCounterValue}");
            });
        }

        private async Task CreateCircles(CancellationToken token)
        {
            await Task.Run(() =>
            {
                List<Task> tasks = new List<Task>();
                Enumerable.Range(1, (int)Num_TotalAmountOfParticularShape.Value).ToList().ForEach(x =>
                {
                    if (token.IsCancellationRequested)
                    {
                        token.ThrowIfCancellationRequested();
                    }

                    var t = CreateCircle();
                    tasks.Add(t);
                    Thread.Sleep(DRAW_TIMEOUT);
                });
                int finalCounterValue = Interlocked.CompareExchange(ref _counter, 0, 0);
                Console.WriteLine($"Final Counter Value: {finalCounterValue}");
                SetText($"All circles added. Last ID: {finalCounterValue}");
            });
        }

        private async Task CreateCircle()
        {
            lock (lockObject)
            {
                _counter++;

                var cx = _rnd.Next(20, skControl.Bounds.Width);
                var cy = _rnd.Next(20, skControl.Bounds.Height);
                var ra = _rnd.Next(20, skControl.Bounds.Height / 2);

                var lineWidth = _rnd.Next(2, 16);

                if (ra >= cx) ra = cx - lineWidth;
                if (ra >= cy) ra = cy - lineWidth;
                if (ra >= (skControl.Bounds.Width - cx)) ra = skControl.Bounds.Width - cx - lineWidth;
                if (ra >= (skControl.Bounds.Height - cy)) ra = skControl.Bounds.Height - cy - lineWidth;

                ShapeColor c = new ShapeColor(
                    _rnd.Next(250),
                    _rnd.Next(250),
                    _rnd.Next(250),
                    (byte)_rnd.Next(50, 255));

                CShape shape = new CCircle(graphics, c, new Point(cx, cy), ra, _counter) { LineWidth = lineWidth };

                Shapes.Add(shape);

                CShape first;
                if (_shapes.Count >= MAX_SHAPES_AMOUNT)
                {
                    first = _shapes.Dequeue();
                }

                _shapes.Enqueue(shape);
                SetText($"Circle added: {shape.Id}");
                skControl.Refresh();
            }

            await Task.Delay(10);
        }

        private async Task CreateRect()
        {
            lock (lockObject)
            {
                _counter++;

                var cx = _rnd.Next(20, skControl.Bounds.Width - 40);
                var cy = _rnd.Next(20, skControl.Bounds.Height - 40);
                var width = _rnd.Next(40, skControl.Bounds.Width / 2);
                var height = _rnd.Next(40, skControl.Bounds.Height / 2);

                var lineWidth = _rnd.Next(2, 16);

                if (cx + width >= skControl.Bounds.Width - lineWidth) width = skControl.Bounds.Width - cx - lineWidth;
                if (cy + height >= skControl.Bounds.Height - lineWidth) height = skControl.Bounds.Height - cy - lineWidth;

                ShapeColor c = new ShapeColor(
                    _rnd.Next(250),
                    _rnd.Next(250),
                    _rnd.Next(250),
                    (byte)_rnd.Next(50, 250));

                CShape shape = new CRect(graphics, c, lineWidth, _counter, cx, cy, width, height);

                Shapes.Add(shape);

                CShape first;
                if (_shapes.Count >= MAX_SHAPES_AMOUNT)
                {
                    first = _shapes.Dequeue();
                }

                _shapes.Enqueue(shape);
                SetText($"Rect added: {shape.Id}");
                skControl.Refresh();
            }
            await Task.Delay(10);
        }

        private async Task CreateRects()
        {
            await Task.Run(() =>
            {
                Enumerable.Range(1, (int)Num_TotalAmountOfParticularShape.Value).ToList().ForEach(x =>
                {
                    _ = CreateRect();
                    Thread.Sleep(DRAW_TIMEOUT);
                });
                int finalCounterValue = Interlocked.CompareExchange(ref _counter, 0, 0);
                Console.WriteLine($"Final Counter Value: {finalCounterValue}");
                SetText($"All rectangles added. Last ID: {finalCounterValue}");
            });
        }

        private async Task CreateLines()
        {
            await Task.Run(() =>
            {
                Enumerable.Range(1, (int)Num_TotalAmountOfParticularShape.Value).ToList().ForEach(x =>
                {
                    _ = CreateLine();
                    Thread.Sleep(DRAW_TIMEOUT);
                });
                int finalCounterValue = Interlocked.CompareExchange(ref _counter, 0, 0);
                SetText($"All lines added. Last ID: {finalCounterValue}");
            });
        }

        private async Task CreateLine()
        {
            lock (lockObject)
            {
                _counter++;

                var cx = _rnd.Next(10, skControl.Bounds.Width - 10);
                var cy = _rnd.Next(10, skControl.Bounds.Height - 10);
                var dx = _rnd.Next(10, skControl.Bounds.Width - 10);
                var dy = _rnd.Next(10, skControl.Bounds.Height - 10);

                var lineWidth = _rnd.Next(2, 16);

                ShapeColor c = new ShapeColor(
                    _rnd.Next(250),
                    _rnd.Next(250),
                    _rnd.Next(250),
                    (byte)_rnd.Next(50, 250));

                CShape shape = new CLine(graphics, c, lineWidth, _counter, cx, cy, dx, dy);

                Shapes.Add(shape);

                CShape first;
                if (_shapes.Count >= MAX_SHAPES_AMOUNT)
                {
                    first = _shapes.Dequeue();
                }

                _shapes.Enqueue(shape);
                SetText($"Line added: {shape.Id}");
                skControl.Refresh();
            }

            await Task.Delay(10);
        }

        private async Task CreateArcs()
        {
            await Task.Run(() =>
            {
                Enumerable.Range(1, (int)Num_TotalAmountOfParticularShape.Value).ToList().ForEach(x =>
                {
                    _ = CreateArc();
                    Thread.Sleep(DRAW_TIMEOUT);
                });
                int finalCounterValue = Interlocked.CompareExchange(ref _counter, 0, 0);
                SetText($"All arcs added. Last ID: {finalCounterValue}");
            });
        }

        private async Task CreateArc()
        {
            lock (lockObject)
            {
                _counter++;

                int mx = skControl.Bounds.Width / 2;
                int my = skControl.Bounds.Height / 2;

                int rx = _rnd.Next(20, mx - 5);
                int ry = _rnd.Next(20, my - 1);

                var cx = mx - rx;
                var cy = my - ry;
                var dx = mx + rx;
                var dy = my + ry;

                var lineWidth = _rnd.Next(2, 16);

                var start = _rnd.Next(360);
                var sweep = _rnd.Next(10, 90);

                ShapeColor c = new ShapeColor(
                    _rnd.Next(250),
                    _rnd.Next(250),
                    _rnd.Next(250),
                    (byte)_rnd.Next(50, 250));

                CShape shape = new CArc(graphics, c, lineWidth, _counter, cx, cy, dx, dy, start, sweep);

                Shapes.Add(shape);

                CShape first;
                if (_shapes.Count >= MAX_SHAPES_AMOUNT)
                {
                    first = _shapes.Dequeue();
                }

                _shapes.Enqueue(shape);
                SetText($"Arc added: {shape.Id}");
                skControl.Refresh();
            }

            await Task.Delay(10);
        }

        private void SetText(string msg)
        {
            if (InvokeRequired)
            {
                Invoke(SetText, msg);
                return;
            }

            Txt_Console.AppendText(msg);
            Txt_Console.AppendText(Environment.NewLine);
            Txt_Console.ScrollToCaret();
        }

        private void DrawLogo(SKCanvas canvas,
            int centerX = 100,
            int centerY = 100,
            int radius = 50)
        {
            float o = 3.1415f * (float)44.0 * 2.0f;
            float ff1 = o / 6.0f;
            float strokeWidth = 4.0f;
            // float triangleHeight = 2.0f;

            // Define the paint for the circle
            SKPaint circlePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                Color = SKColors.Blue
            };

            // Define the paint for the triangles
            SKPaint trianglePaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue
            };

            SKPath circlePath = new SKPath();

            circlePath.AddCircle(centerX, centerY, radius);

            var dashEffect = SKPathEffect.CreateDash([ff1, o / 9.0f], -18);

            circlePaint.PathEffect = dashEffect;

            canvas.DrawPath(circlePath, circlePaint);

            // Define the path for the triangles
            SKPath trianglePath = new SKPath();
            float triangleSize = 10f;
            SKPoint[] points = new SKPoint[3];
            for (int i = 0; i < 4; i++)
            {
                float angle = i * 90.0f;
                float x = centerX + radius * (float)Math.Cos(angle * Math.PI / 180.0);
                float y = centerY + radius * (float)Math.Sin(angle * Math.PI / 180.0);

                points[0] = new SKPoint(x, y - triangleSize);
                points[1] = new SKPoint(x + triangleSize, y + triangleSize);
                points[2] = new SKPoint(x - triangleSize, y + triangleSize);

                // Create rotation matrix
                SKMatrix rotationMatrix = SKMatrix.CreateRotationDegrees(angle - 90.0f, centerX, centerY);

                // Create translation matrix
                SKMatrix translationMatrix = SKMatrix.CreateTranslation(0, 0);

                // Adjust the translation matrix for each triangle
                switch (i)
                {
                    case 0: // right triangle
                        translationMatrix = SKMatrix.CreateTranslation(x - 2.0f * centerX, y - centerY + radius);
                        break;

                    case 1: // bottom triangle
                        translationMatrix = SKMatrix.CreateTranslation(x - 1.5f * centerX + radius, y - centerY - radius);
                        break;

                    case 2: // left triangle
                        translationMatrix = SKMatrix.CreateTranslation(x - centerX + 2 * radius, y - centerY + radius);
                        break;

                    case 3: // top triangle
                        translationMatrix = SKMatrix.CreateTranslation(x - centerX, y - centerY + 3 * radius);
                        break;
                }

                // Combine the rotation and translation matrices
                SKMatrix transformMatrix = SKMatrix.CreateIdentity();
                transformMatrix = transformMatrix
                    .PreConcat(rotationMatrix)
                    .PreConcat(translationMatrix);
                // SKMatrix.PreConcat(ref transformMatrix, rotationMatrix);
                // SKMatrix.PreConcat(ref transformMatrix, translationMatrix);

                // Apply the transform matrix to the triangle path
                trianglePath.Reset();
                trianglePath.AddPoly(points);
                trianglePath.Transform(transformMatrix);

                // Draw the triangle
                canvas.DrawPath(trianglePath, trianglePaint);
            }
        }

        private async void Btn_Cancel_Click(object sender, EventArgs e)
        {
            await Task.Run(() => { _cancellationTokenSource?.Cancel(); });

            await Task.Run(() => { _cancellationTokenSourceCircles?.Cancel(); });
        }
    }
}