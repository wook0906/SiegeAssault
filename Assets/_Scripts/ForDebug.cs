using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForDebug : MonoBehaviour {
    public GameObject ladder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DamageToLadder();
        }
	}
    void DamageToLadder()
    {
        ladder.SendMessage("GetDamage");
    }
}
