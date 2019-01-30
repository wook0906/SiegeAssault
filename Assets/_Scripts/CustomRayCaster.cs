using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomRayCaster : MonoBehaviour {
    public GameObject reticlePoint;
    public GameObject clearSentence;
    [SerializeField]
    GameObject targetObj;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        ShootRay();
        if (Input.GetMouseButtonDown(0))
        {
            targetObj.transform.SetParent(this.transform);
            targetObj.layer = LayerMask.NameToLayer("Grabbed");
        }
        if (Input.GetMouseButtonUp(0))
        {
            targetObj.layer = LayerMask.NameToLayer("GrabbableLayer");
            targetObj.transform.SetParent(GameObject.Find("EventSystem").transform);
        }
    }
    void ShootRay()
    {
        RaycastHit hit;
        GameObject headGO = this.gameObject;
        if (Physics.Raycast(headGO.transform.position, headGO.transform.forward, out hit, 10000,~(1<<0)))
        {
            //Debug.DrawLine(headGO.transform.position, hit.point);
            if (hit.transform != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("GrabbableLayer"))
            {
                SetTargetObj(hit.transform.gameObject);
                reticlePoint.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        else
        {
            reticlePoint.GetComponent<SpriteRenderer>().color = Color.red;
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
}
