extends AudioStreamPlayer

var deer_splat = preload("res://Audio/harvest_deer.wav")
var harvest_other = preload("res://Audio/harvest_nondeer.wav")
var house_built = preload("res://Audio/build_house.wav")
var forge_built = preload("res://Audio/build_forge.wav")
var statue_built = preload("res://Audio/build_statue.mp3")
var building_destroyed = preload("res://Audio/destroy_building.mp3")
var make_tools = preload("res://Audio/make_tools.wav")
var decay_house = preload("res://Audio/decay_house.mp3")
var win_game = preload("res://Audio/win_game.wav")
var make_bars = preload("res://Audio/make_bars.wav")
var Tiles

func _ready():
	Tiles = get_node("/root/GameManager/Game/Tiles")
	if (Tiles == null):
		request_ready()
		return
	Tiles.connect("harvest_deer", Callable(self, "_on_harvest_deer"))
	Tiles.connect("harvest_other", Callable(self, "_on_harvest_other"))
	Tiles.connect("house_built", Callable(self, "_on_house_built"))
	Tiles.connect("forge_built", Callable(self, "_on_forge_built"))
	Tiles.connect("statue_built", Callable(self, "_on_statue_built"))
	Tiles.connect("building_destroyed", Callable(self, "_on_building_destroyed"))
	Tiles.connect("win", Callable(self, "_on_win"))
	Resources.connect("tools_made", Callable(self, "_on_tools_made"))
	Resources.connect("decay_house", Callable(self, "_on_decay_house"))
	Resources.connect("bars_made", Callable(self, "_on_bars_made"))

func PlaySound(sound: AudioStream):
	var clip = AudioStreamPlayer.new()
	clip.set_stream(sound)
	clip.volume_db = -15
	add_child(clip)
	clip.play()

func _on_bars_made():    
	PlaySound(make_bars)

func _on_harvest_deer():
	PlaySound(deer_splat)

func _on_harvest_other():
	PlaySound(harvest_other)

func _on_house_built():
	PlaySound(house_built)    

func _on_forge_built():    
	PlaySound(forge_built)    

func _on_statue_built():    
	PlaySound(statue_built)    

func _on_building_destroyed():    
	PlaySound(building_destroyed)    

func _on_tools_made():    
	PlaySound(make_tools)    

func _on_decay_house():    
	PlaySound(decay_house)

func _on_win():
	PlaySound(win_game)

func _process(_delta: float) -> void:
	if (Game.IsPaused() == true):
		return
	for child in get_children():
		if child.is_playing() == false:
			child.queue_free()
