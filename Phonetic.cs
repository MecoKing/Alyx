using System;

namespace Alyx
{
	public class Phonetic {
		
		static string reformat (string original) {
			string CONSONANTS = "BCDFGHJKLMNPQRSTVWXZ";
			string consonants = "bcdfghjklmnpqrstvwxz";
			string VOWELS = "AEIOUY";
			string vowels = "aeiouy";
			string formatted = "";
			for (int i = 0; i < original.Length; i++) {
				if (CONSONANTS.Contains (original [i].ToString ()))
					formatted += consonants [CONSONANTS.IndexOf (original [i])];
				else if (VOWELS.Contains (original [i].ToString ()))
					formatted += vowels [VOWELS.IndexOf (original [i])];
				else if (original [i] != ' ')
					formatted += original [i];
			}
			return String.Format (" {0}  ", formatted);
		}

		public static string generatePhoneticsFor (string term) {
			string word = reformat (term);
			string phonetic = "";
			for (int i = 1; i < word.Length - 2; i++) {
				if (word [i] == 'a') {
					if (word [i - 1] == 'e')
						phonetic += "";
					else if (word [i - 1] == 'u')
						phonetic += "e";
					else if (word [i + 2] == 'e' || word [i + 1] == 'y' || word [i + 1] == 'i' || word [i + 1] == 'u')
						phonetic += "E";
					else if (word [i + 1] == 'l')
						phonetic += "a";
					else if (word [i + 1] == 'w' || word [i + 1] == ' ')
						phonetic += "A";
					else
						phonetic += "æ";
				} else if (word [i] == 'b') {
					phonetic += "b";
				} else if (word [i] == 'c') {
					if (word [i + 1] == 'e')
						phonetic += "s";
					else if (word [i + 1] == 'h')
						phonetic += "†";
					else
						phonetic += "k";
				} else if (word [i] == 'd') {
					if (word [i + 1] != 'g')
						phonetic += "d";
				} else if (word [i] == 'e') {
					string emptyEndSounds = "bdkvgnmlst";
					if (word [i - 1] == 'e' || word [i - 1] == 'i' || word [i - 1] == 'y' || (emptyEndSounds.Contains (word [i - 1].ToString ()) && word [i + 1] == ' ' && word [i - 2] != ' '))
						phonetic += "";
					else if (word [i + 1] == 'y' && word [i + 2] == 'e')
						phonetic += "¥";
					else if (word [i + 1] == 'e')
						phonetic += "i";
					else if (word [i + 1] == 't' && word [i + 2] == ' ')
						phonetic += "I";
					else
						phonetic += "e";
				} else if (word [i] == 'f') {
					phonetic += "f";
				} else if (word [i] == 'g') {
					if (word [i+1] == 'e')
						phonetic += "¿";
					else
						phonetic += "g";
				} else if (word [i] == 'h') {
					if (word [i - 1] != 'c' && word [i - 1] != 's' && word [i - 1] != 't' && word [i - 1] != 'p')
						phonetic += "h";
				} else if (word [i] == 'i') {
					if (word [i - 1] == 'i' || word [i - 1] == 'a' || word [i - 1] == 'o')
						phonetic += "";
					else if (word [i - 1] == ' ')
						phonetic += "I";
					else if (word [i + 1] == 'e')
						phonetic += "¥";
					else
						phonetic += "i";
				} else if (word [i] == 'j') {
					phonetic += "¿";
				} else if (word [i] == 'k') {
					if (word [i + 1] != 'n')
						phonetic += "k";
				} else if (word [i] == 'l') {
					if (!(word [i-1] == 'a' &&  word [i+1] == 'm') || word [i - 1] == 'l')
						phonetic += "l";
				} else if (word [i] == 'm') {
					phonetic += "m";
				} else if (word [i] == 'n') {
					phonetic += "n";
				} else if (word [i] == 'o') {
					if (word [i - 1] == 'o' || word [i + 1] == 'u')
						phonetic += "";
					else if (word [i + 1] == 'o')
						phonetic += "u";
					else if (word [i + 1] == 'i')
						phonetic += "y";
					else if (word [i + 2] == ' ')
						phonetic += "∆";
					else
						phonetic += "o";
				} else if (word [i] == 'p') {
					if (word [i + 1] == 'h')
						phonetic += "f";
					else
						phonetic += "p";
				} else if (word [i] == 'q') {
					phonetic += "ku";
				} else if (word [i] == 'r') {
					phonetic += "r";
				} else if (word [i] == 's') {
					if (word [i + 1] == 'h')
						phonetic += "∫";
					else if (word [i - 1] == 's')
						phonetic += "";
					else if ((word [i + 1] == 'e' && word [i + 2] == ' ') || word [i + 1] == 'm' || word [i + 1] == 'v' || word [i + 1] == ' ')
						phonetic += 'z';
					else
						phonetic += 's';
				} else if (word [i] == 't') {
					if (word [i + 1] == 'h' && word [i + 2] == ' ')
						phonetic += "Ø";
					else if (word [i + 1] == 'h')
						phonetic += "ƒ";
					else if (word [i - 1] != 't')
						phonetic += 't';
				} else if (word [i] == 'u') {
					if (word [i - 1] == 'a')
						phonetic += "";
					else if (word [i - 1] == 'h')
						phonetic += "u";
					else if (word [i - 1] == 'o' && word [i + 1] == 'l')
						phonetic += "Ω";
					else if (word [i - 1] == 'o' && word [i + 1] == ' ')
						phonetic += "u";
					else if (word [i - 1] == 'o')
						phonetic += "§";
					else if (word [i + 1] == 'e')
						phonetic += "u";
					else if (word [i + 1] == 'a')
						phonetic += "w";
					else
						phonetic += "∆";
				} else if (word [i] == 'v') {
					phonetic += "v";
				} else if (word [i] == 'w') {
					if (word [i-1] != 'a' && word [i-1] != 'e' && word [i-1] != 'o')
					phonetic += "w";
				} else if (word [i] == 'x') {
					phonetic += "ks";
				} else if (word [i] == 'y') {
					if (word [i + 1] == 'o')
						phonetic += "/";
					else if (!(word [i - 1] == 'a' || word [i - 1] == 'e'))
						phonetic += "i";
				} else if (word [i] == 'z') {
					if (word [i-1] != 'z')
						phonetic += "z";
				}
			}
			return phonetic;
		}

		public static Tuple<string, int> createSyllables (string phonetic) {
			string syllable = "";
			int syllableCount = 0;
			string sounds = "∆aæeA£IiΩu¥§Eoy√∑µ";
			foreach (char letter in phonetic) {
				if (sounds.Contains (letter.ToString ())) {
					syllable += "|";
					syllableCount++;
				} else {
					syllable += "-";
				}
			}
			return new Tuple<string, int> (syllable, syllableCount);
		}

	}
}

