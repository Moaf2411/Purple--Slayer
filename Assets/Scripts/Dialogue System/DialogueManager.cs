using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class DialogueManager : MonoBehaviour
{
   [SerializeField] private Data_Dialogue data;
   
   [SerializeField] private Transform topLeftPos,bottomRightPos;

   [SerializeField] private LayerMask whatIsPlayer;

   [SerializeField] private PlayerInputHandler playerInput;

   [SerializeField] private Text dialogueBox;

   [SerializeField] public UnityEngine.UI.Image portraitImage;

   private Queue<string> dialogues;
   private Queue<string> dialoguesReverse;

   private string text;

   private bool actionInput;
   private bool isPrinting;

   private float currentTimeBetweenEachLetter;

   
   
   
   private void Awake()
   {
      
      actionInput = false;
      isPrinting = false;
      
      InitializeTheDialogues();

   }

   
   
   
   
   
   
   
   
   
   
   private void Update()
   {

      if (isPrinting) // to speed up the dialogue by pressing the action button
      {
         actionInput = playerInput.actionInput;
         if (actionInput)
         {
            
            currentTimeBetweenEachLetter =
               Mathf.Clamp(currentTimeBetweenEachLetter - 0.03f, 0f, data.timeBetweenEachLetter);
            
            playerInput.SetActionInput(false);
            actionInput = false;

         }
      }
      
      
      if (!isPrinting)
      {
         actionInput = playerInput.actionInput;
      }
      

    
      if (actionInput && CheckForPlayer())
      {
         currentTimeBetweenEachLetter = data.timeBetweenEachLetter;
         
         if (dialogues.Count > 0)
         {
            text = dialogues.Dequeue();
            StartCoroutine(PrintTheDialogue(text));
         }
         else if (dialogues.Count==0)
         {
            dialogueBox.transform.parent.gameObject.SetActive(false);
            portraitImage.sprite = null;
            text = null;
         }

        
         
         
         playerInput.SetActionInput(false);
         actionInput = false;
      }
     
   }

   
   
   
   
   
   
   
   
   
   

   private bool CheckForPlayer()
   {
      
      return Physics2D.OverlapArea(topLeftPos.position, bottomRightPos.position, whatIsPlayer);
      
   }
   
   
   
   
   
   
   
   

   private IEnumerator PrintTheDialogue(string s)
   {
      
      isPrinting = true;
      dialogueBox.transform.parent.gameObject.SetActive(true);
      portraitImage.sprite = data.dialoguePortrait;
      
      string x = null;
      char[] chars = s.ToCharArray();
      for (int i = 0; i < chars.Length; i++)
      {
         x = x + chars[i];
         dialogueBox.text = x.ToString();
         yield return new WaitForSecondsRealtime(currentTimeBetweenEachLetter);
      }

      isPrinting = false;
  
   }







   private void InitializeTheDialogues() // puts the dialogues from the data file into a queue in the start of the game
   {
      
      dialogues = new Queue<string>();
      dialoguesReverse=new Queue<string>();

      for (int i = 0; i < data.dialogues.Length; i++)
      {
         dialoguesReverse.Enqueue(data.dialogues[i]);
      }
      
      for (int i = 0; i < data.dialogues.Length; i++)
      {
         dialogues.Enqueue(dialoguesReverse.Dequeue());
      }
      
   }
   
   
   
   


   private void OnDrawGizmos()
   {
      
      Gizmos.color=Color.green;
      Gizmos.DrawLine(topLeftPos.position,new Vector3(topLeftPos.position.x + bottomRightPos.position.x-topLeftPos.position.x,topLeftPos.position.y));
      Gizmos.DrawLine(bottomRightPos.position,new Vector3(bottomRightPos.position.x + topLeftPos.position.x-bottomRightPos.position.x,bottomRightPos.position.y));
      Gizmos.DrawLine(topLeftPos.position,new Vector3(topLeftPos.position.x,topLeftPos.position.y+bottomRightPos.position.y-topLeftPos.position.y));
      Gizmos.DrawLine(bottomRightPos.position,new Vector3(bottomRightPos.position.x,bottomRightPos.position.y+topLeftPos.position.y-bottomRightPos.position.y));
      
   }
   
}
