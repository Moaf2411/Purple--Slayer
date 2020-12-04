using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PauseController : MonoBehaviour
{
   
   private bool pauseInput,isPausing;
   private bool downInput,upInput;
   private bool confirmInput;

   private int index , maxIndex , previousIndex;

   private string buttonTag;
   
   
   [SerializeField] private Canvas pauseMenu;

   [SerializeField] private List<UnityEngine.UI.Image> buttons;

   [SerializeField] private List<Sprite> selectedButtons;
   [SerializeField] private List<Sprite> unSelectedButtons;
   
   [Header("Save")]
   [SerializeField] private SaveManager saveManager;


   
   
   
   

   private void Awake()
   {
      
      pauseInput = false;
      isPausing = false;
      confirmInput = false;

      index = 0;
      previousIndex = 0;
      maxIndex = buttons.Count;
      
      
      
      
      pauseMenu.gameObject.SetActive(false);
      
      previousIndex = index;
      buttons[index].sprite = selectedButtons[index];

   }
   
   
   #region Getting Input

   public void OnPauseInput(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         pauseInput = true;
      }
      
   }

   public void OnDownInput(InputAction.CallbackContext context)
   {

      if (context.started)
      {
         downInput = true;
      }

   }

   public void OnUpInput(InputAction.CallbackContext context)
   {

      if (context.started)
      {
         upInput = true;
      }
   }

   public void OnConfirmInput(InputAction.CallbackContext context)
   {
      if (context.started)
      {
         confirmInput = true;
      }
   }
   
   
   
   #endregion

   
   
   
   
   
   private void Update()
   {
      previousIndex = index;

      if (downInput)
      {
         GoDownMenu();
      }

      if (upInput)
      {
         GoUpMenu();
      }

      buttonTag = buttons[index].gameObject.tag;

      if (pauseInput && !isPausing)
      {
         Pause();
      }

      if (isPausing && pauseInput)
      {
        Resume();
      }

      if (confirmInput && isPausing)
      {
         PerformTheFunctionality();
         confirmInput = false;
      }
      
   }

   


  
   
   
   
   
   
   
   
   #region Button Image Handling

  
   
   
   private void GoUpMenu()
   {
      
         index--;
         if (index < 0)
         {
            index = maxIndex - 1;
         }

         buttons[previousIndex].sprite = unSelectedButtons[previousIndex];
         buttons[index].sprite=selectedButtons[index];
         upInput = false;
      
   }

  
   
   
   private void GoDownMenu()
   {
      
      index++;
      if (index==maxIndex)
      {
         index = 0;
      }
      buttons[previousIndex].sprite=unSelectedButtons[previousIndex];
      buttons[index].sprite=selectedButtons[index];
      downInput = false;
      
   }
   

   #endregion

 

   
   
   
   
   
   #region Buttons Functionality

   
   
   
   
   
   
   
   
   
   private void PerformTheFunctionality()
   {
      switch (buttonTag)
      {
         case "Resume":
            Resume();
            break;
            case "Exit":
               Exit();
               break;
               case "Save":
                  Save();
                  break;
               
      }
      
      
      
   }

   
   
   
   
   
   
   private void Pause()
   {
      isPausing = true;
      pauseInput = false;
      Time.timeScale = 0;
      pauseMenu.gameObject.SetActive(true);
      index = 0;
      buttons[previousIndex].sprite=unSelectedButtons[previousIndex];
      buttons[index].sprite=selectedButtons[index];
      confirmInput = false;
   }

   
   
   
   
   
   
   
   
   
   
   private void Resume()
   {
      pauseInput = false;
      isPausing = false;
      pauseMenu.gameObject.SetActive(false);
      Time.timeScale = 1;
      confirmInput = false;
   }

   
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   private void Save()
   {
      saveManager.Save();
      confirmInput = false;
   }

   

  
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   
   private void Exit()
   {
      //TODO save and exit
   }
   

   #endregion

  
   
   
   
   
   
   
   
   
   
   
   
   
   
}
