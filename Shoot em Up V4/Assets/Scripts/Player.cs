using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float _movespeed;

    private bool shooting;
    private bool shielding;

    private float _shotCooldown;
    private float _shotFrequency;
    private float _shieldAmount;
    private float _shieldDecayRate;

    private Vector2 velocity;
    private Rigidbody2D rb;

    private InputMaster inputMaster;
    private ObjectPooler objectPooler;
    private PlayerStats playerStats;

    public GameObject shield;

    private SpriteRenderer spriteRenderer;

    private Animator animator;

    void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Movement.performed += ctx => MoveInput(ctx.ReadValue<Vector2>());

        //player shooting input (single fire)
        inputMaster.Player.Shoot.performed += ctx => Shoot();
        //player shooting input (hold to fire)
        inputMaster.Player.Shoot.performed += ctx => shooting = true;
        inputMaster.Player.Shoot.canceled += ctx => shooting = false;

        inputMaster.Player.Shield.performed += ctx => shielding = true;
        inputMaster.Player.Shield.canceled += ctx => shielding = false;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectPooler = ObjectPooler.Instance;
        playerStats = FindObjectOfType<PlayerStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine(InitialiseStats());
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

        //lets the player hold to shoot
        if (shooting == true && _shotCooldown >= _shotFrequency)
        {
            Shoot();
        }

        //increased the shot cooldown
        //_shotCooldown += 1 * Time.deltaTime;
        _shotCooldown++;

        //checks if the player is currently shielding and has shield to use
        if (shielding == true && _shieldAmount > 0)
        {
            Shield(shielding);
            _shieldAmount -= _shieldDecayRate * Time.deltaTime;
        }
        else if (shielding == false || _shieldAmount <= 0)
        {
            Shield(false);
        }

        animator.SetBool("Stealth", shielding);

    }

    void Shoot()
    {
        objectPooler.SpawnfromPool("Bullet", transform.position, transform.rotation);
        _shotCooldown = 0;
    }

    void Shield(bool _active)
    {
        shield.SetActive(_active);
    }

    void Stealth()
    {

    }

    void MoveInput(Vector2 direction)
    {
        velocity = direction * _movespeed;
    }

    void Movement()
    {
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    void UpdatePlayerStats()
    {
        _movespeed = playerStats.moveSpeed;
        _shotFrequency = playerStats.shotFrequency;
        _shieldAmount = playerStats.shieldAmount;
        _shieldDecayRate = playerStats.shieldDecayRate;
    }

    private IEnumerator InitialiseStats()
    {
        yield return new WaitForSeconds(2f);
        UpdatePlayerStats();
        Debug.Log("INITIALISE PLAYER");
    }

}
