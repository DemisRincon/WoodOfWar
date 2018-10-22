using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private Enemy enemy;
    private double throwTimer = 2.5;
    private float throwCoolDown = 3;
    private bool canThrow = false;
    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Throw();
        if (enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleState());
        }
        if (enemy.Target != null)
        {
            //enemy.Move();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        
    }

    public void Throw()
    {
        throwTimer += Time.deltaTime;
        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;

        }
        if (canThrow)
        {
            canThrow = false;
            enemy.MyAnimatior.SetTrigger("throw");
        }
    }
}