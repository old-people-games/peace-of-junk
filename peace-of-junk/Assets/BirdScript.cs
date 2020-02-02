using System.Collections;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
   public Animator animator;
   public AudioSource audio;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if(!other.CompareTag("Player"))
      {
         return;
      }
      animator.SetTrigger("Win");
      audio.Play();
      StartCoroutine(endGameRoutine());
   }

   public IEnumerator endGameRoutine()
   {
      yield return new WaitForSeconds(10f);
      Application.Quit();
   }
}
