using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	public int playerOnePoints = 0;
	public int playerTwoPoints = 0;

	public Text pOneText;
	public Text pTwoText;

    void Update()
    {}

    public void NewGame()
    {
		playerOnePoints = 0;
		playerTwoPoints = 0;
		pOneText.text = "Happy: " + playerOnePoints;
		pTwoText.text = "Sad: " + playerTwoPoints;

        ClearPlayers();
        RefreshPlayers();
   
    }

	public void addPoint(string tag){
		Debug.Log("addPoint: "+tag);
		if(tag.Equals("Player1")){
			playerOnePoints++;
			pOneText.text = "Happy: " + playerOnePoints;
		}else if(tag.Equals("Player2")){
			playerTwoPoints++;
			pTwoText.text = "Sad: " + playerTwoPoints;
		}
	}

	public void removePoint(string tag){
		Debug.Log("removePoint: "+tag);
		if(tag.Equals("Player1")){
			playerOnePoints--;
			if(playerOnePoints < 0){
				playerOnePoints = 0;
			}
			pOneText.text = "Happy: " + playerOnePoints;
		}else if(tag.Equals("Player2")){
			playerTwoPoints--;
			if(playerTwoPoints < 0){
				playerTwoPoints = 0;
			}
			pTwoText.text = "Sad: " + playerTwoPoints;
		}
	}

	private void ClearPlayers()
    {
        /*foreach (GameObject target in targets)
        {
            target.GetComponent<Target>().DisableRobot();
        }*/
    }

	private void RefreshPlayers()
    {
      /*  foreach (GameObject target in targets)
        {
            target.GetComponent<Target>().RefreshTimers();
        }*/
    }
	  
}
