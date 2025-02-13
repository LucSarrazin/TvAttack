using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TvEnemyIA : MonoBehaviour
{
    public NavMeshAgent Enemy;
    public Animator Animation;
    public Transform transformPlayer;
    public PlayerCamera Playercamera;

    public int life = 100;

    public float delay;
    private bool oneTime = false;

    public CapsuleCollider capsuleCollider;
    public TvEnemyIA script;
    public bool playerIn = false;
    public AudioSource DeathSound;
    private spawner spawn;
    public bool died = false;



    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("GameManager").GetComponent<spawner>();
        spawn.numberEnemyAlive++;
        transformPlayer = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Playercamera = GameObject.FindWithTag("MainCamera").GetComponent<PlayerCamera>();
        Enemy = GetComponent<NavMeshAgent>();
        Enemy.speed = Random.Range(1,3);
    }

    private void Update()
    {
        if(Time.time > .1f) {
            GetComponent<NavMeshAgent>().enabled = true; }
      /*  if(!Enemy.isOnNavMesh) {
            
            Enemy.enabled = false;
            Enemy.enabled = true;
        }*/

        if (life <= 0)
        {
            spawn.numberEnemyAlive--;
            died = true;
            DeathSound.Play();
            print("Death");
            Playercamera.score = Playercamera.score + Random.Range(40,65);
            Animation.enabled = false;
            capsuleCollider.enabled = false;
            Enemy.enabled = false;
            script.enabled = false;
            Destroy(gameObject, 20f);
        }

        if (oneTime == false && playerIn == true)
        {
            oneTime = true;
            StartCoroutine(wait());
        }
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
       // RaycastHit hit;
        //Debug.DrawRay(new Vector3(transform.localPosition.x,position.y,transform.localPosition.z), Vector3.forward * distance, Color.yellow);
       // if (Physics.Raycast(new Vector3(transform.localPosition.x,position.y,transform.localPosition.z), Vector3.forward, out hit, distance))
        //{
            //print(hit.collider);
            Enemy.SetDestination(transformPlayer.position);
            Animation.SetBool("Walking", true);
            // }
            // else
            //{
            //    Animation.SetBool("Walking", false);
            //     Enemy.SetDestination(Enemy.transform.position);
            // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            Animation.SetBool("Attack", false);
            playerIn = false;
            oneTime = false;
        }
    }

    IEnumerator wait()
    {
        Animation.SetBool("Attack", true);
        Playercamera.playerLife = Playercamera.playerLife - 5;
        yield return new WaitForSeconds(delay);
        oneTime = false;
    }
}
