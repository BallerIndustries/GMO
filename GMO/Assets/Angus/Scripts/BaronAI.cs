using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Angus
{
	public class BaronAI : MonoBehaviour
	{
        public Vector3 bulletOffset = new Vector3();
        public Vector2 speed = new Vector2();

        private State CurrentState = State.EnterScene;
        //private const float START_X = 20;
        //private const float START_Y = 0;

        private readonly Vector2 START_POSITION = new Vector2(20, 0);

        private const float SHOOT_Y = 3.60f;
        private const int SHOOT_MAX = 10;
        private const int IDLE_TIME = 120;
        
        private Vector2 movement;
        private WeaponScript[] weapons;

        private int holds = 0;
        private int shootCount = 0;

        public enum State
        {
            EnterScene,
            Idle,
            GoUpAndShoot,
            BackToStart,
        }

        void Start()
        {
            weapons = GetComponentsInChildren<WeaponScript>();
        }

        void Update()
        {
            //UnityEngine.Debug.Log(string.Format("Update() CurrentState = {0}", CurrentState));

            switch (CurrentState)
            {
                case State.EnterScene:
                    movement = new Vector2(-speed.x, 0.0f);

                    if (transform.position.x <= START_POSITION.x)
                    {
                        transform.position = new Vector3(START_POSITION.x, transform.position.y, 0);
                        CurrentState = State.Idle;
                        holds = 60;
                    }
                    break;

                case State.Idle:
                    movement = Vector2.zero;
                    holds++;

                    if (holds > IDLE_TIME)
                        CurrentState = State.GoUpAndShoot;
                    break;

                case State.BackToStart:

                    if (transform.position.y >= START_POSITION.y)
                    {
                        movement = new Vector2(0.0f, -speed.y);
                    }
                    else
                    {
                        movement = Vector2.zero;
                        transform.position = new Vector3(START_POSITION.x, START_POSITION.y, 0);
                        CurrentState = State.Idle;
                        holds = 60;
                    }
                    break;

                case State.GoUpAndShoot:

                    //1. The going up part.
                    if (transform.position.y <= SHOOT_Y)
                    {
                        movement = new Vector2(0, speed.y);
                    }
                    else
                    {
                        movement = Vector2.zero;
                        transform.position = new Vector3(transform.position.x, SHOOT_Y, 0);

                        //Shoot all the guns.
                        foreach (WeaponScript weapon in weapons)
                        {
                            if (weapon != null && weapon.enabled && weapon.CanAttack)
                            {
                                weapon.Attack(true, bulletOffset);
                                shootCount++;
                            }
                        }

                        if (shootCount > SHOOT_MAX)
                        {
                            shootCount = 0;
                            holds = 0;
                            CurrentState = State.BackToStart;
                        }
                    }
                    break;
            }
        }

        void FixedUpdate()
        {
            rigidbody2D.velocity = movement;
        }
	}
}
