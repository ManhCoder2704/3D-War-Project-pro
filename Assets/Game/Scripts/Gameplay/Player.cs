using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _player;
    [SerializeField] private Animator _anim;


    public float speed = 5.0f;
    private bool running = false;
    private float horizontal;
    private float vertical;
    private string currentAnim;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
            if (!running)
            {
                transform.LookAt(transform.position + moveDirection);
            }
            ChangeAnim(Anim.Run);
            _player.AddForce(moveDirection * speed * Time.fixedDeltaTime);
        }
        else
        {
            running = false;
            ChangeAnim(Anim.Idle);
            _player.velocity = Vector3.zero;
        }
        if (Input.GetKey(KeyCode.E))
        {
            ChangeAnim(Anim.Dig);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            ChangeAnim(Anim.Pointing);
        }
    }

    private void ChangeAnim(Anim anim)
    {
        if (currentAnim != anim.ToString())
        {
            _anim.ResetTrigger(anim.ToString());
            currentAnim = anim.ToString();
            _anim.SetTrigger(currentAnim);
        }
    }
}
