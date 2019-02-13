using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeEnemyGenerator : MonoBehaviour {
    public List<GameObject> enemyPrefabs;
    public List<Transform> generatePositions;
    public List<GameObject> ladderSetters;
    public float generateDelay;
    int randomIdx;
    // Use this for initialization
	void Start() {
        if(generateDelay == 0)
        {
            generateDelay = 5f;
        }
        InvokeRepeating("GenerateEnemy", 0f, generateDelay);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void GenerateEnemy()
    {
        randomIdx = Random.Range(0, 3);
        GameObject enemyInst = Instantiate(enemyPrefabs[Random.Range(0,2)]);
        enemyInst.transform.position = generatePositions[randomIdx].transform.position;
        for (int j = 0; j < ladderSetters.Count; j++)
        {
            enemyInst.GetComponent<SiegeEnemy>().points[j] = ladderSetters[j];
        }
    }
}
