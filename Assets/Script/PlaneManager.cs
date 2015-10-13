using UnityEngine;
using System.Collections;

public class PlaneManager : MonoBehaviour 
{
    static PlaneManager _instance = null;

    public GameObject brickPlane;
    public GameObject wheatPlane;
    public GameObject rockPlane;
    public GameObject woodPlane;
    public GameObject sheepPlane;
    public GameObject desertPlane;

    int brickNum = 3;
    int wheatNum = 4;
    int rockNum = 3;
    int woodNum = 4;
    int sheepNum = 4;
    int desertNum = 1;

    int planeNum = 19;
    GameObject[] plane = null;

    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject eight;
    public GameObject nine;
    public GameObject ten;
    public GameObject eleven;
    public GameObject twelve;

    int bigNum = 2;
    int smallNum = 1;

    int numberNum = 18;
    GameObject[] number = null;

    public static PlaneManager Instance()
    {
        if (_instance == null)
        {
            _instance = (PlaneManager)FindObjectOfType(typeof(PlaneManager));

            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<PlaneManager>();
                singleton.name = "PlaneManagerContainer" + typeof(PlaneManager).ToString();

                DontDestroyOnLoad(singleton);
            }
        }

        return _instance;
    }

    public GameObject[] GetPlanes()
    {
        return plane;
    }

    public GameObject FindPlane(float x, float y)
    {
        for(int i = 0; i < planeNum; ++i)
        {
            GameObject obj = plane[i];

            if(obj.transform.position.x == x && obj.transform.position.z == y)
            {
                return obj;
            }
        }

        return null;
    }

    public void Setting()
    {
        ObjsShuffle(plane, planeNum);
        ObjsShuffle(number, numberNum);
        PlaneOn();
    }

    public void GetCard(int dice)
    {
        for (int i = 0; i < planeNum; ++i)
        {
            GameObject obj = plane[i];

            if(obj.GetComponent<Plane>().diceNum != dice)
            {
                continue;
            }

            obj.GetComponent<Plane>().CardOut();
        }
    }

    void Awake()
    {
        plane = new GameObject[planeNum];

        int index = 0;
        PlaneInstantiate(brickPlane, index, brickNum);
        index += brickNum;
        PlaneInstantiate(wheatPlane, index, wheatNum);
        index += wheatNum;
        PlaneInstantiate(rockPlane, index, rockNum);
        index += rockNum;
        PlaneInstantiate(woodPlane, index, woodNum);
        index += woodNum;
        PlaneInstantiate(sheepPlane, index, sheepNum);
        index += sheepNum;
        PlaneInstantiate(desertPlane, index, desertNum);

        number = new GameObject[numberNum];

        index = 0;
        ObjsInstantiate(two, index, smallNum);
        index += smallNum;
        ObjsInstantiate(three, index, bigNum);
        index += bigNum;
        ObjsInstantiate(four, index, bigNum);
        index += bigNum;
        ObjsInstantiate(five, index, bigNum);
        index += bigNum;
        ObjsInstantiate(six, index, bigNum);
        index += bigNum;
        ObjsInstantiate(eight, index, bigNum);
        index += bigNum;
        ObjsInstantiate(nine, index, bigNum);
        index += bigNum;
        ObjsInstantiate(ten, index, bigNum);
        index += bigNum;
        ObjsInstantiate(eleven, index, bigNum);
        index += bigNum;
        ObjsInstantiate(twelve, index, smallNum);
    }

    void Start()
    {

//         plane = new GameObject[planeNum];
// 
//         int index = 0;
//         PlaneInstantiate(brickPlane, index, brickNum);
//         index += brickNum;
//         PlaneInstantiate(wheatPlane, index, wheatNum);
//         index += wheatNum;
//         PlaneInstantiate(rockPlane, index, rockNum);
//         index += rockNum;
//         PlaneInstantiate(woodPlane, index, woodNum);
//         index += woodNum;
//         PlaneInstantiate(sheepPlane, index, sheepNum);
//         index += sheepNum;
//         PlaneInstantiate(desertPlane, index, desertNum);
// 
//         number = new GameObject[numberNum];
// 
//         index = 0;
//         ObjsInstantiate(two, index, smallNum);
//         index += smallNum;
//         ObjsInstantiate(three, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(four, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(five, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(six, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(eight, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(nine, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(ten, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(eleven, index, bigNum);
//         index += bigNum;
//         ObjsInstantiate(twelve, index, smallNum);
    }

    void ObjsInstantiate(GameObject obj, int startIndex, int num)
    {
        for (int i = startIndex; i < startIndex + num; ++i)
        {
            number[i] = Instantiate(obj) as GameObject;
            number[i].transform.position = Vector3.zero;
            number[i].name = obj.name + "_" + i;
            number[i].SetActive(false);
        }
    }

    void PlaneInstantiate(GameObject obj, int startIndex, int num)
    {
        for (int i = startIndex; i < startIndex + num; ++i)
        {
            plane[i] = Instantiate(obj) as GameObject;
            plane[i].transform.position = Vector3.zero;
            plane[i].name = obj.name + "_" + i;
            plane[i].SetActive(false);
        }
    }

    void ObjsShuffle(GameObject[] objs, int num)
    {
        for (int i = 0; i < num; ++i)
        {
            int random = Random.Range(0, num);
            GameObject temp = objs[i];
            objs[i] = objs[random];
            objs[random] = temp;
        }
    }

    void PlaneOn()
    {
        float x = -4.0f;
        float y = 0.0f;
        int lineMax = 5;

        int planeIndex = 0;
        int numberIndex = 0;

        while (planeIndex < planeNum)
        {
            for (int i = 0; i < lineMax; ++i)
            {
                GameObject p = plane[planeIndex];

                if (p.activeSelf != true)
                {
                    p.SetActive(true);

                    p.transform.position = new Vector3(x, 0, y);

                    //Debug.Log("plane" + planeIndex + ", x" + x + ", y" + y);

                    ++planeIndex;
                }

                if (p.GetComponent<Plane>().planeType == RESOURCE.DESERT)
                {
                    x += 2;
                    //Debug.Log("desert");
                    continue;
                }

                if (numberIndex > numberNum - 1)
                {
                    x += 2;
                    continue;
                }

                GameObject n = number[numberIndex];

                if (n.activeSelf != true)
                {
                    n.SetActive(true);

                    n.transform.position = new Vector3(x, 0, y);
                    p.GetComponent<Plane>().diceNum = n.GetComponent<Number>().num;

                    //Debug.Log("number" + numberIndex + "x" + x + ", y" + y);

                    ++numberIndex;
                }

                x += 2;
            }

            if (y < 0.0f)
            {
                y = -y;
                y += 1.5f;
                --lineMax;
                x = -(lineMax - 1);
            }
            else if (y == 0.0f)
            {
                y += 1.5f;
                --lineMax;
                x = -(lineMax - 1);
            }
            else
            {
                y = -y;
                x = -(lineMax - 1);
            }
        }
    }
	
}
