using System;
using UnityEngine;

public class JaFoi : MonoBehaviour
{
   public static bool DialogoPapa = false;
   private void Awake()
   {
      if (DialogoPapa)
      {
         Destroy(GameObject.Find("PlaceHolder_HitboxChaoDestrutivel")); 
      }
      
   }
}
