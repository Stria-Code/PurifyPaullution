using UnityEngine;

public class AutoNextScene : MonoBehaviour
{
    public float waitTime;
    public string scene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("LoadScene",waitTime);
    }

    private void LoadScene()
    {
        SceneController.Instance.LoadScene(scene);
    }
}
