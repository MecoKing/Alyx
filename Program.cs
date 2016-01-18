using System;
using System.Collections.Generic;

namespace Alyx { //Protector of Humanity
	class Program {

		//SETTINGS:
		public static bool showAnalysis = false;
		public const string gender = "Female";

		public static List<Word> vocab = new List<Word> ();
		public static Random rndm = new Random ();
		public static ConsoleColor colour;

		/// <summary> Loads all files and runs all tests. Used once at startup. </summary>
		public static void Startup () {
			colour = Console.ForegroundColor;
			Log.Clear ();
			Word.loadWords ();
			Log.Write ("Program.cs", "INFO", "All files have been loaded...");
			Test.runAllTests ();
			Log.Write ("Program.cs", "INFO", "All tests have completed testing...");

			//Write a polite welcome
			Sentence intro = new Sentence ("Hello, my name is Alyx. How can I be of assistance?");
			Console.Write ("ALYX: ");
			Console.WriteLine ("{0}\n", intro.generate ());
		}

		public static void Main (string[] args) {
			//Initialize the program
			Startup ();

			for (;;) {
				//Take an input and format it for Alyx
				Console.Write ("YOU: ");
				string input = Console.ReadLine ();
				Sentence inSentence = new Sentence (input);
				Console.WriteLine ();
				//Generate a new sentence based on the formatted input sentence
				Console.Write ("ALYX: ");
				Console.WriteLine (inSentence.generate ());
				Console.WriteLine ();
			}
		}

	}
}