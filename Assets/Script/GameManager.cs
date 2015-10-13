using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;


public class GameManager : MonoBehaviour 
{
    static GameManager _instance = null;

    public GAMESTATE gameState = GAMESTATE.NONE;

    Dictionary<GAMESTATE, Action> dicState = new Dictionary<GAMESTATE, Action>();
    Dictionary<GAMESTATE, Action> dicUi = new Dictionary<GAMESTATE, Action>();

    Pos[] playerUiPos = null;
    Rect gameStateRect = new Rect(Screen.width / 2 - 100, 10, 200, 25);

    public GameObject onPlayer;
    public int playerNum = 3;
    int temp;

    struct Pos
    {
        public float x;
        public float y;
    }

    int turnNum = 0;

    float nameX = 20;
    float nameY = 10;
    float nameWidth = 100;
    float nameHeight = 25;

    float infoHeight = 50;

    public static GameManager Instance()
    {
        return _instance;
    }

    public void SettingDone()
    {
        if(gameState == GAMESTATE.FIRSTBUILDING)
        {
            BuildingManager.Instance().OffBuilding();

            BuildingManager.Instance().OnStreet();
            gameState = GAMESTATE.FIRSTSTREET;
        }
        else if(gameState == GAMESTATE.FIRSTSTREET)
        {
            BuildingManager.Instance().OffStreet();
            gameState = GAMESTATE.FIRSTSETTING;
        }
    }

	void Start() 
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerUiPos = new Pos[4];
        playerUiPos[0].x = nameX;
        playerUiPos[0].y = nameY;
        playerUiPos[1].x = Screen.width - nameX - nameWidth;
        playerUiPos[1].y = nameY;
        playerUiPos[2].x = nameX;
        playerUiPos[2].y = Screen.height - nameY - nameHeight - infoHeight;
        playerUiPos[3].x = Screen.width - nameX - nameWidth;
        playerUiPos[3].y = Screen.height - nameY - nameHeight - infoHeight;

        dicState[GAMESTATE.NONE] = null;
        dicState[GAMESTATE.IDLE] = null;
        dicState[GAMESTATE.FIRSTSETTING] = FirstSetting;
        dicState[GAMESTATE.FIRSTBUILDING] = null;
        dicState[GAMESTATE.FIRSTSTREET] = null;
        dicState[GAMESTATE.THROWDICE] = null;
        dicState[GAMESTATE.GETCARD] = GetCard;
        dicState[GAMESTATE.TURN] = null;
        dicState[GAMESTATE.BUILDINGSETTING] = null;
        dicState[GAMESTATE.STREETSETTING] = null;

        dicUi[GAMESTATE.NONE] = WaitUi;
        dicUi[GAMESTATE.IDLE] = IdleUi;
        dicUi[GAMESTATE.FIRSTSETTING] = PlayerUi;
        dicUi[GAMESTATE.FIRSTBUILDING] = FirstBuildingUi;
        dicUi[GAMESTATE.FIRSTSTREET] = FirstStreetUi;
        dicUi[GAMESTATE.THROWDICE] = PlayerUi;
        dicUi[GAMESTATE.GETCARD] = PlayerUi;
        dicUi[GAMESTATE.TURN] = TurnUi;
        dicUi[GAMESTATE.BUILDINGSETTING] = BuildingUi;
        dicUi[GAMESTATE.STREETSETTING] = StreetUi;


	}

    void Update()
    {
        if(dicState[gameState] == null)
        {
            return;
        }

        dicState[gameState]();
    }

    void OnGUI()
    {
        dicUi[gameState]();
    }

    void PlayerUi()
    {
        Rect[] nameRect = new Rect[playerNum];
        Rect[] infoRect = new Rect[playerNum];

        for(int i = 0; i < playerNum; ++i)
        {
            GameObject p = PlayerManager.Instance().GetPlayerNonCamera(i);
            Player player = p.GetComponent<Player>();

            nameRect[i] = new Rect(playerUiPos[i].x, playerUiPos[i].y, nameWidth, nameHeight);
            GUI.Box(nameRect[i], p.name);

            infoRect[i] = new Rect(playerUiPos[i].x, playerUiPos[i].y + nameHeight, nameWidth, infoHeight);
            GUI.Box(infoRect[i], "Card : " + player.cardNum + "\n" + "Score : ");
        }
    }

    void GameStart()
    {
        PlaneManager.Instance().Setting();
        BuildingManager.Instance().Setting();
        CameraManager.Instance().Setting();
        PlayerManager.Instance().CameraSetting();
        onPlayer = PlayerManager.Instance().GetPlayer(0);
        playerNum = PlayerManager.Instance().playerNum;
        turnNum = 0;
    }

    //NONE

    void WaitUi()
    {
        Rect rect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 100);

