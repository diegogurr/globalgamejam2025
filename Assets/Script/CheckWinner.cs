using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class CheckWinner : MonoBehaviour
{
        public TMP_Text winnerText; // Assegna questo campo direttamente nell'Inspector
        public TMP_Text resetting;

void Start()
{
   
    
}

void OnCollisionEnter2D(Collision2D collider)
{
    if (collider.gameObject.tag == "Player")
    {
        bool playerOne = collider.gameObject.GetComponent<BubbleMovement>().isPlayerOne;
        if(collider.gameObject.GetComponentInChildren<BubbleMovement>().isPlayerOne)
            collider.gameObject.GetComponentInChildren<Animator>().Play("ExplodingFishYellow");
            else
            collider.gameObject.GetComponentInChildren<Animator>().Play("ExplodingFishRed");
        collider.gameObject.GetComponentInChildren<Animator>().speed =1;
        collider.gameObject.GetComponent<BubbleMovement>().isGameEnded=true;

        if (collider.gameObject.GetComponent<BubbleMovement>().isPlayerOne)
        {
            AudioManager.instance.PlaySoundSFX("Siu");
            FindObjectOfType<Canvas>().GetComponent<CanvasManager>().endGame("Red");
        }
        else if (!collider.gameObject.GetComponent<BubbleMovement>().isPlayerOne)
        {
            AudioManager.instance.PlaySoundSFX("Ohyeah");
            FindObjectOfType<Canvas>().GetComponent<CanvasManager>().endGame("Yellow");
        }



        CameraShake.instance.Shake(0.5f, 0.1f);
        AudioManager.instance.PlaySoundSFX("Explosion");
        AudioManager.instance.StopLoopingSound();
    

        // Ferma il tempo
        StartCoroutine(Resetting(4));
        
    }
}
    IEnumerator Resetting(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(2);
    }

}
