using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
	public int playerOnePoints = 0;
	public int playerTwoPoints = 0;

	public Text pOneText;
	public Text pTwoText;
	public Text timer;

	public Text winText;

	public float timeLeft = 15.0f;

	public GameObject gameOverMenu;

	private bool isPaused;	

	void Update()
	{

		if (!isPaused) 
		{
			DoPause();
		} 
		else if (isPaused) 
		{
			UnPause ();
		}

		timeLeft -= Time.deltaTime;
		timer.text = "" + Mathf.Round(timeLeft);
		if(timeLeft < 0)
		{
			gameOverMenu.SetActive(true);
			Time.timeScale = 0;

			if(playerOnePoints > playerTwoPoints){
				winText.text = "Jogador 1 Venceu!";
			}else{
				winText.text = "Jogador 2 Venceu!";
			}

			if(playerOnePoints == playerTwoPoints){
				winText.text = "Empate!";
			}

			//Application.LoadLevel("gameOver");
		}
	}

	public void NewGame()
    {
		playerOnePoints = 0;
		playerTwoPoints = 0;
		updateP1Text();
		updateP2Text();


		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex) ;
    }

	public void addPoint(string tag){
		if(tag.Equals("Player1")){
			playerOnePoints++;
			updateP1Text();
		}else if(tag.Equals("Player2")){
			playerTwoPoints++;
			updateP2Text();
		}
	}

	public void removePoint(string tag){
		if(tag.Equals("Player1")){
			playerOnePoints--;
			if(playerOnePoints < 0){
				playerOnePoints = 0;
			}
			updateP1Text();
		}else if(tag.Equals("Player2")){
			playerTwoPoints--;
			if(playerTwoPoints < 0){
				playerTwoPoints = 0;
			}
			updateP2Text();
		}
	}

	private void updateP1Text(){
		pOneText.text = "" + playerOnePoints;
	}

	private void updateP2Text(){
		pTwoText.text = "" + playerTwoPoints;
	}

	public void DoPause()
	{
		isPaused = true;
		Time.timeScale = 0;
	}

	public void UnPause()
	{
		isPaused = false;
		Time.timeScale = 1;
	}

	  
}
