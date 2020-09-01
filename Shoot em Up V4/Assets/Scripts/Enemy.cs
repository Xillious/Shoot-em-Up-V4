using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;

    public Sprite sprite;

    public int health;

    public float moveSpeed;

    public Rigidbody2D rb;

    void Start()
    {

    }



}
