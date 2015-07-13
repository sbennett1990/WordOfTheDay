using System;

namespace WordOfTheDay
{
	public class Program
	{
		/// <summary>
		/// Application Entry Point.
		/// </summary>
		[System.STAThreadAttribute()]
		public static void Main() {
			//WordOfTheDay.App app = new WordOfTheDay.App();
			//app.InitializeComponent();
			//app.Run();

			DictionaryReader reader = new DictionaryReader();

			try {
				Tuple<string, string> word = reader.getNextWord();

				Console.WriteLine();
				Console.WriteLine(word.Item1);
				Console.WriteLine(word.Item2);
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
