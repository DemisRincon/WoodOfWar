using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void DeadEventHandler();

//controladores del jugador
public class Player : Character
{

    //para movermos necesitamos una referencia de el rigidbody
    private static Player instance;

    public event DeadEventHandler Dead;

    //[SerializeField]
    //private Stat healthStat;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }

    }

    [SerializeField]
    private Transform[] groundPoints;// 34 hacemos una referenci para los puntos que indican si estan aterrizado los cuales serviran para no dejarlo saltar a menos que haya aterrizado es un vector pues necesitamos mas de uno para referenciar

    [SerializeField]
    private float groundRadius; //39 creamos el radio de collision para que sea ajustable

    [SerializeField]
    private float jumpforce; //50 agregamos una variable para saber la fuerza con la que salta

    private bool inmortal = false;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float inmortalTime;

    private float direction; //Para verificar la dirección en el eje horizontal

    private bool move; //Para verificar si se mueve con los botones

    private float btnHorizontal;

    [SerializeField]
    private LayerMask whatIsGround; //40 queremos saber que esta aterrizado asi que creamos una layer que nos diga que es lo que esta aterrizado

    [SerializeField]
    private bool airControl; // 55 añadimos un boleano para probar el juego con air control y sin el

    public Rigidbody2D MyRigidbody { get; set; }   //01 esto solo es una variable se necesita hacer una referencia en start()

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    private Vector2 startPos;

    private double throwTimer = .4;
    private double throwCoolDown = .8;
    private bool canThrow = false;

    public override bool IsDead
    {
        get
        {
            if (healthStat.CurrentValue <= 0)
            {
                OnDead();
            }

            return healthStat.CurrentValue <= 0;
        }
    }

    public bool IsFalling
    {
        get
        {
            return MyRigidbody.velocity.y < 0;
        }
    }

    //se cambiaron debido a aque debemos acceder a ellos de otros lados
    public override void Start()
    {
        base.Start();
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        MyRigidbody = GetComponent<Rigidbody2D>();  // 02 con esto obtenemos el rigid al que este atachado el script

    }
    private void Update()
    {
        throwTimer += Time.deltaTime;
        if (!TakingDamage && !IsDead)
        {
            if (transform.position.y <= -20f)
            {
                healthStat.CurrentValue = 0;
                MyAnimatior.SetLayerWeight(1, 0);
                MyRigidbody.velocity = Vector2.zero;
                //GameObject.FindGameObjectWithTag("MainCamera").transform.parent = null;
                //CameraController.DestroyObject();
                MyAnimatior.SetTrigger("die");
            }
            HandleInput();// 28 llamamos a revisar los inputs, esta en update porque no necesitamos llamarlos cada frame
        }
    }

    void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal"); // 05 creamos un flotante que reaccione a los comandos en este caso horizontal

            OnGround = IsGrounded(); // 46 igualamos el boleano al resultado de la funcion

            if (move)
            {
                this.btnHorizontal = Mathf.Lerp(btnHorizontal, direction, Time.deltaTime * 10); //Para retrasar el momento en que el jugador comienza a correr
                HandleMovement(btnHorizontal);
                Flip(direction);
            }

            else
            {
                HandleMovement(horizontal); // 06 llamamos la funcion y enviamos el valor del flotante

                Flip(horizontal);   // 16 llamomos la funcion y le enviamos los valores de hacia que lado nos movemos
            }

            HandleLayers(); // 58 llama a el cambio layer todo el tiempo pero despues de tener la oportunidad de ver si saltó haber asaltado
        }
    }

    public void OnDead()//revisar muerte de doran
    {
        if (Dead != null)
        {
            Dead();
        }
    }

    private void HandleMovement(float horizontal)   // 03 creamos una funcion para controlar el movimiento
    {
        if (IsFalling)
        {
            gameObject.layer = 12;///////verificar layer, la 11 sí es la de player
            MyAnimatior.SetBool("land", true);
        }
        if (!Attack && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpforce));
        }


        MyAnimatior.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal) // 09 creamos una funcion para voltear al personaje
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)// creamos un if donde comparamos hacia donde vemos y el movimento que llevamos, ponemos ambos casos donde nosmovammos para un lado y no estemos volteando a su lado correspondiente, entonces nos voltearemos
        {
            ChangeDirection();
        }
    }


    private void HandleInput()// 24 creamos una funcion para administrar todos los inputs que le jugador haga
    {
        if (Input.GetButtonDown("Jump") && !IsFalling) // 52 si la tecla de espacio esta presionada
        {
            MyAnimatior.SetTrigger("jump");
        }
        if (Input.GetButtonDown("Throw"))
        {
            if (throwTimer >= throwCoolDown)
            {
                canThrow = true;
                throwTimer = 0;

            }
            if (canThrow)
            {
                canThrow = false;
                MyAnimatior.SetTrigger("throw");
                SoundManager.PlaySound("playerHit");
            }
            //MyAnimatior.SetTrigger("throw");
            //SoundManager.PlaySound("playerHit");
        }
        if (Input.GetButtonDown("CristalSword"))// 25 revisa si presiona la tecla de ataque con espada de cristal
        {
            try
            {
                if (SaveMananger.Instance.isWeaponOwned(0)) // revisa si se tiene adquirida la espada de cristal
                {
                    MyAnimatior.SetTrigger("swordAttack");
                }
                else                                        // si aun no tiene la espada, entonces solo realiza un golpe
                {
                    MyAnimatior.SetTrigger("attack");
                    SoundManager.PlaySound("playerHit");
                }
            }
            catch (System.Exception)
            {
                MyAnimatior.SetTrigger("attack");
                SoundManager.PlaySound("playerHit");
                throw;
            }
            
            
        }
        if (Input.GetButtonDown("HSlayer"))// 25 revisa si presiona la tecla de ataque con HSlayer
        {
            try
            {
                if (SaveMananger.Instance.isWeaponOwned(1)) // revisa si se tiene adquirida la espada de cristal
                {
                    MyAnimatior.SetTrigger("hSlayerAttack");
                }
                else                                        // si aun no tiene la espada, entonces solo realiza un golpe
                {
                    MyAnimatior.SetTrigger("attack");
                    SoundManager.PlaySound("playerHit");
                }
            }
            catch (System.Exception)
            {
                MyAnimatior.SetTrigger("attack");
                SoundManager.PlaySound("playerHit");
                throw;
            }
   
        }
    }

    private bool IsGrounded()   // 35 creamos una funcion que nos regresara un verdadero o falso dependiendo si esta aterrizado o no
    {
        if (MyRigidbody.velocity.y <= 0)//36 si la velocidad en y es menor o igual a cero estara aterrizado
        {
            foreach (Transform point in groundPoints)// 37 checamos cada uno de los puntos para saber si estamos aterrizados
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);// 38 checamos si esta coliconando con algo dande un radio de collision este vector cotendra todo con lo que collisiones

                for (int i = 0; i < colliders.Length; i++)//    41 checamos todo lo que hay en el vector
                {
                    if (colliders[i].gameObject != gameObject) //   42 si el colisionador qcon el que esta haciendo contacto es diferente de el ugador entonces sabremos que esta colisionando con algo que corresponde a la mascar
                    {

                        return true; // 43 si encuentra que almenos en un objeto esta collisionando retornatra un valor verdadero a la funcion boleana
                    }
                }

            }

        }
        return false;// 44  si la velocidad es mayor a cero entonces esta en el aire y retorna falso cada vez que entre al ciclo

    }

    private void HandleLayers() // 54 creamos un manejador de capas para el animador
    {
        if (!OnGround) // 55 si no esta en tierra entonces 
        {
            MyAnimatior.SetLayerWeight(1, 1); // 56 cambia el peso de la layer para que sea la que se vea
        }
        else
        {
            MyAnimatior.SetLayerWeight(1, 0);// 57 regresa la layer a su estado original
        }
    }
    public override void ThrowSeed(int value)
    {
        if (!OnGround && value == 1 || OnGround && value == 0)
        {
            base.ThrowSeed(value);
        }

    }

    private IEnumerator IndicateInmortal()
    {
        while (inmortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }

    public override IEnumerator TakeDamage(float daño)
    {
        if (!inmortal)
        {
            healthStat.CurrentValue -= daño;
            if (!IsDead)
            {
                MyAnimatior.SetTrigger("damage");
                inmortal = true;

                StartCoroutine(IndicateInmortal());
                yield return new WaitForSeconds(inmortalTime);
                inmortal = false;
            }
            else
            {
                MyAnimatior.SetLayerWeight(1, 0);
                MyRigidbody.velocity = Vector2.zero;
                MyAnimatior.SetTrigger("die");
            }
        }
    }
    //Hace que el jugador reaparezca en el punto de inicio

    public GameObject panelDeath;
    public override void Death()
    {
        //MyRigidbody.velocity = Vector2.zero;     
        transform.position = startPos;
        MyAnimatior.SetTrigger("idle");
        panelDeath.SetActive(true);
        healthStat.CurrentValue = healthStat.MaxVal;
        GameManager.Instance.CollectedCans = 0;
        GameManager.Instance.CollectedBottles = 0;
        GameManager.Instance.CollectedBags = 0;
        Time.timeScale = 0;

    }

    /*public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "latonCollectable")
        {
            GameManager.Instance.CollectedObjects++;
            Destroy(other.gameObject);
        }
    }*/

    //Desaparece el collectable cuando colisionas con su collider
    //en cantidades grandes el personaje se detiene, por lo tanto no es óptimo
    //Se sustituyó por uno que detecte el trigger, para lo cual se añadio al prefab un 
    //segundo collider, en el cual el más grande sería el trigger para ativarlo, 
    //y el otro collider queda para que no caiga de la plataforma.


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "latonCollectable")
        {
            GameManager.Instance.CollectedCans++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "glassCollectable")
        {
            GameManager.Instance.CollectedBottles++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "bagCollectable")
        {
            GameManager.Instance.CollectedBags++;
            Destroy(other.gameObject);
        }
     
    }
    /*
   
    }*/
    
    public void BtnJump()
    {
        if (!IsFalling)
        {
            MyAnimatior.SetTrigger("jump");
            Jump = true;
        }

    }

    public void BtnAttack()
    {
        MyAnimatior.SetTrigger("attack");
    }

    public void BtnHSlayer()
    {
        MyAnimatior.SetTrigger("hSlayerAttack");
    }

    public void BtnSword()
    {
        MyAnimatior.SetTrigger("swordAttack");
    }

    public void BtnThrow()
    {
        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0;
        }
        if (canThrow)
        {
            canThrow = false;
            MyAnimatior.SetTrigger("throw");
        }
        //MyAnimatior.SetTrigger("throw");
    }

    public void BtnMove(float direction)
    {
        this.direction = direction;
        this.move = true;
    }

    public void BtnStopMove()//Para detener al jugador (que deje de correr al soltar el botón)
    {
        this.direction = 0;
        this.btnHorizontal = 0; //Reinicia el eje horizontal, esto evita que el jugador corra un poco en sentido opuesto al presionar el boton contrario de correr
        move = false;
    }
}
