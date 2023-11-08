using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSmallest : Enemy
{
    [SerializeField] float timeMoving = 1.25f, timeStill = .5f;
    
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Damage(damage);
            Debug.Log("damage applied");
        }
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

}
