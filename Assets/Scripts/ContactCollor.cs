using UnityEngine;
using System.Collections;
using StronglyConnectedComponents;

public class ContactCollor : MonoBehaviour
{
    public Color paintColor1 = Color.magenta;
	public Color paintColor2 = Color.blue;
    Color originalColor;
    Material material;
    BoxCollider trigger;
    float delayTime = 2.0f;
    WaitForSeconds timer;
    Coroutine timeCoroutine;

    Unicorn player1;
	Fitman player2;
	void Awake ()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
        trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size = trigger.size + Vector3.up;

        timer = new WaitForSeconds(delayTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player1"))
        {
            if(timeCoroutine != null)
            {
                StopCoroutine(timeCoroutine);
            }
			if(material.color == paintColor2){
				if(player2 == null){
					player2 = other.GetComponent<Fitman>();
				}
				player2.Remove(this);
			}

			if(material.color != paintColor1){
				material.color = paintColor1;
				if(player1 == null)
				{
					player1 = other.GetComponent<Unicorn>();
				}
				player1.AddTile();
			}
        }

		if(other.CompareTag("Player2"))
		{
			if(timeCoroutine != null)
			{
				StopCoroutine(timeCoroutine);
			}
			if(material.color == paintColor1){
				if(player1 == null)
				{
					player1 = other.GetComponent<Unicorn>();
				}
				player1.Remove(this);
			}
			if(material.color != paintColor2){
				material.color = paintColor2;
				if(player2 == null)
				{
					player2 = other.GetComponent<Fitman>();
				}
				player2.AddTile();
			}
		}
    }

    void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    timeCoroutine = StartCoroutine(WaitToFade());
        //}
    }
    
    IEnumerator WaitToFade()
    {
        yield return timer;
        material.color = originalColor;
      /*  if (player1 != null)
        {
            player1.Remove(this);
        }
		if (player2 != null)
		{
			player2.Remove(this);
		}*/
    }

    public void Shine()
    {
      //  material.color = Color.yellow;
      //  StopCoroutine(timeCoroutine);
    }
}
