using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float speed;
    [SerializeField] private float dmg;

    public float Hp {
        get => hp;
        set => hp = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public float Dmg
    {
        get => dmg;
        set => dmg = value;
    }

    //public Entity(float hp, float speed, float dmg)
    //{
    //    this.Hp = hp;
    //    this.Speed = speed;
    //    this.Dmg = dmg;
    //}

    public abstract void TakeDmg(float dmg);
    public abstract void Die();
}
