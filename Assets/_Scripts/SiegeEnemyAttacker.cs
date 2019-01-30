using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeEnemyAttacker : MonoBehaviour {
    public AudioClip hitSound;
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
            other.gameObject.SendMessage("GetDamage", this.gameObject.name);
        }
    }
}
