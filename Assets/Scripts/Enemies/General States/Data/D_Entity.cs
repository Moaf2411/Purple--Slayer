using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newEntityData" , menuName = "Data/Enemy Data/Entity Data")]
public class D_Entity : ScriptableObject
{
   
   public LayerMask whatIsGround;
   public LayerMask whatIsPlayer;
}
