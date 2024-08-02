using CandytorAPI;

using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Text;


namespace CanTest {
	class Program {
		public static byte[] StringToByteArray(string hex) {
			return Enumerable.Range(0, hex.Length)
							 .Where(x => x % 2 == 0)
							 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
							 .ToArray();
		}

		static CanFrame ParseLine(string Line) {
			string[] Tokens = Line.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
			//double TimeStamp = double.Parse(Tokens[0].Replace("(", "").Replace(")", ""), CultureInfo.InvariantCulture);

			Tokens = Tokens[2].Split('#');
			uint ID = Convert.ToUInt32(Tokens[0], 16);
			byte[] Bytes = StringToByteArray(Tokens[1]);

			return new CanFrame(ID, Bytes);
		}

		static void Main(string[] args) {
			uint[] TxIDs_BusA = [
			   0x0AA,
				0x0BE,
				0x0C9, // RPM, throttle pedal
				0x0D3,
				0x0F9,
				0x110,
				0x120,
				0x170,
				0x18E,
				0x191,
				0x193,
				0x195,
				0x1A1,
				0x1A3,
				0x1BA,
				0x1BC,
				0x1BD,
				0x1C1,
				0x1C3,
				0x1C4,
				0x1C5,
				0x1CF,
				0x1D1,
				0x1DF,
				0x1E7,
				0x1ED,
				0x1EF,
				0x1F4,
				0x1F5,
				0x285,
				0x287,
				0x2C3,
				0x2C5,
				0x2D1,
				0x2D3,
				0x300,
				0x308,
				0x320,
				0x348,
				0x3C1,
				0x3D1,
				0x3D3,
				0x3DC,
				0x3E9,
				0x3F9,
				0x3FB,
				0x3FC,
				0x410,
				0x480,
				0x490,
				0x4C1,
				0x4C7,
				0x4C9,
				0x4D1,
				0x4F1,
				0x4F3,
				0x510,
				0x520,
				0x589,
				0x772
			];

			uint[] TxIDs_BusB = [
				0x91,
				0xA5,
				0xA7,
				0xA8,
				0xD9,
				0x122,
				0x183,
				0x184,
				0x187,
				0x18A,
				0x18B,
				0x18C,
				0x18D,
				0x192,
				0x1C2,
				0x1D1,
				0x1D4,
				0x3BC,
				0x489,
				0x493,
				0x4E3
			];


			string[] Lines = Directory.GetFiles("C:\\Projects\\Candytor\\can_logs2", "*.candump").SelectMany(File.ReadAllLines).ToArray();
			CanFrame[] Frames = Lines.Select(ParseLine).ToArray();


			uint[] DistinctIDs = Frames.Select(F => F.ID).Distinct().Order().ToArray();

			//File.WriteAllText("dist.txt", string.Join("\n", DistinctIDs.Select(DID => string.Format("{0:X}", DID))));

			foreach (var DID in DistinctIDs) {
				Console.WriteLine("Logging {0:X}", DID);
				LogCanIDs(DID, Frames);
			}

			Console.WriteLine("Done!");
			Console.ReadLine();
		}

		static void LogCanIDs(uint ID, CanFrame[] Frames) {
			if (ID == 0)
				return;

			StringBuilder Buf = new StringBuilder();

			Frames = Frames.Where(Frame => Frame.ID == ID).ToArray();

			/*bool AllSame = true;

			for (int i = 1; i < Frames.Length; i++) {
				if (!Frames[i - 1].IsEqual(Frames[i])) {
					AllSame = false;
					break;
				}
			}

			if (AllSame)
				return;*/

			for (int i = 0; i < Frames.Length; i++) {
				Buf.AppendLine(Frames[i].ToString());
			}

			File.WriteAllText(string.Format("canlogs2/{0:X}.log", ID), Buf.ToString());
			//Environment.Exit(0);
		}
	}
}
