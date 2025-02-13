using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    public int time;
    public int repeatRate;
    public Transform[] spawnPoint;

    public bool vagueFinish = false;
    public float numberEnemy = 5;
    public bool oneTime = false;
    public int numberEnemyAlive;
    public bool oneTime2 = false;
    public GameObject[] Medkit;

    public int transformMax = 5;
    public bool zone2 = false;
    public bool zone3 = false;
    public bool zone4 = false;
    public bool zone5 = false;
    public bool oneTime3 = false;
    public bool oneTime4 = false;
    public bool oneTime5 = false;
    public bool oneTime6 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numberEnemyAlive <= 0 && vagueFinish == false)
        {
            if (oneTime2 == false)
            {
                oneTime2 = true;
                vagueFinish = true;
            }
        }
        if (vagueFinish == true)
        {
            if (oneTime == false)
            {
                oneTime = true;
                Medkit[0].SetActive(true);
                Medkit[1].SetActive(true);
                Medkit[2].SetActive(true);
                Medkit[3].SetActive(true);
                Medkit[4].SetActive(true);
                Medkit[5].SetActive(true);
                Medkit[6].SetActive(true);
                Medkit[7].SetActive(true);
                Medkit[8].SetActive(true);
                StartCoroutine(wait());
            }
        }

        if (zone2 == true)
        {
            if (oneTime3 == false)
            {
                oneTime3 = true;
                transformMax = transformMax + 6;
            }
        }

        if (zone3 == true)
        {
            if (oneTime4 == false)
            {
                oneTime4 = true;
                transformMax = transformMax + 6;
            }
        }

        if (zone4 == true)
        {
            if (oneTime5 == false)
            {
                oneTime5 = true;
                transformMax = transformMax + 6;
            }
        }

        if (zone5 == true)
        {
            if (oneTime6 == false)
            {
                oneTime6 = true;
                transformMax = transformMax + 6;
            }
        }
        
    }

    void Spawner()
    {
        Instantiate(enemy, spawnPoint[Random.Range(0,transformMax)].position, quaternion.identity);
    }

    IEnumerator wait()
    {
        vagueFinish = false;
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < numberEnemy; i++)
        {
            Invoke("Spawner",time);
        }
        numberEnemy = numberEnemy * 1.2f;
        oneTime = false;
        yield return new WaitForSeconds(3f);
        oneTime2 = false;
        StopAllCoroutines();
    }
}
