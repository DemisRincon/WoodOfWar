using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{
    private Enemy enemy;

    private float idleTimer;

    private float idleDuration;

    public void Enter(Enemy enemy)
    {
        idleDuration = UnityEngine.Random.Range(1, 10);
        this.enemy = enemy;
    }

    public void Execute()
    {

        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
        Idle();

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        if (other.tag == "fireball" || other.tag == "Hit" || other.tag == "sword" || other.tag == "hSlayer")
        {
            enemy.Target = Player.Instance.gameObject;
        }
    }
    private void Idle()
    {
        enemy.MyAnimatior.SetFloat("speed", 0);// le damos velocidad 0 al enemigo
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
            enemy.Target = null;
        }
    }
}