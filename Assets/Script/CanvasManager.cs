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
        public TMP_Text WinnerText;    
   

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
        BackgroundMusicManager.instance.StopMusic();
        SceneManager.LoadScene(0);
    }
    public void endGame(string text){
        CanvasContainer.SetActive(true);
        menuButton.gameObject.SetActive(true);
        

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
        if (YellowImageWin.gameObject.activeSelf && RedImageWin.gameObject.activeSelf)
        {
            WinnerText.text = "DRAW";
            RectTransform redTransform = RedImageWin.GetComponent<RectTransform>();
            redTransform.anchoredPosition = new Vector2(redTransform.anchoredPosition.x - 60, redTransform.anchoredPosition.y);
        }
        
        StartCoroutine(Resetting(4));
    }
    IEnumerator Resetting(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(2);
    }
   
}
