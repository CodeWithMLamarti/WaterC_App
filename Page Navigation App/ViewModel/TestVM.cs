using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Page_Navigation_App.Data;
using Page_Navigation_App.Model;
using Page_Navigation_App.Model.dto;
using Page_Navigation_App.Utilities;
using Telerik.Windows.Persistence.Core;

namespace Page_Navigation_App.ViewModel
{
    class TestVM : ViewModelBase, INotifyPropertyChanged
    {
        string result = "0%";
        public string Result { get { return result; } set { result = value; OnPropertyChanged();} }
        string interpretation = "Loading...";
        public string Interpretation { get { return interpretation; } set { interpretation = value; OnPropertyChanged();} }
        public double ph { get; set; }
        public double hardness { get; set; }
        public double solids { get; set; }
        public double chloramines { get; set; }
        public double sulfate { get; set; }
        public double conductivity { get; set; }
        public double organic_carbon { get; set; }
        public double trihalomethanes { get; set; }
        public double turbidity { get; set; }
        public double Ph { get { return ph; } set { ph = value; OnPropertyChanged(); } }
        public double Hardness { get { return hardness; } set { hardness = value; OnPropertyChanged(); } }
        public double Solids { get { return solids; } set { solids = value; OnPropertyChanged(); } }
        public double Chloramines { get { return chloramines; } set { chloramines = value; OnPropertyChanged(); } }
        public double Sulfate { get { return sulfate; } set { sulfate = value; OnPropertyChanged(); } }
        public double Conductivity { get { return conductivity; } set { conductivity = value; OnPropertyChanged(); } }
        public double Organic_carbon { get { return organic_carbon; } set { organic_carbon = value; OnPropertyChanged(); } }
        public double Trihalomethanes { get { return trihalomethanes; } set { trihalomethanes = value; OnPropertyChanged(); } }
        public double Turbidity { get { return turbidity; } set { turbidity = value; OnPropertyChanged(); } }

        private static readonly HttpClient httpClient = new HttpClient();
        public ICommand FetchDataCommand { get; }
        public ICommand UploadDataCommand { get; }
        public ICommand SaveDataCommand { get; }
        public string Quality { get; private set; }

        public TestVM()
        {
            FetchDataCommand = new RelayCommand(FetchDataTask);
            UploadDataCommand = new RelayCommand(UploadDataTask);
            SaveDataCommand = new RelayCommand(SaveDataTask);
        }

        public void Interpret(int percentage)
        {
            if(percentage < 50)
            {
                this.Interpretation = "It is not appropriate";
            }
            else if(percentage > 75) {
                this.Interpretation = "It is Good for Drinking";
            } else
            {
                this.Interpretation = "It is Good for Watering";
            }
        }

        public async void SaveDataTask(Object obj)
        {
            try
            {
                WaterSampleCarousel waterSample = new WaterSampleCarousel
                (
                    this.Ph,
                    this.Hardness,
                    this.Solids,
                    Chloramines = this.Chloramines,
                    Sulfate = this.Sulfate,
                    Conductivity = this.Conductivity,
                    Organic_carbon = this.Organic_carbon,
                    Trihalomethanes = this.Trihalomethanes,
                    Turbidity = this.Turbidity,
                    Quality = this.result

                );
                if(waterSample != null)
                {
                    string json = waterSample.ToJson();

                    // Send the JSON to the API
                    string apiUrl = ConnectionInfo.ApiUrl; // Replace with your API endpoint
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync($"{apiUrl}/archive", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        var resultData = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);

                        if (resultData.TryGetValue("message", out string message) && message == "Data inserted successfully")
                        {
                            MessageBox.Show(message); // Display success message
                        }
                        else
                        {
                            MessageBox.Show("Unexpected response from API.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Failed to send document to API. Status Code: {response.StatusCode}");
                    }
                }
            } catch(Exception ex)
            {

            }
        }

