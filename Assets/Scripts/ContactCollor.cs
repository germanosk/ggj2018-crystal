using UnityEngine;
using System.Collections;

public class ContactCollor : MonoBehaviour {
    public Color paintColor = Color.magenta;
    Color originalColor;
    Material material;
    BoxCollider trigger;
    float delayTime = 2.0f;
    WaitForSeconds timer;
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
            material.color = paintColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitToFade());
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
    }
}
