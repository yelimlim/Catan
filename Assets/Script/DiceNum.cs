using UnityEngine;
using System.Collections;

public class DiceNum : MonoBehaviour 
{
    public int diceNum = 0;
    public bool done = false;
    float time = 0.0f;
    
    void Update()
    {
        time += Time.deltaTime;

        if(time > 3.0f)
        {
            done = false;
            gameObject.SetActive(false);

            diceNum = 0;
            time = 0.0f;
        }
    }
}
