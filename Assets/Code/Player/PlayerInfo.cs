using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private float points = 0;
    private float passengers = 0;
    private GameObject destination;
    private float max_passengers = 8;
    private float min_passengers = 1;
    GameObject UIObject;
    UIController UIcontroller;
    Dictionary<int, string> destinations = new Dictionary<int, string>()
        {
        {0,"Liepaja"},
        {1,"Ventspils"},
        {2,"Jelgava"},
        {3,"Riga"},
        {4,"Jekabpils"},
        {5,"Rezekne"},
        {6,"Daugavpils"},
        {7,"Valmiera"}
        };

    // Start is called before the first frame update
    void Awake()
    {
        UIObject = GameObject.Find("UI");
        UIcontroller = UIObject.GetComponent<UIController>();
        UIcontroller.ChangePassengerText(passengers, max_passengers);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log("Colliders tag:" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("City"))
        {
            UnityEngine.Debug.Log("Collided With:" + collision);
            if (collision.gameObject.name == Check_Destination())
            {
                CityInfo cityInfo = collision.gameObject.GetComponent<CityInfo>();
                cityInfo.GivePopulation(Get_passengers());
                Add_points(5 * passengers);
                Remove_passengers();
                Clear_destination();
                
                
                
            }else if(Check_Destination() == null)
            {
                CityInfo cityInfo = collision.gameObject.GetComponent<CityInfo>();
                GenerateDestination(cityInfo);
            }
        }
    }

    public void GenerateDestination(CityInfo location)
    {
        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, 8);
        } while (randomIndex == GetCityIndexFromString(location.GetName()));
        string cityname = destinations[randomIndex];
        
        Set_destination(cityname);
        
        CalculatePassengers(location);
        
    }

    private void CalculatePassengers(CityInfo target)
    {
        int randomPassengers = UnityEngine.Random.Range((int)Get_MinCapacity(), (int)max_passengers);
        float takenPassengers = Mathf.Min(target.GetPopulation(), (float)randomPassengers);
        float remainingPassengers = Mathf.Max(target.GetPopulation() - (float)randomPassengers, 0);
        Add_passengers(takenPassengers);
        target.TakePopulation(takenPassengers);
    }

    private int GetCityIndexFromString(string value)
    {
        foreach(KeyValuePair<int, string> pair in destinations)
        {
            if(pair.Value == value)
            {
                return pair.Key;
            }
        }
        return 0;
    }

    public string Check_Destination()
    {
        if(this.destination == null)
        {
            return null;
        }
        else
        {
            return this.destination.name;
        }
    }

    //Setters
    public void Set_destination(string cityname)
    {
        if(!(cityname == null))
        {
            GameObject city = GameObject.Find(cityname);
            this.destination = city;
            UIcontroller.ChangeDestinationText(destination.name);
        }

    }

    public void Set_MaxCapacity(float number)
    {
        this.max_passengers += number;
        UIcontroller.ChangePassengerText(Get_passengers(), Get_maxpassengers());
    }

    public void Set_MinCapacity(float number)
    {
        this.min_passengers += number;
        UIcontroller.ChangePassengerText(Get_passengers(), Get_maxpassengers());
    }

    public void Clear_destination()
    {
        this.destination = null;
        UIcontroller.ChangeDestinationText("Nav");
    }

    public void Add_points(float points)
    {
        this.points += points;
        UIcontroller.ChangePointText(Get_points());
    }

    public void Remove_points(float points)
    {
        this.points -= points;
        UIcontroller.ChangePointText(Get_points());
    }

    public void Add_passengers(float passengers)
    {
        this.passengers = passengers;
        UIcontroller.ChangePassengerText(Get_passengers(), Get_maxpassengers());
    }

    public void Remove_passengers()
    {
        this.passengers = 0;
        UIcontroller.ChangePassengerText(Get_passengers(), Get_maxpassengers());
    }

    public void set_Transform(float x, float y, float z) 
    {
        transform.position = new Vector3(x, y, z);
    }
    
    public void set_Rotation(float x, float y, float z)
    {
        transform.eulerAngles = new Vector3(x, y, z);
    }

    //Getters

    public float Get_points()
    {
        return points;
    }

    public float Get_passengers()
    {
        return passengers;
    }

    public float Get_maxpassengers()
    {
        return max_passengers;
    }

    public float Get_MinCapacity()
    {
        return min_passengers;

    }

    public float Get_X()
    {
        return transform.position.x;
    }

    public float Get_Y()
    {
        return transform.position.y;
    }

    public float Get_Z()
    {
        return transform.position.z;
    }

    public float Get_RotX()
    {
        return transform.eulerAngles.x;
    }

    public float Get_RotY()
    {
        return transform.eulerAngles.y;
    }

    public float Get_RotZ()
    {
        return transform.eulerAngles.z;
    }
}
