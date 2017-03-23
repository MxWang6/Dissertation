using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// allows us to use lists
	public static GameManager instance = null;
	public BoardManager boardScript;
	private int level = 3;

	void Awake(){

		//Check if instance already exists
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);

		boardScript = GetComponent<BoardManager>();
		InitGame ();

		//GameObject.Find("Miner").GetComponent<Miner>().gridPosition = new Gridlocation( Random.Range(1, (boardScript.GameColumns / 2)), Random.Range(1, (boardScript.GameRows / 2)) );
	}

	void InitGame(){
		boardScript.SetupScene (level);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
