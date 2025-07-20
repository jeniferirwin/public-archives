extends Node

var Paused = false

func Pause():
	Paused = true

func Unpause():
	Paused = false

func IsPaused():
	return Paused
