using System;
using System.Collections.Generic;

namespace Alyx {
	public class Sentence {

		string[] illegalTags = new string[] {"", "pronoun", "noun", "Verb", "adverb", "article", "preposition", "adjective", "conjunction", "question", "command"};

		public Word[] words;
		public string[] tags;

		public Word subject;
//		public Word action;

		/// <summary> Gets every tag from every word (no duplicates). </summary>
		public string[] allTags { get {
				List<string> tagsInWords = new List<string> ();
				foreach (Word term in words) {
					foreach (string tag in term.getTags) {
						if (!tagsInWords.Contains (tag)) { tagsInWords.Add (tag); }
					}
				}
				return tagsInWords.ToArray ();
			}
		}

		/// <summary>  Analyzes a written sentence seperating it into individual words and tags.  </summary>
		public Sentence (string phrase) {
			words = individualWords (reformat (phrase));
			tags = commonTags (tagFrequencies (), 6);
			detectSubject ();

			foreach (string tag in allTags) {
				if (tag == "command")
					Command.checkForOrderIn (this);
			}

			if (Program.showAnalysis) {
				Console.Write ("KNOWN: ");
				foreach (Word term in words)
					Console.Write ("{0} ", term.name);
				Console.WriteLine ();
				Console.Write ("PHONETIC: ");
				foreach (Word term in words)
					Console.Write ("{0} ", term.phonetic);
				Console.WriteLine ();
				Console.Write ("TAGS: ");
				foreach (string tag in tags)
					Console.Write ("{0} ", tag);
				Console.WriteLine ();
				Console.WriteLine ("SUBJECT: {0}", subject.name);
			}
		}

