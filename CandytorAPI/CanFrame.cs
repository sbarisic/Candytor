using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandytorAPI
{
    public record class CanMessage(uint ID, byte[] Bytes, DateTime TimeReceived);

    public class CanFrame
    {
        public uint ID { get; set; }

        public int DLC { get; set; }

        public string Name { get; set; }

        [Browsable(false)]
        public byte[] Bytes { get; set; }

        public string BytesView
        {
            get
            {
                if (Bytes == null)
                    return "NULL";

                return string.Join(" ", Bytes.Select(X => string.Format("{0:X2}", X)));
            }
        }

        public int Frequency { get; set; }

        public int Count { get; set; }

        public CanFrame(uint ID)
        {
            this.ID = ID;
        }


    }
}
