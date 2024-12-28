// Testing...

//var translateMe1 = "The fox jumped over the lazy dog.";
//var translateMe2 = "abcdefghijklmnopqrstuvwxyz";
//var translateMe3 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
//var translateMe4 = string.Join(" ", "abcdefghijklmnopqrstuvwxyz".ToCharArray());
//var translateMe5 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ     A";
//var translateMe6 = "The Quick Brown Fox Jumps Over The Lazy Dog!"; // Mixed case with punctuation
//var translateMe7 = "The quick brown fox 123 jumped over! @#$%^&*()"; // Numbers and special characters
//var translateMe8 = "   Hello, World!   "; // Leading and trailing spaces, punctuation
//var translateMe9 = "aaaaaaaaaaaaaaabbbbbbbbbb"; // Repeated characters to test large repetitions
//var translateMe10 = new string('a', 10000); // Very long string of 'a's to test large input handling
//var translateMe11 = "z" + new string('z', 100); // One character repeated to test large repetitions
//var translateMe12 = ""; // Edge case: Empty string

//string transation1 = Translator.Encode(translateMe1);
//string transation2 = Translator.Encode(translateMe2);
//string transation3 = Translator.Encode(translateMe3);
//string transation4 = Translator.Encode(translateMe4);
//string transation5 = Translator.Encode(translateMe5);
//string transation6 = Translator.Encode(translateMe6);
//string transation7 = Translator.Encode(translateMe7);
//string transation8 = Translator.Encode(translateMe8);
//string transation9 = Translator.Encode(translateMe9);
//string transation10 = Translator.Encode(translateMe10);
//string transation11 = Translator.Encode(translateMe11);
//string transation12 = Translator.Encode(translateMe12);

//string decoded1 = Translator.Decode(transation1);
//string decoded2 = Translator.Decode(transation2);
//string decoded3 = Translator.Decode(transation3);
//string decoded4 = Translator.Decode(transation4);
//string decoded5 = Translator.Decode(transation5);
//string decoded6 = Translator.Decode(transation6);
//string decoded7 = Translator.Decode(transation7);
//string decoded8 = Translator.Decode(transation8);
//string decoded9 = Translator.Decode(transation9);
//string decoded10 = Translator.Decode(transation10);
//string decoded11 = Translator.Decode(transation11);
//string decoded12 = Translator.Decode(transation12);

using PrivateProject_MusicalCodeTranslator.App;

var translationApp = new MusicalCodeTranslatorApp();

translationApp.Run();