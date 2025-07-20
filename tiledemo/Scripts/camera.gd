extends Camera2D

var speed = 3

func _process(_delta):
    if Input.is_action_pressed("ui_up"):
        position.y -= 1 * speed
    if Input.is_action_pressed("ui_down"):
        position.y += 1 * speed
    if Input.is_action_pressed("ui_left"):
        position.x -= 1 * speed
    if Input.is_action_pressed("ui_right"):
        position.x += 1 * speed