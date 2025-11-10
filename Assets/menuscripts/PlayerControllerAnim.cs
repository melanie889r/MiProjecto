using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerControllerAnim : MonoBehaviour
{
    private Animator anim;
    private CharacterController cc;

    // Movement settings
    private float walkSpeed = 2.0f;
    private float runSpeed = 5.0f;
    private float gravity = -9.81f;
    private float jumpHeight = 5f;

    private Vector3 velocity;

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();

        if (anim == null)
            Debug.LogError("Animator not found on player!");
        if (cc == null)
            Debug.LogError("CharacterController not found on player!");
    }

    void Update()
    {
        // --- INPUT ---
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Get camera direction for movement
        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * v + right * h).normalized;
        float inputMagnitude = Mathf.Clamp01(new Vector2(h, v).magnitude);

        // --- ROTATION ---
        if (move.magnitude > 0.01f)
        {
            transform.forward = Vector3.Slerp(transform.forward, move, Time.deltaTime * 10f);
        }

        // --- WALK / RUN ---
        bool running = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = running ? runSpeed : walkSpeed;

        cc.Move(move * currentSpeed * inputMagnitude * Time.deltaTime);

        // --- GRAVITY + JUMP ---
        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keeps grounded
        }

        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Proper jump force
            anim.SetTrigger("Jump");
        }

        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);

        // --- ANIMATOR PARAMETERS ---
        anim.SetFloat("Velocity", inputMagnitude);
        anim.SetBool("Running", running);
    }

    // Optional: Animation event
    public void Jumping()
    {
        Debug.Log("Jumping animation event triggered.");
    }
}
