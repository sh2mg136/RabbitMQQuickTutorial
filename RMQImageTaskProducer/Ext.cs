using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMQImageTaskProducer
{
    internal static class Ext
    {
        private static Random random = new Random();

        public static T? GetRandomEnumValue<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            Contract.Assert(values != null);
            int randomIndex = random.Next(values.Length);
            Contract.Assume(values != null);
            return (T?)values.GetValue(randomIndex);
        }
    }
}