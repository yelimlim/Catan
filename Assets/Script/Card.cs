using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour 
{
    public RESOURCE resource = RESOURCE.NONE;
    public float cardYMax = -1.85f;
    public float cardYMin = -3.5f;

    Hashtable up;
    Hashtable down;

    void Start()
    {
        Vector3 pos = gameObject.transform.position;

        up = new Hashtable();
        up.Add("Position", new Vector3(pos.x, cardYMax, pos.z));
        up.Add("easetype", iTween.EaseType.easeOutQuad);
        up.Add("time", 0.2f);

        down = new Hashtable();
        down.Add("Position", new Vector3(pos.x, cardYMin, pos.z));
        down.Add("easetype", iTween.EaseType.easeOutQuad);
        down.Add("time", 0.2f);
    }

    void OnMouseEnter()
    {
        Vector3 pos = gameObject.transform.position;
        up["Position"] = new Vector3(pos.x, cardYMax, pos.z);
        iTween.MoveTo(gameObject, up);
    }
    
    void OnMouseExit()
    {
        Vector3 pos = gameObject.transform.position;
        down["Position"] = new Vector3(pos.x, cardYMin, pos.z);
        iTween.MoveTo(gameObject, down);

    }
}
