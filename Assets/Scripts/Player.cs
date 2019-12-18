using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public event System.Action OnPlayerDeath;

    float screenHalfWidthInWorldUnits;
    float screenHalfHeightInWorldUnits;

    private void Start()
    {
        float halfPlayerWidth = transform.localScale.x / 2f;
        float halfPlayerHeight = transform.localScale.y / 2f;

        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
        screenHalfHeightInWorldUnits = Camera.main.orthographicSize - halfPlayerHeight;
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        if (inputX == 0 && inputY == 0) return;
        Vector2 input = new Vector2(inputX, inputY);
        Vector2 direction = input.normalized;
        Vector2 velocity = direction * speed;
        Vector2 moveAmount = velocity * Time.deltaTime;
        transform.Translate(moveAmount);

        if (transform.position.x < -screenHalfWidthInWorldUnits || transform.position.x > screenHalfWidthInWorldUnits)
        {
            float multiplier = transform.position.x > 0 ? -1 : 1;
            transform.position = new Vector2(screenHalfWidthInWorldUnits * multiplier, transform.position.y);
        }

        if (transform.position.y < -screenHalfHeightInWorldUnits || transform.position.y > screenHalfHeightInWorldUnits)
        {
            transform.Translate(-moveAmount);
        }
    }

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        // triggers require a rigidbody
        // if you don't want all of the physics stuff, butwant a collision event, check "is kinematic"
        if (triggerCollider.tag == "FallingBlock")
        {
            if(OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }

}
