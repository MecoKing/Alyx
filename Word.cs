using System;
using System.Collections.Generic;
using System.IO;

namespace Alyx {
	public class Word {

		public string name;
		public string tags;
		public string phonetic;

		/// <summary> Arranges the tags associated with this word in an array. </summary>
		public string[] getTags { get {
				List<string> myTags = new List<string> ();
				string individual = ""; 
				for (int i = 0; i < tags.Length; i++) {
					if (tags [i] == ' ') {
						if (individual != "")
							myTags.Add (individual);
						individual = "";
					} else if (i == tags.Length - 1) {
						individual += tags [i];
						myTags.Add (individual);
						individual = "";
					} else
						individual += tags [i];
				}
				return myTags.ToArray ();
			}
		}

		/// <summary> Creates a new word object from a given script. </summary>
		/// <param name="script">name: tag anotherTag nextTag ...</param>
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

		/// <summary> Returns an array of words that have some of the given tags.
		/// Adding '&' before a tag ensures that all previously tagged words also contain the current tag. </summary>
		public static Word[] wordsTaggedFromCollection (Word[] collection, params string[] searchTags) {
			List<Word> taggedWords = new List<Word> ();
			foreach (string tag in searchTags) {
				foreach (Word term in collection) {
					if (tag.StartsWith ("&")) {
						if (taggedWords.Contains (term) && !term.tags.Contains (tag.Substring (1, tag.Length - 2)))
							taggedWords.Remove (term);
					} else {
						//Should tags stack up if meeting more than one search?
						if (term.tags.Contains (tag) && !taggedWords.Contains (term))
							taggedWords.Add (term);
					}
				}
			}
			return taggedWords.ToArray ();
		}

		/// <summary> Gets a word with the given name from the collection if it exists. </summary>
		public static Word fromCollection (string term, Word[] collection) {
			foreach (Word word in collection) {
				if (word.name == term)
					return word;
			}
			return null;
		}

		/// <summary> Loads all the words from Vocab.txt into the vocabulary array. </summary>
		public static void loadWords () {
			using (StreamReader reader = new StreamReader ("Vocab.txt")) {
				string line;
				while ((line = reader.ReadLine ()) != null) {
					Program.vocab.Add (new Word (line));
				}
			}
		}

		/// <summary> Adds a term to the list of unknown words. </summary>
		public static void addUnknown (string term) {
			using (StreamWriter writer = new StreamWriter ("UnknownWords.txt", true)) {
				writer.WriteLine (term);
			}
		}



	}
}