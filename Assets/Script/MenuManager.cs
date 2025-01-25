using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
