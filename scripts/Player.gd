extends KinematicBody2D

const speed = 1000;

# Called when the node enters the scene tree for the first time.
func _ready():
    pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
    var velocity := Vector2()
    
    if Input.is_action_pressed("move_right"):
        velocity.x += 1
    elif Input.is_action_pressed("move_left"):
        velocity.x -= 1
    
    if Input.is_action_pressed("move_up"):
        velocity.y -= 1
    elif Input.is_action_pressed("move_down"):
        velocity.y += 1
        
    if Input.is_action_just_pressed("action_craft"):
        var manager = $"/root/GameManager"
        manager.open_crafting()
        
    move_and_collide(velocity * speed * delta)
