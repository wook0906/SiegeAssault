using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeArrow : MonoBehaviour {

    public AudioClip hitArrowSound;

    Rigidbody arrowRigid;
    AudioSource audio;
    Transform Bow;
    bool isShoot;
    // Use this for initialization
    void Start () {
        arrowRigid = this.GetComponent<Rigidbody>();
        Bow = GameObject.Find("Bow").transform;
        audio = this.gameObject.AddComponent<AudioSource>();
        audio.clip = hitArrowSound;
	}
	
	// Update is called once per frame
	void Update () {
        if (isShoot)
        {
            this.GetComponent<Rigidbody>().AddForce(Physics.gravity / 2f);
        }
    }

    void DelayedDestroy()
    {
        Destroy(this.gameObject);
    }
    public void Shoot(float force)
    {
        Invoke("DelayedDestroy",5f);
        //arrowRigid.useGravity = true;
        arrowRigid.isKinematic = false;
        this.GetComponent<Collider>().enabled = true;
        arrowRigid.AddForce(Bow.forward * force * 2000f);
        isShoot = true;
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            audio.Play();
            collision.gameObject.SendMessage("GetDamage", this.gameObject.name);
            this.transform.SetParent(collision.transform);
            arrowRigid.useGravity = false;
            arrowRigid.isKinematic = true;
        } 
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            audio.Play();
            collision.gameObject.SendMessage("GetDamage", this.gameObject.name);
            arrowRigid.useGravity = false;
            arrowRigid.isKinematic = true;
            //Destroy(this.gameObject);
        }
    }
}
