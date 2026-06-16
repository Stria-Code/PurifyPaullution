using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static SceneController;

public class PongManager : MonoBehaviour
{
    private int lives;
    private List<GameObject> obstacles;
    public GameObject projectilePrefab;
    private float spawnTime;
    private float winTime;
    private float minY;
    private float maxY;
    private bool hasFinished = false;
    private float startTime;
    private float timeInScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeInScene = 0f;
        startTime = Time.time;
        lives = 3;
        spawnTime = 3f;
        winTime = 65f;
        minY = -4.8f;
        maxY = 4.8f;

        obstacles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timeInScene = Time.time - startTime;
        //Delay in spawning projectiles
        if (Time.time >= spawnTime)
        {
            SpawnProjectile();
            spawnTime = Time.time + 3;
        }

        CheckObstaclePos();
        CheckLoseCondition();
        CheckWinCondition();
    }


    public void AddObstacles(GameObject obstacle)
    {
        obstacles.Add(obstacle);
    }


    void CheckObstaclePos()
    {
        for (int i = obstacles.Count - 1; i >= 0; i--)
        {
            GameObject obstacle = obstacles[i];

            if (obstacle == null)
            {
                obstacles.RemoveAt(i);
                continue;
            }

            //Destroies obstacles if they've got past the player
            if (obstacle.transform.position.x < -9)
            {
                Destroy(obstacle);
                obstacles.RemoveAt(i);
                lives--;
            }
        }
    }

    void CheckLoseCondition()
    {
        if (hasFinished)
        {
            return;
        }

        if (lives <= 0)
        {
            hasFinished = true;
            ResetMiniGame();
        }
    }

    public int GetLives()
    {
        return lives;
    }

    void CheckWinCondition()
    {

        if (hasFinished)
        {
            return;
        }

        //This timer is for 2 minutes
        if (timeInScene >= winTime && lives > 0)
        {
            hasFinished = true;
            SceneController.Instance.didPong = true;
            SceneController.Instance.nextSpawnPoint = SpawnPointID.FromPong;
            SceneController.Instance.GetNextDialogue();
            SceneController.Instance.LoadScene("Cutscene");
        }
    }

    //Instantiate projectiles
    void SpawnProjectile()
    {
        float randomPos = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(transform.position.x, randomPos);
        GameObject instance = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        AddObstacles(instance);
    }


    void ResetMiniGame()
    {
        SceneController.Instance.LoadScene("Pong Minigame");
        obstacles.Clear();
        lives = 3;
    }


}
