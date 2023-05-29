using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GoMenu : MonoBehaviour
{

    public PlayerInfo player;
    public GameObject cities;

    private string path;
    private string pathpop;

    void Start()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        pathpop = Application.dataPath + Path.AltDirectorySeparatorChar + "SavePopulation.json";
        if (File.Exists(path))
        {
            UnityEngine.Debug.Log("SaveData.json exists");
            loadPlayerData();
        }

        if (File.Exists(pathpop))
        {
            UnityEngine.Debug.Log("SavePopulation.json exists");
            loadPopulationData();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveData tempsave = new SaveData
            {
                points = player.Get_points(),
                passengers = player.Get_passengers(),
                max_passengers = player.Get_maxpassengers(),
                min_passengers = player.Get_MinCapacity(),
                destination = player.Check_Destination(),
                x = player.Get_X(),
                y = player.Get_Y(),
                z = player.Get_Z(),
                rotx = player.Get_RotX(),
                roty = player.Get_RotY(),
                rotz = player.Get_RotZ()
            };

            CityInfo riga = GameObject.Find("Rīga").GetComponent<CityInfo>();
            CityInfo daugavpils = GameObject.Find("Daugavpils").GetComponent<CityInfo>();
            CityInfo jelgava = GameObject.Find("Jelgava").GetComponent<CityInfo>();
            CityInfo jekabpils = GameObject.Find("Jēkabpils").GetComponent<CityInfo>();
            CityInfo liepaja = GameObject.Find("Liepāja").GetComponent<CityInfo>();
            CityInfo rezekne = GameObject.Find("Rēzekne").GetComponent<CityInfo>();
            CityInfo valmiera = GameObject.Find("Valmiera").GetComponent<CityInfo>();
            CityInfo ventspils = GameObject.Find("Ventspils").GetComponent<CityInfo>();

            SaveDataPopulation tempsavepop = new SaveDataPopulation
            {
                Riga = riga.GetPopulation(),
                Daugavpils = daugavpils.GetPopulation(),
                Jelgava = jelgava.GetPopulation(),
                Jekabpils = jekabpils.GetPopulation(),
                Liepaja = liepaja.GetPopulation(),
                Rezekne = rezekne.GetPopulation(),
                Valmiera = valmiera.GetPopulation(),
                Ventspils = ventspils.GetPopulation()
            };


            savePlayerData(tempsave);
            savePopulationData(tempsavepop);
            SceneManager.LoadScene(0);
        }
    }

    private void savePlayerData(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }

    private void savePopulationData(SaveDataPopulation data)
    {
        string json = JsonUtility.ToJson(data);
        UnityEngine.Debug.Log(json);
        using StreamWriter writer = new StreamWriter(pathpop);
        writer.Write(json);
    }


    private void loadPlayerData()
    {
        string json = File.ReadAllText(path);
        SaveData loadeddata = JsonUtility.FromJson<SaveData>(json);
        player.Add_points(loadeddata.points);
        player.Add_passengers(loadeddata.passengers);
        player.Set_MaxCapacity(loadeddata.max_passengers - 8);
        player.Set_MinCapacity(loadeddata.min_passengers - 1);
        if(!(loadeddata.destination == "")) {
            player.Set_destination(loadeddata.destination);
        }
        player.set_Transform(loadeddata.x, loadeddata.y, loadeddata.z);
        player.set_Rotation(loadeddata.rotx, loadeddata.roty, loadeddata.rotz);
    }

    private void loadPopulationData()
    {
        UnityEngine.Debug.Log(pathpop);

        CityInfo riga = GameObject.Find("Rīga").GetComponent<CityInfo>();
        CityInfo daugavpils = GameObject.Find("Daugavpils").GetComponent<CityInfo>();
        CityInfo jelgava = GameObject.Find("Jelgava").GetComponent<CityInfo>();
        CityInfo jekabpils = GameObject.Find("Jēkabpils").GetComponent<CityInfo>();
        CityInfo liepaja = GameObject.Find("Liepāja").GetComponent<CityInfo>();
        CityInfo rezekne = GameObject.Find("Rēzekne").GetComponent<CityInfo>();
        CityInfo valmiera = GameObject.Find("Valmiera").GetComponent<CityInfo>();
        CityInfo ventspils = GameObject.Find("Ventspils").GetComponent<CityInfo>();

        string json = File.ReadAllText(pathpop);
        SaveDataPopulation loadeddata = JsonUtility.FromJson<SaveDataPopulation>(json);
        riga.setPopulation(loadeddata.Riga);
        daugavpils.setPopulation(loadeddata.Daugavpils);
        jelgava.setPopulation(loadeddata.Jelgava);
        jekabpils.setPopulation(loadeddata.Jekabpils);
        liepaja.setPopulation(loadeddata.Liepaja);
        rezekne.setPopulation(loadeddata.Rezekne);
        valmiera.setPopulation(loadeddata.Valmiera);
        ventspils.setPopulation(loadeddata.Ventspils);

        UnityEngine.Debug.Log(loadeddata.ToString());
    }
}

public class SaveData
{

    //data
    public float points;
    public float passengers;
    public float max_passengers;
    public float min_passengers;
    public string destination;

    //position
    public float x;
    public float y;
    public float z;

    //rotation

    public float rotx;
    public float roty;
    public float rotz;
}

public class SaveDataPopulation
{
    public float Riga;
    public float Daugavpils;
    public float Jelgava;
    public float Jekabpils;
    public float Liepaja;
    public float Rezekne;
    public float Valmiera;
    public float Ventspils;
}