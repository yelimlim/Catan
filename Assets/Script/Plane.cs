using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plane : MonoBehaviour 
{
    public RESOURCE planeType = RESOURCE.NONE;
    public int diceNum = 0;
    public GameObject card;

//     int buildingMax = 3;
//     int buildingNum = 0;
    List<GameObject> building = new List<GameObject>();

    public void AddBuilding(GameObject b)
    {
        building.Add(b);
    }

    public void CardOut()
    {
        for(int i = 0; i < building.Count; ++i)
        {
            GameObject obj = building[i];

            if(!obj.GetComponent<Building>().owner)
            {
                continue;
            }

            Debug.Log(obj.name + obj.GetComponent<Building>().owner.name);

            if(obj.GetComponent<Building>().buildingType == BUILDING.CITY)
            {
                obj.GetComponent<Building>().owner.GetComponent<Player>().AddCard(card, 2);
            }
            else if(obj.GetComponent<Building>().buildingType == BUILDING.VILLAGE)
            {
                obj.GetComponent<Building>().owner.GetComponent<Player>().AddCard(card, 1);
            }
        }
    }
}
