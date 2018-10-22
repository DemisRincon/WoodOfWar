using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{


    // 17 hacemos referencia de el animador  para poder cambiar nuestras animacion

    [SerializeField]
    protected Transform seedPos;

    //[SerializeField]
    //protected Transform seedPos2; //esta posicion es para la segunda semilla

    //[SerializeField]
    //protected Transform seedPos3; //esta posicion es para la tercera semilla

    [SerializeField]
    protected float movementSpeed;    // 07 añadimos velocidad para poder controlar la velocidad del personaje

    protected bool facingRight;   // 08 creamos una variable para saber a donde voltea nuestro personaje la cual moveremos y nos permitira voltear el sprite del personaje

    //Elprefab de semilla, se usa para crear una instancia de la semilla
    [SerializeField]
    protected GameObject seed;

    [SerializeField]           //La segunda semilla es para los jefes
    protected GameObject seed2;

    [SerializeField]           //La tercera semilla es para los jefes
    protected GameObject seed3;

    //La salud del personaje (variable anterior)
    //[SerializeField]
    //protected int health;

    //La salud del personaje
    [SerializeField]
    protected Stat healthStat;

    [SerializeField]
    private EdgeCollider2D hitCollider;

    [SerializeField]
    private EdgeCollider2D swordCollider;

    [SerializeField]
    private EdgeCollider2D hSlayerCollider;

    [SerializeField]
    private List<string> damageSources;

    public abstract bool IsDead { get; }

    public bool Attack { get; set; }

    public bool TakingDamage { get; set; }

    public Animator MyAnimatior { get; private set; }

    public EdgeCollider2D HitCollider //regresa el hit collider para hacerlo accesible desde otras funciones
    {
        get
        {
            return hitCollider;
        }
    }

    public EdgeCollider2D SwordCollider //regresa el sword collider para hacerlo accesible desde otras funciones
    {
        get
        {
            return swordCollider;
        }
    }
    public EdgeCollider2D HSlayerCollider //regresa el sword collider para hacerlo accesible desde otras funciones
    {
        get
        {
            return hSlayerCollider;
        }
    }

    // Use this for initialization
    public virtual void Start()
    {
        if (this.gameObject.name == "bossCan")
        {

        }

        facingRight = true; // 10 seteamos en verdadero el estar viendo a la derecha

        MyAnimatior = GetComponent<Animator>();// 18 hacemos referencia al animador en la inicializacion

        //Inicializa la salud del personaje y del enemigo
        healthStat.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract IEnumerator TakeDamage(float damageAmount);

    public abstract void Death();

    public virtual void ChangeDirection()
    {
        facingRight = !facingRight;

        //Da vuelta al personaje al cambiar la escala
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public virtual void ThrowSeed(int value)
    {
        if (facingRight)
        {
            if (gameObject.tag == "latonBoss")//Si es es boss, dispara tres bolas en diferentes direcciones
            {
                GameObject tmp = (GameObject)Instantiate(seed, seedPos.position, Quaternion.identity);
                tmp.GetComponent<Seed>().Initialize(Vector2.right);

                GameObject tmp2 = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 45)));
                tmp2.GetComponent<Seed>().Initialize(Vector2.one);

                GameObject tmp3 = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp3.GetComponent<Seed>().Initialize(Vector2.up);
            }
            else if (gameObject.tag == "humanBoss")//Si es es boss, dispara tres bolas en diferentes direcciones
            {
                GameObject tmp = (GameObject)Instantiate(seed, seedPos.position, Quaternion.identity);
                tmp.GetComponent<Seed>().Initialize(Vector2.right);

                GameObject tmp2 = (GameObject)Instantiate(seed2, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 45)));
                tmp2.GetComponent<Seed>().Initialize(Vector2.one);

                GameObject tmp3 = (GameObject)Instantiate(seed3, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp3.GetComponent<Seed>().Initialize(Vector2.up);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                tmp.GetComponent<Seed>().Initialize(Vector2.right);

            }
        }
        else
        {
            if (gameObject.tag == "latonBoss")//Si es es boss, dispara tres bolas en diferentes direcciones
            {
                GameObject tmp = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                tmp.GetComponent<Seed>().Initialize(Vector2.left);

                GameObject tmp2 = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 135)));
                tmp2.GetComponent<Seed>().Initialize((Vector2.one) - (2 * Vector2.right));

                GameObject tmp3 = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp3.GetComponent<Seed>().Initialize(Vector2.up);
            }
            else if (gameObject.tag == "humanBoss")//Si es es boss, dispara tres bolas en diferentes direcciones
            {
                GameObject tmp = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                tmp.GetComponent<Seed>().Initialize(Vector2.left);

                GameObject tmp2 = (GameObject)Instantiate(seed2, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 135)));
                tmp2.GetComponent<Seed>().Initialize((Vector2.one) - (2 * Vector2.right));

                GameObject tmp3 = (GameObject)Instantiate(seed3, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                tmp3.GetComponent<Seed>().Initialize(Vector2.up);
            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(seed, seedPos.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                tmp.GetComponent<Seed>().Initialize(Vector2.left);

            }
        }

    }

    public void MeleeAttack()
    {
        HitCollider.enabled = true;
    }

    public void SwordAttack()
    {
        SwordCollider.enabled = true;
    }
    public void HSlayerAttack()
    {
        HSlayerCollider.enabled = true;
    }

    /// <summary>
    /// Si el personaje choca con otro objeto
    /// </summary>
    /// <param name="other"></param>
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Edge" && (this.gameObject.tag == "Enemy" || this.gameObject.tag == "glassEnemy" || this.gameObject.tag == "enemyBag" || this.gameObject.tag == "latonBoss" || this.gameObject.tag == "humanBoss"))
        {
            ChangeDirection();
        }

    
        if (other.gameObject.name == "TransitionScene" && this.gameObject.tag=="Player")
        {
            Debug.Log("trigger enter");
            movementSpeed = 0;
        }
        //Si el objeto que tocamos es una fuente de daño
        if (damageSources.Contains(other.tag)) //aumenta la cantidad de condiciones dependiendo de la 
                                               //cantidad de ataques que existen, incluyendo los ataques de doran y los de todos los enemigos
                                               //El numero dentro de la función TakeDamage() es la cantidad de daño que hará ese ataque
        {
            if (other.tag == "fireball" || other.tag == "enemyFireball")
            {
                StartCoroutine(TakeDamage(10));
            }
            else if (other.tag == "Hit" || other.tag == "HitLaton")
            {
                StartCoroutine(TakeDamage(15));
            }
            else if (other.tag == "sword")
            {
                StartCoroutine(TakeDamage(25));
            }
            else if (other.tag == "hSlayer")
            {
                StartCoroutine(TakeDamage(40));
            }
            else if (other.tag == "hitBoss")
            {
                StartCoroutine(TakeDamage(20));
            }
            //StartCoroutine(TakeDamage(0)); //Originalmente solo queda esta linea en la condicion if (damageSources...)
            //Debug.Log("Ataque");
        }
    }
}
