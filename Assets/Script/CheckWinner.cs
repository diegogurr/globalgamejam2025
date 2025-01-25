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
    winnerText.gameObject.SetActive(false);  // Inizialmente disabilita il testo
    resetting.gameObject.SetActive(false);
    
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
        winnerText.gameObject.SetActive(true);
        resetting.gameObject.SetActive(true);
        Canvas canvas = FindObjectOfType<Canvas>();
        CanvasManager canvasManager = canvas.GetComponent<CanvasManager>();  // Ottieni il CanvasManager dal Canvas
        canvasManager.menuButton.gameObject.SetActive(true);

        CameraShake.instance.Shake(0.5f, 0.1f);

        if (playerOne)
        {
            winnerText.text = "Giocatore 1 ha vinto";
        }
        else
        {
            winnerText.text = "Giocatore 2 ha vinto";
        }

        // Ferma il tempo
        StartCoroutine(Resetting(3));
        
    }
}
    IEnumerator Resetting(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(2);
    }

}
