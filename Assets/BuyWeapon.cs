using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyWeapon : MonoBehaviour
{
    public bool inReach;
    public GameObject Weapon;
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
            if (Player.score >= prix && Player.mitra == false)
            {
                Player.score = Player.score - prix;
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
        inReach = false;
        price.SetActive(false);
        Player.twoGun = true;
        Player.mitra = true;
    }
}
