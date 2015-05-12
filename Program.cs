using System;
using System.Collections.Generic;

namespace Alyx { //Protector of Humanity
	class Program {

		public static List<Word> vocab = new List<Word> ();

		//Loads all the files and stuff. Used once... at startup.
		public static void Startup () {
			Word.loadWords ();
			Log.Write ("Program.cs", "GENERAL", "All file loading methods have completed their tasks captain!");
		}

		public static void Main (string[] args) {
			Startup ();
			
			for (;;) {
				string input = Console.ReadLine ();
				Sentence inSentence = new Sentence (input);
				foreach (Word term in inSentence.words)
					Console.Write ("{0} ", term.name);
				Console.WriteLine ("\n{0} {1} {2}", inSentence.tags [0], inSentence.tags [1], inSentence.tags [2]);
			}
		}
	}
}