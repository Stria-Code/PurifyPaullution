using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneController;

public class MatchImagesManager : MonoBehaviour
{
    [SerializeField] public MatchPlayer player;
    private List<LineRenderer> lines;
    [SerializeField] public List<GameObject> images;
    private bool hasFinished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lines = new List<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWinCondition();
        CheckLoseCondition();
    }

    private void CheckWinCondition()
    {
        if (hasFinished)
        {
            return;
        }

        if (player.GetMatches() >= 3)
        {
            hasFinished = true;

            SceneController.Instance.didMatchImages = true;
            SceneController.Instance.nextSpawnPoint = SpawnPointID.FromMatch;
            SceneController.Instance.GetNextDialogue();
            SceneController.Instance.LoadScene("Cutscene");
        }
    }


    private void CheckLoseCondition()
    {
        if (hasFinished)
        {
            return;
        }

        if (player.GetLives() <= 0)
        { 
            hasFinished = true;

            ResetMiniGame();
        }
    }

    public void AddLines(LineRenderer line)
    {
        lines.Add(line);
    }

    private void ResetMiniGame()
    {
        player.SetLives(3);
        player.SetMatches(0);

        SceneController.Instance.LoadScene("Match Minigame"); //load this scene again
    }
}
