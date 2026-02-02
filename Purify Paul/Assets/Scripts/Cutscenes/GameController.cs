using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameScene currentScene;
    public BottomBarController bottomBar;
    public AudioController audioController;
    //public SpriteSwitcher spriteSwitcher;
    //public ChooseController chooseController; //stuff from old dialogue system - we probably won't need dialogue choices

    private State state = State.IDLE;

    private enum State
    {
        IDLE, ANIMATE, CHOOSE
    }

    void Start()
    {
        if (currentScene == null)
        {
            currentScene = SceneController.Instance.GetNextDialogue();
        }

        StoryScene storyScene = currentScene as StoryScene;
        bottomBar.PlayScene(storyScene);
        PlayAudio(storyScene.sentences[0]);
    }

    void Update()
    {
        QuitGame();
        if (Input.GetKeyDown(KeyCode.Space) ||  Input.GetMouseButtonDown(0))
        {
            if(state == State.IDLE && bottomBar.IsCompleted())
            {
                if (state == State.IDLE && bottomBar.IsLastSentence())
                {
                    if ((currentScene as StoryScene).nextScene == null)
                    {
                        SceneController.Instance.LoadScene("Scene"); // needs a scene to play if there are no more scenes to play.
                    }
                    else
                    {
                        PlayScene((currentScene as StoryScene).nextScene);
                    }
                }
                else
                { 
                    bottomBar.PlayNextSentence();
                    PlayAudio((currentScene as StoryScene).sentences[bottomBar.GetSentenceIndex()]);
                }
            }
        }
    }

    public void PlayScene(GameScene scene)
    {
        StartCoroutine(SwitchScene(scene));
    }

    private IEnumerator SwitchScene(GameScene scene)
    {
        state = State.ANIMATE;
        currentScene = scene;

        if (scene is StoryScene)
        {
            StoryScene storyScene = scene as StoryScene;

            PlayAudio(storyScene.sentences[0]);
            bottomBar.ClearText();
            yield return null;

            bottomBar.PlayScene(storyScene);
            state = State.IDLE;
        }
    }

    private void QuitGame()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music, sentence.sound);
    }
}
