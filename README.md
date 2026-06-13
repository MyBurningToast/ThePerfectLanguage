# The Perfect Programming Language (tug-o-lang)

An esoteric programming language based on tug of war.

A "rope" (array) sits in memory. A pointer starts in the middle and
gets pulled left or right by your program. Each cell of the rope can
have a character pushed onto it. When one side "gives up," the rope
is read out from the pointer in that direction. That's your output.
Imagine your friend letting go of the rope.

If the pointer gets pulled off either end of the rope, that side
"wins" automatically.

## Syntax

| Instruction | Effect |
|---|---|
| `ROPE <n>` | Initialize a rope of size `n` |
| `PULL L` / `PULL R` | Move the pointer left/right |
| `PUSH <char>` | Write a character at the pointer's position |
| `GIVEUP L` / `GIVEUP R` | End the program, reading the rope from the pointer in that direction |

All other lines are ignored and treated as a comment

## Example

```
ROPE 12
PULL R
PULL R
PULL R
PULL R
PULL R
PUSH H
PULL L
PUSH E
PULL L
PUSH L
PULL L
PUSH L
PULL L
PUSH O
PULL L
PULL L
PUSH W
PULL L
PUSH O
PULL L
PUSH R
PULL L
PUSH L
PULL L
PUSH D
PULL R
PULL R
PULL R
PULL R
PULL R
PULL R
PULL R
PULL R
PULL R
PULL R
GIVEUP L
```

Output: `HELLO WORLD`

## Why
Why not? I just wanted to make a completely useless programming language :)
