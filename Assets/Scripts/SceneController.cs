using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public Animator fadeAnimator;      // reference to UI animator
    public bool didPlatformer;
    public bool didPong;
    public bool didMatchImages;

    public SpawnPointID nextSpawnPoint;

    public enum SpawnPointID
    {
        Default,
        FromPong,
        FromMatch
    }

    void Awake()
    {
        // Singleton setup
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        nextSpawnPoint = SpawnPointID.Default;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }


    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        // Fade out before switching
        fadeAnimator.SetTrigger("FadeOut");
        yield return WaitForAnimation("FadeOut");

        //Wait for 3 seconds
        yield return new WaitForSeconds(3f);


        // Load new scene
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            yield return null;
        }

        // Fade in once loaded
        fadeAnimator.SetTrigger("FadeIn");
        yield return WaitForAnimation("FadeIn");
    }


    private IEnumerator WaitForAnimation(string stateName)
    {
        // wait until state starts
        while (!fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }

        // wait until animation completes
        while (fadeAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
    }

    public StoryScene GetNextDialogue()
    {
        // logic for choosing dialogue based on flags

        if(didPlatformer && didMatchImages && didPong)
        {
            return Resources.Load<StoryScene>("CS4_Ending");
        }

        if (didPong)
        {
            return Resources.Load<StoryScene>("CS3_AfterPong");
        }

        if (didMatchImages)
        {
            return Resources.Load<StoryScene>("CS2_AfterMatch");
        }


        return Resources.Load<StoryScene>("CS1_Opening");
    }
}
