using System;
using System.IO;

namespace Alyx {
	public class Log {

		public static void Write (string file, string tag, string message) {
			using (StreamWriter writer = new StreamWriter ("Debug.txt", true)) {
				writer.WriteLine ("[{0}] {1}: {2}", file, tag, message);
			}
		}

	}
}

