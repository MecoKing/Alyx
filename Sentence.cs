using System;

namespace Alyx {
	public class Sentence {

		public Word[] words;
		public string[] tags;

		public Sentence (string phrase) {
			Console.WriteLine (reformat (phrase));
		}

		public string reformat (string phrase) {
			string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string abc = "abcdefghijklmnopqrstuvwxyz";
			string formatted = "";
			for (int i = 0; i < phrase.Length; i++) {
				if (i == 0 || phrase [i - 1] == ' ') {
					if (abc.Contains (phrase [i].ToString ()))
						formatted += ABC [abc.IndexOf (phrase [i])];
					else if (!"@#$%^&*()+=-|{}[]/><~_".Contains (phrase [i].ToString ()))
						formatted += phrase [i];
				} else if (ABC.Contains (phrase [i].ToString ()))
					formatted += abc [ABC.IndexOf (phrase [i])];
				else if (!"@#$%^&*()+=-|{}[]/><~_".Contains (phrase [i].ToString ()))
					formatted += phrase [i];
			}
			return formatted;
		}

		public void seperateWords (string phrase) {
			
		}

	}
}

