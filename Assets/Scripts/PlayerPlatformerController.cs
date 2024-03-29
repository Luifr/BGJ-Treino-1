﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	public bool canMove = true;

	// Use this for initialization
	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	protected override void ComputeVelocity()
	{

		if (!canMove)
			return;

		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis("Horizontal");

		if (Input.GetButtonDown("Jump") && grounded)
		{
			velocity.y = jumpTakeOffSpeed;
		}
		else if (Input.GetButtonUp("Jump"))
		{
			if (velocity.y > 0)
			{
				velocity.y = velocity.y * 0.5f;
			}
		}
		//if (!spriteRenderer.flipX) Debug.Log(move.x);
		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.1f) : (move.x < 0.0f));
		if (flipSprite)
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool("grounded", grounded);
		animator.SetBool("isMoving", move.x > 0.01f || move.x < -0.01f);

		targetVelocity = move * maxSpeed;
	}
}