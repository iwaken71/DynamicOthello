using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour {
	
	public GameObject blackpanel,whitepanel;
	public GameObject winpanel,losepanel;
	public GameObject startpanel;
	//public AudioSource[] sound;
	bool start = true;
	bool son = false;
	// Use this for initialization
	void Start () {
		start = true;
		son = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (ScoreScript.GAME) {
			//sound[2].Play ();
			//sound[2].volume = 0.15f;
			winpanel.SetActive (false);
			losepanel.SetActive (false);
			startpanel.SetActive(false);
			start = false;
			if (ScoreScript.TURNCOLOR == 2) {
				blackpanel.SetActive(false);
				whitepanel.SetActive (true);
			} else {
				blackpanel.SetActive(true);
				whitepanel.SetActive (false);
			}
		} else {
			//sound[2].volume = 0;
			if(start){
				startpanel.SetActive(true);
			}
			
			int mycolor = PlayerController.mycolor;
			blackpanel.SetActive(false);
			whitepanel.SetActive (false);
			if(ScoreScript.BLACKCOUNT >= ScoreScript.WHITECOUNT){
				if(mycolor == 1){
					winpanel.SetActive (true);
					losepanel.SetActive (false);
				}else{
					winpanel.SetActive (false);
					losepanel.SetActive (true);
				}
			}else if(ScoreScript.BLACKCOUNT < ScoreScript.WHITECOUNT){
				if(mycolor == 1){
					winpanel.SetActive (false);
					losepanel.SetActive (true);
				}else{
					winpanel.SetActive (true);
					losepanel.SetActive (false);
				}
			}
		}
		
		
	}
}
