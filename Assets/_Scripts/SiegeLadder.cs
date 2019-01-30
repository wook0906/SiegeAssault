using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeLadder : MonoBehaviour {
    public int ladderHp;
    [SerializeField]
    Transform ladderTop;
    [SerializeField]
    List<GameObject> enemys;
    // Use this for initialization
    void Start () {
        if (ladderHp == 0)
        {
            ladderHp = 2;
        }
        ladderTop = this.transform.Find("LadderTop");
	}
	
	// Update is called once per frame
	void Update () {
        MissingCheck();	
	}
    public void GetDamage()
    {
        ladderHp--;
        if (ladderHp == 0)
        {
            foreach (var item in enemys)
            {
                item.GetComponent<SiegeEnemy>().ChangeEnemyState(SiegeEnemy.EnemyState.hitRecover);
            }
            enemys.Clear();
            this.gameObject.SetActive(false);
            this.transform.parent.SendMessage("TimeStampRenew");
            ladderHp = 2;
        }
    }
    public Transform GetLadderTopPosition()
    {
        return ladderTop;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (var item in enemys)
            {
                if (item == other.gameObject)
                {
                    return;
                }
            }
            enemys.Add(other.gameObject);
            if (other.gameObject.GetComponent<SiegeEnemy>().GetEnemyState() != SiegeEnemy.EnemyState.rideLadder)
            {
                other.gameObject.SendMessage("ChangeEnemyState", SiegeEnemy.EnemyState.rideLadder);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemys.Remove(other.gameObject);
            other.gameObject.GetComponent<SiegeEnemy>().ChangeEnemyState(SiegeEnemy.EnemyState.hitRecover);
        }
    }
    void MissingCheck()
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                enemys.RemoveAt(i);
            }
        }
    }
}
