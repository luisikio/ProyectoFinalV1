using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
     private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _renderer;
        private Animator _animator;
       
        
        public float JumpForce = 10;
        public float velocity = 10;
        
        private static readonly int right = 1;
        private static readonly int left = -1;
        
          
        private static readonly int Animation_idle = 0;
        private static readonly int Animation_run = 1;
        private static readonly int Animation_jump = 2;
        private static readonly int Animation_ataque = 3;
        private static readonly int Animation_escalar = 4;
        private static readonly int Animation_dead = 5;
       
        
    void Start()
    { _rigidbody2D = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        ChangeAnimation(Animation_idle);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ChangeAnimation(Animation_run);
            Desplazarse(right);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ChangeAnimation(Animation_run);
            Desplazarse(left);
        }
        if (Input.GetKey(KeyCode.C))
        {
            ChangeAnimation(Animation_ataque);
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            _rigidbody2D.AddForce(Vector2.up*JumpForce,ForceMode2D.Impulse);
            ChangeAnimation(Animation_jump);
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
        if (tag == "MonedaOro")
        {
            
            Destroy(other.gameObject);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        var tag = other.gameObject.tag;
        Debug.Log(tag);
        if (tag == "Escalable" && Input.GetKey(KeyCode.UpArrow))
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10);
        }
    }


    private void ChangeAnimation(int animation)
    {
        _animator.SetInteger("Estado",animation);
    }
}
