using System;
using System.IO;

namespace Alyx {
	public class Log {

		//Used for logging information to the debug file.
		//Include a filename and number, a tag (BUG, ENHANCEMENT, etc.) and a funny message.
		//Automatically pastes the date and time for you!
		public static void Write (string file, string tag, string message) {
			using (StreamWriter writer = new StreamWriter ("Debug.txt", true)) {
				writer.WriteLine ("[{0}, {1}] {2}: {3}", DateTime.Now.ToString (), file, tag, message);
			}
		}

	}
}

