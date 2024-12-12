using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;
    private Vector3 moveDirection;
    public float gravityScale = 1;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    private Transform movingPlatform;
    private Vector3 lastPlatformPosition;
    private bool onMovingPlatform; 

    public Animator anim;
    public SoundEffectsPlayer soundEffectsPlayer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (knockBackCounter <= 0)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            moveDirection = new Vector3(
                horizontalInput * moveSpeed,
                moveDirection.y,
                0);

            if (controller.isGrounded && Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
                onMovingPlatform = false; // Reset flag on jump
                soundEffectsPlayer.JumpSound();
            }

            // Rotate the model based on the movement direction
            if (horizontalInput < 0) // Moving left
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (horizontalInput > 0) // Moving right
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;

        // Apply platform movement offset only if still on the platform
        if (onMovingPlatform && controller.isGrounded)
        {
            Vector3 platformDelta = movingPlatform.position - lastPlatformPosition;
            controller.Move(platformDelta);  // Follow the platform's movement
            lastPlatformPosition = movingPlatform.position;
        }

        controller.Move(moveDirection * Time.deltaTime);

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (!controller.isGrounded)
        {
            onMovingPlatform = false; // Ensure the flag is reset when player leaves the ground
        }
    }

    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        direction.y = 1f;
        moveDirection = direction * knockBackForce;
        onMovingPlatform = false; // Reset platform tracking during knockback
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("MovingPlatform"))
        {
            movingPlatform = hit.collider.transform;
            lastPlatformPosition = movingPlatform.position;
            onMovingPlatform = true; // Enable platform tracking when on platform
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            movingPlatform = null;
            onMovingPlatform = false; // Reset flag when leaving platform
        }
    }
}
