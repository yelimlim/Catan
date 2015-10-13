using UnityEngine;
using System.Collections;

public class UiManager : MonoBehaviour 
{
    static UiManager _instance = null;

    GameManager g_GameManager = null;

    public static UiManager Instance()
    {
        if (_instance == null)
        {
            _instance = (UiManager)FindObjectOfType(typeof(UiManager));

            if (_instance == null)
            {
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<UiManager>();
                singleton.name = "UiManagerContainer" + typeof(UiManager).ToString();

                DontDestroyOnLoad(singleton);
            }
        }

        return _instance;
    }

    void Start()
    {
        g_GameManager = GameManager.Instance();
    }

	void OnGUI()
    {
        if(g_GameManager.onPlayer == null)
        {
            return;
        }

        float x = (Screen.width / 2.0f) - 100;

        Rect rect = new Rect(x, 10, 200, 25);
        GUI.Box(rect, g_GameManager.onPlayer.name);
    }
}
