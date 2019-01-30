using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeAssaultManager : MonoBehaviour {
    public static SiegeAssaultManager S;
    int numOfArrivedEnemy;
    // Use this for initialization
    private void Awake()
    {
        S = this;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void EnemyArrive()
    {
        numOfArrivedEnemy++;
        if (numOfArrivedEnemy >= 5)
        {
            print("앜ㅋ 못막음 우린 끝이양");
            //GameOver
        }
    }
}
