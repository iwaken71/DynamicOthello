using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	public GameObject board;
	public Text[] Blabel,Wlabel;//,TurnLabel;
	GameManager GM;
	//int blackcount,whitecount;

	public static int WHITECOUNT,BLACKCOUNT;
	public static int TURNCOUNT,TURNCOLOR; 
	public static bool GAME;
	public static bool PAUSE;


	// Use this for initialization
	void Start () {
		WHITECOUNT = 2;
		BLACKCOUNT = 2;
		TURNCOUNT = 1;
		TURNCOLOR = 1;
		GAME = false;
		PAUSE = false;

		GM = board.GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Blabel.Length; i++) {
			Blabel[i].text = BLACKCOUNT.ToString ();
			Wlabel[i].text = WHITECOUNT.ToString ();
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);


		}
	
	}
}
