using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    public static WeaponManager S;
    public GameObject bow;
    public GameObject sword;
    AudioSource audio;
    Transform weaponSlot;
    public enum WeaponType
    {
        idle,bow,sword
    }
    [SerializeField]
    WeaponType curWeapon;

    // Use this for initialization
    private void Awake()
    {
        S = this;
    }
    void Start () {
        weaponSlot = GameObject.Find("WeaponSlot").transform;
        audio = this.gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void ChangeWeapon(GameObject go)
    {
        SetWeapon(go);
    }
    void SetWeapon(GameObject go)
    {
        switch (go.tag)
        {
            case "Bow":
                sword.SetActive(false);
                bow.SetActive(true);
                curWeapon = WeaponType.bow;
                break;
            case "Sword":
                bow.SetActive(false);
                sword.SetActive(true);
                curWeapon = WeaponType.sword;
                break;
            default:
                break;
        }
    }
    
    
}
