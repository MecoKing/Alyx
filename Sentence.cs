using System;

namespace Alyx {
	public class Sentence {

		public Word[] words;
		public string[] tags;

		public Sentence (string phrase) {
			Console.WriteLine (reformat (phrase));
		}

		public string reformat (string fragment) {
			string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string abc = "abcdefghijklmnopqrstuvwxyz";
			string formatted = "";
			for (int i = 0; i < fragment.Length; i++) {
				if (i == 0) {
					if (abc.Contains (fragment [i].ToString ()))
						formatted += ABC [abc.IndexOf (fragment [i])];
				} else if (fragment [i - 1] == ' ') {
					if (abc.Contains (fragment [i].ToString ()))
						formatted += ABC [abc.IndexOf (fragment [i])];
				} else if (ABC.Contains (fragment [i].ToString ()))
					formatted += abc [ABC.IndexOf (fragment [i])];
				else if (abc.Contains (fragment [i].ToString ()))
					formatted += fragment [i];
				else if (fragment [i] == ' ')
					formatted += " ";
			}
			return formatted;
		}
	}
}

