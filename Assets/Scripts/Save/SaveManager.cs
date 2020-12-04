using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    //TODO we need to implement Loading the save file (from the main menu not from the pause menu)
    //TODO and implement dealing with multiple save files
    
    
    private Save save;
    
    
    
    
    public void Save()
    {
        
        save=new Save();
        
        save.saveFileName = @"C:\Users\Mo-Af\Documents\zzzz";
        save.saveFileName += @"\save1.json";
        
        save.x = FindObjectOfType<Player>().transform.position.x;
        save.y = FindObjectOfType<Player>().transform.position.y;
        save.z = FindObjectOfType<Player>().transform.position.z;
       
        WriteOnFile(save.saveFileName);
        
    }
    
    
    
    
    
    
    
    
    

    private void WriteOnFile(string address)
    {
        
        JsonSerializer serializer=new JsonSerializer();
        serializer.Formatting = Formatting.Indented;
        
        using (StreamWriter writer=new StreamWriter(address))
        {
            using (JsonWriter jsonWriter=new JsonTextWriter(writer))
            {
                
                try
                {
                    serializer.Serialize(jsonWriter,save);
                    
                }
                catch (Exception e)
                {
                    Debug.Log("Save Unsuccessful");
                }   
                
            }
            writer.Close();
        }
        
       
        
    }
    
    
    
}
