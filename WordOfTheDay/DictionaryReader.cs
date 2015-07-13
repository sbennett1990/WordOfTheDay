using System;
using System.IO;

namespace WordOfTheDay
{
	public class DictionaryReader
	{
		private string dictionaryFile;
		private string dictionaryPath;

		/// <summary>
		/// Constructor.
		/// </summary>
		public DictionaryReader() {
			dictionaryFile = Properties.Settings.Default.DictionaryFileName;
			dictionaryPath = Properties.Settings.Default.DictionaryPath;

			if (dictionaryPath.Equals(string.Empty)) {
				dictionaryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
		}

		/// <summary>
		/// Get the next word and its definition from the dictionary file.
		/// </summary>
		/// <returns>A (word, definition) tuple</returns>
		/// <exception cref="FileNotFoundException">
		/// Thrown if the dictionary file could not be found
		/// </exception>
		public Tuple<string, string> getNextWord() {
			string path = dictionaryPath + "\\" + dictionaryFile;

			if (!File.Exists(path)) {
				throw new FileNotFoundException("Could not find file: " + path);
			}

			string[] fileRecords = File.ReadAllLines(path);

			Tuple<string, string> word;
			int wordIndex;

			try {
				wordIndex = nextWordIndex(fileRecords);

				word = Tuple.Create<string, string>(
					fileRecords[wordIndex].Trim(), 
					fileRecords[wordIndex + 1].Trim()
				);
			}
			catch {
				throw new Exception("ERROR: Could not get the next word");
			}

			/* 
			 * Put this call outside the try/catch block so that any Exceptions thrown while 
			 * trying to write the file can be bubbled up to the calling code. 
			 */
			writeNewIndex(path, wordIndex, fileRecords);

			return word;
		}

		/// <summary>
		/// Determine the index (actually, the line number of the file) of the next 
		/// word. The index will wrap around if the last word in the dictionary had 
		/// already been displayed. 
		/// </summary>
		/// <remarks>
		/// The first line in the file should contain the last word number that was 
		/// displayed. Using that number, this method figures out the actual index 
		/// of the next word that should be displayed. This assumes that the dictionary 
		/// file is structured correctly. 
		/// </remarks>
		/// <param name="fileRecords">
		/// An array containing every line of the dictionary file
		/// </param>
		/// <returns>The line number of the next word to display</returns>
		private int nextWordIndex(string[] fileRecords) {
			string previousIndexString = fileRecords[0];
			int previousIndex;
			int nextIndex;

			try {
				previousIndex = int.Parse(previousIndexString);

				if (previousIndex < 1 || previousIndex > fileRecords.Length) {
					nextIndex = 1;
				}
				else {
					nextIndex = ((2 * previousIndex) - 1) + 2;

					// Check to make sure we have not gone beyond the end of the dictionary
					if (nextIndex >= fileRecords.Length) {
						// Reset the index to the first word if the end is reached
						nextIndex = 1;
					}
				}
			}
			catch {
				nextIndex = 1;
			}

			return nextIndex;
		}

		/// <summary>
		/// Write the word number that was displayed during this run to the file. 
		/// </summary>
		/// <param name="path">Full path to the dictionary file</param>
		/// <param name="newIndex">The word number that was displayed during this run</param>
		/// <param name="fileContents">The previous contents of the dictionary file</param>
		private void writeNewIndex(string path, int newIndex, string[] fileContents) {
			if (newIndex < 1 || newIndex > fileContents.Length) {
				newIndex = 1;
			}
			else {
				newIndex = (newIndex + 1) / 2;
			}

			fileContents[0] = newIndex.ToString();

			File.WriteAllLines(path, fileContents);
		}
	}
}
