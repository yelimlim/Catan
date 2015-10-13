using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
    public BUILDING buildingType = BUILDING.NONE;
    public GameObject plane = null;
    public GameObject owner = null;

    public Material alpha;

    void Start()
    {
        GetComponent<Renderer>().material = alpha;
    }

    void OnMouseOver()
    {
        if(owner != null)
        {
            return;
        }

        Material ownerColor = GameManager.Instance().onPlayer.GetComponent<Player>().playerColor;

        GetComponent<Renderer>().material.color = new Color(ownerColor.color.r, ownerColor.color.g, ownerColor.color.b, 0.5f);
    }

    void OnMouseExit()
    {
        if (owner != null)
        {
            return;
        }

        GetComponent<Renderer>().material = alpha;
    }

    void OnMouseDown()
    {
        //Make Dictionary???

        GAMESTATE gameState = GameManager.Instance().gameState;

        if(gameState == GAMESTATE.FIRSTBUILDING || gameState == GAMESTATE.FIRSTSTREET)
        {
            FirstSetting();
        }
        else
        {
            Setting();
        }
    }

    void FirstSetting()
    {
        switch (buildingType)
        {
            case BUILDING.VILLAGE:

                if (owner != null)
                {
                    buildingType = BUILDING.CITY;
                    return;
                }
                if(!BuildingManager.Instance().BuildingRuleOne(gameObject.transform.position.x, gameObject.transform.position.z))
                {
                    return;
                }

                GameManager.Instance().onPlayer.GetComponent<Player>().AddBuilding(gameObject);
                
                break;
            case BUILDING.STREET:
                if (owner != null)
                {
                    return;
                }
                break;
        }

        owner = GameManager.Instance().onPlayer;
        GetComponent<Renderer>().material = owner.GetComponent<Player>().playerColor;
        GameManager.Instance().SettingDone();
    }

    void Setting()
    {
        switch(buildingType)
        {
            case BUILDING.VILLAGE:
                if (owner != null)
                {
                    buildingType = BUILDING.CITY;
                    return;
                }
                if(!BuildingManager.Instance().BuildingRuleOne(gameObject.transform.position.x, gameObject.transform.position.z))
                {
                    return;
                }
                if (!BuildingManager.Instance().BuildingRuleTwo(gameObject.transform.position.x, gameObject.transform.position.z, GameManager.Instance().onPlayer))
                {
                    return;
                }

                GameManager.Instance().onPlayer.GetComponent<Player>().AddBuilding(gameObject);
                
                break;
            case BUILDING.STREET:
                if(owner != null)
                {
                    return;
                }
                break;
            case BUILDING.CITY:
                if(owner != null)
                {
                    return;
                }
                break;
        }

        if (!GameManager.Instance().onPlayer.GetComponent<Player>().PayBuilding(buildingType))
        {
            return;
        }

        owner = GameManager.Instance().onPlayer;
        GetComponent<Renderer>().material = owner.GetComponent<Player>().playerColor;
    }
}
