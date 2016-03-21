using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public GameObject board;
	public GameObject CursorManager;
	public Camera camera;
	public AudioSource[] sound;
	private GameManager manager;
	private CursorScript CScript;
	private Vector3 center;
	private Vector3 pos;
	private AI ai;
	private GameObject BGM;
	
	public static int mycolor;
	private bool canmove;
	private float timer;
	private GameObject hitsound, bom;
	int mode = 1;
	
	public int distance = 100;
	
	// Use this for initialization
	void Start () {
		manager = board.GetComponent<GameManager> ();
		CScript = CursorManager.GetComponent<CursorScript> ();
		ai = CursorManager.GetComponent<AI> ();
		BGM = GameObject.FindGameObjectWithTag("BGM");
		
		
		distance = 100;
		PlayerController.mycolor = 3;
		canmove = true;
		timer = 0;
		hitsound = Resources.Load ("HitSound") as GameObject;
		bom = Resources.Load ("Detonator-Simple") as GameObject;
		mode = 1;

		
	}
	
	// Update is called once per frame
	void Update () {
		if (ScoreScript.GAME) {
			if(!ScoreScript.PAUSE){
				//fixed cursor
				LockedCursor();
				IsMove ();
				center = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
				Shot ();
				
			}else{
				FreeCursor();
			}
		} else {
			FreeCursor();

			
		}
		
		
	}
	
	void Shot(){
		Ray ray;
		RaycastHit hit;
		bool utsu = false;
		if (mode == 1) {
			ray = camera.ScreenPointToRay (center);
		} else {
			ray = camera.ScreenPointToRay (Input.mousePosition);
		}
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return)) {
			sound[0].Play();
			utsu = true;
		}
		
		if (Physics.Raycast (ray, out hit, distance)) {
			if (hit.transform.gameObject.tag == "Trigger") {
				GameObject obj = hit.transform.gameObject;
				TriggerScript tr = obj.GetComponent<TriggerScript> ();
				CScript.Check (tr.GetX (), tr.GetY ());
				if(utsu && canmove){
					if(!manager.canPut(ScoreScript.TURNCOLOR, tr.GetX (), tr.GetY ())){
						Instantiate(bom,hit.point,Quaternion.identity);
					}
					manager.Putstone (ScoreScript.TURNCOLOR, tr.GetX (), tr.GetY ());
					GameObject se = Instantiate(hitsound,hit.point,Quaternion.identity) as GameObject;
					Destroy(se,10);
					
				}
			} else{
				if(utsu){
					GameObject se = Instantiate(hitsound,hit.point,Quaternion.identity) as GameObject;
					Destroy(se,10);
					Instantiate(bom,hit.point,Quaternion.identity);
				}
				if(hit.transform.gameObject.tag == "Top"){
					if(utsu){
						hit.transform.gameObject.SendMessage("Change");
					}
				}else if(hit.transform.gameObject.tag == "BGMHit"){
					if(utsu){
						BGM.SendMessage("BGMClick");
					}
				}else {
					CScript.Check (-1, -1);
				}
			}
		} else {
			CScript.Check (-1, -1);
		}
	}
	
	private void IsMove(){
		if (mycolor == 3) {
			canmove = true;
		} else {
			if(mycolor == ScoreScript.TURNCOLOR){
				canmove = true;
			}else{
				canmove = false;
			}
		}
	}
	public void SetMyColor(int color){
		if (color == 1 || color == 2 || color == 3) {
			PlayerController.mycolor = color;
			ScoreScript.GAME = true;
		}
	}
	public int GetMyColor(){
		return PlayerController.mycolor;
	}
	private void FreeCursor(){
		Screen.lockCursor = false;
		Cursor.visible = true;
	}
	private void LockedCursor(){
		if (mode == 1) {
			Screen.lockCursor = true;
			Cursor.visible = false;
		}
	}
	
	
}
