using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //my face when unity won't use the stuff im giving it

public class SpriteSwitcher : MonoBehaviour
{
    //for some reason this fuckass code gives errors sometimes
    //UnityEngine.UI is there but it doesn't use it, like a homeless man ignoring £1000
    public bool isSwitched = false;
    public Image image1;
    public Image image2;
    public Animator animator;

    public void SwitchImage(Sprite sprite)
    {
        if(!isSwitched)
        {
           // image2.sprite = sprite; //these specifically, unity will try to tell you some bs that images dont use sprites but they do
           // animator.SetTrigger("SwitchFirst"); //comment these out if you need im out of energy for anything to do with dialogue
        }
        else
        {
          //  image1.sprite = sprite;
          //  animator.SetTrigger("SwitchSecond");
        }
        isSwitched = !isSwitched;
    }

    public void SetImage(Sprite sprite)
    {
        if (!isSwitched)
        {
           // image1.sprite = sprite;
        }
        else
        {
           // image2.sprite = sprite;
        }
    }
}
