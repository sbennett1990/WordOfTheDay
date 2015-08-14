using System;
using System.Net;
using libcmdline;

namespace WordOfTheDay
{
	public class Program
	{
		const string usage = "usage: word [-u] [-d]";

		/// <summary>
		/// Fetch the file specified in the url and save it in the saveLocation.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="saveLocation"></param>
		public static void download(string url, string saveLocation) {
			using (WebClient client = new WebClient()) {
				try {
					Console.WriteLine(string.Format("Downloading file: {0}", url));
					
					client.DownloadFile(url, saveLocation);

					Console.WriteLine("Done!");
					Console.WriteLine(string.Format("Saved file to: {0}", saveLocation));
				}
				catch (Exception e) {
					Console.WriteLine("error: " + e.Message + System.Environment.NewLine);
					Environment.Exit(1);
				}
			}
		}

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
				download(dictionaryURL, dictionaryPath);
			});
			cmdLine.registerSpecificSwitchMatchHandler("d", (sender, e) => {
				dictionaryPath = e.Value;
			});
			// D flag is for updating the configuration file with the path to the specified dictionary
			//cmdLine.registerSpecificSwitchMatchHandler("D", (sender, e) => {

			//});

			try {
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
