using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    BoxCollider2D myBoxCollider2D;
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        IsFacingRight();
    }

    void Move()
    {
        if(IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(speed, myRigidBody.velocity.y);
        } else {
            myRigidBody.velocity = new Vector2(-speed, myRigidBody.velocity.y);
        }
    }


    //I will use this method do determine if the enemy is going to the right or to the left
    private bool IsFacingRight()
    {
        //I know that the scale is 1 if the enemy is going right, otherwise the scale is -1. 
        return transform.localScale.x > 0;      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector3(-Mathf.Sign(myRigidBody.velocity.x), 1f, 1f); 
    }
}
