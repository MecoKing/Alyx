using System;
using System.Collections.Generic;

namespace Alyx {
	public class Simplifier {

		static Dictionary<string, string[]> simplifications = new Dictionary<string, string[]> () {
			//CONJUNCTIONS
			{"What Is", new string[] {"What's"}},
			{"It Is", new string[] {"It's"}},
			{"Do Not", new string[] {"Don't"}},
			{"Was Not", new string[] {"Wasn't"}},
			//GROUPS
			{"Be", new string[] {"Am", "Was", "Is", "Are", "Were", "Being", "Been"}},
			//REVERSALS
			{"Good", new string[] {"Not Bad", "Not So Bad", "Not Too Bad", "Not Half Bad"}},
			{"Alright", new string[] {"Not Awful"}}
		};

		static string makeInfinitive (string word) {
			string notVerbs = "Morning Evening";
			if (word.EndsWith ("ed") && !notVerbs.Contains (word))
				return word.Remove (word.Length - 2, 2);
			else if (word.EndsWith ("ing") && !notVerbs.Contains (word))
				return word.Remove (word.Length - 3, 3);
			else
				return word;
		}

		static string makeNonPossessive (string word) {
			Dictionary <string, string> exceptions = new Dictionary<string, string> () {
				{"His", "Him"}, {"Hers", "Her"}, {"Their", "They"}, {"Mine", "Me"}, {"Your", "You"}
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
			string notPlural = "Was Analysis This Success Always";
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

		public static string simplify (string input) {
			//Try simplifying
			foreach (string key in simplifications.Keys) {
				foreach (string term in simplifications [key]) {
					if (input.Contains (term)) {
						input = input.Replace (term, key);
						break;
					}
				}
			}
			//Get rid of any plural or possessive nouns
			string substring = "";
			for (int ind = 0; ind < input.Length; ind++) {
				string wordStops = " .,:;?!";
				if (wordStops.Contains (input [ind].ToString ()) && substring != "") {
					input = input.Replace (substring, makeNonPossessive (substring));
					input = input.Replace (substring, makeSingular (substring));
					input = input.Replace (substring, makeInfinitive (substring));
					substring = "";
				} else if (ind == input.Length - 1) {
					substring += input [ind];
					input = input.Replace (substring, makeNonPossessive (substring));
					input = input.Replace (substring, makeSingular (substring));
					input = input.Replace (substring, makeInfinitive (substring));
					substring = "";
				} else {
					substring += input [ind];
				}
			}
			//Try simplifying again...
			foreach (string key in simplifications.Keys) {
				foreach (string term in simplifications [key]) {
					if (input.Contains (term)) {
						input = input.Replace (term, key);
						break;
					}
				}
			}


//			for (int pos = 0; pos < input.words.Length; pos++) {
//				input.words [pos] = new Word (makeNonPossessive (input.words [pos].name) + ": " + input.words [pos].tags);
//				input.words [pos] = new Word (makeSingular (input.words [pos].name) + ": " + input.words [pos].tags);
//				
//			}
			return input;
		}
	}
}

