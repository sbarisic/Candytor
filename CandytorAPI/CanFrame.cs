using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandytorAPI {
	public record class CanMessage(uint ID, byte[] Bytes, DateTime TimeReceived);

	public class CanFrame {
		public uint ID {
			get; set;
		}

		public int DLC {
			get; set;
		}

		public string Name {
			get; set;
		}

		[Browsable(false)]
		public byte[] Bytes {
			get; set;
		}

		public string BytesView {
			get {
				if (Bytes == null)
					return "NULL";

				return string.Join(" ", Bytes.Select(X => string.Format("{0:X2}", X)));
			}
		}

		public int Frequency {
			get; set;
		}

		public int Count {
			get; set;
		}

		public CanFrame(uint ID) {
			this.ID = ID;
		}

		public CanFrame(uint ID, byte[] Bytes) : this(ID) {
			this.Bytes = Bytes;
			this.DLC = Bytes.Length;
		}

		public bool IsEqual(CanFrame Other) {
			if (Other.ID != ID)
				return false;

			if (Other.DLC != DLC)
				return false;

			for (int i = 0; i < DLC; i++) {
				if (Other.Bytes[i] != Bytes[i])
					return false;
			}

			return true;
		}

		public override string ToString() {
			return string.Format("{0:X4} - {1}", ID, BytesView).ToUpper();
		}
	}
}
