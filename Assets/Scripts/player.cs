using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private FrameAnimationController animController;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        animController = GetComponent<FrameAnimationController>();
    }

    void Update()
    {
        var moveAnim = Move();
        var attackAnim = Attack();
        var animName = FrameAnimationController.CombineMoveAttackAnim(moveAnim, attackAnim);
        animController.Change(animName);
        animController.Play();
    }

    string Move()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = moveSpeed * playerInput.normalized;

        if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (playerInput.x != 0 || playerInput.y != 0)
        {
            return "move";
        }
        else
        {
            return "";
        }
    }

    string Attack()
    {
        var isPressed = Input.GetMouseButtonDown(1); // 0 = chuột phải | 1 = chuột trái

        if (isPressed)
        {
            // Logic attack
        }

        if (isPressed)
        {
            return "attack";
        }
        else
        {
            return "";
        }
    }
}
