using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    private Animator anim;
    private PlayerStamina stamina;
    private Player player;
    private CircleCollider2D circleCollider2D;

    private void Start()
    {
        player = GetComponent<Player>();
        stamina = GetComponentInChildren<PlayerStamina>();
        anim = GetComponent<Animator>();
        boxCollider2D.enabled = false;
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            circleCollider2D.enabled = false;
            HandleBlock();
        }
        else
        {
            circleCollider2D.enabled = true;
            StopBlock();
        }
    }

    private void HandleBlock()
    {
        stamina.UseStamina(player.characterData.blockStamina * Time.deltaTime);
        anim.SetBool("isBlockedEnemy", false);
        anim.SetBool("isBlocking", true);
        boxCollider2D.enabled = true;
    }


    private void StopBlock()
    {
        boxCollider2D.enabled = false;
        anim.SetBool("isBlocking", false);
    }

    public void BlockedEnemy()
    {
        Debug.Log("Blocked Enemy");
        stamina.UseStamina(player.characterData.blockedStamina * Time.deltaTime);
        anim.SetBool("isBlockedEnemy", true);
        HandleBlock();
    }
}
