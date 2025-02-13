using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class medkit : MonoBehaviour
{
    public bool inReach;
    public GameObject Medkit;
    private PlayerCamera Player;
    public TextMeshProUGUI priceText;
    public GameObject price;

    public bool vendre;
    public int prix;
    
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
            else if (vendre == false)
            {
                recup();
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
        Player.playerLife = Player.playerLife + 25;
        inReach = false;
        Medkit.SetActive(false);
        price.SetActive(false);
    }
}

