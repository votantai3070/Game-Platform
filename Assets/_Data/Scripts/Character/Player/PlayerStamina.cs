using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : Player, IStaminable
{

    public static PlayerStamina Instance;
    public Slider staminaBar;
    public float staminaRecoveryRate = 10f;
    public float staminaDelay = 1f;
    private float _staminaDelayTimer;

    public float CurrentStamina { get; set; }
    public float MaxStamina { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        MaxStamina = characterData.stamina;
        CurrentStamina = MaxStamina;
        if (staminaBar != null)
        {
            staminaBar.maxValue = MaxStamina;
            staminaBar.value = CurrentStamina;
        }
    }

    private void Update()
    {
        RecoverStamina(staminaRecoveryRate * Time.deltaTime);
    }

    public void UseStamina(float amount)
    {
        if (HasEnoughStamina(amount))
        {
            CurrentStamina -= amount;
            _staminaDelayTimer = staminaDelay;
            if (staminaBar != null)
            {
                staminaBar.value = CurrentStamina;
            }
        }

    }

    public void RecoverStamina(float amount)
    {
        if (_staminaDelayTimer > 0)
        {
            _staminaDelayTimer -= Time.deltaTime;
            return;
        }
        CurrentStamina = Mathf.Min(CurrentStamina + amount, MaxStamina);
        if (staminaBar != null)
        {
            staminaBar.value = CurrentStamina;
        }
    }

    public bool HasEnoughStamina(float amount)
    {
        return CurrentStamina >= amount;
    }
}
