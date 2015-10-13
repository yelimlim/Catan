using UnityEngine;
using System.Collections;

public class ThrowDice : MonoBehaviour 
{
    public Transform cameraTransform;

    public GameObject dice;
    GameObject[] dices = null;
    int diceNum = 2;

    public float forwardPowerMax = 10.0f;
    public float upPowerMax = 5.0f;

    public Transform diceStartTransform;

    public int num = 0;

    public bool didThrow = false;

    //PlayerState playerState = null;

    public void Throw()
    {
        Debug.Log("throw");
        
        for (int i = 0; i < diceNum; ++i)
        {
            GameObject obj = dices[i];
            if (obj.activeSelf == true)
            {
                Debug.Log("continue");
                continue;
            }
            obj.SetActive(true);

            obj.transform.position = diceStartTransform.position;
            obj.transform.Rotate(new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f)));

            Debug.Log(obj.transform.position);
            obj.GetComponent<Rigidbody>().velocity = cameraTransform.forward * forwardPowerMax + Vector3.up * Random.Range(1.0f, upPowerMax);
            obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }
    }

    public int GetDiceNum()
    {
        num = 0;

        for (int i = 0; i < diceNum; ++i)
        {
            GameObject obj = dices[i];

            if(!obj.GetComponent<DiceNum>().done)
            {
                return -1;
            }

            num += obj.GetComponent<DiceNum>().diceNum;
        }

        return num;
    }

	void Start () 
    {
        cameraTransform = Camera.main.transform;

        dices = new GameObject[diceNum];

        for(int i = 0; i < diceNum; ++i)
        {
            dices[i] = Instantiate(dice) as GameObject;
            dices[i].name = "Dice_" + i;
            dices[i].SetActive(false);
        }

        //playerState = GetComponent<PlayerState>();
	}

    void Update()
    {
//         if(!playerState.isTurn)
//         {
//             return;
//         }
        

//         if(Input.GetButtonDown("Fire1"))
//         {
//             didThrow = true;
// 
//             for(int i = 0; i< diceNum; ++i)
//             {
//                 GameObject obj = dices[i];
//                 if(obj.activeSelf == true)
//                 {
//                     continue;
//                 }
//                 obj.SetActive(true);
// 
//                 obj.transform.position = diceStartTransform.position;
//                 obj.GetComponent<Rigidbody>().velocity = cameraTransform.forward * forwardPowerMax + Vector3.up * Random.Range(1.0f,upPowerMax);
//                 obj.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
//             }
//         }
    }
}
