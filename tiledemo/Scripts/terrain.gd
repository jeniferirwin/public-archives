extends TileMapLayer

var trees = 0
var deer = 0
var stone = 0
var iron = 0
var houses = 0
var smelters = 0
var statues = 0
var bars = 0
const WORLD_SIZE = 256
const TileType = Enums.TileType

signal win
signal harvest_deer
signal harvest_other
signal house_built
signal forge_built
signal statue_built
signal building_destroyed

func _ready():
	Resources.connect("decay_house", Callable(self, "_on_decay_house"))
	Initialize()

func Initialize():
	flatten()
	populate()
	update_internals()

func _on_decay_house():
	var foundHouses = find_houses()
	if foundHouses.size() == 0:
		return
	foundHouses.shuffle()
	var house = foundHouses[0]
	self.set_cell(Vector2i(house.x, house.y), 1, Vector2i(TileType.NONE, 0), 0)
	Resources.change_resource(TileType.HOUSE, -1)
	if Resources.Pool[TileType.HOUSE] < 0:
		Resources.Pool[TileType.HOUSE] = 0
	update_internals()

func find_houses():
	var houseArray = []
	for x in range(-WORLD_SIZE, WORLD_SIZE):
		for y in range(-WORLD_SIZE, WORLD_SIZE):
			if (self.get_cell_atlas_coords(Vector2i(x, y)) == Vector2i(TileType.HOUSE, 0)):
				houseArray.append(Vector2i(x, y))
	return houseArray

func flatten():
	for x in range(-WORLD_SIZE, WORLD_SIZE):
		for y in range(-WORLD_SIZE, WORLD_SIZE):
			self.set_cell(Vector2(x, y), 1, Vector2(0, 0), 0)

func populate():
	var rng = RandomNumberGenerator.new()
	rng.randomize()
	for i in range(0, 5000):
		var x = rng.randi_range(-WORLD_SIZE, WORLD_SIZE)
		var y = rng.randi_range(-WORLD_SIZE, WORLD_SIZE)
		if (self.get_cell_atlas_coords(Vector2i(x, y)) != Vector2i(0, 0)):
			continue
		var resource = rng.randi_range(0, 4)
		var chance = rng.randi_range(0, 100)
		if (chance < ((1.0/resource+1.0)*100)):
			self.set_cell(Vector2i(x, y), 1, Vector2i(resource, 0), 0)

func harvest(tile: Vector2i):
	var resType = self.get_cell_atlas_coords(tile).x
	var harvestPower = max(1, min(Resources.Pool[TileType.TOOLS], Resources.Pool[TileType.HOUSE] * 3))
	if resType == TileType.NONE or resType == TileType.HOUSE or resType == TileType.SMELTER or resType == TileType.STATUE:
		return
	if resType == TileType.DEER:
		Resources.change_resource(TileType.DEER, max(10, floor(5 * harvestPower)))
	elif resType == TileType.WOOD:
		Resources.change_resource(TileType.WOOD, max(1, floor(0.5 * harvestPower)))
	elif resType == TileType.STONE:
		Resources.change_resource(TileType.STONE, max(1, floor(0.3 * harvestPower)))
	elif resType == TileType.IRON:
		Resources.change_resource(TileType.IRON, max(1, floor(0.1 * harvestPower)))
	self.set_cell(Vector2i(tile.x, tile.y), 1, Vector2i(0, 0), 0)
	if resType == TileType.DEER:
		emit_signal("harvest_deer")
	else:
		emit_signal("harvest_other")

func destroy(tile: Vector2i):
	var resType = self.get_cell_atlas_coords(tile).x
	if resType < TileType.HOUSE:
		return
	if resType == TileType.HOUSE:
		Resources.change_resource(TileType.HOUSE, -1)
		Resources.change_resource(TileType.WOOD, 2)
	elif resType == TileType.SMELTER:
		Resources.change_resource(TileType.SMELTER, -1)
		Resources.change_resource(TileType.STONE, 2)
	elif resType == TileType.STATUE:
		Resources.change_resource(TileType.STATUE, -1)
		Resources.change_resource(TileType.IRON, 2)
	self.set_cell(Vector2i(tile.x, tile.y), 1, Vector2i(0, 0), 0)
	emit_signal("building_destroyed")

func build_house(tile: Vector2i):
	if self.get_cell_atlas_coords(tile).x != TileType.NONE:
		return
	if Resources.Pool[TileType.WOOD] >= 5:
		Resources.change_resource(TileType.WOOD, -5)
		Resources.change_resource(TileType.HOUSE, 1)
		self.set_cell(Vector2i(tile.x, tile.y), 1, Vector2i(TileType.HOUSE, 0), 0)
		emit_signal("house_built")

func build_smelter(tile: Vector2i):
	if self.get_cell_atlas_coords(tile).x != TileType.NONE:
		return
	if Resources.Pool[TileType.STONE] >= 10:
		Resources.change_resource(TileType.STONE, -10)
		Resources.change_resource(TileType.SMELTER, 1)
		self.set_cell(Vector2i(tile.x, tile.y), 1, Vector2i(TileType.SMELTER, 0), 0)
		emit_signal("forge_built")

func build_statue(tile: Vector2i):
	if self.get_cell_atlas_coords(tile).x != TileType.NONE:
		return
	if Resources.Pool[TileType.BARS] >= 20:
		Resources.change_resource(TileType.BARS, -20)
		Resources.change_resource(TileType.STATUE, 1)
		self.set_cell(Vector2i(tile.x, tile.y), 1, Vector2i(TileType.STATUE, 0), 0)
		emit_signal("statue_built")
	if Resources.Pool[TileType.STATUE] >= 10:
		Game.Pause()
		emit_signal("win")

	
func _process(_delta: float):
	if (Game.IsPaused() == true):
		return
	if Input.is_action_just_pressed("debug_win"):
		Game.Pause()
		emit_signal("win")
	if Input.is_action_pressed("harvest"):
		var mouse_pos = get_local_mouse_position()
		var tile = self.local_to_map(mouse_pos)
		harvest(tile)
	if Input.is_mouse_button_pressed(MOUSE_BUTTON_RIGHT):
		var mouse_pos = get_local_mouse_position()
		var tile = self.local_to_map(mouse_pos)
		destroy(tile)
	if Input.is_action_just_pressed("build_house"):
		var mouse_pos = get_local_mouse_position()
		var tile = self.local_to_map(mouse_pos)
		build_house(tile)
	if Input.is_action_just_pressed("build_forge"):
		var mouse_pos = get_local_mouse_position()
		var tile = self.local_to_map(mouse_pos)
		build_smelter(tile)
	if Input.is_action_just_pressed("build_statue"):
		var mouse_pos = get_local_mouse_position()
		var tile = self.local_to_map(mouse_pos)
		build_statue(tile)
