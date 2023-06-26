using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Character_Move : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator anim;
    [SerializeField]
    private LayerMask is_grounded;

    [SerializeField]
    private GameObject oneway, lamps1, lamps2, lamps3;
    [SerializeField]
    private OneWayController owController;

    [SerializeField]
    private AudioSource JumpPJ; // Coje el sonido que va a ser el Salto

    [SerializeField]
    private float speed, maxspeed, jump_for, value, sense_att;
    private bool jump, touch_ice, reflected, alive, jumpwall;
    public bool grounded;
    private int countjw;

    void Awake()
    {
        reflected = false;
        alive = true;
        jumpwall = false;
        speed = 10f;
        maxspeed = 2f;
        jump_for = 3f;
        countjw = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Jump", rb.velocity.y);
        Debug.DrawRay(this.transform.position + new Vector3(0, -0.12f, 0), Vector2.right * 0.1f * -sense_att, Color.cyan);
        //Debug.DrawRay(this.transform.position + (new Vector3(0.06f, 0, 0) * sense_att), Vector2.down * 0.15f, Color.blue);
        //Debug.DrawRay(this.transform.position + (new Vector3(-0.06f, 0, 0) * sense_att), Vector2.down * 0.15f, Color.red);


        if (IsGrounded())
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if (IsCorner())
        {
            Debug.Log("Esquina");
            this.transform.position += new Vector3(0.01f * -sense_att, 0.01f, 0);
        }

        if (grounded == true)
        {
            countjw = 0;
            if (Input.GetKeyDown(KeyCode.W) && alive) // Este funciona solamente cuando el personaje se encuentra en el suelo
            {
                jump = true;
                JumpPJ.Play(); // Cuando salta suena
            }
        }
        if (grounded == false)
        {
            if (Input.GetKeyDown(KeyCode.W) && alive && IsWall() && !jump) // Salto contra la pared
            {
                jumpwall = true;
                countjw++;
            }
        }

        if (grounded == true && gameObject.layer != 9) //Lo que permite el cambio entre un one way platform y que vuelva a la plataformas normales
        {
            oneway.layer = 10;
            lamps1.layer = 10;
            lamps2.layer = 10;
            lamps3.layer = 10;
            gameObject.layer = 9;
        }
        if (grounded == true && gameObject.layer == 9 && owController.oneway && Input.GetKey(KeyCode.S))
        {
            oneway.layer = 0;
            lamps1.layer = 0;
            lamps2.layer = 0;
            lamps3.layer = 0;
            gameObject.layer = 8;
        }

    }

    private void FixedUpdate()
    {
        Walk();
        Jump();
    }

    private void Walk() // Funcion sobre la caminata del personaje
    {
        if (Input.GetKey(KeyCode.D) && alive)
        {
            sense_att = -1f; // Este valor es para darle un sentido cuando el personaje esta atacando, este se llamara en el codigo Active_Attack para mover al personaje al atacar
            rb.AddForce(Vector2.right * speed * 1f); // Se le agrega una fuerza para mover al personaje, dependiendo de la velocidad(speed) y el sentido en el que este
            //dirRig = true;
            if (reflected) //Este condicional es para voltear al personaje en el sentido que esta moviendose
            {
                transform.localScale = new Vector3(0.6f, 0.6f, 0);
                reflected = false;
            }

        }

        if (Input.GetKey(KeyCode.A) && alive) // Todo lo mismo de arriba pero con el sentido contrario
        {
            sense_att = 1f;
            rb.AddForce(Vector2.right * speed * -1f);
            //dirLef = true;
            if (!reflected)
            {
                transform.localScale = new Vector3(-0.6f, 0.6f, 0);
                reflected = true;
            }
        }

        float limitespeed = Mathf.Clamp(rb.velocity.x, -maxspeed, maxspeed); //Se limita la velocidad para que el personaje no llegue a una velocidad inmensa sin sentido
        rb.velocity = new Vector2(limitespeed, rb.velocity.y);
    }

    private void Jump() //Funcion del salto
    {
        if (jump == true)
        {
            rb.AddForce(Vector2.up * jump_for, ForceMode2D.Impulse); // Al saltar se la agrega una fuerza en el vector.up (0,1) que seria el vector Y, el force.impulse es una fuerza instantanea que se llama solo 1 vez
            jump = false; // Este condicional es el que limita el salto del personaje
        }

        if (jumpwall)
        {
            if (countjw <= 5)
            {
                rb.AddForce(Vector2.up * 4f, ForceMode2D.Impulse); // Aca se le agrega otro impulso en el salto de la pared para que el personaje suba, le puse 4f por que era lo ideal al probarlo
                JumpPJ.Play(); // Cuando salta suena, cambie este aca para corregir el error de que cuando saltaba y llegaba al limite, si seguia oprimiendo w seguia sonando(Miguel)
                float limitejump = Mathf.Clamp(rb.velocity.y, -0f, 3f); // se limita el salto
                rb.velocity = new Vector2(0, limitejump);
            }
            jumpwall = false;
        }
    }

    private bool IsGrounded()
    {
        bool ground_m, ground_d, ground_a;
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.15f, is_grounded))
        {
            ground_m = true;
        }
        else
        {
            ground_m = false;
        }
        if (Physics2D.Raycast(this.transform.position + (new Vector3(-0.06f, 0, 0) * sense_att), Vector2.down, 0.15f, is_grounded))
        {
            ground_d = true;
        }
        else
        {
            ground_d = false;
        }
        if (Physics2D.Raycast(this.transform.position + (new Vector3(0.06f, 0, 0) * sense_att), Vector2.down, 0.15f, is_grounded))
        {
            ground_a = true;
        }
        else
        {
            ground_a = false;
        }
        if (ground_d)
        {
            if (!ground_m)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        else
        if (ground_a)
        {
            if (!ground_m)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
    /*private bool IsGrounded()
    {
        if (Physics2D.Raycast(this.transform.position + (new Vector3(0.05f, 0, 0) * sense_att), Vector2.down, 0.15f, is_grounded))
        {
            return true;
        }
        else
        {
            return false;
        }
    }*/
    private bool IsWall()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.right * (-sense_att), 0.1f, is_grounded))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsCorner()
    {
        if (Physics2D.Raycast(this.transform.position + new Vector3(0,-0.1f,0), Vector2.right * (-sense_att), 0.1f, is_grounded))
        {
            if(!IsGrounded())
            {
                if(!IsWall())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }



    public void Dead(bool isdied) // Funcion que hace que el personaje muera y no se pueda mover
    {
        if (isdied)
        {
            speed = 0f;
            jump_for = 0f;
            alive = false;
        }
        else
        {
            alive = true;
            speed = 10f;
            jump_for = 3f;
        }
    }

    public void MoveAttack() // Funcion que permite el pequeño movimiento al atacar a un enemigo
    {
        rb.AddForce(Vector2.right * 50f * sense_att);

    }
    public void MoveAttackW() // Funcion que permite el pequeño movimiento al atacar a un enemigo
    {
        rb.AddForce(Vector2.right * 100f * sense_att);

    }
    public bool Vive()
    {
        return alive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (grounded)
            {
                rb.AddForce(Vector2.right * 50f * sense_att);
            }
            if (!grounded)
            {
                rb.AddForce(Vector2.right * 50f * sense_att);
                rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
                float limitejump = Mathf.Clamp(rb.velocity.y, -0f, 2f); // se limita el salto
                rb.velocity = new Vector2(rb.velocity.x, limitejump);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnBajada"))
        {
            speed = 0f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnBajada"))
        {
            speed = 10f;
        }
    }


}
