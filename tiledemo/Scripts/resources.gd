extends Node

signal resource_changed
signal decay_house
signal tools_made
signal bars_made

@onready var Pool = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
@onready var Ticker

func _ready() -> void:
	Ticker = get_node("/root/GameManager/Game/Timer")
	if (Ticker == null):
		request_ready()
		return
	Ticker.connect("timeout", Callable(self, "_on_timer_timeout"))

func Initialize() -> void:
	Pool = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]

func change_resource(type: Enums.TileType, amount: int) -> void:
	Pool[type] += amount
	emit_signal("resource_changed", amount)

func _on_timer_timeout() -> void:
	if (Game.IsPaused() == true):
		return
	if Pool[Enums.TileType.HOUSE] > 0:
		Resources.change_resource(Enums.TileType.DEER, -Pool[Enums.TileType.HOUSE])
		if Pool[Enums.TileType.DEER] <= 0:
			Pool[Enums.TileType.DEER] = 0
			emit_signal("decay_house")
	if Pool[Enums.TileType.SMELTER] > 0 and Pool[Enums.TileType.IRON] > 0 and Pool[Enums.TileType.HOUSE] > 0 and Pool[Enums.TileType.WOOD] > 0:
		var amount = min(Pool[Enums.TileType.IRON], Pool[Enums.TileType.SMELTER], Pool[Enums.TileType.HOUSE], Pool[Enums.TileType.WOOD])
		Resources.change_resource(Enums.TileType.IRON, -amount)
		Resources.change_resource(Enums.TileType.BARS, amount)
		emit_signal("bars_made")

			
func create_tools() -> void:
	if Pool[Enums.TileType.STONE] > 0 and Pool[Enums.TileType.HOUSE] > 0:
		var amount = min(Pool[Enums.TileType.STONE], Pool[Enums.TileType.HOUSE])
		if amount < 1:
			return
		Resources.change_resource(Enums.TileType.STONE, -amount)
		Resources.change_resource(Enums.TileType.TOOLS, amount)
		emit_signal("tools_made")

func _process(_delta: float) -> void:
	if (Game.IsPaused() == true):
		return
	if Input.is_action_just_pressed("create_tools"):
		create_tools()
	if Input.is_action_just_pressed("debug_give_resources"):
		Resources.change_resource(Enums.TileType.DEER, 1000)
		Resources.change_resource(Enums.TileType.WOOD, 1000)
		Resources.change_resource(Enums.TileType.STONE, 1000)
		Resources.change_resource(Enums.TileType.IRON, 1000)
		Resources.change_resource(Enums.TileType.BARS, 1000)
		Resources.change_resource(Enums.TileType.TOOLS, 1000)
