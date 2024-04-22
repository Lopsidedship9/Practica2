using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true; 

    public GameObject sheepPrefab; 
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    private float timeBetweenSpawns = 4f;
    private float Timer = 0f;

    private List<GameObject> sheepList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            Timer = Timer +1f;
            Timer = (float)Math.Round(Timer,1);
            print(Timer);
            print(timeBetweenSpawns);
        }
        if(Timer % 1700 == 0 && timeBetweenSpawns > 0.5)
        {
            timeBetweenSpawns = timeBetweenSpawns - 0.5f;
        }
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[UnityEngine.Random.Range(0, sheepSpawnPositions.Count)].position;
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation); 
        sheepList.Add(sheep); 
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    private IEnumerator SpawnRoutine()
    {
        while(canSpawn) 
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }  

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }
        sheepList.Clear();
    }
}
