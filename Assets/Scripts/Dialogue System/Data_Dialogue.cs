using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


[CreateAssetMenu(fileName = "newDialogueData",menuName = "Data/Dialogue/Dialogue Data")]
public class Data_Dialogue : ScriptableObject
{
  
  
  [Header("Dialogues")]
  [SerializeField] public string[] dialogues;
  
  [Header("Printing")]
  public float timeBetweenEachLetter = 0.3f;
 

  [Header("Dialogue Portrait")] public Sprite dialoguePortrait;


}
