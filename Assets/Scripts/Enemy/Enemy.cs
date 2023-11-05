using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Stats")]
    //how fast the enemy moves
    public float speed=1;
    //how much damage the enmey does when attack
    public float damage=1;
    //how often the enemy attacks
    public float fireRate=3;
    //checker to see if the enemy is currently attacking
    protected bool attacking=false;
    //enemy health
    public float health=5;
    //points awarded when enemy is killed
    public float killPoints=5;

    [Header("Setup")]
    //the player characters transform 
    public Transform player;
    //on attack SFX
    public AudioSource attackSFX;



    //when spawned finds the transform for the player
    public virtual void Awake()
    {
        //player = GameObject.Find("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
    }
    //enemy movement
    public abstract void Move();
    //destroys the enemy gameobject when health reaches 0
    public virtual void Destory()
    {
        Debug.Log("Enemy Killed!");
    }

    public virtual void Attack()
    {
        Debug.Log("Enemy Attack!");
    }
}
