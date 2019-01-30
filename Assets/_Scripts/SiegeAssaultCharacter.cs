using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeAssaultCharacter : MonoBehaviour {
    public static SiegeAssaultCharacter S;
    public enum CharState
    {
        idle, controlled, flee
    }
    public bool isControlled = false;
    public GameObject reticlePoint;
    GameObject targetObj;
    [SerializeField]
    CharState curState;
    [SerializeField]
    CharState prevState;
    [SerializeField]
    LayerMask customLayerMask;
    // Use this for initialization
    private void Awake()
    {
        S = this;
    }
    void Start () {
        ChangeCharState(CharState.flee);
        customLayerMask = ~((1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Default")));
    }
	public GameObject GetTargetPos()
    {
        return targetObj;
    }
	// Update is called once per frame
	void Update () {
        if (!isControlled)
        {
            ShootRay();
            if (Input.GetMouseButtonDown(0))
            {
                if (targetObj == null)
                {
                    return;
                }
                if(targetObj.layer == LayerMask.NameToLayer("Weapon"))
                {
                    WeaponManager.S.ChangeWeapon(targetObj);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                
            }
        }
	}
    void ShootRay()
    {
        RaycastHit hit;
        GameObject headGO = this.gameObject;
        if (Physics.Raycast(headGO.transform.position, headGO.transform.forward, out hit, 10000, customLayerMask))
        {
            //Debug.DrawLine(headGO.transform.position, hit.point);
            if (hit.transform != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                SetTargetObj(hit.transform.gameObject);
                reticlePoint.SetActive(true);
                reticlePoint.GetComponent<SpriteRenderer>().material.color = Color.green;
            }
            if(hit.transform!=null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                SetTargetObj(hit.transform.gameObject);
                reticlePoint.SetActive(true);
                reticlePoint.GetComponent<SpriteRenderer>().material.color = Color.red;
            }
        }
        else
        {
            reticlePoint.SetActive(false);
            targetObj = null;
        }
    }
    void SetTargetObj(GameObject obj)
    {
        if (targetObj == null)
        {
            targetObj = obj;
        }
    }
    public void ChangeCharState(CharState cs)
    {
        prevState = curState;
        curState = cs;
    }
    public void StateUpdate()
    {
        switch (curState)
        {
            case CharState.idle:
                break;
            case CharState.controlled:
                isControlled = true;
                reticlePoint.SetActive(false);
                break;
            case CharState.flee:
                isControlled = false;
                reticlePoint.SetActive(true);
                break;
            default:
                break;
        }
    }
}
