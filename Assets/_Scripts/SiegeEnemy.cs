using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeEnemy : MonoBehaviour {
    public enum EnemyState
    {
        idle,setLadder,rideLadder,move,dead,attack,hitRecover
    }
    public List<GameObject> points; //ladder points
    public int life;
    public float attackDelay;
    public float stunTime;
    public float speed;

    [SerializeField]
    EnemyState curState;
    EnemyState prevState;
    bool isStun;
    bool turn =false;
    float timeStamp;
    GameObject wayPoint;
    int goalPointIdx;
    // Use this for initialization
	void Start () {
        goalPointIdx = Random.Range(0, 3);
        wayPoint = points[goalPointIdx];
	}
	public EnemyState GetEnemyState()
    {
        return curState;
    }
	// Update is called once per frame
	void Update () {
        StateUpdate();
	}
    public void GetDamage(string from)
    {
        print("으악! 나는"+this.gameObject.name+"인데, "+from+"이 날 때림");
        life--;
        if (life == 0)
        {
            print("난죽택");
            ChangeEnemyState(EnemyState.dead);
            return;
        }
        ChangeEnemyState(EnemyState.hitRecover);

    }
    public void ChangeEnemyState(EnemyState es)
    {
        prevState = curState;
        curState = es;
    }
    void StateUpdate()
    {
        switch (curState)
        {
            case EnemyState.idle:
                ReturnToPrevState();
                break;
            case EnemyState.setLadder:
                //SetLadder();
                break;
            case EnemyState.rideLadder:
                RideLadder();
                break;
            case EnemyState.move:
                Move(wayPoint.transform.position);
                break;
            case EnemyState.dead:
                Dead();
                break;
            case EnemyState.attack:
                //Attack();
                break;
            case EnemyState.hitRecover:
                HitRecover();
                break;
            default:
                break;
        }

    }
    public void SetWayPoint(GameObject go)
    {
        wayPoint = go;
    }
    private void Move(Vector3 point)
    {
        LookPoint(point);
        MoveToDirection(point);
    }
    void LookPoint(Vector3 point)
    {
        //Vector3 dir = point - this.transform.position;
        //float angle = Vector3.SignedAngle(this.transform.forward, dir, Vector3.up);//this.transform.up);
        //this.transform.Rotate(Vector3.up, angle);
        this.transform.LookAt(point);
        Quaternion rot = this.transform.rotation;
        rot.x = 0f;
        rot.z = 0f;
        this.transform.rotation = rot;
    }
    void MoveForward()
    {
        //animation
        Vector3 pos = this.transform.position;
        pos += this.transform.forward * 2f * Time.deltaTime;
        this.transform.position = pos;
    }
    void Dead()
    {
        //animation
        //if(animationEnd){ Destroy(this.gameObject);}
        Invoke("DelayedDestroy", 1.5f);
    }
    /*void SetLadder()
    {
        //call ladder setter
        ChangeEnemyState(EnemyState.rideLadder);
    }*/
    void RideLadder()
    {
        if (wayPoint.name != "LadderTop")
        {
            SetWayPoint(wayPoint.transform.Find("Ladder/LadderTop").gameObject);
        }
        speed = 1f;
        if (this.GetComponent<Rigidbody>().useGravity != false)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
        }
        Move(wayPoint.transform.position);
    }
    void MoveToDirection(Vector3 pos)
    {
        if(IsArrive(this.transform.position, pos))
        {
            print("도챀쿠");
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos, speed * Time.deltaTime);
        }
    }
    /*void Attack()
    {
        if (Time.time - timeStamp > attackDelay)
        {
            //EnemyAttacker Active;
            ChangeEnemyState(EnemyState.idle);
            timeStamp = Time.time;
        }
    }*/
    void HitRecover()
    {
        isStun = true;
        this.GetComponent<Rigidbody>().useGravity = true;
        if(Time.time - timeStamp > stunTime)
        {
            isStun = false;
            ChangeEnemyState(EnemyState.idle);
            timeStamp = Time.time;
        }
    }
    void ReturnToPrevState()
    {
        if (isStun)
        {
            ChangeEnemyState(prevState);
            timeStamp = Time.time;
        }
        ChangeEnemyState(EnemyState.move);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("KillZone"))
        {
            Invoke("DelayedDestroy",1.5f);
            SiegeAssaultManager.S.EnemyArrive();
        }
    }
    bool IsArrive(Vector3 pos1, Vector3 pos2)
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist < 0.1f)
        {
            return true;
        }
        return false;
    }
    void DelayedDestroy()
    {
        Destroy(this.gameObject);
    }
}
