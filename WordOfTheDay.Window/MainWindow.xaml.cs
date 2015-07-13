using System;

using WordOfTheDay;

namespace Window
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		DictionaryReader reader;

		/// <summary>
		/// Constructor.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
			reader = new DictionaryReader();
			populateContents();
		}

		/// <summary>
		/// Put the word and its definition in the window. 
		/// </summary>
		private void populateContents() {
			try {
				Tuple<string, string> word = reader.getNextWord();

				this.wordLable.Content = word.Item1;
				this.definitionBlock.Text = word.Item2;
			}
			catch (Exception e) {
				this.wordLable.Content = "ERROR";
				this.definitionBlock.Text = e.Message;
			}
		}
	}
}
