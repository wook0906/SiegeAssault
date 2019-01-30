using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeSword : MonoBehaviour {
    GameObject hitBox;
    public float swordAttackDelay;
    public AudioClip swordSwingSound;
    public AudioClip EquipSound;
    float timeStamp = 0f;
    AudioSource audio;
    // Use this for initialization
	void Start () {
        hitBox = GameObject.Find("SwordHitBox/Cube");
        if (this.GetComponent<AudioSource>() == null)
        {
            audio = this.gameObject.AddComponent<AudioSource>();
            audio.clip = EquipSound;
            audio.Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - timeStamp > swordAttackDelay)
            {
                hitBox.GetComponent<BoxCollider>().enabled = true;
                hitBox.GetComponent<MeshRenderer>().enabled = true;//이후삭제
                audio.PlayOneShot(swordSwingSound);
                //Sword Animation
                timeStamp = Time.time;
            }
        }
        if (Time.time - timeStamp > 0.1f)
        {
            hitBox.GetComponent<MeshRenderer>().enabled = false;//이후삭제
            hitBox.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
