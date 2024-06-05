using Newtonsoft.Json;
using Page_Navigation_App.Data;
using Page_Navigation_App.Model;
using Page_Navigation_App.Model.dto;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

internal static class ArchiveHelpers
{
    private static readonly HttpClient httpClient = new HttpClient();

    public async static Task<ObservableCollection<WaterSampleCarousel>> LoadDataAsync()
    {
        try
        {
            string apiUrl = $"{ConnectionInfo.ApiUrl}/archive"; // Replace with your actual API endpoint
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                var samples = JsonConvert.DeserializeObject<ObservableCollection<WaterSampleCarousel>>(responseString);
                /*
                ObservableCollection<WaterSampleCarousel> waterSampleCarousels = new ObservableCollection<WaterSampleCarousel>();
                for( int i = 0; i < samples.Count; i++)
                {
                    WaterSampleCarousel waterSampleCarousel = new WaterSampleCarousel();
                    waterSampleCarousel.Number = $"Sample {i + 1}";
                    waterSampleCarousel.Ph = samples[i].ph;
                    waterSampleCarousel.Hardness = samples[i].Hardness;
                    waterSampleCarousel.Quality = samples[i].Turbidity;
                    waterSampleCarousels.Add(waterSampleCarousel);
                }
                return waterSampleCarousels;
                */
                return samples;

            }
            else
            {
                MessageBox.Show($"Failed to get data from API. Status Code: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}");
            return null;
        }
    }

public static ObservableCollection<Personnel> GetArchives()
{
        ObservableCollection<Personnel> waterSamples = new ObservableCollection<Personnel>();
        Personnel personnel = new Personnel();
        personnel.FirstName = "Margaret";
        personnel.LastName = "Peacock";
        personnel.Position = "Sales Representative";
        personnel.Age = 24;
        waterSamples.Add(personnel);
        Personnel personnel1 = new Personnel();
        personnel1.FirstName = "Margaret";
        personnel1.LastName = "Peacock";
        personnel1.Position = "Sales Representative";
        personnel1.Age = 24;
        waterSamples.Add(personnel1);
        Personnel personnel12 = new Personnel();
        personnel12.FirstName = "Margaret";
        personnel12.LastName = "Peacock";
        personnel12.Position = "Sales Representative";
        personnel12.Age = 24;
        waterSamples.Add(personnel12);
        Personnel personnel13 = new Personnel();
        personnel13.FirstName = "Margaret";
        personnel13.LastName = "Peacock";
        personnel13.Position = "Sales Representative";
        personnel13.Age = 24;
        waterSamples.Add(personnel13);

        //waterSamples.Add(personnel);
        //waterSamples.Add(personnel2);
        //waterSamples.Add(personnel3);
        //waterSamples.Add(personnel4);

    
    

    return waterSamples;
}
}