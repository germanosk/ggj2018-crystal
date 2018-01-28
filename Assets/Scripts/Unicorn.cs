using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StronglyConnectedComponents;
using System.Linq;

public class Unicorn : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public Animator animator;

    Dictionary<int,Vertex<int>> visitedVertex;
    ContactCollor lastTile;

    void Awake()
    {
        visitedVertex = new Dictionary<int, Vertex<int>>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            animator.SetFloat("x",- moveDirection.x);
            animator.SetFloat("y", moveDirection.z);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void AddTile(ContactCollor tile)
    {
        int currentHash = tile.GetHashCode();
        Debug.Log("Hash: " + currentHash);
        Vertex<int> currentVertex;

        if (!visitedVertex.ContainsKey(currentHash))
        {
            visitedVertex[currentHash] = new Vertex<int>(currentHash);
        }
        currentVertex = visitedVertex[currentHash];
        if (lastTile != null
            //&& !visitedVertex[lastTile.gameObject.GetHashCode()].Dependencies.Contains(currentVertex)
            //&& !currentVertex.Dependencies.Contains(visitedVertex[lastTile.GetHashCode()])
            )
        {
            currentVertex.Dependencies.Add(visitedVertex[lastTile.GetHashCode()]);
            if(visitedVertex[lastTile.GetHashCode()].Dependencies.Count > 1)
            {
                Debug.Log("Dependencies Greater than 1");
            }
        }
        lastTile = tile;
        //using https://github.com/danielrbradley/CycleDetection to detect Cycle
        var detector = new StronglyConnectedComponentFinder<int>();
        var values = visitedVertex.Values.ToList();
        var cycles = detector.DetectCycle(values);
        Debug.Log("Cicles: " + cycles.Count);
        Debug.Log("Cicle Single: " + cycles.Single().Count());
        Debug.Log("Cicle Cycles: " + cycles.Cycles().Count());

        if (cycles.Cycles().Count() > 0)
        {
            Debug.LogWarning("Cicle Found!");
           // Debug.Break();
        }
    }

    public void Remove(ContactCollor tile)
    {
        visitedVertex.Remove(tile.gameObject.GetHashCode());
    }
}
