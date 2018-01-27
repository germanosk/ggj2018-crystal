using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject creditsMenu;

	//string current = "menu";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	} 

	public void InitGame(){
		//current = "game";
		//mainMenu.SetActive (false);
		SceneManager.LoadScene("Main", LoadSceneMode.Single);

	}

	public void showCredits(){
		//current = "credits";
		mainMenu.SetActive (false);
		creditsMenu.SetActive (true);
	}

	public void showMenu(){
		//current = "menu";
		mainMenu.SetActive (true);
		creditsMenu.SetActive (false);
	}
}
