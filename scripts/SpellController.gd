extends Node2D

var spell_index: int = 0;

# Called when the node enters the scene tree for the first time.
func _ready():
    pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
    if Input.is_key_pressed(KEY_1):
        spell_index = 0
    elif Input.is_key_pressed(KEY_2):
        spell_index = 1
    elif Input.is_key_pressed(KEY_3):
        spell_index = 2
    elif Input.is_key_pressed(KEY_4):
        spell_index = 3
        
    if Input.is_action_pressed("action_cast_spell"):
        _cast_spell()


func _cast_spell():
#    print("Casting spell at " + spell_index as String)
    $"/root/SpellInventory".call("CastSpellAtSlot", spell_index, get_parent())
