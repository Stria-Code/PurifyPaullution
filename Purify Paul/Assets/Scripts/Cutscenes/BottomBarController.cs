using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottomBarController : MonoBehaviour
{
    public TextMeshProUGUI barText;
    public TextMeshProUGUI personNameText;
    public AudioClip textSound; //this would be used for voicelines, animal crossing-esque sounds but we don't have any audio atm

    private int sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;
    private AudioSource audioSource;

    private enum State
    {
        PLAYING, COMPLETED
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
        
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex;
    }

    public void Hide()
    {
        if (!isHidden)
        {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void ClearText()
    {
        barText.text = "";
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text));
        personNameText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        personNameText.color = currentScene.sentences[sentenceIndex].speaker.textColor;
        audioSource.clip = textSound;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeText(string text)
    {
        //just rests the text
        barText.text = "";

        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            barText.text += text[wordIndex];
            
            if(text[wordIndex].Equals('.') || text[wordIndex].Equals(',') || text[wordIndex].Equals('?') || text[wordIndex].Equals('!') || text[wordIndex].Equals('*')) 
            { //this shit is for pauses whenever there's punctuation like a full stop etc
                yield return new WaitForSeconds(0.5f);
            }
            else if (text[wordIndex].Equals(' ') || text[wordIndex].Equals('\''))
            {
                yield return new WaitForSeconds(0.025f);
            }
            else //yeah animal crossing sounds happen here
            {
                //audioSource.pitch = Random.Range(0.9f, 1.05f);
                //audioSource.Stop();
               // audioSource.Play();
                yield return new WaitForSeconds(0.025f);
            }
            

            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
