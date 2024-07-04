using RabbitMQ.Client;
using RMQImageTasks;
using System.Diagnostics.Contracts;
using System.Text;
using System.Text.Json;

namespace RMQImageTaskProducer
{
    public partial class Form1 : Form
    {
        private bool _work = false;
        private readonly ConnectionFactory _factory;
        private readonly Bogus.Faker _faker;

        public Form1()
        {
            InitializeComponent();

            _factory = new ConnectionFactory() { HostName = "localhost" };

            Contract.Requires(_factory != null);
            Contract.Assert(_factory != null);

            _faker = new Bogus.Faker();
            Contract.Requires(_faker != null);
            Contract.Assert(_faker != null);
        }

        private Queue<TaskInfo> _queue = new Queue<TaskInfo>();
        private ReaderWriterLock writerLock = new ReaderWriterLock();

        private RpcClient rpcClient = new RpcClient();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Image processing sevice :)
            timer1.Tick += ((object? sender, EventArgs e) =>
            {
                DoWork();
            });

            // UpperCaseService :)
            timer2.Tick += Timer2_Tick;

            rpcClient.OnCompleted += (t, s) => SetText($"Response: {s}");
        }

        private void Timer1_Tick(object? sender, EventArgs e) { }

        private async void Timer2_Tick(object? sender, EventArgs e)
        {
            var num_val = (int)Num_UpperCaseTaskMax.Value;
            if (num_val > 0)
            {
                var amount = _faker.Random.Int(1, num_val);
                for (int i = 0; i < amount; i++)
                {
                    var id = _faker.Random.Int(1, 9999999);
                    await SendUpperCaseTask(id, _faker.Lorem.Word());
                }
            }
        }

        private async Task SendUpperCaseTask(int id, string word)
        {
            await Task.Run(() =>
            {
                TaskInfo ucTask = new TaskInfo(id, word);
                var response = rpcClient.Call(ucTask);
                // SetText($"Response: {response}");
            });
        }

        private void SetControlState(bool enabled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetControlState(enabled)));
                return;
            }

            Btn_StartStop.Text = enabled ? "Stop" : "Start";
            Txt_Status.Text = enabled ? "Running..." : "Stopped";
        }

        private void Btn_StartStop_Click(object sender, EventArgs e)
        {
            if (_work)
            {
                timer1.Stop();
                timer2.Stop();
            }
            else
            {
                if (Chk_ImageServiceEnabled.Checked)
                    timer1.Start();
                if (Chk_UpperCaseServiceEnabled.Checked)
                    timer2.Start();
            }
            _work = !_work;
            SetControlState(_work);
        }

        private static readonly Random _rnd = new Random();
        private static int _counter = 0;
        private static readonly object lockObject = new object();

        private List<ImageTask> CreateTasks(int amount = 1)
        {
            List<ImageTask> result = new List<ImageTask>(5);

            // Interlocked.Increment(ref _counter);

            lock (lockObject)
            {
                for (int i = 0; i < amount; i++)
                {
                    _counter++;

                    result.Add(new ImageTask
                    {
                        Id = _counter,
                        ImagePath = $"/{_faker.Commerce.ProductName()}/{_faker.Commerce.Ean13()}.jpg",
                        TaskType = _rnd.Next(10) < 5 ? TaskTypes.RESIZE : TaskTypes.WATERMARK
                    });
                }
            }

            int finalCounterValue = Interlocked.CompareExchange(ref _counter, 0, 0);
            Console.WriteLine($"Final Counter Value: {finalCounterValue}");

            return result;
        }

        private void SetText(string message)
        {
            if (InvokeRequired)
            {
                Invoke(SetText, message);
                return;
            }

            richTextBox1.AppendText(message);
            richTextBox1.AppendText(Environment.NewLine);
            richTextBox1.ScrollToCaret();
        }

        private void DoWork()
        {
            Contract.Requires(_factory != null);
            Contract.Assert(_factory != null);

            var cnt = _rnd.Next(2, 20);
            var tasks = CreateTasks(cnt);

            SetText($"New Job Package: {tasks.Count}");

            tasks.ForEach(x => SetText(x.ToString()));

            SetText("");

            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "image_tasks",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                foreach (var imageTask in tasks)
                {
                    var message = JsonSerializer.Serialize(imageTask);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "image_tasks",
                                         basicProperties: properties,
                                         body: body);

                    // Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private void Num_UpperCaseTaskMax_ValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Focus();
        }
    }
}