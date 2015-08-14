using System;
using libcmdline;

namespace WordOfTheDay
{
	public class Program
	{
		const string usage = "usage: word [-u] [-d]";

		/// <summary>
		/// Application Entry Point.
		/// </summary>
		public static void Main(string[] args) {
			DictionaryReader reader = new DictionaryReader();
			string dictionaryPath = Properties.Settings.Default.DictionaryPath;
			string dictionaryURL = Properties.Settings.Default.DictionaryURL;

			/* Set up the command line flag handler(s) */
			CommandLineArgs cmdLine = new CommandLineArgs();
			cmdLine.PrefixRegexPatternList.Add("-{1}");

			cmdLine.registerSpecificSwitchMatchHandler("u", (sender, e) => {
				FetchDictionaryFile.get(dictionaryURL, dictionaryPath);
			});
			cmdLine.registerSpecificSwitchMatchHandler("d", (sender, e) => {
				dictionaryPath = e.Value;
			});

			try {
				//for (int i = 0; i < args.Length; i++) {
				//	switch (args[i]) {
				//		case "-f":
				//			FetchDictionaryFile.get(dictionaryURL, dictionaryPath);
				//			break;
				//		case "-d":
				//			// d flag is for specifying a dictionary file on the command line
				//			break;
				//		case "-D":
				//			// D flag is for updating the configuration file with the path to the specified dictionary
				//			break;
				//		default:
				//			break;
				//	}
				//}
				cmdLine.processCommandLineArgs(args);

				Tuple<string, string> word = reader.getNextWord();

				Console.WriteLine();
				Console.WriteLine(word.Item1);
				Console.WriteLine(" - " + word.Item2);
				Console.WriteLine();
#if (DEBUG)
				Console.Read();
#endif
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
#if (DEBUG)
				Console.Read();
#endif
			}
		}
	}
}
