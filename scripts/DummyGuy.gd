extends StaticBody2D

var totalDamage: float = 0;

# Called when the node enters the scene tree for the first time.
func _ready():
    $DamageLabel.text = "Health: " + str($HealthController.Health) \
        + "\nDamage: 0" + "\nTotal: " + str(totalDamage)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
    var status = ""
    for child in $EffectController.get_children():
        status += child.effectName + "\n"
    $StatusLabel.text = status
    

func _on_HealthController_OnDamaged(amount: float):
    print("damage taken " + str(amount))
    totalDamage += amount
    $DamageLabel.text = "Health: " + str($HealthController.Health) \
        + "\nDamage: " + str(amount) + "\nTotal: " + str(totalDamage)
