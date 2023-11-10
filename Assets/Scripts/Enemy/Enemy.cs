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
    public float fireRate=1.5f;
    //checker to see if the enemy is currently attacking
    protected bool attacking=false;
    //enemy health
    public float health=5;
    //points awarded when enemy is killed
    public float killPoints=5;
    //range in which enemy can attack
    public float attackRange;

    [Header("Setup")]
    //sprite array for on hit for enemies
    public Sprite[] spriteArray;
    //the player characters transform 
    public Transform player;
    //on attack SFX
    public AudioSource attackSFX;

    protected bool dead = false;
    protected SpriteRenderer currentSprite = null;

    //when spawned finds the transform for the player
    public virtual void Awake()
    {
        player = GameObject.Find("Player").transform;
        //agent = GetComponent<NavMeshAgent>();
        //gets sprite render for changing sprite on enemy hit
        currentSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    //enemy movement
    public abstract void Move();
    //destroys the enemy gameobject when health reaches 0
    public virtual void Death()
    {
        gameObject.SetActive(false);
        dead = true;
        Destroy(gameObject,.25f);
        Debug.Log("Enemy Killed!");
    }
    public virtual void TakeDamage(float dmgTaken)
    {
        //put enemy take damage FX here

        //subtracts damage taken from current enemy health and destroy enemy game object if health is less than 0
        health -= dmgTaken;
        if (health <= 0) Invoke(nameof(Death), .25f);
    }
    public virtual void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public virtual void SetHealth(float hp)
    {
        health = hp;
    }
    public virtual void ResetAttack()
    {
        attacking = false;
    }
    public virtual void ResetSprite()
    {
        currentSprite.sprite = spriteArray[0];
    }
}
