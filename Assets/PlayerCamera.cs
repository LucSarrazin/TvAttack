using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using Slider = UnityEngine.UI.Slider;
using UnityEngine.Rendering.PostProcessing;

public class PlayerCamera : MonoBehaviour
{
  //  public float maxSensitivity = 100f;
  //  public Transform player;
   // private float xRot = 0f;

    public float maxDistance = 10f;
    public float offset;
    public TvEnemyIA enemy;
    public int playerLife = 100;
    public Slider lifeSlider;
    public AudioSource gunSound;
    public AudioSource gunRifleSound;
    public AudioSource reloadSound;
    public float fireRate = 0;
    public Animator gunAnimation;
    public int bullet = 9;
    public int bulletRifle = 15;
    public int bulletSave;
    public int bulletRifleSave;
    public TextMeshProUGUI munition;
    public bool oneTime = false;
    public ParticleSystem gunParticle;
    public ParticleSystem gunShellsParticle;
    public int degats = 5;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public AudioSource ImpactSound;

    public bool mitra = false;
    public bool oneTime2 = false;
    public ParticleSystem gunRifleParticle;
    public ParticleSystem gunRifleShellsParticle;
    public Animator gunRifleAnimation;
    public GameObject gunRifle;
    public GameObject gunPistol;

    public bool twoGun = false;
    public bool interact = false;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + "";
        if (mitra == true)
        {
            munition.text = bulletRifle + "/100";
        }
        else
        {
            munition.text = bullet + "/100";
        }
        lifeSlider.value = playerLife;
        if (playerLife <= 0)
        {
            Destroy(gameObject);
        }

        if (playerLife >= 100)
        {
            playerLife = 100;
        }

        if (fireRate <= 0)
        {
            fireRate = 0;
        }
        if (fireRate >= 0)
        {
            fireRate = fireRate - Time.deltaTime;
        }

