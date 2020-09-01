using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{

    public Enemy enemy;

    public string enemyName;

    public int health;

    public float moveSpeed;

    public Sprite sprite;

    public Rigidbody2D rb;

    private InputMaster inputMaster;
    private ObjectPooler objectPooler;

    private void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Enemy.Shoot.performed += ctx => Shoot();
    }

    void Start()
    {
        enemyName = enemy.name;
        sprite = enemy.sprite;
        health = enemy.health;
        moveSpeed = enemy.moveSpeed;
        GetComponent<SpriteRenderer>().sprite = enemy.sprite;
        rb = GetComponent<Rigidbody2D>();
        rb = enemy.rb;
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

    void Update()
    {

    }

    void Shoot()
    {
        objectPooler.SpawnfromPool("EnemyBullet", transform.position, transform.rotation);
        Debug.Log("enemy shooting");
    }
}
