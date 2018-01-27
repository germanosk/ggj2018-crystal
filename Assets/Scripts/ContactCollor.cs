using UnityEngine;

public class ContactCollor : MonoBehaviour {
    public Color paintColor = Color.magenta;
    Color originalColor;
    Material material;
    BoxCollider trigger;

	void Awake ()
    {
        material = GetComponent<Renderer>().material;
        originalColor = material.color;
        trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size = trigger.size + Vector3.up;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            material.color = paintColor;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            material.color = paintColor;
        }
    }
}
