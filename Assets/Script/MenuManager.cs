using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start(){
        
        PlayerPrefs.SetInt("YellowScore", 0);
        PlayerPrefs.SetInt("RedScore", 0);

        
    }
    public void LoadGame(){
        SceneManager.LoadScene(2);
    }
    public void LoadOptions(){
        SceneManager.LoadScene(1);
    }
    public void Quit(){
        Application.Quit();
    }
    public void perDiego(){
        SceneManager.LoadScene(2);
    }
}
