using System;
using System.IO;

namespace Alyx {
	public class Log {

		//Used for logging information to the debug file.
		//Include a filename and number, a tag (BUG, ENHANCEMENT, etc.) and a funny message.
		public static void Write (string file, string tag, string message) {
			using (StreamWriter writer = new StreamWriter ("Debug.txt", true)) {
				writer.WriteLine ("[{0}, {1}]: {2}", file, tag, message);
			}
		}

		//Clear the Debug file and rewrite the current date and time
		public static void Clear () {
			using (StreamWriter writer = new StreamWriter ("Debug.txt", false)) {
				writer.WriteLine (DateTime.Now);
			}
		}
	}
}