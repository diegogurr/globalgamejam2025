using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class CanvasManager : MonoBehaviour
{
        public TMP_Text winnerText; // Assegna questo campo direttamente nell'Inspector
        public TMP_Text resetting;    
        public Button menuButton;
    void Start(){
        menuButton.gameObject.SetActive(false);
        winnerText.gameObject.SetActive(false);
        resetting.gameObject.SetActive(false);
    }
    public void LoadMenu(){
        SceneManager.LoadScene(1);
    }
    public void endGame(string text){
        menuButton.gameObject.SetActive(true);
        winnerText.gameObject.SetActive(true);
        winnerText.text=text;
        resetting.gameObject.SetActive(true);
        StartCoroutine(Resetting(3));
    }
    IEnumerator Resetting(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(0);
    }
}
