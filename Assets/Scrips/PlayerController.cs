using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _renderer;
    private Animator _animator;
  
    public float velocity = 10;
        
    private static readonly int right = 1;
    private static readonly int left = -1;
        
         
    private static readonly int Animation_idle = 0;
    private static readonly int Animation_run = 1;

    private Puntajes Scors;

    void Start()
    { 
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        Scors = FindObjectOfType<Puntajes>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        ChangeAnimation(Animation_idle);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Desplazarse(right);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Desplazarse(left);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10);
        }
    }
    private void Desplazarse(int position)
    {
        _rigidbody2D.velocity = new Vector2(velocity * position, _rigidbody2D.velocity.y);
        _renderer.flipX = position == left;
        ChangeAnimation(Animation_run);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tag = other.gameObject.tag;
        if (tag == "Pinchos")
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (tag == "Pinchos2")
        {
            SceneManager.LoadScene("SegundEcena");
        }
        if (tag == "Nivel2")
        {
            SceneManager.LoadScene("SegundEcena");
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        var tag = collision.gameObject.tag;
        Debug.Log(tag);
        if (tag == "Escalable" && Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemigo"))
        {
            Scors.MenosVida(1);
        }
        if (other.gameObject.CompareTag("Moneda"))
        {
            Scors.SumMonedas(1);
        }
    }


    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger("Estado",animation);
    }
}
