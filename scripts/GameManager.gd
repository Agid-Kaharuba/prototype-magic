extends Node2D

var level: PackedScene
var crafting: bool

# Called when the node enters the scene tree for the first time.
func _ready():
    pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#    pass

func open_crafting():
    level = PackedScene.new()
    level.pack(get_tree().current_scene)
    get_tree().change_scene("res://scenes/UI/Crafter.tscn")
    crafting = true
    
func close_crafting():
    if crafting:
        crafting = false
        get_tree().change_scene_to(level)
        level = null
    
