using MVCBikeShop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace WPF_API
{
    public partial class AddBikeWindow : Window
    {
        private int CurrentBikeId { get; set; }
        //await Task.Run(()=>{ });

    private List<Manufactury> Manufacturies = GetAllManufacturies();
        private List<MVCBikeShop.Models.Type> Types = GetAllTypes();
        private List<Material> Materials = GetAllMaterials();
        private List<BreakType> BreakTypes = GetAllBreakTypes();
        private Bike bike = null;

        public AddBikeWindow(int selectedBikeId)
        {
            InitializeComponent();
            CurrentBikeId = selectedBikeId;
            //MessageBox.Show(GetAllManufacturies().Count.ToString());
            //var toScreen= Task.Run(()=>{ 
            if(selectedBikeId != 0)
            {
                bike = GetAllBikes().FirstOrDefault(x => x.BikeId == selectedBikeId);
                ManufacturyCB.SelectedItem = Manufacturies.Where(x => x.ManufacturyId == bike.ManufacturyId).Select(x => x.ManufacturyTitle).FirstOrDefault();
                TypeCB.SelectedItem = Types.Where(x => x.TypeId == bike.TypeId).Select(x => x.TypeTitle).FirstOrDefault();
                MaterialCB.SelectedItem = Materials.Where(x => x.MaterialId == bike.MaterialId).Select(x => x.MaterialTitle).FirstOrDefault();
                BreakTypeCB.SelectedItem = BreakTypes.Where(x => x.BreakTypeId == bike.BreakTypeId).Select(x => x.BreakTypeTitle).FirstOrDefault();
                titleTB.Text = bike.BikeTitle;
                SpeedCountTB.Text=bike.SpeedCount.ToString();
                      SizeTB.Text=bike.Size.ToString();
                     WheelTB.Text=bike.WheelDiameter.ToString();
                 PhotoPathTB.Text=bike.PhotoPath.ToString();
                PriceTB.Text = bike.Price.ToString();
                //});
                //toScreen.Wait();
                //ManufacturyCB.Items.AddRange(GetAllManufacturies().Select(x => x.ManufacturyTitle));
            }
            foreach (var manufactury in Manufacturies.Select(x => x.ManufacturyTitle))
            {
                ManufacturyCB.Items.Add(manufactury);
            }
            foreach (var type in Types.Select(x => x.TypeTitle))
            {
                TypeCB.Items.Add(type);
            }
            foreach (var material in Materials.Select(x => x.MaterialTitle))
            {
                MaterialCB.Items.Add(material);
            }
            foreach (var breakType in BreakTypes.Select(x => x.BreakTypeTitle))
            {
                BreakTypeCB.Items.Add(breakType);
            }

        }

        private void AddGoodButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewBike(bike);
            this.Close();
        }
        private void AddNewBike(Bike bikeToEdit)
        {
            try
            {
                bool add = false; // for adding datas to server
                bool update = false;//for updating datas on server

                if (bikeToEdit == null)
                {
                    bikeToEdit = new Bike();
                    add = true;
                }

                //await Task.Run(() =>
                //{
                    if (!String.IsNullOrEmpty(ManufacturyCB.Text) && !String.IsNullOrEmpty(TypeCB.Text) && !String.IsNullOrEmpty(titleTB.Text) && !String.IsNullOrEmpty(MaterialCB.Text) && !String.IsNullOrEmpty(SpeedCountTB.Text) && !String.IsNullOrEmpty(WheelTB.Text) && !String.IsNullOrEmpty(BreakTypeCB.Text) && !String.IsNullOrEmpty(PhotoPathTB.Text) && !String.IsNullOrEmpty(PriceTB.Text))
                    {
                        if (CurrentBikeId != 0)
                        {
                            bikeToEdit.BikeId = CurrentBikeId;
                        }
                        bikeToEdit.BikeTitle = titleTB.Text;
                        bikeToEdit.ManufacturyId = Manufacturies.Where(x => x.ManufacturyTitle.ToLower() == ManufacturyCB.Text.ToLower()).Select(x => x.ManufacturyId).FirstOrDefault();
                        bikeToEdit.TypeId = Types.Where(x => x.TypeTitle.ToLower() == TypeCB.Text.ToLower()).Select(x => x.TypeId).FirstOrDefault(); ;
                        bikeToEdit.MaterialId = Materials.Where(x => x.MaterialTitle.ToLower() == MaterialCB.Text.ToLower()).Select(x => x.MaterialId).FirstOrDefault();
                        bikeToEdit.BreakTypeId = BreakTypes.Where(x => x.BreakTypeTitle.ToLower() == BreakTypeCB.Text.ToLower()).Select(x => x.BreakTypeId).FirstOrDefault();
                        bikeToEdit.Size = Int32.Parse(SizeTB.Text);
                        bikeToEdit.SpeedCount = Int32.Parse(SpeedCountTB.Text);
                        bikeToEdit.WheelDiameter = Double.Parse(WheelTB.Text);
                        bikeToEdit.PhotoPath = PhotoPathTB.Text;
                        bikeToEdit.Price = Decimal.Parse(PriceTB.Text);
                        update = true;
                    }
                    else
                    {
                        MessageBox.Show("Not all datas are filled", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                //});
                //await Task.Run(() =>
                //{

                    if (add)
                    {
                        // await Task.Run(() => { 

                        string URI = "https://localhost:44348/api/bike";
                        var jsonToSend = JsonConvert.SerializeObject(bikeToEdit);
                        MessageBox.Show(jsonToSend);
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                            string HtmlResult = wc.UploadString(URI, jsonToSend);
                        //MessageBox.Show(HtmlResult);
                        }
                        // });
                        MessageBox.Show("Datas was loaded on server", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    //else
                    //{
                    //    MessageBox.Show("Datas wasn't loaded on server", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //}
                    else if (update)
                    {
                        // await Task.Run(() => { 

                        string URI = "https://localhost:44348/api/bike";
                        var jsonToSend = JsonConvert.SerializeObject(bikeToEdit);
                        MessageBox.Show(jsonToSend);
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                            string HtmlResult = wc.UploadString(URI, "PUT", jsonToSend);
                        }
                        // });
                        MessageBox.Show("Bike was updated and loaded on server", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Bike wasn't updated", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                //});
            }

            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        private void SpeedCountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text) == null || !(e.Text).All(char.IsDigit))
            {
                e.Handled = true;
            }
        }
        private void SizeTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text) == null || !(e.Text).All(char.IsDigit))
            {
                e.Handled = true;
            }
        }
        private void WheelTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text) == null || !(e.Text).All(char.IsDigit))
            {
                e.Handled = true;
            }
        }
        private void Price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text) == null || !(e.Text).All(char.IsDigit))
            {
                e.Handled = true;
            }
        }

        private void SpeedCount__PreviewTextInput(object sender, TextCompositionEventArgs e)
        { 
            if ((e.Text) == null || !(e.Text).All(char.IsDigit))
            {
                e.Handled = true;
            }
        }
        private static List<Manufactury> GetAllManufacturies()
        {
            List<Manufactury> manufacturies = null;
            string json = new WebClient().DownloadString("https://localhost:44348/api/manufactury");
            if (!string.IsNullOrEmpty(json))
            {
                manufacturies = JsonConvert.DeserializeObject<List<Manufactury>>(json);
               // MessageBox.Show("Manufacturies were loaded");
            }
            else
            {
                MessageBox.Show("Manufacturies weren't loaded");
            }
            return manufacturies;
        }
        private static List<MVCBikeShop.Models.Type> GetAllTypes()
        {
            List <MVCBikeShop.Models.Type> types = null;
            string json = new WebClient().DownloadString("https://localhost:44348/api/type");
            if (!string.IsNullOrEmpty(json))
            {
                types = JsonConvert.DeserializeObject<List<MVCBikeShop.Models.Type>>(json);
               // MessageBox.Show("Types was loaded");
            }
            else
            {
                MessageBox.Show("Types wasn't loaded");
            }
            return types;
        }
        private static List<Material> GetAllMaterials()
        {
            List<Material> materials = null;
            string json = new WebClient().DownloadString("https://localhost:44348/api/material");
            if (!string.IsNullOrEmpty(json))
            {
                materials = JsonConvert.DeserializeObject<List<Material>>(json);
                // MessageBox.Show("Materials were loaded");
            }
            else
            {
                MessageBox.Show("Materials weren't loaded");
            }
            return materials;
        }
        private static List<BreakType> GetAllBreakTypes()
        {
            List<BreakType> breakType = null;
            string json = new WebClient().DownloadString("https://localhost:44348/api/break");
            if (!string.IsNullOrEmpty(json))
            {
                breakType = JsonConvert.DeserializeObject<List<BreakType>>(json);
                //MessageBox.Show("BreakTypes were loaded");
            }
            else
            {
                MessageBox.Show("BreakTypes weren't loaded");
            }
            return breakType;
        }

        //private List<Bike> GetAllBikes()
        //{
        //    List<Bike> bikes = null;
        //    string json = new WebClient().DownloadString("https://localhost:44348/api/bike");
        //    if (!string.IsNullOrEmpty(json))
        //    {
        //        bikes = JsonConvert.DeserializeObject<List<Bike>>(json);
        //        //MessageBox.Show("Bikes were loaded");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Bikes weren't loaded");
        //    }
        //    return bikes;
        //}
        private List<Bike> GetAllBikes()
        {
            using (WebClient wc = new WebClient())
            {
                List <Bike> bikesList = null;
                string HtmlResult = wc.DownloadString($"https://localhost:44348/api/bike");
                if(!String.IsNullOrEmpty(HtmlResult))
                {
                    bikesList = JsonConvert.DeserializeObject<List<Bike>>(HtmlResult);
                }
                else
                {
                    MessageBox.Show("Bikes weren't loaded");
                }
                return bikesList;
            }
        }
    }
}
