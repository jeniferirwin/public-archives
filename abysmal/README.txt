TeviTinTin++

This system is made up of a handful of rather strongly coupled scripts.
It's not really a system that you can just 'drop in' to your existing
tintin++ setup - I recommend treating this as a base system and introducing
your own scripts into it. This particular setup is not really meant for imm
characters, because the prompt shows way more information than an imm needs.
The current setup was made with mortal test characters in mind.

Note that you can make a brand new character with these scripts active, but
you have to #config {repeat enter} {off} to get past the first few screens.

=== STARTUP PROCESS ===

== SHELL SCRIPT ==

The startup process is launched by a shell script that sets some environment
variables: character name, password, host and port. Modify the 'example' file
to your liking. I keep these in my /home/user/bin folder, one for each
character that I play. With the files in my bin folder, all I have to do is
type the name of that character on the command line to jump right in.

== BOOTSTRAP.TT ==

tt++ is invoked with bootstrap.tt as the first file it reads. bootstrap.tt is
responsible for setting up logging and then actually logging in. bootstrap.tt
then reads the contents of core.tt.

== CORE.TT ==

core.tt is responsible for loading additional .tt files (I call them 'modules'
or 'mods'). It also creates variables that hold a list of all modules. This is
also where the very important 'reload' and 'killall' aliases are found.

When making changes to scripts, you can 'reload' to quickly apply them. If
you have a problem or just want to flush the entire system for whatever reason,
type 'killall'. You can then 'reload' again if you wish.

Note that 'killall' will only flush things that have been properly loaded in as
modules. If you REALLY want to kill EVERYTHING, use #kill - but this will make
it so that you can't use 'reload' until you either #read core.tt or restart
tintin.

== MODULES FOLDER AND THE RELOAD ALIAS ==

There are two folders within the modules folder, 'reload' and 'optional.' The
'reload' folder contains all of the modules that will automatically be read
in when tt++ starts or when the 'reload' #alias is used.

In order for a module to be able to be cleanly unloaded and reloaded, it must
have two things: An entry within #var mods[filename], and wrappers around all
of its actions, aliases and functions.

Check the various aliases and such for examples of these wrappers. The idea
is that every file stores all of its variables within its own class. These
classes can be automagically killed by the 'reload' alias inside of core.tt.
As long as all variables in a file are wrapped within the appropriate class
open/close commands, it should be possible to use the 'reload' alias to
perform a completely clean 'soft reboot' of tintin and all the modules without
having to disconnect from the MUD. (Think of 'reload' as a copyover.)

I know that the 'mods[filenames][script]' stuff is a little odd. I'm going
to investigate the possibility of making this bit less confusing.

== THE OPTIONAL FOLDER ==

Scripts in this folder aren't as strictly managed as those in the reload
folder, and they're mainly for quick little side bits of code that don't
really do a lot of complicated stuff. I don't yet have a way to gracefully
load and unload these, so you'll need to simply use the #read command, or
modify a file to work with the 'reload' folder if you like.

== THE LOGS FOLDER ==

Raw ANSI logs are automatically placed in here. The logs can be read in
color with less -R

== THE MODULES ==

Here is a brief description of each module that makes up the base system.
Some of these files will have more information in #nop statements inside
of them.

= basics.tt =

Contains a variety of quality-of-life things such as healing aliases,
bag manipulation stuff, rewielding and some helper functions.

= config.tt =

Contains #config commands that you can modify to your liking.

= daminfo.tt =

Bakes damage number information into lines where damage is dealt, and also
puts percentages on 'big nasty wounds and scratches' etc. lines.

= data.tt =

Battle data collection module. This behemoth was hard to write and is even
harder to understand. I may do a major rewrite of this in the future, but
for now, this is where you're going to find the stuff that counts the number
of misses, dodges, parries, hits, etc.

After each battle, the information is printed in a nice little card that was
extremely hard to format.

= mower.tt =

There's no other way to put it: This is semi-automatic gold botting! It was
designed for use in the Underdark, but can work anywhere. See the file for
details.

= prompt.tt =

This is probably the single most important file in the whole folder. The
prompt serves as an 'update loop' for functions that aren't dependent on
a ticker or delay timer. statdiff.tt and tracker.tt depend on prompt.tt.

In order for the prompt to actually work, you have to set the appropriate
prompt in-game first. Fortunately, the 'setprompt' alias will do it for you!

This is one of my proudest accomplishments and it is the most beautiful
prompt ever made.

= spamprotection.tt =

Sends a string of randomized characters to the MUD to interrupt spam, so
that the MUD doesn't kick you. This is not always desirable; if you happen
to write a script that malfunctions and goes into an infinite loop of
spam, the MUD probably won't kick you to stop it. Type killall (or, more
severely, #kill) to quickly stop any haywire scripts.

= statdiff.tt =

Provides information on how your stats change from one prompt to another.
Do a bit of fighting, casting and walking around and you'll see what I
mean.

= tracker.tt =

Another combat data gathering module, this one tracks your combat state,
counts rounds as they happen (using a timer), and also displays ticks
when possible. The character must have some kind of 'full' level to
show ticks, though. Fullness is the only prompt token that always changes
in a predictable direction on every tick, so I had to use that value.

This module is responsible for calculating things like 'experience per round.'
I may roll this into data.tt at some point in the future.

= zzupdate.tt =

There is a reason for the odd name. This file MUST be the last one loaded.
Its function is analogous to a 'game loop,' except that this loop hinges on
the prompt. Every time the prompt is received, the update alias in zzupdate.tt
gets run. This is where to check for and call other scripts that want to hook
to the prompt. By default, statdiff.tt and tracker.tt are called here.