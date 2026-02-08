using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneController;

public class PlatformerManager : MonoBehaviour
{
    bool isCompleted;
    PlatformerPlayer platformerPlayer;
    [SerializeField]GameObject cameraBounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get the player
        platformerPlayer = FindFirstObjectByType<PlatformerPlayer>();

        //Get the spawn points
        SpawnPoint[] points = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);

        SpawnPointID target = SceneController.Instance.nextSpawnPoint;

        //Checks each spawn point
        foreach (SpawnPoint point in points)
        {
            if (point.id == target)
            {
                //Sets the new player pos
                platformerPlayer.transform.position = point.transform.position;

                //Sets the new camera pos
                if (cameraBounds != null)
                {
                    cameraBounds.transform.position = point.cameraOffset;
                }

                //Sets the camera size
                BoxCollider2D collider = cameraBounds.GetComponent<BoxCollider2D>();
                if (collider != null)
                {
                    collider.size = point.cameraBoundsSize;
                }

                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        
        SceneController.Instance.didPlatformer = true;
        SceneController.Instance.LoadScene(sceneName);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Platformer");
    }
}
