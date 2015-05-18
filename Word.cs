using System;
using System.Collections.Generic;
using System.IO;

namespace Alyx {
	public class Word {

		public string name;
		public string tags;
		public string phonetic;

		//A script will look like this:
		//    name: tag anotherTag thirdTag moarTags
		public Word (string script) {
			string subScript = "";
			for (int i = 0; i < script.Length; i++) {
				if (script [i] == ':') {
					name = subScript;
					subScript = "";
				} else if (i == script.Length - 1) {
					subScript += script [i];
					tags = subScript;
					subScript = "";
				} else
					subScript += script [i];
			}
			phonetic = Phonetic.generatePhoneticsFor (name);
		}

		//Returns a collection of words that have the given tags.
		//Add '&' to the start of a searchTag to get tags out of the previous that only have that tag
		//i.e. noun, animal, &domestic gets all the nouns and all the animals and returns only the ones that are also domestic
		//whereas noun, animal, domestic returns all the noun, animal and domestic words.
		public static Word[] wordsTaggedFromCollection (Word[] collection, params string[] searchTags) {
			List<Word> taggedWords = new List<Word> ();
			foreach (string tag in searchTags) {
				foreach (Word term in collection) {
					if (tag.StartsWith ("&")) {
						if (taggedWords.Contains (term) && !term.tags.Contains (tag.Substring (1, tag.Length - 2)))
							taggedWords.Remove (term);
					} else {
						if (term.tags.Contains (tag) && !taggedWords.Contains (term))
							taggedWords.Add (term);
					}
				}
			}
			return taggedWords.ToArray ();
		}

		//Gets a word with the same name as the given string from a collection
		//If no such word exists, returns null (Watch out for that!)
		public static Word fromCollection (string term, Word[] collection) {
			foreach (Word word in collection) {
				if (word.name == term)
					return word;
			}
			return null;
		}

		//Loads all the words from the dictionary file Vocab.txt into the game
		public static void loadWords () {
			using (StreamReader reader = new StreamReader ("Vocab.txt")) {
				string line;
				while ((line = reader.ReadLine ()) != null) {
					Program.vocab.Add (new Word (line));
				}
			}
		}

		public static void addUnknown (string term) {
			using (StreamWriter writer = new StreamWriter ("UnknownWords.txt", true)) {
				writer.WriteLine (term);
			}
		}

	}
}