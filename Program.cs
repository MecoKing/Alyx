using System;
using System.Collections.Generic;

namespace Alyx { //Protector of Humanity
	class Program {

		public static List<Word> vocab = new List<Word> ();

		//Loads all the files and stuff. Used once... at startup.
		public static void Startup () {
			Log.Clear ();
			Word.loadWords ();
			Log.Write ("Program.cs", "GENERAL", "All file loading methods have completed their tasks captain!");
		}

		public static void Main (string[] args) {
			Startup ();
			
			for (;;) {
				string input = Console.ReadLine ();
				Sentence inSentence = new Sentence (input);
				Console.WriteLine ();
				Console.WriteLine (inSentence.generate ());
				Console.WriteLine ();
			}
		}
	}
}