using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Lore : ScriptableObject
{

    [SerializeField] string loreTitle;
    
    [TextArea]
    [SerializeField] string loreText;
    [SerializeField] int indexValue;

    public string LoreTitle => loreTitle;
    public string LoreText => loreText;
    public int IndexValue => indexValue;


}
