extends Label

func _process(_delta: float) -> void:
	if (Game.IsPaused() == true):
		return
	var amount = Resources.Pool[Enums.TileType[self.name.to_upper()]]
	var displayName = self.name
	if self.name == "Deer":
		displayName = "Meat"
	text = "%s: %d" % [displayName, amount]
