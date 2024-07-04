﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQImageTaskProducer
{
    internal class TaskInfo : IRmqTaskInfo.IRmqTaskInfo
    {
        public TaskInfo(int id, string message)
        {
            Id = id;
            Message = message;
        }

        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}