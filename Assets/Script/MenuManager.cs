using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGame(){
        SceneManager.LoadScene(0);
    }
    public void LoadOptions(){
        SceneManager.LoadScene(3);
    }
    public void Quit(){
        Application.Quit();
    }
    public void perDiego(){
        SceneManager.LoadScene(2);
    }
}
