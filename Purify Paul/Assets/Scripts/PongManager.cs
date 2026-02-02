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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lives = 3;
        spawnTime = 3f;
        winTime = 120f;
        minY = -4.8f;
        maxY = 4.8f;

        obstacles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if(lives <= 0)
        {
            //GameOverScreen
            //SceneManager.LoadScene(4); whatever scene is a gameover screen
        }
    }

    void CheckWinCondition()
    {
        if(Time.time >= winTime && lives > 0)
        {
            SceneController.Instance.didPong = true;
            SceneController.Instance.nextSpawnPoint = SpawnPointID.FromPong;
            SceneController.Instance.LoadScene("DialogueScene");
        }
    }

    void SpawnProjectile()
    {
        float randomPos = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(transform.position.x, randomPos);
        GameObject instance = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        AddObstacles(instance);
    }


    void ResetMiniGame()
    {
        obstacles.Clear();
        lives = 3;
    }


}
