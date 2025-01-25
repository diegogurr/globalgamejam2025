using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour {

	public GameObject explosion;
	



	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){	
            
                other.gameObject.GetComponent<BubbleShooting>().ChangeBubbleSize(1f);
			
			Destroy (gameObject);
			Instantiate (explosion, gameObject.transform.position, gameObject.transform.rotation);
		}	
	}
}
