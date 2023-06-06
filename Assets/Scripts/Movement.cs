using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Movement : MonoBehaviour
    {
        public LayerMask whatIsGround;
        public Transform groundCheck;
        public bool isGrounded;
        public float jumpForce;
        public float speed;
        Rigidbody2D rb;
        [SerializeField] bool stop;
        public bool Stop
        {
            get { return stop; }
            set { stop = value; }
        }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (!Stop)
            {
                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                    isGrounded = false;
                }
            }
        }

        void FixedUpdate()
        {
            if (!Stop)
            {
                isGrounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);

                float x = Input.GetAxis("Horizontal");
                Vector3 move = new Vector3(x * speed, rb.velocity.y, 0f);
                rb.velocity = move;
            }
            
        }
    }
}
