using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 GetHorizontalMovementInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), 0);
    }
}
