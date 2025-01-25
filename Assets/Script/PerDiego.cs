using UnityEngine;
using UnityEngine.UI;
public class PerDiego : MonoBehaviour

{
    public Button button; // Riferimento al bottone
    public string[] urls = {
        "https://www.instagram.com/parkenharbor/p/DFBZcdChZYi/",
        "https://www.instagram.com/parkenharbor/p/DEp9h1hSLzJ/",
        "https://www.instagram.com/parkenharbor/p/DE3DB9pSdBf/?img_index=1",
        "https://www.instagram.com/parkenharbor/p/CqGqQOFJwZO/",
        "https://www.instagram.com/parkenharbor/p/CqinRUKrwMf/",
        "https://www.instagram.com/parkenharbor/p/CrmZAroLnEK/",
        "https://www.instagram.com/parkenharbor/p/CraH9AgO7Va/",
        "https://www.instagram.com/parkenharbor/p/Cwk44k_rSWr/",
        "https://www.instagram.com/parkenharbor/p/CwV1n4HJXN_/",
        "https://www.instagram.com/parkenharbor/p/CxBd-mNpjr0/",
        "https://www.instagram.com/parkenharbor/p/CxfIR7ou3VY/",
        "https://www.instagram.com/parkenharbor/p/CyGtFVgLXQ5/?img_index=1",
        "https://www.instagram.com/parkenharbor/p/CzB03HNrzTf/",
        "https://www.instagram.com/parkenharbor/p/C-GPVO2SV3m/",
        "https://www.instagram.com/parkenharbor/p/C9aYzMgJ85o/",
        "https://www.instagram.com/parkenharbor/p/C9kx-lfS3cc/?img_index=1",
        "https://www.instagram.com/parkenharbor/p/C90cY1pSt_X/",
        "https://www.instagram.com/parkenharbor/p/C8us2pFJh_u/",
        "https://www.instagram.com/parkenharbor/p/C9FvHvBOvpO/?img_index=1",
        "https://www.instagram.com/parkenharbor/p/C8XnMNHSpwk/",
        "https://www.instagram.com/parkenharbor/p/C5m5Z_5subm/",
        "https://www.instagram.com/parkenharbor/p/C77IX0GJRmz/",
        "https://www.instagram.com/parkenharbor/p/DCBFDjDs6aX/",
        "https://www.instagram.com/parkenharbor/p/C_3SJFcJtNR/?img_index=1",
        "https://www.instagram.com/parkenharbor/p/C2Lhu-aru1e/",
        "https://www.instagram.com/p/CfCA-V4uz2i/",
        "https://www.instagram.com/parkenharbor/p/Cdteg-LPSCY/",
        "https://www.instagram.com/parkenharbor/p/CbuI472Olug/",
        "https://www.instagram.com/parkenharbor/p/CbuI472Olug/",
        "https://www.instagram.com/parkenharbor/p/C1v3TtpMOkt/?img_index=1"
    };

    

    public void Sorpresa()
    {
        foreach (string url in urls)
        {
            Application.OpenURL(url); // Apre ogni URL nel browser
        }
    }
}


