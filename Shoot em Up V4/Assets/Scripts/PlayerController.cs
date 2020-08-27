using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{



    private Vector2 _moveVelocity;

    private float _moveSpeed;

    private Rigidbody2D rb;

    private PlayerStats playerStats;




    private void Awake()
    {

    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    void Start()
    {
        StartCoroutine(InitialiseStats());
        playerStats = FindObjectOfType<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //ApplyMovement();
    }
    void Update()
    {


        //CheckInput();
        //Debug.Log(_moveSpeed);
    }

    void ApplyMovement()
    {
        rb.MovePosition(rb.position + _moveVelocity * Time.fixedDeltaTime);
    }

    void CheckInput()
    {
        Vector2 _moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveVelocity = _moveInput.normalized * _moveSpeed;
    }

    public void UpdatePlayerStats()
    {
        _moveSpeed = playerStats.moveSpeed;


    }
    private IEnumerator InitialiseStats()
    {
        yield return new WaitForSeconds(1f);
        UpdatePlayerStats();
        Debug.Log("update player stats");
    }

}
