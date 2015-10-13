using UnityEngine;
using System.Collections;

public class DiceCollider : MonoBehaviour 
{
    public int num = 0;

    float t = 0.0f;


    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Plane")
        {
            t = 0.0f;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(GetComponentInParent<DiceNum>().done)
        {
            return;
        }

        if (collider.transform.tag == "Plane")
        {
            t += Time.deltaTime;

            if(t > 2.0f)
            {
                GetComponentInParent<DiceNum>().diceNum += num;

                int n = GetComponentInParent<DiceNum>().diceNum;

                Debug.Log("diceNum" + n);

                GetComponentInParent<DiceNum>().done = true;
            }
        }
    }
}
