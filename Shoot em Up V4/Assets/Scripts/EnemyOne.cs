using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : MonoBehaviour
{

    public Enemy enemy;

    public string enemyName;

    public Sprite sprite;

    public int health;

    public float moveSpeed;

    void Start()
    {
        enemyName = enemy.name;
        sprite = enemy.sprite;
        health = enemy.health;
        moveSpeed = enemy.moveSpeed;
        GetComponent<SpriteRenderer>().sprite = enemy.sprite;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
