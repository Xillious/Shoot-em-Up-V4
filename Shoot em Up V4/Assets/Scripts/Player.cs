using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float movespeed = 2f;
    Vector2 velocity;
    private Rigidbody2D rb;

    private InputMaster inputMaster;
    private ObjectPooler objectPooler;

    bool canShoot = true;

    bool shooting;

    float shotCooldown = 0f;
    float shotFrequency = 0.05f;

    void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Movement.performed += ctx => MoveDirection(ctx.ReadValue<Vector2>());
        inputMaster.Player.Shoot.performed += ctx => Shoot();

        inputMaster.Player.ShootTest.performed += ctx => shooting = true;
        inputMaster.Player.ShootTest.canceled += ctx => shooting = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPooler = ObjectPooler.Instance;
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }


    private void FixedUpdate()
    {

        Movement();
    }

    void Update()
    {
        Debug.Log(shotCooldown);
        if (shooting == true && shotCooldown >= shotFrequency)
        {
            Shoot();
        }
        shotCooldown += 1 * Time.deltaTime;
    }

    void Shoot()
    {
        objectPooler.SpawnfromPool("Bullet", transform.position, transform.rotation);
        shotCooldown = 0;
    }

    void ShootTest()
    {
        Debug.Log("shooting");
    }

    void StopShooting()
    {
        Debug.Log("STOP Shooting");
        canShoot = false;
    }

    void MoveDirection(Vector2 direction)
    {
        //Debug.Log("moving" + direction);
        // velocity = direction.x * movespeed;
        //rb.MovePosition(rb.position + direction * Time.deltaTime);
        //rb.MovePosition(direction + movespeed * Time.deltaTime);
        //rb.AddForce(direction * movespeed);

        //rb.MovePosition(rb.position + direction * movespeed);


        velocity = direction * movespeed;

        //rb.position += direction * movespeed;
        //rb.transform.position += direction * Time.deltaTime;
    }

    void Movement()
    {
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
}
