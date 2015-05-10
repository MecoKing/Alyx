using System;
using System.Collections.Generic;

namespace Alyx { //Protector of Humanity
	class Program {

		public static List<Word> vocab = new List<Word> ();

		public static void Startup () {
			Word.loadWords ();
		}

		public static void Main (string[] args) {
			Startup ();
			
			for (;;) {
				string input = Console.ReadLine ();
			}
		}
	}
}
