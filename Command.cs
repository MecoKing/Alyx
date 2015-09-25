using System;
using System.Collections.Generic;

namespace Alyx {
	public class Command {

		static string orders = "Show Hide Clean";
		static string modifiers = "Analysis Unknowns";
		/// <summary> Lists the modifiers that work with each order. </summary>
		static Dictionary<string, string> orderMods = new Dictionary<string, string> () {
			{"Show", "Analysis"},
			{"Hide", "Analysis"},
			{"Clean", "Unknowns"}
		};

		static string command;

		/// <summary> Checks for a command in the given sentence. Runs the command if found. </summary>
		public static void checkForOrderIn (Sentence phrase) {
			List<string> possibleOrder = new List<string> ();
			List<string> possibleModifier = new List<string> ();

			foreach (Word term in phrase.words) {
				if (orders.Contains (term.name))
					possibleOrder.Add (term.name);
				else if (modifiers.Contains (term.name))
					possibleModifier.Add (term.name);
			}

			foreach (string order in possibleOrder) {
				foreach (string mod in possibleModifier) {
					if (orderMods [order].Contains (mod))
						//What are the odds that two commands would appear in one sentence?
						command = string.Format ("{0} {1}", order, mod);
				}
			}
			if (command != null) {
				if (Program.showAnalysis)
					Console.WriteLine ("COMMAND: {0}", command);
				runCommand ();
			}
		}

		static void runCommand () {
			//Try to keep these in alphabetical order
			if (command == "Hide Analysis")
				Program.showAnalysis = false;
			else if (command == "Show Analysis")
				Program.showAnalysis = true;
			else if (command == "Clean Unknowns")
				Word.cleanUnknowns ();
		}
	}
}

