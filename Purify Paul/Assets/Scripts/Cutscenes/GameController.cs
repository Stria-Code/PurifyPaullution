using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SceneController;

public class GameController : MonoBehaviour
{
    private GameScene currentScene;
    public BottomBarController bottomBar;
    public AudioController audioController;
    public StoryScene endingScene;
    public Image charImage;
    public Sprite newSprite;
    public int dilfMomentInt;
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
                    
                SceneController.Instance.LoadScene((currentScene as StoryScene).nextScene);
                    
                }
                else
                {
                    if ((currentScene as StoryScene) == endingScene)
                    {
                        for (int i = (currentScene as StoryScene).sentences.Count - 1; i >= 0; i--)
                        {
                            //StoryScene.Sentence sentence = (currentScene as StoryScene).sentences[i];

                            if (i == dilfMomentInt)
                            {
                                charImage.sprite = newSprite;
                            }
                        }
                    }
                    StoryScene.Sentence sentence = bottomBar.PlayNextSentence();
                    PlayAudio(sentence);
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
        audioController.PlaySound(sentence.sound);
    }
}
