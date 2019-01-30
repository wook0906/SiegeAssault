using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeLadderManager : MonoBehaviour {
    [SerializeField]
    GameObject ladder;
    public float setDelay;
    float timeStamp;
    // Use this for initialization
	void Start () {
        ladder = this.transform.Find("Ladder").gameObject;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider coll)
    {
        if(coll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (Time.time - timeStamp > setDelay)
            {
                ladder.SetActive(true);
            }
        }   
    }
    public void TimeStampRenew()
    {
        timeStamp = Time.time;
    }
}