        public async void UploadDataTask(Object obj)
        {
            try
            {
                WaterSampleDTO waterSample = new WaterSampleDTO
                (
                    ph = this.Ph,
                    Hardness = this.Hardness,
                    Solids = this.Solids,
                    Chloramines = this.Chloramines,
                    Sulfate = this.Sulfate,
                    Conductivity = this.Conductivity,
                    Organic_carbon = this.Organic_carbon,
                    Trihalomethanes = this.Trihalomethanes,
                    Turbidity = this.Turbidity
                );
                if (waterSample != null)
                {
                    // Convert the random document to JSON
                    string json = waterSample.ToJson();

                    // Send the JSON to the API
                    string apiUrl = ConnectionInfo.ApiUrl; // Replace with your API endpoint
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync($"{apiUrl}/predict", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        var resultData = JsonConvert.DeserializeObject<Dictionary<string, List<double>>>(responseString);
                        var percentage = resultData["prediction"][0]; // Assuming there's only one prediction value
                        this.Result = $"{percentage}%"; // Corrected assignment to the property
                        Interpret((int)percentage);
                    }
                    else
                    {
                        MessageBox.Show($"Failed to send document to API. Status Code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something Went Wrong!");
            }
        }

        public async void FetchDataTask(Object obj)
        {
            try
            {

                MongoClient client = new MongoClient();
                var waterInfo = client.GetDatabase("WaterC").GetCollection<waterModel>("waterFeatures");

                // Define the aggregation pipeline with $sample and $project stages
                var pipeline = new BsonDocument[]
                {
            new BsonDocument("$sample", new BsonDocument("size", 1)), // Sample size of 1
            new BsonDocument("$project", new BsonDocument("_id", 0))  // Exclude the _id field
                };

                // Execute the aggregation pipeline and get the random document
                var randomDocument =await waterInfo.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();

                if (randomDocument != null)
                {

                    // Convert the random document to JSON
                    string json = randomDocument.ToJson();

                    // Deserialize JSON string to waterModel object
                    waterModel waterData = JsonConvert.DeserializeObject<waterModel>(json);

                    // Assign the values to the ViewModel properties
                    if (waterData != null)
                    {
                        this.Ph = waterData.ph;
                        this.Hardness = waterData.Hardness;
                        this.Solids = waterData.Solids;
                        this.Chloramines = waterData.Chloramines;
                        this.Sulfate = waterData.Sulfate;
                        this.Conductivity = waterData.Conductivity;
                        this.Organic_carbon = waterData.Organic_carbon;
                        this.Trihalomethanes = waterData.Trihalomethanes;
                        this.Turbidity = waterData.Turbidity;
                    }

                    // Send the JSON to the API
                    string apiUrl = ConnectionInfo.ApiUrl; // Replace with your API endpoint
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync($"{apiUrl}/predict", content);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        var resultData = JsonConvert.DeserializeObject<Dictionary<string, List<double>>>(responseString);
                        var percentage = resultData["prediction"][0]; // Assuming there's only one prediction value
                        this.Result = $"{percentage}%"; // Corrected assignment to the property
                        Interpret((int)percentage);
                    }
                    else
                    {
                        MessageBox.Show($"Failed to send document to API. Status Code: {response.StatusCode}");
                    }
                }
                else
                {
                    // Handle the case where no document was found
                    MessageBox.Show("No documents found.");
                }
            } catch
            {
                MessageBox.Show("Something Went Wrong!");
            }
        }




        /*
       public async void FetchDataTask(Object obj)
        {
            MongoClient client = new MongoClient(ConnectionInfo.ConnectionString);
            var waterInfo = client.GetDatabase("WaterC").GetCollection<BsonDocument>("waterFeatures");

            // Define the aggregation pipeline with $sample stage
            var pipeline = new BsonDocument[]
            {
            new BsonDocument("$sample", new BsonDocument("size", 1)) // Sample size of 1
            };

            // Execute the aggregation pipeline and get the random document
            var randomDocument = waterInfo.Aggregate<BsonDocument>(pipeline).FirstOrDefault();

            if (randomDocument != null)
            {
                // Convert the random document to JSON
                string json = randomDocument.ToJson();

                // Send the JSON to the API
                string apiUrl = ConnectionInfo.ApiUrl; // Replace with your API endpoint
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync($"{apiUrl}/predict", content);
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Document successfully sent to API.");
                }
                else
                {
                    Console.WriteLine($"Failed to send document to API. Status Code: {response.StatusCode}");
                }
            }
            else
            {
                // Handle the case where no document was found (unlikely with $sample size 1 if collection has documents)
                Console.WriteLine("No documents found.");
            }
        }
        */
    }
}
