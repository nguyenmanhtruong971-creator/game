using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

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

        // Trái phải + đi xéo = 1 bộ
        // Lên = 1 bộ
        // Xuống = 1 bộ

        if (playerInput.x != 0) // Trái phải + đi xéo
        {
            return "move-1";
        }
        else if (playerInput.y > 0)
        {
            return "move-2";
        }
        else if (playerInput.y < 0)
        {
            return "move-3";
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
