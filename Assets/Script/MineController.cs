using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {

	public GameObject explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){	
			Debug.Log("Funziono " + other.gameObject.GetComponent<BubbleShooting>());
            
                other.gameObject.GetComponent<BubbleShooting>().ChangeBubbleSize(1f);
			
			Destroy (gameObject);
			Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
		}	
	}
}