//         Rect textRect = new Rect(Screen.width / 2 - 100, 50, 200, 50);
// 
//         String text = "";
// 
//         String tempToString;
// 
//         if(temp == 0)
//         {
//             tempToString = "";
//         }
//         else
//         {
//             tempToString = temp.ToString();
//         }
//          
//         text = GUI.TextField(textRect, temp.ToString());
// 
//         if (Int32.TryParse(text, out temp))
//         {
// 
//             if(temp == 3 || temp == 4)
//             {
//                 playerNum = temp;
//             }
//         }
//         else
//         {
//             playerNum = 3;
//         }

        if(GUI.Button(rect, "GameStart"))
        {
            GameStart();
            gameState = GAMESTATE.FIRSTSETTING;
        }
    }

    //idle

    void IdleUi()
    {
        PlayerUi();


        Rect rect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 100);

        GUI.Box(gameStateRect, onPlayer.name + " - DiceTime");

        if(GUI.Button(rect, "Roll Dice!"))
        {
            onPlayer.GetComponent<ThrowDice>().Throw();
            gameState = GAMESTATE.GETCARD;
        }
    }


    void FirstSetting()
    {
        if(turnNum == PlayerManager.Instance().playerNum * 2)
        {
            onPlayer = PlayerManager.Instance().GetPlayer(0);
            gameState = GAMESTATE.IDLE;
        }
        else if(turnNum >= PlayerManager.Instance().playerNum)
        {
            turnNum++;
            int num = (PlayerManager.Instance().playerNum * 2) - turnNum;
            Debug.Log(num);
            onPlayer = PlayerManager.Instance().GetPlayer(num);
            BuildingManager.Instance().OnBuilding();
            gameState = GAMESTATE.FIRSTBUILDING;
        }
        else
        {
            turnNum++;
            onPlayer = PlayerManager.Instance().GetPlayer(turnNum - 1);
            BuildingManager.Instance().OnBuilding();
            gameState = GAMESTATE.FIRSTBUILDING;
        }
    }

    void FirstBuildingUi()
    {
        PlayerUi();
        GUI.Box(gameStateRect, onPlayer.name);
    }

    void FirstStreetUi()
    {
        PlayerUi();
        GUI.Box(gameStateRect, onPlayer.name);
    }

    void GetCard()
    {
        int dice = onPlayer.GetComponent<ThrowDice>().GetDiceNum();

        if (dice < 0)
        {
            //Debug.Log("diceoff");
            return;
        }

        Debug.Log("diceon");
        onPlayer.GetComponent<ThrowDice>().didThrow = false;
        PlaneManager.Instance().GetCard(dice);
        gameState = GAMESTATE.TURN;
    }

    void TurnUi()
    {
        PlayerUi();

        int buttonWidth = 100;
        int buttonHeight = 100;

        Rect buildingButton = new Rect((Screen.width / 2) - (buttonWidth * 3 / 2), Screen.height / 2 - 50, buttonWidth, buttonHeight);
        Rect streetButton = new Rect((Screen.width / 2) - (buttonWidth / 2), Screen.height / 2 - 50, buttonWidth, buttonHeight);
        Rect nextbutton = new Rect((Screen.width / 2) + (buttonWidth / 2), Screen.height / 2 - 50, buttonWidth, buttonHeight);

        GUI.Box(gameStateRect, onPlayer.name + " - Turn");

        if(GUI.Button(buildingButton, "Building"))
        {
            BuildingManager.Instance().OnBuilding();
            gameState = GAMESTATE.BUILDINGSETTING;
        }

        if (GUI.Button(streetButton, "Street"))
        {
            BuildingManager.Instance().OnStreet();
            gameState = GAMESTATE.STREETSETTING;
        }

        if (GUI.Button(nextbutton, "Finish Turn"))
        {
            onPlayer = PlayerManager.Instance().NextPlayer();
            gameState = GAMESTATE.IDLE;
        }
    }

    void BuildingUi()
    {
        PlayerUi();

        Rect button = new Rect(Screen.width / 2 - 100, Screen.height - 60, 200, 50);

        GUI.Box(gameStateRect, onPlayer.name + " - BuildingSetting");

        if(GUI.Button(button, "Setting Done"))
        {
            BuildingManager.Instance().OffBuilding();
            gameState = GAMESTATE.TURN;
        }
    }

    void StreetUi()
    {
        PlayerUi();

        Rect button = new Rect(Screen.width / 2 - 100, Screen.height - 60, 200, 50);

        GUI.Box(gameStateRect, onPlayer.name + " - StreetSetting");

        if(GUI.Button(button, "Setting Done"))
        {
            BuildingManager.Instance().OffStreet();
            gameState = GAMESTATE.TURN;
        }
    }
}
