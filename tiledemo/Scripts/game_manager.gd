extends Control

var MainMenuScene
var GameScene
var WinScene
var StartButton
var RestartButton
var Tiles

func _ready() -> void:
	MainMenuScene = get_node("MainMenu")
	GameScene = get_node("Game")
	WinScene = get_node("WinScene")
	StartButton = get_node("MainMenu/StartButton")
	RestartButton = get_node("WinScene/RestartButton")
	Tiles = get_node("Game/Tiles")
	StartButton.connect("pressed", Callable(self, "_on_start_pressed"))
	RestartButton.connect("pressed", Callable(self, "_on_restart_pressed"))
	Tiles.connect("win", Callable(self, "_on_win"))

func _on_start_pressed() -> void:
	Tiles.Initialize()
	Resources.Initialize()
	print("Game initialized")
	$Game/Camera.transform.origin = Vector2(0, 0)
	Game.Unpause()
	GameScene.visible = true
	WinScene.visible = false
	MainMenuScene.visible = false
	$Game/Camera.enabled = true
	$MainMenu/Camera.enabled = false

func _on_restart_pressed() -> void:
	GameScene.visible = false
	WinScene.visible = false
	MainMenuScene.visible = true
	$Game/Camera.enabled = false
	$MainMenu/Camera.enabled = true

func _on_win() -> void:
	WinScene.visible = true
	MainMenuScene.visible = false
	$Game/Camera.enabled = false
	$MainMenu/Camera.enabled = true
