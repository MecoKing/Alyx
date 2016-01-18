using System;

namespace Alyx {
	public class Conjugator {

		//INFINITIVE ed, ing
		static string[,] verbs = new string[,] {
			//A
			{"Add", "Added", "Adding"}, {"Analyze", "Anzlyzed", "Analyzing"},
			//B
			{"Back", "Backed", "Backing"}, {"Be", "Was", "Being"}, {"Bore", "Bored", "Boring"},
			{"Break", "Broke", "Breaking"}, {"Brief", "Briefed", "Briefing"}, {"Bug", "Bugged", "Bugging"},
			//C
			{"Clean", "Cleaned", "Cleaning"}, {"Code", "Coded", "Coding"}, {"Come", "Came", "Coming"},
			//D
			{"Do", "Did", "Doing"},
			//E
			{"Expand", "Expanded", "Expanding"},
			//F
			{"Feel", "Felt", "Feeling"}, {"Fill", "Filled", "Filling"}, {"Find", "Found", "Finding"},
			{"Follow", "Followed", "Following"},
			//G
			{"Generate", "Generated", "Generating"}, {"Get", "Got", "Getting"}, {"Give", "Gave", "Giving"},
			{"Go", "Went", "Going"}, {"Green", "Greened", "Greening"}, {"Greet", "Greeted", "Greeting"},
			//H
			{"Hate", "Hated", "Hating"}, {"Have", "Had", "Having"}, {"Head", "Headed", "Heading"},
			{"Hear", "Heard", "Hearing"}, {"Help", "Helped", "Helping"}, {"Hide", "Hid", "Hiding"},
			//I
			//J
			{"Jump", "Jumped", "Jumping"},
			//K
			{"Kiss", "Kissed", "Kissing"}, {"Know", "Knew", "Knowing"},
			//L
			{"Lead", "Led", "Leading"}, {"Learn", "Learned", "Learning"}, {"Leave", "Left", "Leaving"},
			{"Like", "Liked", "Liking"}, {"Live", "Lived", "Living"}, {"Look", "Looked", "Looking"},
			//M
			{"Make", "Made", "Making"}, {"Mean", "Meant", "Meaning"}, {"Move", "Moved", "Moving"},
			//N
			{"Name", "Named", "Naming"}, {"Need", "Needed", "Needing"}, {"Nose", "Nosed", "Nosing"},
			{"Notice", "Noticed", "Noticing"},
			//O
			{"Order", "Ordered", "Ordering"}, {"Own", "Owned", "Owning"},
			//P
			{"Pencil", "Pencilled", "Pencilling"}, {"Phrase", "Phrased", "Phrasing"}, {"Please", "Pleased", "Pleasing"},
			{"Present", "Presented", "Presenting"}, {"Process", "Processed", "Processing"}, {"Program", "Programmed", "Programming"},
			//Q
			//R
			{"Recognize", "Recognized", "Recognizing"}, {"Right", "Righted", "Righting"},
			//S
			{"Say", "Said", "Saying"}, {"See", "Saw", "Seeing"}, {"Scent", "Scented", "Scenting"},
			{"Sense", "Sensed", "Sensing"}, {"Show", "Showed", "Showing"}, {"Sight", "Sighted", "Sighting"},
			{"Slave", "Slaved", "Slaving"}, {"Smell", "Smelled", "Smelling"}, {"Sort", "Sorted", "Sorting"},
			{"Sound", "Sounded", "Sounding"}, {"Space", "Spaced", "Spacing"}, {"Speak", "Spoke", "Speaking"},
			{"Spend", "Spent", "Spending"}, {"Spit", "Spat", "Spitting"}, {"State", "Stated", "Stating"},
			{"Start", "Started", "Starting"},
			//T
			{"Talk", "Talked", "Talking"}, {"Taste", "Tasted", "Tasting"}, {"Test", "Tested", "Testing"},
			{"Think", "Thought", "Thinking"}, {"Throw", "Threw", "Throwing"}, {"Time", "Timed", "Timing"},
			//U
			{"Understand", "Understood", "Understanding"},
			//V
			//W
			{"Wait", "Waited", "Waiting"}, {"Weather", "Weathered", "Weathering"}, {"Welcome", "Welcomed", "Welcoming"},
			//XYZ
			{"Yellow", "Yellowed", "Yellowing"},
		};

		public static string conjugate (string word, string tense) {
			if (tense == "Simple Present")
				return simplePresentTense (word);
			else if (tense == "Simple Past")
				return simplePastTense (word);
			else if (tense == "Simple Future")
				return simpleFutureTense (word);
			else
				return word;
		}

		static string simplePresentTense (string word) {
			for (int i = 0; i < verbs.GetLength (0); i++) {
				if (verbs [i, 0] == word)
					return String.Format ("{0}", verbs [i, 0]);
			}
			return word;
		}
		static string simplePastTense (string word) {
			for (int i = 0; i < verbs.GetLength (0); i++) {
				if (verbs [i, 0] == word)
					return String.Format ("{0}", verbs [i, 1]);
			}
			return word;
		}
		static string simpleFutureTense (string word) {
			for (int i = 0; i < verbs.GetLength (0); i++) {
				if (verbs [i, 0] == word)
					return String.Format ("Will {0}", verbs [i, 0]);
			}
			return word;
		}
	}
}

