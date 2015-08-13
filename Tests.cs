using System;

namespace Alyx {
	public class Test {
		public static void runAllTests () {
			testForIllegalTags ();
		}

		static void testForIllegalTags () {
			string[] legalTags = new string[] {
				"new", "immature", "young", "adult", "aged", "old", "single", "few", "some", "many", "all", "mammal", "reptile", "amphibian", "bird", "insect", "fish",
				"good", "neutral", "bad", "red", "orange", "yellow", "green", "blue", "violet", "pink", "brown", "black", "grey", "white", "second", "minute", "hour", "day",
				"week", "month", "year", "generic", "abstract", "specific", "language", "art", "music", "math", "science", "religion", "politics", "commerce", "male", "female",
				"true", "false", "positive", "negative", "builder", "warrior", "healer", "bright", "dim", "dark", "noun", "adverb", "adjective", "Verb", "pronoun", "article",
				"conjunction", "name", "preposition", "front", "right", "back", "left", "in", "out", "above", "below", "plural", "collective", "question", "exclamation",
				"self", "possession", "happy", "sad", "love", "anger", "bored", "municipal", "continental", "planetary", "galactic", "universal", "sense", "see", "hear", "feel",
				"smell", "taste", "tiny", "small", "medium", "big", "large", "huge", "animal", "plant", "human", "alien", "living", "dead", "residential", "commercial", "industrial",
				"entertainment", "business", "nature", "governmental", "cold", "temperate", "warm", "hot", "past", "present", "start", "end", "future", "emotional", "physical",
				"digital", "virtual", "material", "spiritual", "social", "mental", "cultural",

				"italy",
			};
			foreach (Word term in Program.vocab) {
				foreach (string tag in term.getTags ()) {
					bool tagIsLegal = false;
					foreach (string legal in legalTags) {
						if (tag == legal) {
							tagIsLegal = true;
							break;
						}
					}
					if (!tagIsLegal)
						Log.Write ("Vocab.txt", "BUG", String.Format ("{0} contains illegal tag: {1}", term.name, tag));
				}
			}
		}

	}
}

