using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
    static CameraManager _instance = null;

    public GameObject CardCamera;
    public GameObject[] CardCameras;
    int cameraNum;

    public int cameraDistance = 20;
    public int z = 100;

    int onCamerNum = 0;

    public static CameraManager Instance()
    {
        if (_instance == null)
        {
            _instance = (CameraManager)FindObjectOfType(typeof(CameraManager));

            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<CameraManager>();
                singleton.name = "CameraManagerContainer" + typeof(CameraManager).ToString();

                DontDestroyOnLoad(singleton);
            }
        }

        return _instance;
    }

    public void Setting()
    {
        CameraInstantiate();
    }

    public void OnPlayerCamera(int playerNum)
    {
//         if(CardCameras[onCamerNum].activeSelf == true)
//         {
//             CardCameras[onCamerNum].SetActive(false);
//         }
//         else
//         {
//             Debug.Log("!!!");
//         }

        CardCameras[onCamerNum].SetActive(false);

        CardCameras[playerNum].SetActive(true);

        onCamerNum = playerNum;

        Debug.Log("Camera" + onCamerNum);
    }

    public GameObject GetCamera(int playerNum)
    {
        return CardCameras[playerNum];
    }

    void Awake()
    {
        cameraNum = PlayerManager.Instance().playerNum;
        CardCameras = new GameObject[cameraNum];
    }

    void CameraInstantiate()
    {
        for (int i = 0; i < CardCameras.Length; ++i)
        {
            CardCameras[i] = Instantiate(CardCamera) as GameObject;
            CardCameras[i].transform.position = new Vector3(i * cameraDistance, 0, z);
            CardCameras[i].name = CardCamera.name + "_" + i;
            CardCameras[i].SetActive(false);
        }
    }
}
