using UnityEngine;
using System.Collections;
using StronglyConnectedComponents;

public class ContactCollor : MonoBehaviour {
    public Vertex<ContactCollor> vertex;
    public Color paintColor = Color.magenta;
    Color originalColor;
    Material material;
    BoxCollider trigger;
    float delayTime = 2.0f;
    WaitForSeconds timer;
    Coroutine timeCoroutine;

    Unicorn player;
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
        if(other.CompareTag("Player"))
        {
            if(timeCoroutine != null)
            {
                StopCoroutine(timeCoroutine);
            }
            material.color = paintColor;
            if(player == null)
            {
                player = other.GetComponent<Unicorn>();
            }
            player.AddTile(vertex);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeCoroutine = StartCoroutine(WaitToFade());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            material.color = paintColor;
        }
    }

    IEnumerator WaitToFade()
    {
        yield return timer;
        material.color = originalColor;
        if (player != null)
        {
            player.Remove(vertex);
        }
    }

    public void Shine()
    {
        material.color = Color.yellow;
        StopCoroutine(timeCoroutine);
    }
}
