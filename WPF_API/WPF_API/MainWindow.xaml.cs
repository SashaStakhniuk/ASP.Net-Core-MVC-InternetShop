using MVCBikeShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_API
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;

            dataGridBikes.DataContext = GetAll();
        }
        private List<Bike> GetAll()
        {
            dataGridBikes.DataContext = null;
            List<Bike> bikes = null;
            string json = new WebClient().DownloadString("https://localhost:44348/api/bike");
            if(!string.IsNullOrEmpty(json))
            {
                bikes = JsonConvert.DeserializeObject<List<Bike>>(json);
               // MessageBox.Show("Datas was loaded");
            }
            else
            {
                MessageBox.Show("Datas wasn't loaded");
            }
            return bikes;
        }
       

        private void AddGoodButton_Click(object sender, RoutedEventArgs e)
        {
          
            AddBikeWindow abw = new AddBikeWindow(0);
            abw.ShowDialog();            
            dataGridBikes.DataContext = GetAll();
        }

        private void EditGoodButton_Click(object sender, RoutedEventArgs e)
        {
            Bike selectedBike = (Bike)dataGridBikes.SelectedCells[0].Item;

            int id = (int)selectedBike.BikeId;
            MessageBox.Show(id.ToString());
            AddBikeWindow editBikeWindow = new AddBikeWindow(id);
            editBikeWindow.ShowDialog();
            dataGridBikes.DataContext = GetAll();
        }

        private async void RemoveGoodButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGridBikes.SelectedItems.Count == 1)
                {
                    MessageBoxResult result = MessageBox.Show("Are you shure?", "Deleting selected bike", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        Bike selectedBike = (Bike)dataGridBikes.SelectedCells[0].Item;

                        int Id = (int)selectedBike.BikeId;

                        await DeleteBike(Id);

                        MessageBox.Show("Bike and related datas was deleted");
                        dataGridBikes.DataContext = GetAll();
                    }
                }
                else
                {
                    MessageBox.Show("No one good was selectes", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //static async Task<HttpStatusCode> DeleteProductAsync(string id)
            //{
            //    HttpResponseMessage response = await client.DeleteAsync(
            //        $"api/products/{id}");
            //    return response.StatusCode;
            //}
           
        }
        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:44348");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        private async Task DeleteBike(int id)
        {
            using (var httpClient = GetHttpClient())
                await httpClient.DeleteAsync($"api/bike/{id}");
        }

        //private void DeleteBike(int id)
        //{
        //    //string URI = $"https://localhost:44348/api/bike/{id}";
        //    //var jsonToSend = JsonConvert.SerializeObject(bikeToEdit);
        //    //MessageBox.Show(jsonToSend);
        //    using (WebClient wc = new WebClient())
        //    {
        //        //wc.Headers[HttpRequestHeader.ContentType] = "application/json";
        //        string HtmlResult = wc.UploadString($"https://localhost:44348/api/bike/{id}", "Delete");
        //    }
        //}

        //private async Task EditBike(Bike bike)
        //{
        //    using (var httpClient = GetHttpClient())
        //        await httpClient.PutAsync($"api/bike",bike);
        //}

    }
}
