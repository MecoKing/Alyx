using System;
using System.Collections.Generic;
using System.IO;

namespace Alyx {
	public class Word {

		string name;
		string tags;
		string phonetic;

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

		// |tag = OR includes tag
		// &tag = AND includes tag
		// wordsTaggedFromCollection (Program.vocab, "noun", "&mammal", "&animal");
		public static Word[] wordsTaggedFromCollection (Word[] collection, params string[] searchTags) {
			List<Word> taggedWords = new List<Word> ();
			foreach (string tag in searchTags) {
				foreach (Word term in collection) {
					if (tag.StartsWith ("|") || !tag.StartsWith ("&")) {
						if (term.tags.Contains (tag.Remove (0, 1)))
							taggedWords.Add (term);
					} else if (tag.StartsWith ("&")) {
						if (taggedWords.Contains (term)) {
							if (!term.tags.Contains (tag.Remove (0, 1)))
								taggedWords.Remove (term);
						}
					}
				}
			}
			return taggedWords.ToArray ();
		}

		public static void loadWords () {
			using (StreamReader reader = new StreamReader ("Vocab.txt")) {
				string line;
				while ((line = reader.ReadLine ()) != null) {
					Program.vocab.Add (new Word (line));
				}
			}
		}

	}
}

