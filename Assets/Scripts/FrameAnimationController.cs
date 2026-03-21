using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class FrameAnimationController : MonoBehaviour
{
    [SerializeField] private float animSpeed = 0.2f;

    private SpriteResolver spriteResolver;
    
    private string currentAnimName;
    private float animTimeCounter;
    private int frameCountForCurrentAnim;
    private int frameIndex;

    private void Awake()
    {
        spriteResolver = GetComponent<SpriteResolver>();
    }

    public static string CombineMoveAttackAnim(string moveAnim, string attackAnim)
    {
        var moveIsEmpty = string.IsNullOrEmpty(moveAnim);
        var attackIsEmpty = string.IsNullOrEmpty(attackAnim);

        if (moveIsEmpty && attackIsEmpty)
        {
            return "";
        }

        if (moveIsEmpty)
        {
            return attackAnim;
        }

        if (attackIsEmpty)
        {
            return moveAnim;
        }

        return $"{moveAnim}-{attackAnim}";
    }

    public void Change(string animName)
    {
        // Chuẩn hoá (normalize) animName thành "idle" nếu giá trị truyền vào là rỗng
        if (string.IsNullOrEmpty(animName))
        {
            animName = "idle";
        }
        
        // Vì hàm ChangeAnimation được gọi trong Update
        // Nên sẽ chạy liên tục 60fps (là của game)
        if (string.Equals(animName, currentAnimName))
        {
            return;
        }

        // Khi animName không bằng currentAnimName
        // thì gán animName vào currentAnimName
        // sau đó đổi animation thành animName
        // và reset hết các biến liên quan
        currentAnimName = animName;
        spriteResolver.SetCategoryAndLabel(animName, "1");
        frameCountForCurrentAnim = spriteResolver.spriteLibrary.spriteLibraryAsset.GetCategoryLabelNames(animName).Count();
        frameIndex = 0;
        animTimeCounter = 0f;
    }

    public void Play()
    {
        // Logic chạy animation theo frame
        // 1. Tốc độ 1 frame => 200ms - chung cho tất cả anim => 5FPS
        // 2. Số lượng frame của 1 anim
        // 3. Tên anim mà mình chạy
        //
        // Câu hỏi:
        // 1. Tên anim lấy ở đâu ra?
        // 2. Số lượng frame của anim lấy ở đâu ra?

        // Câu hỏi 1: Tên anim lấy ở đâu ra?
        // Code logic xác định khi nào thì đổi sang 1 anim cụ thể
        //    a. Cho player: xác định bằng input - mỗi input tương ứng 1 anim cụ thể
        //    b. Cho enemy: xác định bằng AI => code

        // Câu hỏi 2: Số lượng frame của anim lấy ở đâu ra?
        

        // Câu hỏi 3: Điều kiện thay đổi anim của player?
        // 1. Khi nào thì idle?
        // 2. Khi nào thì run?
        // ...

        // Triển khai logic chạy animation:
        // 1. Chờ 1 khoảng thời gian theo giây => 200ms = 0.2s
        // Time.deltaTime = khoảng thời gian giữa mỗi game frame, hay mỗi lần hàm Update được gọi
        // Time.deltaTime = 1s/60fps = 0.016s
        // Tại sao có khoảng thời gian chờ? Vì game frame chạy rất nhanh, còn anim frame chạy chậm hơn
        // nên anim phải chờ đủ 0.2s thì mới đổi frame
        
        // Tăng tiến animTimeCounter cho đến khi bằng 0.2s
        animTimeCounter += Time.deltaTime;

        // Nếu animTimeCounter chưa chạm ngưỡng 0.2s thì thoát ra khỏi hàm này
        if (animTimeCounter < animSpeed)
        {
            return;
        }

        // Nếu animTimeCounter chạm ngưỡng 0.2s thì reset counter về 0
        animTimeCounter = 0f;

        // 2. Đủ thời gian chờ thì tăng frame lên 1
        frameIndex += 1;

        // 3. Kiểm tra frame vừa tăng có vượt quá số lượng frame tối đa của anim đang chạy hay không?
        //    a. Nếu vượt quá: làm cho frame quay lại 1
        //    b. Nếu không: không làm gì đặc biệt
        if (frameIndex >= frameCountForCurrentAnim) // Tại sao >= ?
                                                    // Vì "index" có nghĩa là giá trị đi từ 0 đến N-1
        {
            frameIndex = 0;
        }

        // 4. Gán frame vừa tăng vào spriteResolver
        var frameLabel = (frameIndex + 1).ToString();
        spriteResolver.SetCategoryAndLabel(currentAnimName, frameLabel);
    }
}
