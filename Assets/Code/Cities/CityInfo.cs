using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Text.RegularExpressions;

public class CityInfo : MonoBehaviour
{
    private TextMeshPro[] textMeshPro;
    string cityName;
    float population;
    int city_ID;
    string population_temp;

    Dictionary<string, int> cities = new Dictionary<string, int>()
        {
        {"Rīga", 0},
        {"Daugavpils", 1},
        {"Jelgava", 2},
        {"Jēkabpils", 3},
        {"Unknown", 4},
        {"Liepāja", 5},
        {"Rēzekne", 6},
        {"Valmiera",7},
        {"Ventspils", 8}
        };

    // Start is called before the first frame update
    void Start()
    {
        cityName = gameObject.name;
        textMeshPro = GetComponentsInChildren<TextMeshPro>();
        city_ID = cities[cityName];
        
        if (!File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar + "SavePopulation.json"))
        {
            GeneratePopulation();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro[0].text = cityName + " (Iedzīvotāju skaits: " + population + ")";
    }




    void GeneratePopulation()
    {
        string pattern = @"[1-9]";
        string path = Application.dataPath + "/Population.json";
        string jsonString = File.ReadAllText(path);
        JObject jsonData = JObject.Parse(jsonString);
        JToken result = jsonData.SelectToken("data["+ city_ID +"].values");
        if (result != null)
        {
            string propertyValue = Regex.Replace(result.ToString(), "[^0-9.]", "");
            population_temp = propertyValue;
            population = float.Parse(propertyValue);
        }
    }

   public string GetName()
    {
        return cityName;
    }
   public float GetPopulation()
    {
        return population;
    }

   public void setPopulation(float number)
    {
        this.population = number;
    }

   public void TakePopulation(float number)
    {
        if(population - number >= 0)
        {
            population = population - number;
        }
    }

   public void GivePopulation(float number)
    {
        population -= number;
    }
}
