using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    [SerializeField]
    private GameObject collectableCans;

    [SerializeField]
    private GameObject collectableBottles;

    [SerializeField]
    private GameObject collectableBags;

    [SerializeField]
    private Text collectableTxtBottles;

    [SerializeField]
    private Text collectableTxtCans;

    [SerializeField]
    private Text collectableTxtBags;

    //cantidad de objetos recolectados (latones, etc, para ser usado en cualquier escena con cualquier enemigo)
    private int collectedBottles;

    private int collectedCans;

    private int collectedBags;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    //Prefab colectable: laton, etc
    public GameObject CollectableCans
    {
        get
        {
            return collectableCans;
        }
    }

    //Prefab colectable: laton, etc
    public GameObject CollectableBottles
    {
        get
        {
            return collectableBottles;
        }
    }

    //Prefab colectable: laton, etc
    public GameObject CollectableBags
    {
        get
        {
            return collectableBags;
        }
    }

    //cantidad de objetos recolectados (latones, etc)
    public int CollectedBottles
    {
        get
        {
            return collectedBottles;
        }

        set
        {
            collectableTxtBottles.text = value.ToString();
            this.collectedBottles = value;
        }
    }

    //Cantidad de latas
    public int CollectedCans
    {
        get
        {
            return collectedCans;
        }

        set
        {
            collectableTxtCans.text = value.ToString();
            this.collectedCans = value;
        }
    }

    //Cantidad de latas
    public int CollectedBags
    {
        get
        {
            return collectedBags;
        }

        set
        {
            collectableTxtBags.text = value.ToString();
            this.collectedBags = value;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
