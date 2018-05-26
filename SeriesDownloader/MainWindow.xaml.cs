using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuickType;

namespace SeriesDownloader
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string _apikey = "8b5cb16b6745e38ac445cedaad08a319";
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{

			string val = seachText.Text;
			if (!string.IsNullOrEmpty(val))
			{
				GetSearchResults(val);
			}
		}
		
		

		#region Helpers

		private void GetSearchResults(string name)
		{
		    string url = "https://api.themoviedb.org/3/search/tv?api_key=" + _apikey + "&query=" + name;
			string json = GetWebResult(url);
			var data = MovieDbResult.FromJson(json);
			SetSearchGrid(data);
		}

		private void SetSearchGrid(MovieDbResult data)
		{
			SearchGrid.ItemsSource = data.MovieDbItems;

		}

		private Int64 GetImdbId(long movieId)
		{
			var url = "http://api.themoviedb.org/3/tv/" + movieId + "?api_key=" + _apikey + "&append_to_response=external_ids";//id  = return from above url id
			var json= GetWebResult(url);
			MovieDbDetailedResult result = MovieDbDetailedResult.FromJson(json);
			return Convert.ToInt64(result.ImdbId);
		}

		private void OpenMagent(string magnet)
		{
			Process.Start(magnet);
		}

		private double ByteToMb(long bytes)
		{
			return (bytes / 1024f) / 1024f;
		}

		private string GetWebResult(string URL)
		{
			string result;
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
			request.ContentType = "application/json; charset=utf-8";
			request.PreAuthenticate = true;
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			using (Stream responseStream = response.GetResponseStream())
			{
				StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
				result = reader.ReadToEnd();
			}

			return result;
		}

		private void LoadTorrentsGrid(MovieDBItem item)
		{

			Int64 imdbId =  GetImdbId(item.Id);
		}

		#endregion

		#region Events


		private void SearchGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (sender != null)
			{
				DataGrid grid = sender as DataGrid;
				if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
				{
					DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
					MovieDBItem item = dgr.Item as MovieDBItem;
					LoadTorrentsGrid(item);
				}
			}
		}




		#endregion
	}
}