		/// <summary> Reformats the way the sentence is written to make it computer readable. </summary>
		public string reformat (string phrase) {
			string ABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string abc = "abcdefghijklmnopqrstuvwxyz";
			string illegal = ".,?!@#$%^&*()+=-|{}[]/><~_";
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

		/// <summary> Seperates the phrase into a list of KNOWN words. </summary>
		public Word[] individualWords (string phrase) {
			List<Word> terms = new List<Word> ();
			string nonWordChars = " ,.?:;!";
			string substring = "";
			for (int i = 0; i < phrase.Length; i++) {
				//If reached the end of a word
				if (nonWordChars.Contains (phrase [i].ToString ()) || i == phrase.Length - 1) {
					if (i == phrase.Length - 1) { substring += phrase [i]; }
					//Try and get the word from the list of vocabulary
					Word newTerm = Word.fromCollection (substring, Program.vocab.ToArray ());
					//If it exists add it as a word in the sentence
					if (newTerm != null)
						terms.Add (newTerm);
					//Check to see if removing an s makes it a legal word
					else if (substring.EndsWith ("'s")) {
						Word newTermNotPlural = Word.fromCollection (substring.Remove (substring.Length - 2, 2), Program.vocab.ToArray ());
						if (newTermNotPlural != null)
							terms.Add (newTermNotPlural);
						else
							Word.addUnknown (substring);
					} else
						Word.addUnknown (substring);
					substring = "";
				} else
					substring += phrase [i];
			}
			return terms.ToArray ();
		}

		/// <summary> Guesses at what the subject of the sentence should be based on the previous... </summary>
		public void detectSubject () {
			for (int i = words.Length - 1; i > 0; i--) {
				if (words [i].tags.Contains ("noun") || words [i].tags.Contains ("name")) {
					subject = words [i];
					break;
				}
			}
			//THIS IS BAD
			//Makes up a random subject if no suitable word was found...
			if (subject == null)
				subject = wordThatFollowsTags ("noun");
		}
		/// <summary> Checks to see if the guessed subject fits nicely with the common tags </summary>
		public bool subjectFollowsTags () {
			double tagsPassed = 0;
			foreach (string tag in tags) {
				if (subject.tags.Contains (tag))
					tagsPassed++;
			}
			//Percentage might have to change...
			return (tagsPassed / tags.Length >= 0.70);
		}

		/// <summary> Detemines the frequency of tags shown in the words array </summary>
		public Dictionary<string, int> tagFrequencies () {
			Dictionary <string, int> tagCounter = new Dictionary<string, int> ();
			//For every tag in every word in the known words list
			foreach (string tag in allTags) {
				//If the tag has already appeared, add 1 to its frequency
				if (tagCounter.ContainsKey (tag))
					tagCounter [tag]++;
				//Otherwise, add it as a new tag with a frequency of 1
				else
					tagCounter.Add (tag, 1);
			}
			//Remove any tags we don't want from the list of frequent tags...
			foreach (string illegalTag in illegalTags)
				tagCounter.Remove (illegalTag);
			return tagCounter;
		}

		/// <summary> Returns the most frequent tags from a list of frequencies. </summary>
		/// <param name="returnCount">The number of frequent tags to return.</param>
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

		//DOCUMENT ME!!!
		/// <summary> Generates a new sentence from the analyzed words and tags. </summary>
		public string generate () {
			List<Word> generatedPhrase = new List<Word> ();
			string sentenceModel = generateSentenceModel ();
			if (Program.showAnalysis)
				Console.WriteLine ("SYNTAX: {0}", sentenceModel);
			bool printedSubject = false;
			string substring = "";
			for (int i = 0; i < sentenceModel.Length; i++) {
				if (sentenceModel [i] == ' ' || i == sentenceModel.Length - 1) {
					if (i == sentenceModel.Length - 1)
						substring += sentenceModel [i];
					if ((substring == "name" || substring == "pronoun" || substring == "noun") && !printedSubject) {
						if (subjectFollowsTags ())
							generatedPhrase.Add (subject);
						else {
							generatedPhrase.Add (wordThatFollowsTags (substring));
							if (Program.showAnalysis) {
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine ("FAIL: Subject did not have enough to do with the sentence!");
								Console.ForegroundColor = Program.colour;
							}
						}
						printedSubject = true;
					} else
						generatedPhrase.Add (wordThatFollowsTags (substring));
					substring = "";
				} else
					substring += sentenceModel [i];
			}
			string phrase = "";
			foreach (Word term in generatedPhrase)
				phrase += term.name + " ";
			return phrase;
		}

		/// <summary> Gets a random word that follows all the given tags. </summary>
		public Word wordThatFollowsTags (string type) {
			List<string> searchTags = new List<string> ();
			foreach (string tag in tags)
				searchTags.Add (tag);
			searchTags.Add ("&" + type);
			Word[] taggedWords = Word.wordsTaggedFromCollection (Program.vocab.ToArray (), searchTags.ToArray ());
			if (taggedWords.Length > 0)
				return taggedWords [Program.rndm.Next (taggedWords.Length)];
			else {
				taggedWords = Word.wordsTaggedFromCollection (Program.vocab.ToArray (), type);
				return taggedWords [Program.rndm.Next (taggedWords.Length)];
			}
		}

		/// <summary> Generates a model to create a sentence with. </summary>
		public string generateSentenceModel () {
			Dictionary<string, string[]> likelyTypes = new Dictionary<string, string[]> () {
				{ "article", new string[] { "adjective", "adjective", "noun" } },
				{ "pronoun", new string[] { "adverb", "adverb", "Verb" } },
				{ "adjective", new string[] { "adjective", "noun", "noun" } },
				{ "adverb", new string[] { "adverb", "Verb", "Verb" } },
				{ "noun", new string[] { "adverb", "adverb", "Verb" } },
				{ "Verb", new string[] { "preposition", "preposition", "conjunction", "article", "article", "article", "pronoun" } },
				{ "preposition", new string[] { "article", "article", "pronoun" } },
				{ "conjunction", new string[] { "pronoun", "article", "article" } },
			};
			string model = "";
			bool endSentence = false;
			string currentType = (Program.rndm.Next (2) == 0) ? "article" : "pronoun";
			while (!endSentence) {
				model += currentType + " ";
				currentType = likelyTypes [currentType] [Program.rndm.Next (likelyTypes [currentType].Length)];
				if (model.Contains ("Verb") && (model.EndsWith ("noun ") || model.EndsWith ("pronoun ")))
					endSentence = true;
			}
			return model;
		}
	}
}