using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
{
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        //take damage from player bullets
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.GetDamage());
            }
        }
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        //exit if already attacking
        //makes sure other has a playerHealth component 
        if (playerHealth != null)
        {
            //sets that the enemy is attacking to true and waits the time between attacks(fireRate) before resetting back to false
            Debug.Log("Enemy Attacked!");
            //applies damage to player
            playerHealth.Damage(damage);
        }
    }
}
