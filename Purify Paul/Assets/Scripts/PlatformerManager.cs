using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneController;

public class PlatformerManager : MonoBehaviour
{
    bool isCompleted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPoint[] points = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        SpawnPointID target = SceneController.Instance.nextSpawnPoint;

        foreach (SpawnPoint point in points)
        {
            if (point.id == target)
            {
                transform.position = point.transform.position;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteMiniGame()
    {
        SceneController.Instance.didPlatformer = true;
        SceneController.Instance.LoadScene("DialogueScene");
    }

    //Added by Morgan
    public void LoadScene(string sceneName)
    {
        SceneController.Instance.LoadScene(sceneName);
    }
}
