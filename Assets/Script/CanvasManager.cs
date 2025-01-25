using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public void endGame(){
        
            menuButton.gameObject.SetActive(true);
            winnerText.gameObject.SetActive(true);
            resetting.gameObject.SetActive(true);
    
    }
}
