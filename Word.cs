using System;

namespace Alyx {
	public class Word {

		string name;
		string tags;
		string phonetic;

		//A script will look like this: [name: tag anotherTag thirdTag moarTags]
		public Word (string script) {
			string subScript = "";
			for (int i = 0; i < script.Length; i++) {
				if (script [i] == ':') {
					name = subScript;
					subScript = "";
				} else if (i == script.Length - 1) {
					subScript += script [i];
					tags = subScript;
					subScript = "";
				} else
					subScript += script [i];
			}
			phonetic = Phonetic.generatePhoneticsFor (name);
		}
	}
}

