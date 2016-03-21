using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour {

	public GameObject reticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (reticle != null) {
			reticle.SetActive (ScoreScript.GAME);
		}

	
	}

	public void ToGame(){
		Application.LoadLevel ("Game");
	}
	public void ToStart(){
		Application.LoadLevel ("Start");
	}
}
