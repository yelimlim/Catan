using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour 
{
    static BuildingManager _instance = null;

    struct Pos
    {
        public float x;
        public float y;
    }

    struct StreetPos
    {
        public Pos s;
        public Pos e;
    }

    struct Street
    {
        public Pos pos;
        public int rotate;
    }

    struct PosContain
    {
        public GameObject building;
        public List<GameObject> street;
        public List<Pos> streetPos;
    }

    public GameObject buildingPos;
    public GameObject street;

    int buildingPosNum = 54;
    GameObject[] buildingPosObject = null;

    int streetNum = 72;
    GameObject[] streetObject = null;

    HashSet<Pos> buildingPosHash;
    HashSet<StreetPos> streetPosHash;

    Dictionary<Pos, List<GameObject>> planeDic = null;
    Dictionary<StreetPos, Street> streetDic = null;
    Dictionary<Pos, PosContain> posDic = null;

    public static BuildingManager Instance()
    {
        if (_instance == null)
        {
            _instance = (BuildingManager)FindObjectOfType(typeof(BuildingManager));

            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<BuildingManager>();
                singleton.name = "BuildingManagerContainer" + typeof(BuildingManager).ToString();

                DontDestroyOnLoad(singleton);
            }
        }

        return _instance;
    }

    public void Setting()
    {
        SettingBuildingPos();
        BuildingPosInstantiate();
        StreetInstantiate();
    }

    public void OnBuilding()
    {
        for (int i = 0; i < buildingPosNum; ++i)
        {
            GameObject obj = buildingPosObject[i];

            if (obj.activeSelf == true)
            {
                continue;
            }

            if(obj.GetComponent<Building>().owner != null)
            {
                continue;
            }

            obj.SetActive(true);
        }
    }

    public void OffBuilding()
    {
        for (int i = 0; i < buildingPosNum; ++i)
        {
            GameObject obj = buildingPosObject[i];

            if (obj.activeSelf == false)
            {
                continue;
            }

            if (obj.GetComponent<Building>().owner != null)
            {
                continue;
            }

            obj.SetActive(false);
        }
    }

    public void OnStreet()
    {
        for (int i = 0; i < streetNum; ++i)
        {
            GameObject obj = streetObject[i];

            if (obj.activeSelf == true)
            {
                continue;
            }

            if (obj.GetComponent<Building>().owner != null)
            {
                continue;
            }

            obj.SetActive(true);
        }
    }

    public void OffStreet()
    {
        for (int i = 0; i < streetNum; ++i)
        {
            GameObject obj = streetObject[i];

            if (obj.activeSelf == false)
            {
               
                continue;
            }

            if (obj.GetComponent<Building>().owner != null)
            {
                continue;
            }

            obj.SetActive(false);
        }
    }

    public GameObject FindBuildingPos(float x, float y)
    {
        for (int i = 0; i < buildingPosNum; ++i)
        {
            GameObject obj = buildingPosObject[i];

            if (obj.transform.position.x == x && obj.transform.position.z == y)
            {
                return obj;
            }
        }

        return null;
    }

    public bool BuildingRuleOne(float x, float y)
    {
        Pos tempPos;
        tempPos.x = x;
        tempPos.y = y;

        foreach(Pos p in posDic[tempPos].streetPos)
        {
            if(posDic[p].building.GetComponent<Building>().owner != null)
            {
                return false;
            }
        }

        return true;
    }

    public bool BuildingRuleTwo(float x, float y, GameObject owner)
    {
        Pos tempPos;
        tempPos.x = x;
        tempPos.y = y;

        foreach(GameObject obj in posDic[tempPos].street)
        {
            if(obj.GetComponent<Building>().owner == owner)
            {
                return true;
            }
        }

        return false;
    }

    void Awake()
    {
        buildingPosObject = new GameObject[buildingPosNum];
        streetObject = new GameObject[streetNum];
        buildingPosHash = new HashSet<Pos>();
        streetPosHash = new HashSet<StreetPos>();
        planeDic = new Dictionary<Pos, List<GameObject>>();
        streetDic = new Dictionary<StreetPos, Street>();
        posDic = new Dictionary<Pos, PosContain>();
    }

    void Start()
    {

    }

    void SettingBuildingPos()
    {
        GameObject[] planes = PlaneManager.Instance().GetPlanes();

        //Debug.ClearDeveloperConsole();

        for(int i = 0; i< planes.Length; ++i)
        {
            GameObject obj = planes[i];

            float x = obj.transform.position.x;
            float y = obj.transform.position.z;

            Pos[] posTemp = new Pos[6];
           
            posTemp[0].x = x + 1.0f;
            posTemp[0].y = y + 0.5f;

            posTemp[1].x = x + 1.0f;
            posTemp[1].y = y - 0.5f;

            posTemp[2].x = x;
            posTemp[2].y = y - 1.0f;

            posTemp[3].x = x - 1.0f;
            posTemp[3].y = y - 0.5f;

            posTemp[4].x = x - 1.0f;
            posTemp[4].y = y + 0.5f;

            posTemp[5].x = x;
            posTemp[5].y = y + 1.0f;

//             buildingPos.Add(a);
//             buildingPos.Add(b);
//             buildingPos.Add(c);
//             buildingPos.Add(d);
//             buildingPos.Add(e);
//             buildingPos.Add(f);

            for (int j = 0; j < posTemp.Length; ++j)
            {
                if (!planeDic.ContainsKey(posTemp[j]))
                {
                    List<GameObject> list = new List<GameObject>();
                    planeDic.Add(posTemp[j], list);
                }

                buildingPosHash.Add(posTemp[j]);
                planeDic[posTemp[j]].Add(obj);
            }

            StreetPos[] streetTemp = new StreetPos[6];
            Street[] street = new Street[6];

            streetTemp[0].s = posTemp[5];
            streetTemp[0].e = posTemp[4];
            street[0].pos.x = x - 0.5f;
            street[0].pos.y = y + 0.75f;
            street[0].rotate = 150;

            streetTemp[1].s = posTemp[1];
            streetTemp[1].e = posTemp[2];
            street[1].pos.x = x + 0.5f;
            street[1].pos.y = y - 0.75f;
            street[1].rotate = 150;

            streetTemp[2].s = posTemp[5];
            streetTemp[2].e = posTemp[0];
            street[2].pos.x = x + 0.5f;
            street[2].pos.y = y + 0.75f;
            street[2].rotate = 30;

            streetTemp[3].s = posTemp[3];
            streetTemp[3].e = posTemp[2];
            street[3].pos.x = x - 0.5f;
            street[3].pos.y = y - 0.75f;
            street[3].rotate = 30;

            streetTemp[4].s = posTemp[4];
            streetTemp[4].e = posTemp[3];
            street[4].pos.x = x - 1.0f;
            street[4].pos.y = y;
            street[4].rotate = 90;

            streetTemp[5].s = posTemp[0];
            streetTemp[5].e = posTemp[1];
            street[5].pos.x = x - 1.0f;
            street[5].pos.y = y;
            street[5].rotate = 90;

            for(int j = 0; j < streetTemp.Length; ++j)
            {
//                 Debug.Log(obj.name);
//                 Debug.Log(j);
//                 Debug.Log(streetTemp[j].s.x);
//                 Debug.Log(streetTemp[j].e.x);

                if(!streetDic.ContainsKey(streetTemp[j]))
                {
                    streetDic.Add(streetTemp[j], street[j]);
                }

                streetPosHash.Add(streetTemp[j]);
            }
        }

        Debug.Log("BuildingPosNum" + planeDic.Count);
        Debug.Log("streetPosNum" + streetPosHash.Count);
        Debug.Log("streetNum" + streetDic.Count);
    }

    void BuildingPosInstantiate()
    {
        int index = 0;

        foreach(Pos i in buildingPosHash)
        {
            buildingPosObject[index] = Instantiate(buildingPos) as GameObject;
            buildingPosObject[index].transform.position = new Vector3(i.x, 0, i.y);
            buildingPosObject[index].name = buildingPos.name + "_" + index;
            buildingPosObject[index].GetComponent<Building>().buildingType = BUILDING.VILLAGE;
            buildingPosObject[index].SetActive(false);

            PosContain temp = new PosContain();
            temp.building = buildingPosObject[index];
            List<GameObject> list = new List<GameObject>();
            temp.street = list;
            List<Pos> posList = new List<Pos>();
            temp.streetPos = posList;
            posDic.Add(i, temp);

            List<GameObject> l = planeDic[i];

            for (int j = 0; j < l.Count; ++j)
            {
                GameObject p = l[j];

                p.GetComponent<Plane>().AddBuilding(buildingPosObject[index]);
            }
            ++index;
        }
    }

    void StreetInstantiate()
    {
        int index = 0;

        foreach(StreetPos i in streetPosHash)
        {
            Street s = streetDic[i];

            streetObject[index] = Instantiate(street) as GameObject;
            streetObject[index].transform.position = new Vector3(s.pos.x, 0, s.pos.y);
            streetObject[index].transform.Rotate(0, s.rotate, 0);
            streetObject[index].name = street.name + "_" + index;
            streetObject[index].GetComponent<Building>().buildingType = BUILDING.STREET;
            streetObject[index].SetActive(false);

            posDic[i.s].street.Add(streetObject[index]);
            posDic[i.e].street.Add(streetObject[index]);
            posDic[i.s].streetPos.Add(i.e);
            posDic[i.e].streetPos.Add(i.s);

            ++index;
        }
    }
}
