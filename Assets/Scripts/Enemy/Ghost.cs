using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public override void Move()
    {
        //makes a vector in the direction of the player and sets its magnitiude to 1
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //turns the direction into an angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //moves towards the player
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        //rotates towards the player
        //transform.rotation = Quaternion.Euler(Vector3.forward*angle);
    }
}
