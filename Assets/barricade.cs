using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class barricade : MonoBehaviour
{
    public bool inReach;
    public GameObject barrage;
    private PlayerCamera Player;

    public bool vendre;
    public int prix;
    public spawner spawn;
    public bool zone2 = false;
    public bool zone3 = false;
    public bool zone4 = false;
    public bool zone5 = false;
    public TextMeshProUGUI priceText;
    public GameObject price;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inReach == true && Input.GetKeyDown(KeyCode.E) || inReach == true && Player.interact == true)
        {
            if (vendre == true)
            {
                if (Player.score >= prix)
                {
                    Player.score = Player.score - prix;
                    recup();
                }
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arm"))
        {
            inReach = true;
            priceText.text = "Price : " + prix;
            price.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Arm"))
        {
            inReach = false;
            price.SetActive(false);
        }
    }

    public void recup()
    {
        inReach = false;
        if (zone2 == true)
        {
            spawn.zone2 = true;
        }
        if (zone3 == true)
        {
            spawn.zone3 = true;
        }
        if (zone4 == true)
        {
            spawn.zone4 = true;
        }
        if (zone5 == true)
        {
            spawn.zone5 = true;
        }
        barrage.SetActive(false);
        price.SetActive(false);
    }
}
