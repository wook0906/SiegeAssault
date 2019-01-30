using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeLadderTop : MonoBehaviour {
    //public GameObject enemyMovePoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.gameObject.transform.position = GameObject.Find("Goal").transform.position;
            other.gameObject.GetComponent<Collider>().isTrigger = true;
            other.gameObject.GetComponent<SiegeEnemy>().enabled = false;
        }
    }
}
