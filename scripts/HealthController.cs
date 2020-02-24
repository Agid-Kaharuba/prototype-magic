using Godot;

public class HealthController : Node2D
{
    [Export]
    private int maxHealth = 500;
    [Export]
    private bool ignoreDamage = false;
    private float health;
    [Signal]
    public delegate void OnDamaged(int amount);
    [Signal]
    public delegate void OnKilled();
    [Signal]
    public delegate void OnHealed(int amount);

    public float Health
    {
        get { return health; }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public override void _Ready()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (!ignoreDamage)
        {
            health -= amount;

            if (health <= 0)
            {
                EmitSignal(nameof(OnKilled));
            }
        }
        EmitSignal(nameof(OnDamaged), amount);
    }

    public void TakeHeal(float amount)
    {
        health += amount;
        EmitSignal(nameof(OnHealed), amount);
    }
}