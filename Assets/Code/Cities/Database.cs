
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class Database : MonoBehaviour
{


     
    public string json;
    private string url;
    





    // Start is called before the first frame update
    void Awake()
    {
        if(!File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar  + "Population.json"))
        {
            GetData();
        }
        
        
    }


    void GetData()
    {
        string url = "https://data.stat.gov.lv/api/v1/en/OSP_PUB/START/POP/IR/IRS/IRS050";
        var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        httpRequest.Method = "POST";

        httpRequest.Accept = "application/json";
        httpRequest.ContentType = "application/json";

        var data = @"{
          ""query"": [
            {
              ""code"": ""INDICATOR"",
              ""selection"": {
                ""filter"": ""item"",
                ""values"": [
                  ""URBAN_DENSITY""
                ]
              }
            },
            {
              ""code"": ""AREA"",
              ""selection"": {
                ""filter"": ""vs:VS_AREA_LV_RPNP_PLAN"",
                ""values"": [
                  ""LV0010000"",
                  ""LV0050000"",
                  ""LV0090000"",
                  ""LV0110000"",
                  ""LV0130000"",
                  ""LV0170000"",
                  ""LV0210000"",
                  ""LV0250000"",
                  ""LV0270000""
                ]
              }
            },
            {
              ""code"": ""TIME"",
              ""selection"": {
                ""filter"": ""item"",
                ""values"": [
                  ""2022""
                ]
              }
            }
          ],
          ""response"": {
            ""format"": ""JSON""
          }
        }";

        using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
        {
            streamWriter.Write(data);
        }
        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            var result = streamReader.ReadToEnd();
            File.WriteAllText(Path.Combine(Application.dataPath, "Population.json"), result);
        }
        
    } 

}