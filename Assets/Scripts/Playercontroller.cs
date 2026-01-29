using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] public Animator anim;
    [SerializeField] public float speed;
    [SerializeField] public float shift=2;
    [HideInInspector] public bool isDead;
    [HideInInspector]public bool isStart;
    [SerializeField] public int score;
    [HideInInspector] public float floatScore;
    [HideInInspector] float passedTime;
    [SerializeField] int health;

    public bool is2xActive, isShieldActive, isMagnetActive;
    float beforeSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        MoveCharacter();
    }

    /// <summary>
    /// Bu metot karakterin temel hareketi
    /// </summary>
    void MoveCharacter()
    {
        if (!isStart) return;
        if (isDead) return;

        if(is2xActive)
        {
            floatScore += Time.deltaTime;
        }
        floatScore += Time.deltaTime;
        if (floatScore > 1) 
        {
            score += 1;
            floatScore = 0;

        }

        if (passedTime > 5 )
        {
            speed += 0.5f;
            passedTime = 0;
        }

        // Sürekli ileri hareket
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Sola hareket (A tuþu)
        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -0.5f)
        {
            //transform.Translate(new Vector3(-shift, 0, 0));
            transform.DOMoveX(transform.position.x - shift, 0.5f).SetEase(Ease.Linear);
        }
        // Saða hareket (D tuþu)
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 0.5f)
        {
            // transform.Translate(new Vector3(shift, 0, 0));
            transform.DOMoveX(transform.position.x + shift, 0.5f).SetEase(Ease.Linear);
        }
       

   
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))

        {
            int damage = other.gameObject.GetComponent<Obstacle>().damage;
            
            if(isShieldActive)

            {
                Destroy(other.gameObject);
                isShieldActive = false; 
            }

            else

            {
                CheckHealth(damage, other.gameObject);
            }
                       
        }
    }
    
    void CheckHealth(int damage , GameObject other)
    {
        health -= damage;
        
        if (health <= 0)
        {
            anim.SetBool("Death", true);
            isDead = true;
        }

        else
        {
            Destroy(other.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable")) 
        {
            Collectables collectable = other.GetComponent<Collectables>();

            switch(collectable.CollectablesEnum)
            {
                case CollectablesEnum.Coin:
                    AddScore(collectable.toBeAddedScore);
                    break;
                case CollectablesEnum.Shield:
                    ActivateShield();
                    break;
                    case CollectablesEnum.Health:
                    AddHealth(collectable.toBeAddedHealth);
                    break;
                case CollectablesEnum.Score2X:
                    ActivateBonus();
                    break;
                case CollectablesEnum.SpeedUp:
                    AddSpeed(collectable.toBeAddedSpeed);
                    break;
                case CollectablesEnum.Magnet:
                    ActivateMagnet();
                    break;

            }
            Destroy(other.gameObject);
        }
    }

    private void AddSpeed(int toBeAddedSpeed)
    {
        beforeSpeed = speed;
        speed += toBeAddedSpeed;

        Invoke("BackToOriginalSpeed" , 5f);
    }

    void BackToOriginalSpeed()
    {
        speed -= beforeSpeed;   
    }

    void AddScore(int toBeAddedScore)
    {
       if(is2xActive)
        {
            toBeAddedScore *= 2;
        }
        score += toBeAddedScore;
    }

    void ActivateShield()
    {
        isShieldActive = true;
        Invoke("DeActivateShield", 5f);
    }

    void DeActivateShield()
    {
        isShieldActive = false;
    }

    void AddHealth(int toBeAddedHealth)
    {
        health += toBeAddedHealth;

        if (health <= 0)
        {
            anim.SetBool("Death", true);
            isDead = true;
        }
    }

    void ActivateBonus()
    {
        is2xActive = true;
        Invoke("DeActivateBonus", 5f);
    }

    void DeActivateBonus()
    {
        is2xActive = false;
    }

    void ActivateMagnet()
    {
        isMagnetActive = true;
        Invoke("DeActivateMagnet", 5f);
    }

    void DeActivateMagnet()
    {
        isMagnetActive = false;
    }
}
