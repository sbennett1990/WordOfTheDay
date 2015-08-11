using System;

namespace WordOfTheDay
{
	public class Program
	{
		const string usage = "usage: word [-f]";

		/// <summary>
		/// Application Entry Point.
		/// </summary>
		public static void Main(string[] args) {
			DictionaryReader reader = new DictionaryReader();
			string dictionaryFile = Properties.Settings.Default.DictionaryFileName;
			string dictionaryPath = Properties.Settings.Default.DictionaryPath;


			try {
				for (int i = 0; i < args.Length; i++) {
					switch (args[i]) {
						case "-f":
							FetchDictionaryFile.get("", dictionaryPath + "\\" + dictionaryFile);
							break;
						default:
							break;
					}
				}

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
