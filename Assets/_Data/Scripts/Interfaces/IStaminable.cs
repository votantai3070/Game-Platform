public interface IStaminable
{
    float CurrentStamina { get; set; }
    float MaxStamina { get; }
    void UseStamina(float amount);
    void RecoverStamina(float amount);
    bool HasEnoughStamina(float amount);
}
