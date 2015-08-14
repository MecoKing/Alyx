using System;
using System.Collections.Generic;

namespace Alyx {
	public class Sentence {

		string[] sentenceModels = new string[] {
			//Verb tags must start with a capital or adverbs will be mistaken for verbs!
			"article adjective adjective noun Verb adverb article adjective noun",
			"pronoun adverb adjective noun adverb Verb adjective noun",
			"pronoun Verb adverb preposition article adjective noun",
			"exclamation adjective preposition Verb pronoun",
			"pronoun Verb adverb",
		};


		public Word[] words;
		public string[] tags;

		//Creates a new sentence of seperate known words and frequent tags from a given phrase
		public Sentence (string phrase) {
			words = individualWords (reformat (phrase));
			tags = commonTags (tagFrequencies (), 4);

			foreach (Word term in words)
				Console.Write ("{0} ", term.name);
			Console.WriteLine ();
			foreach (string tag in tags)
				Console.Write ("{0} ", tag);
			Console.WriteLine ();
				foreach (Word term in words)
					Console.Write ("{0} ", term.phonetic);
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
					else
						Word.addUnknown (substring);
					substring = "";
				} else if (i == phrase.Length - 1) {
					substring += phrase [i];
					Word newTerm = Word.fromCollection (substring, Program.vocab.ToArray ());
					if (newTerm != null)
						terms.Add (newTerm);
					else
						Word.addUnknown (substring);
					substring = "";
				} else
					substring += phrase [i];
			}
			return terms.ToArray ();
		}

		//Determines the frequency of tags showing up in the words array
		public Dictionary<string, int> tagFrequencies () {
			Dictionary <string, int> tagCounter = new Dictionary<string, int> ();
			foreach (Word word in words) {
				string substring = "";
				for (int i = 0; i < word.tags.Length; i++) {
					if (word.tags [i] == ' ') {
						if (tagCounter.ContainsKey (substring))
							tagCounter [substring]++;
						else
							tagCounter.Add (substring, 1);
						substring = "";
					} else if (i == word.tags.Length - 1) {
						substring += word.tags [i];
						if (tagCounter.ContainsKey (substring))
							tagCounter [substring]++;
						else
							tagCounter.Add (substring, 1);
						substring = "";
					} else
						substring += word.tags [i];
				}
			}
			foreach (string illegalTag in new string[] {"", "pronoun", "noun", "Verb", "adverb", "article", "preposition", "adjective", "conjunction", "question"})
				tagCounter.Remove (illegalTag);
			return tagCounter;
		}

		//Returns the top <returnCount> most frequent tags from the given Dictionary
		public string[] commonTags (Dictionary<string, int> frequencies, int returnCount) {
			List<string> orderedTags = new List<string> ();
			while (frequencies.Count != 0) {
				Tuple<string, int> frequentTag = new Tuple<string, int> ("NULLTAG", 1024);
				foreach (string tag in frequencies.Keys) {
					if (frequencies [tag] <= frequentTag.Item2)
						frequentTag = new Tuple<string, int> (tag, frequencies [tag]);
				}
				frequencies.Remove (frequentTag.Item1);
				orderedTags.Add (frequentTag.Item1);
			}
			string[] tagArray = orderedTags.ToArray ();
			if (tagArray.Length < returnCount)
				return tagArray;
			else {
				List<string> returnTags = new List<string> ();
				for (int i = 1; i <= returnCount; i++)
					returnTags.Add (tagArray [tagArray.Length - i]);
				return returnTags.ToArray ();
			}
		}

		//Generates a sentence using the given tags
		public string generate () {
			List<Word> generatedPhrase = new List<Word> ();
			string sentenceModel = sentenceModels [Program.rndm.Next (sentenceModels.Length)];
			Log.Write ("Sentence.cs", "CHECK", sentenceModel); //For testing whether the sentence model is used to fullest potential...
			string substring = "";
			for (int i = 0; i < sentenceModel.Length; i++) {
				if (sentenceModel [i] == ' ' || i == sentenceModel.Length - 1) {
					if (i == sentenceModel.Length - 1)
						substring += sentenceModel [i];
					List<string> searchTags = new List<string> ();
					foreach (string tag in tags)
						searchTags.Add (tag);
					searchTags.Add ("&" + substring);
					Word[] taggedWords = Word.wordsTaggedFromCollection (Program.vocab.ToArray (), searchTags.ToArray ());
					if (taggedWords.Length > 0)
						generatedPhrase.Add (taggedWords [Program.rndm.Next (taggedWords.Length)]);
					substring = "";
				} else
					substring += sentenceModel [i];
			}
			string phrase = "";
			foreach (Word term in generatedPhrase)
				phrase += term.name + " ";
			return phrase;
		}

	}
}