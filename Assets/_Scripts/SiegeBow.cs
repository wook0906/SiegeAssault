using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeBow : MonoBehaviour {
    public GameObject arrowPrefab;
    public AudioClip EquipSound;
    public AudioClip bowStringSound;
    public AudioClip shootArrowSound;
    public float shootDelay;

    float timeStamp = 0;
    AudioSource audio;
    GameObject arrowInst;
    Transform bowString;
    Vector3 originBowPosition;
    Transform arrowAnchor;
    float shootForce;
    float touchAxis;
    // Use this for initialization
	void Start () {
        bowString = GameObject.Find("bow_holder").transform;
        originBowPosition = bowString.localPosition;
        arrowAnchor = GameObject.Find("arrowGenerateAnchor").transform;
        if (this.GetComponent<AudioSource>() == null)
        {
            audio = this.gameObject.AddComponent<AudioSource>();
            audio.clip = EquipSound;
            audio.Play();
        }
        if(shootDelay == 0)
        {
            shootDelay = 0.5f;
        }
	}

    // Update is called once per frame
    void Update() {
        if (Time.time - timeStamp > shootDelay)
        {
            if (Input.GetMouseButton(0))
            {
                touchAxis = Input.GetAxis("Horizontal");
                Vector3 pos = bowString.transform.localPosition;
                pos.x += (-touchAxis) * 0.1f;
                if (bowString.localPosition.x < 0.1f)
                {
                    pos.x = 0.1f;
                }
                if (bowString.localPosition.x > 0.8f)
                {
                    pos.x = 0.8f;
                }
                bowString.transform.localPosition = pos;
            }
            if (Input.GetMouseButtonDown(0))
            {
                arrowInst = Instantiate(arrowPrefab);
                arrowInst.transform.position = arrowAnchor.position;
                arrowInst.transform.rotation = arrowAnchor.rotation;
                arrowInst.transform.SetParent(bowString);
                audio.PlayOneShot(bowStringSound);
            }
            if (Input.GetMouseButtonUp(0) && arrowInst != null)
            {
                touchAxis = 0f;
                arrowInst.transform.SetParent(null);
                arrowInst.GetComponent<SiegeArrow>().Shoot(bowString.localPosition.x);
                audio.PlayOneShot(shootArrowSound);
                bowString.localPosition = originBowPosition;
                timeStamp = Time.time;
            }
        }
	}
}
