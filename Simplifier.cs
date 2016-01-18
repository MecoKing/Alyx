using System;
using System.Collections.Generic;

namespace Alyx {
	public class Simplifier {

		static Dictionary<string, string[]> simplifications = new Dictionary<string, string[]> () {
			{"Be", new string[] {"Am", "Was", "Is", "Are", "Were", "Being", "Been"}},
		};

		static string makeNonPossessive (string word) {
			Dictionary <string, string> exceptions = new Dictionary<string, string> () {
				{"His", "Him"}
			};
			if (exceptions.ContainsKey (word))
				return exceptions [word];
			else if (word.EndsWith ("'s"))
				return word.Remove (word.Length - 2, 2);
			else if (word.EndsWith ("'"))
				return word.Remove (word.Length - 1, 1);
			else
				return word;
		}

		static string makeSingular (string word) {
			string notPlural = "Was Analysis";
			Dictionary <string, string> exceptions = new Dictionary<string, string> () {
				{"Mice", "Mouse"}
			};
			if (exceptions.ContainsKey (word))
				return exceptions [word];
			else if (word.EndsWith ("s") && !notPlural.Contains (word))
				return word.Remove (word.Length - 1, 1);
			else
				return word;
		}

		public static Sentence simplify (Sentence input) {
			for (int pos = 0; pos < input.words.Length; pos++) {
				input.words [pos] = new Word (makeNonPossessive (input.words [pos].name) + ": " + input.words [pos].tags);
				input.words [pos] = new Word (makeSingular (input.words [pos].name) + ": " + input.words [pos].tags);
				foreach (string key in simplifications.Keys) {
					foreach (string term in simplifications [key]) {
						if (input.words [pos].name == term) {
							input.words [pos] = new Word (key + ": " + input.words [pos].tags);
							break;
						}
					}
				}
			}
			return input;
		}
	}
}

