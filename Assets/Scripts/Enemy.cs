using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    private Rigidbody2D rb;

    public float movHor = 0f;
    public float speed = 3f;
    
    public bool isGroundFloor = true;
    public bool isGroundFront = false;
    public bool isMoving = false;

    public LayerMask groundLayer;
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    private RaycastHit2D hit;
    private bool hasReversedDirectionThisFrame = false;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Evitar caer precipicio
        isGroundFloor = (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
            new Vector3(movHor, 0, 0), frontGrndRayDist, groundLayer));

        // Solo invertir la dirección si no ha sido invertida ya en este fotograma
        if (isGroundFloor && !hasReversedDirectionThisFrame)
        {
            movHor = movHor * -1;
            hasReversedDirectionThisFrame = true;
        }
        else
        {
            // Si hay suelo o ya hemos invertido la dirección, restablece la variable
            hasReversedDirectionThisFrame = false;
        }

        // Choque pared
        if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
            movHor = movHor * -1;

        // Choque otro Enemigo
        hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z),
            new Vector3(movHor, 0, 0), frontDist);

        if (hit.transform != null && hit.transform.CompareTag("Enemy"))
            movHor = movHor * -1;

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(movHor * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.obj.getDamage();
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            getKilled();
        }
    }

    void getKilled()
    {
        gameObject.SetActive(false);
    }
}
