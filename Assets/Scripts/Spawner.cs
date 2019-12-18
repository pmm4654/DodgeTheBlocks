using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingBlockPrefab;
    
    // Difficulty variables
    public Vector2 secondsBetweenSpawnsMinMax;
    public int maxDifficulty = 3;

    // Difficulty tracking
    float difficultyIncreasedAtTime;

    // Block Size variables
    [Range(.1f, 2f)]
    public float minBlockSize = .3f;
    [Range(.1f, 2f)]
    public float maxBlockSize = 1.5f;

    public float spawnAngleMax;

    float nextSpawnTime;


    Vector2 screenHalfSizeWorldUnits;
    // Start is called before the first frame update
    void Start()
    {
        float screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        float screenHalfHeightInWorldUnits = Camera.main.orthographicSize;
        screenHalfSizeWorldUnits = new Vector2(screenHalfWidthInWorldUnits, screenHalfHeightInWorldUnits);
    }

    // Update is called once per frame
    void Update()
    {
        //if (ShouldIncreaseDifficulty()) IncreaseDifficulty();

        if(Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            float spawnSize = Random.Range(minBlockSize, maxBlockSize);
            
            float leftBoundary = -screenHalfSizeWorldUnits.x;
            float rightBoundary = screenHalfSizeWorldUnits.x;
            Vector2 spawnPosition = new Vector2(Random.Range(leftBoundary, rightBoundary), screenHalfSizeWorldUnits.y + spawnSize);

            GameObject newBlock = (GameObject)Instantiate(fallingBlockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle)); // 0 rotation
            newBlock.transform.localScale = Vector2.one * spawnSize;
        }
    }

    //private bool ShouldIncreaseDifficulty()
    //{
    //    float timeSinceDifficultyIncreate = Time.time - difficultyIncreasedAtTime;
    //    return Mathf.Round(timeSinceDifficultyIncreate) != 0 && Mathf.Round(timeSinceDifficultyIncreate) % 10 == 0 && secondsBetweenSpawns > ShortestSecondsBetweenSpawns();
    //}

    //void IncreaseDifficulty()
    //{
    //    print("Increased difficulty");
    //    secondsBetweenSpawns *= .9f;
    //    difficultyIncreasedAtTime = Time.time;
    //}

    //private float ShortestSecondsBetweenSpawns()
    //{
    //    return secondsBetweenSpawns / maxDifficulty;
    //}

}
