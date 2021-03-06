﻿using System.Collections;
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

   // Dictionary<int,Vertex<int>> visitedVertex;
    ContactCollor lastTile;

	public Game game;

    void Awake()
    {
      //  visitedVertex = new Dictionary<int, Vertex<int>>();
    }

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>(); 
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("P1 Horizontal"), 0, Input.GetAxis("P1 Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            animator.SetFloat("x",- moveDirection.x);
            animator.SetFloat("y", moveDirection.z);
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

	public void AddTile()
    {
		game.addPoint("Player1");
	      
    }

    public void Remove(ContactCollor tile)
    {
       // visitedVertex.Remove(tile.gameObject.GetHashCode());
		game.removePoint("Player1");
    }
}
