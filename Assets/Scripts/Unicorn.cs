﻿using System.Collections;
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

    Dictionary<ContactCollor,Vertex<ContactCollor>> visitedVertex;
    Vertex<ContactCollor> lastVertex;

    void Awake()
    {
        visitedVertex = new Dictionary<ContactCollor, Vertex<ContactCollor>>();
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
        Vertex<ContactCollor> currentVertex;
        if (!visitedVertex.ContainsKey(tile))
        {
            visitedVertex[tile] = new Vertex<ContactCollor>(tile);
        }
        currentVertex = visitedVertex[tile];
        if (lastVertex != null && !lastVertex.Dependencies.Contains(currentVertex))
        {
            currentVertex.Dependencies.Add(lastVertex);
        }
        lastVertex = currentVertex;
        //var tcd = new TarjanCycleDetectStack();
        //var cycle_list = tcd.DetectCycle(visitedVertex);
        if (visitedVertex.Count <3) return;
        var detector = new StronglyConnectedComponentFinder<ContactCollor>();
        var cycles = detector.DetectCycle(visitedVertex.Values);
        Debug.Log("Vertex visited "+visitedVertex.Count);
        if ( cycles.Count > 0)
        {
            Debug.LogWarning("Cicle Found!");
           // Debug.Break();
        }
    }

    public void Remove(ContactCollor tile)
    {
        visitedVertex.Remove(tile);
    }
}
