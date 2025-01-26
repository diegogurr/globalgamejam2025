using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image image1;
    public Image image2;
    bool a=true;
    public void loadMenu(){
        SceneManager.LoadScene(0);
    }
    public void change (){
        if(a){
            a=false;
            image1.gameObject.SetActive(false);
            image2.gameObject.SetActive(true);

        }else{
            a=true;
            image1.gameObject.SetActive(true);
            image2.gameObject.SetActive(false);
        }
    }
}
