using System;
using System.IO;
using System.Collections.Generic;

namespace Alyx {
	public class Markov {
		//SYNTAX
		public static void readSyntax (string[] syntax) {
			//FORMAT:
			string[] syntaxArray = new string[syntax.Length + 2];
			syntaxArray [0] = "START";
			for (int i = 0; i < syntax.Length; i++)
				syntaxArray [i + 1] = syntax [i];
			syntaxArray [syntaxArray.Length - 1] = "END";

			for (int pos = 0; pos < syntaxArray.Length; pos++) {
				string keyPairing = (pos == 0) ? syntaxArray [pos] : String.Format ("{0} {1}", syntaxArray [pos - 1], syntaxArray [pos]);
				if (!keyPairing.EndsWith ("END"))
					addNext (keyPairing, syntaxArray [pos + 1]);
			}
		}

		public static string[] writeSyntax () {
			List<string> generatedSyntax = new List<string> ();
			string previous = "";
			string current = "START";
			string next = "";
			while (next != "END") {
				if (current == "START")
					next = getFollowerFor (current);
				else
					next = getFollowerFor (previous + " " + current);
				generatedSyntax.Add (previous);
				previous = current;
				current = next;
			}
			generatedSyntax.Add (previous);
			generatedSyntax.Remove ("START");
			generatedSyntax.Remove ("");
			return generatedSyntax.ToArray ();
		}

		static string getFollowerFor (string key) {
			using (StreamReader reader = new StreamReader ("Syntax.txt")) {
				string line;
				while ((line = reader.ReadLine ()) != null) {
					MarkovLine mLine = new MarkovLine (line);
					if (mLine.key == key)
						return mLine.possibleFollowings [Program.rndm.Next (mLine.possibleFollowings.Length)];
				}
			}
			return "END";
		}

		static void addNext (string key, string next) {
			List<MarkovLine> database = new List<MarkovLine> ();
			using (StreamReader reader = new StreamReader ("Syntax.txt")) {
				bool keyExists = false;
				string line;
				while ((line = reader.ReadLine ()) != null) {
					MarkovLine mLine = new MarkovLine (line);
					if (mLine.key == key) {
						keyExists = true;
						mLine.addFollower (next);
					}
					database.Add (mLine);
				}
				if (!keyExists) {
					MarkovLine newLine = new MarkovLine (String.Format ("{0}: {1}", key, next));
					database.Add (newLine);
				}
			}
			using (StreamWriter writer = new StreamWriter ("Syntax.txt", false)) {
				foreach (MarkovLine syntax in database)
					writer.WriteLine (syntax.ToString ());
			}
		}

		public static string[] arrayFromLine (string line) {
			List<string> array = new List<string> ();
			string separators = " ";
			string substring = "";
			for (int c = 0; c < line.Length; c++) {
				if (separators.Contains (line [c].ToString ())) {
					if (substring != "")
						array.Add (substring);
					substring = "";
				} else
					substring += line [c];
			}
			if (substring != "")
				array.Add (substring);
			
			return array.ToArray ();
		}
	}

	class MarkovLine {
		public string key;
		public string[] possibleFollowings;

		public MarkovLine (string line) {
			int c = 0;
			key = "";
			while (line [c] != ':') {
				key += line [c];
				c++;
			}
			line = line.Remove (0, key.Length + 1);
			possibleFollowings = Markov.arrayFromLine (line);
		}

		public void addFollower (string term) {
			if (possibleFollowings.Length < 20) {
				List<string> syntax = new List<string> ();
				foreach (string follower in possibleFollowings)
					syntax.Add (follower);
				syntax.Add (term);
				possibleFollowings = syntax.ToArray ();
			} else {
				for (int i = 19; i > 1; i++)
					possibleFollowings [i] = possibleFollowings [i - 1];
				possibleFollowings [0] = term;
			}
		}

		public override string ToString () {
			string stringVersion = key + ":";
			foreach (string follower in possibleFollowings)
				stringVersion += String.Format (" {0}", follower);
			return stringVersion;
		}
	}
}

