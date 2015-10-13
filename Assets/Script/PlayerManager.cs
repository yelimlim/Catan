using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour 
{
    static PlayerManager _instance = null;

    public GameObject player;
    public Material[] playerColors;

    public int playerNum = 2;
    GameObject[] players = null;

    int playerMaxNum;
    int playerIndex;

    public static PlayerManager Instance()
    {
        if (_instance == null)
        {
            _instance = (PlayerManager)FindObjectOfType(typeof(PlayerManager));

            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<PlayerManager>();
                singleton.name = "PlayerManagerContainer" + typeof(PlayerManager).ToString();

                DontDestroyOnLoad(singleton);
            }
        }

        return _instance;
    }

    public GameObject NextPlayer()
    {
        playerIndex = (playerIndex + 1) % playerNum;
        CameraManager.Instance().OnPlayerCamera(playerIndex);
        Debug.Log("turn" + playerIndex);
        return players[playerIndex];
    }

    public GameObject GetPlayer(int num)
    {
        CameraManager.Instance().OnPlayerCamera(num);
        return players[num];
    }

    public GameObject GetPlayerNonCamera(int num)
    {
        return players[num];
    }

    public void CameraSetting()
    {
        for(int i = 0; i < playerNum; ++i)
        {
            players[i].GetComponent<Player>().playerCamera = CameraManager.Instance().GetCamera(i).transform;
        }
    }

    void Start()
    {
        players = new GameObject[playerNum];
        playerIndex = 0;

        PlayerInstansiate();
    }

    void PlayerInstansiate()
    {
        for (int i = 0; i < playerNum; ++i)
        {
            players[i] = Instantiate(player) as GameObject;
            players[i].transform.position = Vector3.zero;
            players[i].name = player.name + "_" + i;
            players[i].GetComponent<Player>().playerColor = playerColors[i];
            players[i].SetActive(true);
        }
    }

}
