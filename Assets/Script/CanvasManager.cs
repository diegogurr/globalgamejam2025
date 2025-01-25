using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CanvasManager : MonoBehaviour
{
    //public TMP_Text winner;
    public Button menuButton;
    void Start(){
        menuButton.gameObject.SetActive(false);
        /*if(FindObjectOfType<TMP_Text>().gameObject.name=="WinnerText")
        winner=FindObjectOfType<TMP_Text>();
        winner.gameObject.SetActive(false);*/
    }
    public void LoadMenu(){
        SceneManager.LoadScene(1);
    }
}
