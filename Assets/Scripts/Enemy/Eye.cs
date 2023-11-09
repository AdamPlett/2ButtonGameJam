using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
{
    public float timeFireballIsActive=2;
    [SerializeField] float fireballSpeed = 5;
    public GameObject fireball;
    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(player.position, transform.position) < attackRange) Attack();
        else Move();
    }
    public override void Move()
    {
        //makes a vector in the direction of the player and sets its magnitiude to 1
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //flips sprite in the direction of movement
        if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);
        if (direction.x > 0) transform.localScale = new Vector3(1, 1, 1);
        //turns the direction into an angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //moves towards the player
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        //rotates towards the player
        //transform.rotation = Quaternion.Euler(Vector3.forward*angle);
    }
    //checks if enemy is already attacking, if not than it attacks
    private void Attack()
    {
        //exit if already attacking
        if (attacking == true) return;
        //sets fireball velocity in direction of the player
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        Vector3 directionV3 = direction;
        Vector2 fireballVelocity = direction * fireballSpeed *100;
        //sets that the enemy is attacking to true and waits the time between attacks(fireRate) before resetting back to false
        attacking = true;
        Transform fireballStart = gameObject.transform;
        fireballStart.position = fireballStart.position + directionV3 * 15;
        GameObject fireballInstance = Instantiate(fireball, gameObject.transform);
        EyeFireBall eyeFireball = fireballInstance.GetComponent<EyeFireBall>();
        eyeFireball.setDamage(damage);
        Rigidbody2D fireballRB = fireballInstance.GetComponent<Rigidbody2D>();
        fireballInstance.transform.parent = null;
        fireballRB.AddForce(fireballVelocity);
        Destroy(fireballInstance, timeFireballIsActive);

        Debug.Log("Enemy Fireball!");
        Invoke(nameof(ResetAttack), fireRate);
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
                //if bullet is not pericing than destroy bullet
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
        //makes sure other has a playerHealth component 
        if (playerHealth != null)
        {
            //sets that the enemy is attacking to true and waits the time between attacks(fireRate) before resetting back to false
            Debug.Log("Enemy Attacked!");
            //applies damage to player
            playerHealth.Damage(damage);
            //play attack sfx
            //attackSFX?.Play();
        }
    }
}
