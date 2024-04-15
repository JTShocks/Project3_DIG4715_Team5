using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPopup : MonoBehaviour
{
    
    Coroutine tutorial;
    Animator animator;
    [SerializeField] TextMeshProUGUI tut_text;

    void OnEnable(){AbilitiesManager.OnUnlockAbility += ShowTutorial;}

    void OnDisable(){AbilitiesManager.OnUnlockAbility -= ShowTutorial;}

    void Awake(){
        animator = GetComponent<Animator>();
    }


    void ShowTutorial(Ability ability)
    {

        tutorial = StartCoroutine(TutorialText(ability));
        
    }

    IEnumerator TutorialText(Ability ability)
    {
        tut_text.text = ability.tutorialText;
        animator.SetTrigger("ActivateText");
        yield return new WaitForSeconds(5);
        //Set all the text, have it fade in, then fade out after a few seconds
        animator.SetTrigger("ClearText");
        yield return new WaitForSeconds(1);
        tut_text.text = "";
    }
}
