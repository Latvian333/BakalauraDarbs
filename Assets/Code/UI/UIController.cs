using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Permissions;

public class UIController : MonoBehaviour
{

    public PlayerInfo player;
    public GameObject DestinationText;
    public GameObject PassengersText;
    public GameObject PointText;
    public GameObject shop;
 

    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void ChangeDestinationText(string Destination)
    {
        TextMeshProUGUI DestinationTextMesh = DestinationText.GetComponent<TextMeshProUGUI>();
        DestinationTextMesh.text = "Galamērķis: " + Destination;
    }

    public void ChangePassengerText(float passengers, float maxcapacity)
    {
        TextMeshProUGUI PassengerTextMesh = PassengersText.GetComponent<TextMeshProUGUI>();
        PassengerTextMesh.text = "Pasažieri: " + passengers + "/" + maxcapacity;
    }

    public void ChangePointText(float points)
    {
        TextMeshProUGUI PointTextMesh = PointText.GetComponent<TextMeshProUGUI>();
        PointTextMesh.text = "Punkti: " + points;
    }

    public void ToggleShop()
    {
        if (shop.activeSelf)
        {
            shop.SetActive(false);
        }
        else
        {
            shop.SetActive(true);
        }
    }

    public void PurchaseMaxCapacity()
    {
        if((player.Get_points() - 10) >= 0)
        {
            player.Set_MaxCapacity(2);
            player.Remove_points(10);
        }
    }

    public void PurchaseMinCapacity()
    {
        if ((player.Get_points() - 10) >= 0 && !(player.Get_MinCapacity() + 1 > player.Get_maxpassengers()))
        {
            player.Set_MinCapacity(1);
            player.Remove_points(10);
        }
    }
}
