using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSmaller : Enemy
{
    public GameObject slimeSmallest;
    [SerializeField] float spawnOffset = 1, timeMoving = 1.25f, timeStill = .5f;

    private bool isMoving = false;

    public override void Awake()
    {
        setMovingFalse();
        base.Awake();
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving == true) Move();
    }
    public override void Move()
    {   
        //makes a vector in the direction of the player and sets its magnitiude to 1
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //flips sprite in the direction of movement
        if (direction.x < 0) transform.localScale = new Vector3(1, 1, 1);
        if (direction.x > 0) transform.localScale = new Vector3(-1, 1, 1);
        //turns the direction into an angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //moves towards the player
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void setMovingFalse()
    {
        isMoving = false;
        Invoke(nameof(setMovingTrue), timeStill);
    }
    private void setMovingTrue()
    {
        isMoving = true;
        Invoke(nameof(setMovingFalse), timeMoving);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //take damage from player bullets
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.GetDamage());

                //changes sprite to take enemy hit than resets back to default sprite
                currentSprite.sprite = spriteArray[1];
                Invoke(nameof(ResetSprite), .25f);
                //if bullet is not a piercing bullet than destroy
                if (!bullet.GetPiercing())
                {
                    Destroy(other.gameObject);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
    PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        //exit if already attacking
        if (attacking == true) return;
        //makes sure other has a playerHealth component 
        if (playerHealth != null)
        {
            //sets that the enemy is attacking to true and waits the time between attacks(fireRate) before resetting back to false
            attacking = true;
            Debug.Log("Enemy Attacked!");
            Invoke(nameof(ResetAttack), fireRate);
            //applies damage to player
            playerHealth.Damage(damage);
            //play attack sfx
            attackSFX?.Play();
        }
    }
    public override void Death()
    {
        if (dead) return;
        //spawns the two smallest slime enemy types
        Instantiate(slimeSmallest, transform.position + transform.right * spawnOffset, transform.rotation);
        Instantiate(slimeSmallest, transform.position + transform.right * -spawnOffset, transform.rotation);
        //calls the virtual function "death" to destory the gameobject apon death
        base.Death();
    }
}
