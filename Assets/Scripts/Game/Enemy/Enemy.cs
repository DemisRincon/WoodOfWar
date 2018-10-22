using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : Character
{

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField]
    private GameObject dropEmemyCan;

    [SerializeField]
    private GameObject dropEnemyBottle;

    [SerializeField]
    private GameObject dropEnemyBag;

    [SerializeField]
    private float meleeRange;

    [SerializeField]
    private float throwRange;

    private bool inmortal = false;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float inmortalTime;

    public Rigidbody2D MyRigidbody { get; set; }

    //Edges integrados en los enemigos, cambiados por edges independientes
    //[SerializeField]
    //private Transform leftEdge;

    //[SerializeField]
    //private Transform rightEdge;

    private Canvas healthCanvas;

    BoxCollider2D advancingCollider;

    //private Vector2 startPos;//

    private bool dropItem = true;

    private float time = 0f;

    float secondsToCount = 30;

    private bool spawnBags = true;
    private bool spawnBottles = true;
    private bool spawnCans = true;

    public bool InMeleeRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
            }
            return false;
        }
    }

    public bool InThrowRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= throwRange;
            }
            return false;
        }
    }

    public override bool IsDead
    {
        get
        {
            return healthStat.CurrentValue <= 0;
        }
    }

    public override void Start()
    {
        base.Start();
        //startPos = transform.position;        
        Player.Instance.Dead += new DeadEventHandler(RemoveTarget);

        //Pone al enemigo en estado de Idle
        ChangeState(new IdleState());
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthCanvas = transform.GetComponentInChildren<Canvas>();
        MyRigidbody = GetComponent<Rigidbody2D>();  // 02 con esto obtenemos el rigid al que este atachado el script
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (gameObject.tag == "latonBoss")
        {
            //time += Time.deltaTime;
            if (time >= 10)
            {
                Debug.Log("semilla lanzada");
                time = 0;
                GameObject tmp = Instantiate(dropEmemyCan, seedPos.position, Quaternion.identity);


            }
        }
        if (gameObject.tag == "humanBoss")
        {
            if (time >= secondsToCount)
            {
                prefabSpawn(3);
                spawnBottles = true;
                spawnBags = true;
                //time = 0;          
            }
            if (time >= 10 && spawnBottles)
            {
                prefabSpawn(1);
                spawnBottles = false;
                //time = 0;          
            }
            if (time >= 20 && spawnBags)
            {
                prefabSpawn(2);
                spawnBags = false;
                //time = 0;          
            }
            //if (time >= 10)
            //{
            //    Debug.Log("semilla lanzada");
            //    //time = 0;
            //    GameObject tmp = Instantiate(dropEmemyCan, seedPos.position, Quaternion.identity);


            //}
            //if (time >= 20)
            //{
            //    Debug.Log("semilla lanzada");
            //    //time = 0;
            //    GameObject tmp = Instantiate(dropEnemyBottle, seedPos.position, Quaternion.identity);


            //}
            //if (time >= secondsToCount)
            //{
            //    Debug.Log("semilla lanzada");
            //    time = 0;
            //    GameObject tmp = Instantiate(dropEnemyBag, seedPos.position, Quaternion.identity);


            //}
        }


        if (!IsDead)
        {
            if (!TakingDamage)
            {
                currentState.Execute();

                if (transform.position.y <= -20f)
                {
                    healthStat.CurrentValue = 0;
                    MyAnimatior.SetLayerWeight(1, 0);
                    MyRigidbody.velocity = Vector2.zero;
                    dropItem = false;
                    MyAnimatior.SetTrigger("die");
                }
            }
            LookAtTarget();
        }
    }

    private void prefabSpawn(int enemyType)
    {
        if (enemyType == 1)
        {
            for (int i = 0; i < 1; i++)
            {
                GameObject tmp = Instantiate(dropEmemyCan, seedPos.position, Quaternion.identity);
                //Instantiate(drop, boss.transform.position, prefabBottle.transform.rotation);
                //time = 0;
            }
        }
        if (enemyType == 2)
        {
            for (int i = 0; i < 1; i++)
            {
                GameObject tmp = Instantiate(dropEnemyBottle, seedPos.position, Quaternion.identity);
                //Instantiate(prefabBag, boss.transform.position, prefabBag.transform.rotation);
                //time = 0;
            }
        }
        if (enemyType == 3)
        {

            for (int i = 0; i < 1; i++)
            {
                GameObject tmp = Instantiate(dropEnemyBag, seedPos.position, Quaternion.identity);
                //Instantiate(prefabCan, boss.transform.position, prefabCan.transform.rotation);
                time = 0;
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();

        }
        currentState = newState;
        currentState.Enter(this);
    }


    public void Move()
    {
        if (!Attack && !(currentState is IdleState))
        {
            //Mientras no llegue al edge, se mueve el enemigo,el edge interno ha sido sustituido po redges externos                                  

            if ((GetDirection().x > 0 /*&& transform.position.x < rightEdge.position.x*/) || (GetDirection().x < 0 /*&& transform.position.x > leftEdge.position.x*/))
            {
                if (this.gameObject.tag == "enemyBag") //Activar estas opciones y cambiar el nomnbre del tag para el enemigo que cambia de tamaño
                {
                    advancingCollider = GetComponent<BoxCollider2D>();
                    advancingCollider.size = new Vector2(2, 3);
                }

                //Pone la velocidad del jugador en 1
                MyAnimatior.SetFloat("speed", 1);

                //Mueve al enemigo en la dirección correcta
                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            //Si está en estado de caminata (patrol), y llega al edge, entonces cambiará de dirección
            else if (currentState is PatrolState)
            {
                if (this.gameObject.tag == "enemyBag")
                {
                    advancingCollider.size = new Vector2(2, 5);
                }
                //ChangeDirection();
                ChangeState(new IdleState());
            }
            else if (currentState is RangedState)
            {
                if (this.gameObject.tag == "enemyBag")
                {
                    advancingCollider.size = new Vector2(2, 5);
                }
                Target = null;
                ChangeState(new IdleState());
            }
        }
        else
        {
            if (this.gameObject.tag == "enemyBag")
            {
                advancingCollider.size = new Vector2(2, 5);
            }
        }

    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public override void ChangeDirection()
    {
        //Makes a reference to the enemys canvas
        Transform tmp = transform.Find("CanvasEnemyHealth").transform;

        //Stores the position, so that we know where to move it after we have flipped the enemy
        Vector3 pos = tmp.position;

        ///Removes the canvas from the enemy, so that the health bar doesn't flip with it
        tmp.SetParent(null);

        ///Changes the enemys direction
        base.ChangeDirection();

        //Puts the health bar back on the enemy.
        tmp.SetParent(transform);

        //Pits the health bar back in the correct position.
        tmp.position = pos;
    }
    /// 
    ///////////////////////////////////////////////////////////////////////////////////////
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;//if acortado
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
    }

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new PatrolState());
    }

    private void LookAtTarget()
    {
        if (Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;
            if (xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
                 
            if (this.gameObject.tag == "enemyBag")
            {
                advancingCollider.size = new Vector2(2, 5);
            }
            
        }
    }

    private IEnumerator IndicateInmortal() // Vuelve inmortal al enemigo por un momento
    {
        while (inmortal)
        {
            spriteRenderer.color = Color.red;
            //spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);

            spriteRenderer.color = Color.white;
            //spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    public GameObject collisionador;


    public override IEnumerator TakeDamage(float daño)
    {
        if (!inmortal)//Si no es inortal, recibirá daño
        {
            //Target = null;
            if (!healthCanvas.isActiveAndEnabled)
            {
                healthCanvas.enabled = true;
            }

            //reduce la vida
            healthStat.CurrentValue -= daño;
            if (!IsDead)  //Si el enemigo no está muerto aún, reproduce la animación de daño
            {
                MyAnimatior.SetTrigger("damage");

                if (this.gameObject.tag == "latonBoss" || this.gameObject.tag == "humanBoss")
                {
                    inmortal = true;

                    StartCoroutine(IndicateInmortal());
                    yield return new WaitForSeconds(inmortalTime);
                    inmortal = false;
                }

            }

            else //Si el enemigo está muerto, se asegura de reproducir la animación de muerte
            {
                MyRigidbody.velocity = Vector2.zero;
                MyAnimatior.SetTrigger("die");
                //collisionador.gameObject.SetActive(true);
                yield return null;
            }
        }

    }
    /*Elimina al enemigo del juego
    public override void Death()
    {
        Destroy(gameObject);
    }
    */


    public override void Death()
    {
        if (this.gameObject.tag == "Enemy" || this.gameObject.tag == "glassEnemy" || this.gameObject.tag == "enemyBag")
        {
            Destroy(gameObject);
            if (dropItem)//El objeto collectable se asigna en el gamemanager, ahí se añade el prefab correspondiente
            {
                //Referencia al objeto collectable
                if (gameObject.tag == "Enemy")
                {
                    GameObject collectable = (GameObject)Instantiate(GameManager.Instance.CollectableCans, new Vector3(transform.position.x, transform.position.y - 2), Quaternion.identity);

                    Physics2D.IgnoreCollision(collectable.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    dropItem = false;
                }

                else if (gameObject.tag == "glassEnemy")
                {
                    GameObject collectable = (GameObject)Instantiate(GameManager.Instance.CollectableBottles, new Vector3(transform.position.x, transform.position.y - 2), Quaternion.identity);

                    Physics2D.IgnoreCollision(collectable.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    dropItem = false;
                }

                else if (gameObject.tag == "enemyBag")
                {
                    GameObject collectable = (GameObject)Instantiate(GameManager.Instance.CollectableBags, new Vector3(transform.position.x, transform.position.y - 2), Quaternion.identity);

                    Physics2D.IgnoreCollision(collectable.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                    dropItem = false;
                }
             

                //Se asegura que la colisión entre el ccollectable y el enemigo sea ignorada

            }
        }
        Destroy(gameObject);
        //Elimina al enemigo del juego
        dropItem = true;
        //gameObject.SetActive(false);

    

    //Elimina al enemigo del juego*, lo vuelve a aparecer (revive)
    /*//Reestablecen al enemigo (revive)
    healthStat.CurrentValue = healthStat.MaxVal;
    transform.position = startPos;

    MyAnimatior.ResetTrigger("die");
    MyAnimatior.SetTrigger("idle");
    healthCanvas.enabled = false;
    */
}
}
