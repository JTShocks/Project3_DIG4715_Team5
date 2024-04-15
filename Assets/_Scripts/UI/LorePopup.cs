using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LorePopup : MonoBehaviour
{
    // Start is called before the first frame update




    
    Coroutine tutorial;
    Animator animator;
    [SerializeField] TextMeshProUGUI lore_text;
    [SerializeField] TextMeshProUGUI lore_Title;

        void OnEnable()
    {
        LoreManager.OnPickupLore += ShowLore;
    }



    void OnDisable(){LoreManager.OnPickupLore -= ShowLore;}

    void Awake(){
        animator = GetComponent<Animator>();
    }


    void ShowLore(Lore lore)
    {

        tutorial = StartCoroutine(LoreText(lore));
        
    }

    IEnumerator LoreText(Lore lore)
    {
        lore_text.text = lore.LoreText;
        lore_Title.text = lore.LoreTitle;
        //animator.SetTrigger("ActivateText");
        yield return new WaitForSeconds(5);
        //Set all the text, have it fade in, then fade out after a few seconds
        //animator.SetTrigger("ClearText");
        yield return new WaitForSeconds(1);
        lore_text.text = "";
        lore_Title.text = "";
    }


}