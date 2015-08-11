using System;
using System.IO;
using System.Net;

namespace WordOfTheDay
{
	public class FetchDictionaryFile
	{
		/// <summary>
		/// Fetch the file specified in the url and save it in the saveLocation.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="saveLocation"></param>
		public static void get(string url, string saveLocation) {
			using (WebClient client = new WebClient()) {
				try {
					Console.WriteLine(string.Format("Downloading file: {0}", url));
					client.DownloadFile(url, saveLocation);
					Console.WriteLine("Done!");
				}
				catch (Exception e) {
					Console.WriteLine("error: " + e.Message + System.Environment.NewLine);
					Environment.Exit(1);
				}
			}
		}
	}
}
