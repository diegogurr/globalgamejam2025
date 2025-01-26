using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class CanvasManager : MonoBehaviour
{
        public Image YellowImageWin; // Assegna questo campo direttamente nell'Inspector
        public Image RedImageWin;
        
        public TMP_Text RedWinCounter;    
        public TMP_Text YellowWinCounter;    

        public Button menuButton;
        public GameObject CanvasContainer;
        
    void Start(){
        if(PlayerPrefs.GetInt("YellowScore")==null){
            PlayerPrefs.SetInt("YellowScore", 0);
        }
        if(PlayerPrefs.GetInt("RedScore")==null){
            PlayerPrefs.SetInt("RedScore", 0);
        }
        menuButton.gameObject.SetActive(false);
        YellowImageWin.gameObject.SetActive(false);
        RedImageWin.gameObject.SetActive(false);
        RedWinCounter.gameObject.SetActive(false);
        YellowWinCounter.gameObject.SetActive(false);
        CanvasContainer.SetActive(false);

    }
    public void LoadMenu(){
        SceneManager.LoadScene(0);
    }
    public void endGame(string text){
        Debug.Log("Dentro end Game");
        CanvasContainer.SetActive(true);

        menuButton.gameObject.SetActive(true);
        Debug.Log("Loaded");

        if(text=="Yellow"){
        YellowImageWin.gameObject.SetActive(true);
        PlayerPrefs.SetInt("YellowScore", PlayerPrefs.GetInt("YellowScore")+1);
        }else if(text=="Red"){
        RedImageWin.gameObject.SetActive(true);
        PlayerPrefs.SetInt("RedScore", PlayerPrefs.GetInt("RedScore")+1);
        }        
        YellowWinCounter.gameObject.GetComponent<TMP_Text>().text=PlayerPrefs.GetInt("YellowScore").ToString();
        RedWinCounter.gameObject.GetComponent<TMP_Text>().text=PlayerPrefs.GetInt("RedScore").ToString();

        RedWinCounter.gameObject.SetActive(true);
        YellowWinCounter.gameObject.SetActive(true);

        StartCoroutine(Resetting(3));
    }
    IEnumerator Resetting(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(2);
    }
}
