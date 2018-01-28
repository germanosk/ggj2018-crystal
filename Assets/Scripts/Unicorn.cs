using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StronglyConnectedComponents;

public class Unicorn : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    public Animator animator;

    List<Vertex<ContactCollor>> visitedVertex;

    void Awake()
    {
        visitedVertex = new List<Vertex<ContactCollor>>();
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

    public void AddTile(Vertex<ContactCollor> tile)
    {
        if (!visitedVertex.Contains(tile))
        {
            visitedVertex.Add(tile);
        }
        //var tcd = new TarjanCycleDetectStack();
        //var cycle_list = tcd.DetectCycle(visitedVertex);
        if (visitedVertex.Count <3) return;
        var detector = new StronglyConnectedComponentFinder<ContactCollor>();
        var cycles = detector.DetectCycle(visitedVertex);
        Debug.Log("Vertex visited "+visitedVertex.Count);
        if ( cycles.Count > 0)
        {
            Debug.LogWarning("Cicle Found!");
           // Debug.Break();
        }
    }

    public void Remove(Vertex<ContactCollor> tile)
    {
        visitedVertex.Remove(tile);
    }
}
