using Godot;

public class ProjectilePhysics : SpellPhysics
{
    private ProjectilePhysicist physicist;
    private Vector2 direction;
    private float delay = 0;
    private int count = 0;

    public ProjectilePhysicist Physicist
    {
        get { return physicist; }
    }

    public ProjectilePhysics(Caster caster, ProjectilePhysicist physicist) : base(caster) 
    {
        this.physicist = physicist;
        this.direction = caster.GetAimVector();
    }

    public override void _PhysicsProcess(float delta)
    {
        if (count < physicist.ProjectileCount)
        {
            if (physicist.Spread > 0)
            {
                SpreadProjectile();
                count = physicist.ProjectileCount;
            }
            else if (delay <= 0)
            {
                CreateProjectile();
                delay = physicist.CastDelay;
                count++;
            }
        }
        delay -= delta;

        if (GetChildCount() == 0)
        {
            GetParent().RemoveChild(this);
        }
    }

    public override bool IsSpellFinished()
    {
        return count >= physicist.ProjectileCount && delay < -physicist.CooldownOnFinish;
    }

    private void SpreadProjectile()
    {
        // TODO make better Spread projectile physics using better formula.

        float radSpace = 0.04f * Mathf.Sqrt(physicist.ProjectileCount);
        float rad = radSpace;
        float count = physicist.ProjectileCount;

        if (physicist.ProjectileCount % 2 != 0)
        {
            CreateProjectile();
            count--;
        }

        for (int i = 0; i < count; i++)
        {
            ProjectileEntity projectile = CreateProjectile();
            projectile.Rotate(rad);
            projectile.Direction = projectile.Direction.Rotated(rad);
            rad += rad > 0 ? radSpace : -radSpace;
            rad *= -1;
        }
    }

    private ProjectileEntity CreateProjectile()
    {
        ProjectileEntity entity = new ProjectileEntity(this, direction);
        AddChild(entity);
        entity.GlobalPosition = caster.Entity.GlobalPosition;
        return entity;
    }
}