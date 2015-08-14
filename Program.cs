using System;
using System.Collections.Generic;

namespace Alyx { //Protector of Humanity
	class Program {

		//SETTINGS:
		public static bool showAnalysis = false;

		public static List<Word> vocab = new List<Word> ();
		public static Random rndm = new Random ();

		//Loads all the files and stuff. Used once... at startup.
		public static void Startup () {
			//Clear the log load the vocabulary and write a message to the debug log
			Log.Clear ();
			Word.loadWords ();
			Log.Write ("Program.cs", "INFO", "All file loading methods have completed their tasks captain!");
			Test.runAllTests ();
			Log.Write ("Program.cs", "INFO", "All tests have completed testing...");

			//Write a polite welcome
			Console.WriteLine ("Hello I am Alyx, a unique syntax based chatbot!\n");
		}

		//The Main method
		public static void Main (string[] args) {
			//Initialize the program
			Startup ();

			for (;;) {
				//Take an input and format it for Alyx
				string input = Console.ReadLine ();
				Sentence inSentence = new Sentence (input);
				Console.WriteLine ();
				//Generate a new sentence based on the formatted input sentence
				Console.WriteLine (inSentence.generate ());
				Console.WriteLine ();
			}
		}
	}
}