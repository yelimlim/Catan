using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
    public PLAYER playerId = PLAYER.NONE;

    public Material playerColor;

    public Transform playerCamera;

    public float cardXSize = 1.5f;
    public float cardLowY = -3.5f;
    public float cameraCardDistance = 5.0f;
    public int cardMaxNum = 5;
    float cameraXMaxSize = 0.0f;

    public int cardNum = 0;
    Dictionary<RESOURCE, List<GameObject>> cardDic = null;
    List<GameObject> resourceCard;
    List<GameObject> building;

    public void AddBuilding(GameObject obj)
    {
        Debug.Log(obj.name);

        building.Add(obj);
    }

    public void AddCard(GameObject obj, int num)
    {
        for(int i = 0; i < num; ++i)
        {
            GameObject card = Instantiate(obj) as GameObject;
            card.transform.position = new Vector3(0, cardLowY, playerCamera.position.z + cameraCardDistance);
            card.SetActive(false);

            cardNum++;
            cardDic[obj.GetComponent<Card>().resource].Add(card);
        }

        SettingCard();
        Debug.Log(obj.name + num);
    }

    public void SettingCard()
    {
        if(cardNum > cardMaxNum)
        {
            OverSetting();
        }
        else
        {
            NormalSetting();
        }
    }

    public bool PayBuilding(BUILDING building)
    {
        switch(building)
        {
            case BUILDING.VILLAGE:
                if(cardDic[RESOURCE.BRICK].Count < 1)
                {
                    //UI
                    return false;
                }
                if (cardDic[RESOURCE.WOOD].Count < 1)
                {
                    //UI
                    return false;
                }
                if(cardDic[RESOURCE.WHEAT].Count < 1)
                {
                    //UI
                    return false;
                }
                if (cardDic[RESOURCE.SHEEP].Count < 1)
                {
                    //UI
                    return false;
                }

                Destroy(cardDic[RESOURCE.BRICK][0]);
                cardDic[RESOURCE.BRICK].RemoveAt(0);

                Destroy(cardDic[RESOURCE.WOOD][0]);
                cardDic[RESOURCE.WOOD].RemoveAt(0);

                Destroy(cardDic[RESOURCE.WHEAT][0]);
                cardDic[RESOURCE.WHEAT].RemoveAt(0);

                Destroy(cardDic[RESOURCE.SHEEP][0]);
                cardDic[RESOURCE.SHEEP].RemoveAt(0);
                break;

            case BUILDING.CITY:
                if (cardDic[RESOURCE.WHEAT].Count < 2)
                {
                    //UI
                    return false;
                }
                if (cardDic[RESOURCE.ROCK].Count < 3)
                {
                    //UI
                    return false;
                }

                for (int i = 0; i < 2; ++i )
                {
                    Destroy(cardDic[RESOURCE.WHEAT][0]);
                    cardDic[RESOURCE.WHEAT].RemoveAt(0);
                }

                for (int i = 0; i < 3; ++i)
                {
                    Destroy(cardDic[RESOURCE.ROCK][0]);
                    cardDic[RESOURCE.ROCK].RemoveAt(0);
                }
                    break;

            case BUILDING.STREET:
                if (cardDic[RESOURCE.WOOD].Count < 1)
                {
                    //UI
                    return false;
                }
                if (cardDic[RESOURCE.BRICK].Count < 1)
                {
                    //UI
                    return false;
                }

                Destroy(cardDic[RESOURCE.BRICK][0]);
                cardDic[RESOURCE.BRICK].RemoveAt(0);

                Destroy(cardDic[RESOURCE.WOOD][0]);
                cardDic[RESOURCE.WOOD].RemoveAt(0);
                break;
        }

        SettingCard();
        return true;
    }

    void Start()
    {
        cardDic = new Dictionary<RESOURCE, List<GameObject>>();
        cardDic[RESOURCE.BRICK] = new List<GameObject>();
        cardDic[RESOURCE.ROCK] = new List<GameObject>();
        cardDic[RESOURCE.SHEEP] = new List<GameObject>();
        cardDic[RESOURCE.WHEAT] = new List<GameObject>();
        cardDic[RESOURCE.WOOD] = new List<GameObject>();

        resourceCard = new List<GameObject>();
        building = new List<GameObject>();

        cameraXMaxSize = cardXSize * (cardMaxNum - 1);
    }

    void NormalSetting()
    {
        float xSize = cardXSize * cardNum;
        float startPos = playerCamera.position.x - (xSize / 2);
        float cardPos = startPos + (cardXSize / 2);
        float cardZPos = playerCamera.position.z + cameraCardDistance;

        foreach (KeyValuePair<RESOURCE, List<GameObject>> kv in cardDic)
        {
            foreach(GameObject obj in kv.Value)
            {
                obj.transform.position = new Vector3(cardPos, cardLowY, cardZPos);
                cardPos += cardXSize;
                cardZPos -= 0.01f;

                if(obj.activeSelf == false)
                {
                    obj.SetActive(true);
                }
            }
        }
    }

    void OverSetting()
    {
        float distance = cameraXMaxSize / (cardNum - 1);
        float cardPos = playerCamera.transform.position.x - (cameraXMaxSize / 2);
        float cardZPos = playerCamera.position.z + cameraCardDistance;

        foreach (KeyValuePair<RESOURCE, List<GameObject>> kv in cardDic)
        {
            foreach (GameObject obj in kv.Value)
            {
                obj.transform.position = new Vector3(cardPos, cardLowY, cardZPos);
                cardPos += distance;
                cardZPos -= 0.01f;

                if (obj.activeSelf == false)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
