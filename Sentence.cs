using System;
using System.Collections.Generic;

namespace Alyx {
	public class Sentence {

		public Word[] words;
		public string[] tags;

		//Creates a new sentence of seperate known words and frequent tags from a given phrase
		public Sentence (string phrase) {
			words = individualWords (reformat (phrase));
			foreach (Word term in words)
				Console.Write ("{0} ", term.name);
			Console.WriteLine ();
		}

		//Reformats the words in the phrase string
		//    Switches capitals so the computer can read
		//    Removes illegal characters
		public string reformat (string phrase) {
			string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string abc = "abcdefghijklmnopqrstuvwxyz";
			string illegal = "@#$%^&*()+=-|{}[]/><~_";
			string formatted = "";
			for (int i = 0; i < phrase.Length; i++) {
				if (i == 0 || phrase [i - 1] == ' ') {
					if (abc.Contains (phrase [i].ToString ()))
						formatted += ABC [abc.IndexOf (phrase [i])];
					else if (!illegal.Contains (phrase [i].ToString ()))
						formatted += phrase [i];
				} else if (ABC.Contains (phrase [i].ToString ()))
					formatted += abc [ABC.IndexOf (phrase [i])];
				else if (!illegal.Contains (phrase [i].ToString ()))
					formatted += phrase [i];
			}
			return formatted;
		}

		//Seperates the phrase into a collection of known words.
		//Ignores unknown words.
		public Word[] individualWords (string phrase) {
			List<Word> terms = new List<Word> ();
			string nonWordChars = " ,.?:;!";
			string substring = "";
			for (int i = 0; i < phrase.Length; i++) {
				if (nonWordChars.Contains (phrase [i].ToString ())) {
					Word newTerm = Word.fromCollection (substring, Program.vocab.ToArray ());
					if (newTerm != null)
						terms.Add (newTerm);
					substring = "";
				} else if (i == phrase.Length - 1) {
					substring += phrase [i];
					Word newTerm = Word.fromCollection (substring, Program.vocab.ToArray ());
					if (newTerm != null)
						terms.Add (newTerm);
					substring = "";
				} else
					substring += phrase [i];
			}
			return terms.ToArray ();
		}

	}
}