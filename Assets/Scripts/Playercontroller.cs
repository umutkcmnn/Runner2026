using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] public Animator anim;
    [SerializeField] public float speed;
    [SerializeField] public float shift=2;
    [HideInInspector] public bool isDead;
    [HideInInspector]public bool isStart;
    [SerializeField] public int score;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;
        if (isDead) return;
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
            anim.SetBool("Death", true);
            isDead = true;  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin")) 
        {
            score += 10;
            Destroy(other.gameObject);
        }
    }



}