        if (mitra == true)
        {
            gunPistol.SetActive(false);
            gunRifle.SetActive(true);
        }
        else
        {
            gunPistol.SetActive(true);
            gunRifle.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Mouse2) && twoGun == true)
        {
                if (mitra == true)
                {
                    gunPistol.SetActive(true);
                    gunRifle.SetActive(false);
                    bulletRifleSave = bulletRifle;
                    munition.text = "" + bulletSave;
                    mitra = false;
                }
                else
                {
                    gunPistol.SetActive(false);
                    gunRifle.SetActive(true);
                    bulletSave = bullet;
                    munition.text = "" + bulletRifleSave;
                    mitra = true;
                }
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
        
       

       if (Input.GetKeyDown(KeyCode.R) && oneTime == false && bullet < 9 && mitra == false)
       {
           StopAllCoroutines();
           oneTime = true;
           StartCoroutine(reload());
       }
       if (Input.GetKeyDown(KeyCode.R) && oneTime == false && bulletRifle < 15 && mitra == true)
       {
           StopAllCoroutines();
           oneTime2 = true;
           StartCoroutine(reload2());
       }
        
        
       Debug.DrawRay(new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.forward * maxDistance, Color.yellow);
                if (Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0 && bullet >= 1 && oneTime == false && mitra == false)
                {
                    gunAnimation.SetBool("Shooting", true);
                    gunParticle.Play();
                    gunShellsParticle.Play();
                    StartCoroutine(wait());
                    gunSound.Play();
                    fireRate = 1f;
                    bullet--;
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward * maxDistance, out hit))
                    {
                        ImpactSound = hit.collider.gameObject.GetComponent<AudioSource>();
                        Debug.DrawRay(new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.forward * maxDistance, Color.yellow);
                        print(hit.collider);
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            ImpactSound.Play();
                            enemy = hit.collider.GetComponent<TvEnemyIA>();
                            if (enemy.died == false)
                            {
                                enemy.life = enemy.life - degats;
                                score = score + Random.Range(5,15);
                                print("Toucher");
                            }
                            else
                            {
                                print("Déjà Mort espèce d'idiot");
                            }
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.Mouse0) && fireRate <= 0 && bulletRifle >= 1 && oneTime2 == false && mitra == true)
                {   
                    gunRifleAnimation.SetBool("Shooting", true);
                    gunRifleParticle.Play();
                    gunRifleShellsParticle.Play();
                    StartCoroutine(wait2());
                    gunRifleSound.Play();
                    fireRate = 0.5f;
                    bulletRifle--;
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward * maxDistance, out hit))
                    { 
                           ImpactSound = hit.collider.gameObject.GetComponent<AudioSource>();
                           Debug.DrawRay(new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.forward * maxDistance, Color.yellow);
                           print(hit.collider);
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            ImpactSound.Play();
                            enemy = hit.collider.GetComponent<TvEnemyIA>();
                            if (enemy.died == false)
                            {
                                enemy.life = enemy.life - degats - 15;
                                score = score + Random.Range(5,15);
                                print("Toucher");
                            }
                            else
                            {
                                print("Déjà Mort espèce d'idiot");
                            }
                        }
                    } 
                
                }

                IEnumerator wait()
                {
                    yield return new WaitForSeconds(1f);
                    gunAnimation.SetBool("Shooting", false);
                }

                IEnumerator wait2()
                {
                    yield return new WaitForSeconds(0.5f);
                    gunRifleAnimation.SetBool("Shooting", false);
                }
                
                IEnumerator reload()
                {
                    
                    gunAnimation.SetBool("Reload", true);
                    yield return new WaitForSeconds(0.2f);
                    reloadSound.Play();
                    yield return new WaitForSeconds(0.5f);
                    bullet = 9;
                    gunAnimation.SetBool("Reload", false);
                    oneTime = false;
                }
        
                IEnumerator reload2()
                {
                    
                    gunRifleAnimation.SetBool("Reload", true);
                    yield return new WaitForSeconds(0.2f);
                    reloadSound.Play();
                    yield return new WaitForSeconds(0.5f);
                    bulletRifle = 15;
                    gunRifleAnimation.SetBool("Reload", false);
                    oneTime2 = false;
                }
                
    }
    // Bouton pour le téléphone
    public void Reload()
    {
        if (oneTime == false && bullet < 9 && mitra == false)
        {
            StopAllCoroutines();
            oneTime = true;
            StartCoroutine(reload());
        }
        if (oneTime2 == false && bullet < 15 && mitra == true)
        {
            StopAllCoroutines();
            oneTime2 = true;
            StartCoroutine(reload2());
        }
        
        IEnumerator reload()
        {
                    
            gunAnimation.SetBool("Reload", true);
            yield return new WaitForSeconds(0.2f);
            reloadSound.Play();
            yield return new WaitForSeconds(0.5f);
            bullet = 9;
            gunAnimation.SetBool("Reload", false);
            oneTime = false;
        }
        
        IEnumerator reload2()
        {
                    
            gunRifleAnimation.SetBool("Reload", true);
            yield return new WaitForSeconds(0.2f);
            reloadSound.Play();
            yield return new WaitForSeconds(0.5f);
            bulletRifle = 15;
            gunRifleAnimation.SetBool("Reload", false);
            oneTime2 = false;
        }
    }

    public void ChangementDarmes()
    {
        if (twoGun == true)
        {
            if (mitra == true)
            {
                gunPistol.SetActive(true);
                gunRifle.SetActive(false);
                mitra = false;
            }
            else
            {
                gunPistol.SetActive(false);
                gunRifle.SetActive(true);
                mitra = true;
            }
        }
    }


    public void interagirButton()
    {
        if(interact == false)
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
    }
	
	public void Shooting()
	{
		if (fireRate <= 0 && bullet >= 1 && oneTime == false && mitra == false)
                {
                    gunAnimation.SetBool("Shooting", true);
                    gunParticle.Play();
                    gunShellsParticle.Play();
                    StartCoroutine(wait());
                    gunSound.Play();
                    fireRate = 1f;
                    bullet--;
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward * maxDistance, out hit))
                    {
                        ImpactSound = hit.collider.gameObject.GetComponent<AudioSource>();
                        Debug.DrawRay(new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.forward * maxDistance, Color.yellow);
                        print(hit.collider);
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            ImpactSound.Play();
                            enemy = hit.collider.GetComponent<TvEnemyIA>();
                            if (enemy.died == false)
                            {
                                enemy.life = enemy.life - degats;
                                score = score + Random.Range(5,15);
                                print("Toucher");
                            }
                            else
                            {
                                print("Déjà Mort espèce d'idiot");
                            }
                        }
                    }
                }
		if (fireRate <= 0 && bulletRifle >= 1 && oneTime2 == false && mitra == true)
                {   
                    gunRifleAnimation.SetBool("Shooting", true);
                    gunRifleParticle.Play();
                    gunRifleShellsParticle.Play();
                    StartCoroutine(wait2());
                    gunRifleSound.Play();
                    fireRate = 0.5f;
                    bulletRifle--;
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.forward * maxDistance, out hit))
                    { 
                           ImpactSound = hit.collider.gameObject.GetComponent<AudioSource>();
                           Debug.DrawRay(new Vector3(transform.position.x,transform.position.y,transform.position.z), transform.forward * maxDistance, Color.yellow);
                           print(hit.collider);
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            ImpactSound.Play();
                            enemy = hit.collider.GetComponent<TvEnemyIA>();
                            if (enemy.died == false)
                            {
                                enemy.life = enemy.life - degats - 15;
                                score = score + Random.Range(5,15);
                                print("Toucher");
                            }
                            else
                            {
                                print("Déjà Mort espèce d'idiot");
                            }
                        }
                    } 
                
                }
		IEnumerator wait()
                {
                    yield return new WaitForSeconds(1f);
                    gunAnimation.SetBool("Shooting", false);
                }

        IEnumerator wait2()
                {
                    yield return new WaitForSeconds(0.5f);
                    gunRifleAnimation.SetBool("Shooting", false);
                }
	}
}
