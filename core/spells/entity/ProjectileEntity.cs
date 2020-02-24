using Godot;

public class ProjectileEntity : Area2D
{
    private float age;
    private readonly ProjectilePhysics physics;
    private Vector2 direction;
    private CollisionShape2D collisionShape;

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public ProjectileEntity(ProjectilePhysics physics, Vector2 direction)
    {
        this.Name = this.GetType().Name;
        this.physics = physics;
        this.direction = direction;
        Rotate(Mathf.Pi + direction.Angle());
        Connect("body_entered", this, nameof(OnBodyEntered));
        AddCollider();
        AddSprite("res://assets/magic_projectile.png");
    }

    private void AddSprite(string path)
    {
        Sprite sprite = new Sprite();
        sprite.Texture = GD.Load(path) as Texture;
        AddChild(sprite);
    }

    private void AddCollider()
    {
        collisionShape = new CollisionShape2D();
        CircleShape2D circleShape = new CircleShape2D();
        circleShape.Radius = physics.Physicist.Size;
        collisionShape.Shape = circleShape;
        AddChild(collisionShape);
    }

    public override void _Process(float delta)
    {
        Vector2 velocity = direction * physics.Physicist.Speed;
        Position += velocity * delta;
    }

    public override void _PhysicsProcess(float delta)
    {
        age += delta;

        if (age >= 15)
        {
            QueueFree();
        }
    }

    public void OnBodyEntered(Node2D node)
    {
        GD.Print($"Node in contact with {node.Name}!");
        HitContext hitContext = new HitContext(node, direction);
        physics.Physicist.HitActions.ForEach(action => action(hitContext));
        hitContext.ApplyHitActions();
        QueueFree();
    }
}