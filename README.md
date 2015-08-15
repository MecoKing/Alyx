# Alyx
A linguistic (hopefully) intelligent chatbot written in C#


Alyx is a console based chat bot written in C# that runs entirely (for now) on disk (No network connection).
The algorithm focuses mainly on 4 categories: Language, Knowledge, Philosophy and Service.

======

Language:
- Alyx CAN divide sentences into individual words
- Alyx CAN discern the main idea of a sentence by counting tags attached to words
- Alyx CAN recreate its own sentences based on tags from the previous sentence
- Alyx CAN generate sentence structures using a modified markov chain

Knowledge:
- Alyx WILL be able to answer questions based on keywords
- Alyx WILL be able to learn new facts from what the user says

Philosophy:
- Alyx WILL be able to ask questions to learn things it does not know yet
- Alyx WILL be able to make inferences based on facts it already knows

Service:
- Alyx WILL be able to execute given commands

Additionally:
- Alyx MAY have an emotion engine backing its sentence generation
- Alyx MAY have network access for search commands
- Alyx MAY pass the turing test (doubt it...)
