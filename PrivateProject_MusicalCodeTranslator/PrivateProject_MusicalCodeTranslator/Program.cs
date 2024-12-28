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

//string transation1 = MusicalCodeTranslator.Encode(translateMe1);
//string transation2 = MusicalCodeTranslator.Encode(translateMe2);
//string transation3 = MusicalCodeTranslator.Encode(translateMe3);
//string transation4 = MusicalCodeTranslator.Encode(translateMe4);
//string transation5 = MusicalCodeTranslator.Encode(translateMe5);
//string transation6 = MusicalCodeTranslator.Encode(translateMe6);
//string transation7 = MusicalCodeTranslator.Encode(translateMe7);
//string transation8 = MusicalCodeTranslator.Encode(translateMe8);
//string transation9 = MusicalCodeTranslator.Encode(translateMe9);
//string transation10 = MusicalCodeTranslator.Encode(translateMe10);
//string transation11 = MusicalCodeTranslator.Encode(translateMe11);
//string transation12 = MusicalCodeTranslator.Encode(translateMe12);

//string decoded1 = MusicalCodeTranslator.Decode(transation1);
//string decoded2 = MusicalCodeTranslator.Decode(transation2);
//string decoded3 = MusicalCodeTranslator.Decode(transation3);
//string decoded4 = MusicalCodeTranslator.Decode(transation4);
//string decoded5 = MusicalCodeTranslator.Decode(transation5);
//string decoded6 = MusicalCodeTranslator.Decode(transation6);
//string decoded7 = MusicalCodeTranslator.Decode(transation7);
//string decoded8 = MusicalCodeTranslator.Decode(transation8);
//string decoded9 = MusicalCodeTranslator.Decode(transation9);
//string decoded10 = MusicalCodeTranslator.Decode(transation10);
//string decoded11 = MusicalCodeTranslator.Decode(transation11);
//string decoded12 = MusicalCodeTranslator.Decode(transation12);

using PrivateProject_MusicalCodeTranslator.App;
using PrivateProject_MusicalCodeTranslator.Translation;
using PrivateProject_MusicalCodeTranslator.UserInteraction;

var translatorApp = new TranslatorApp(new TranslatorConsoleUserInteraction(), new MusicalCodeTranslator());

translatorApp.Run();