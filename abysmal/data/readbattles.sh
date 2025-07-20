#!/bin/bash

# Strips tintin color codes out of the battles.txt file.

cat battles.txt | sed 's/<[0-9][0-9][0-9]>//g'
