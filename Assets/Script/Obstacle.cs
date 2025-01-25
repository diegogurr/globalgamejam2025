using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
   private void Awake()
   {
      Destroy(gameObject, 15f);
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.tag == "SuddenDeath")
      {
         Destroy(this.gameObject);
      }
   }
}
