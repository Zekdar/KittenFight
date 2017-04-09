# KittenFight

This repo is my solution for the Battle Dev of March 2017 (https://questionsacm.isograd.com/codecontest/pdf/kittenfight.pdf).

# Code review

Please review my code, tell me what you think about it and what could be done better. This is why I decided to share my code so I can improve my code skills :)

# Objective
In this challenge, you need to determine the best possible outcome of a kitten fight. There are three kinds of kitten: Fire (F), Water (E), Plant (P). The fight results are as follows:
- If two kitten of the same kind fight, both are knocked out.
- A Water kitten wins over a Fire kitten
- A Fire kitten wins over a Plant kitten
- A Plant kitten wins over a water kitten

When it comes to team fighting, each player has a team of kitten. They both start witha first kitten. The loosing kitten needs to be replaced by another kitten from his team, this other kitten will fight the winner of the previous fight. The fight finishes when a player has no more kitten. If during a fight, both kitten are knocked out, they both need to be replaced. If consequently, both player have no more kitten, then it’s a draw.

You are facing a pretty basic opponent in a team fight. His strategy is that when one of his kitten is knocked out, he replaces it with the next one on a predetermined list. And you know this predetermined list. So you can probably elaborate a strategy that will make you win.

Clue: as the data sample are small, brutal force is an option.

# Data
## Input

- Row 1: an integer N comprised between 1 and 5, representing the number of kittens in your team.
- Row 2: a string of N characters representing the kitten in your team: FEFP means two fire kittens, one water kitten and one plant kitten.
- Row 3: an integer M comprised between 1 and 10, representing the number of kittens in your opponent team.
- Row 4: a series of M characters representing the kitten in your opponent team. He will present them in the fight in the order defined by this string.

## Output

A string of N+1 characters, whose first character is either :
- `-` if you can’t win the fight
- `=` if the best result that you can get is a draw
- `+` if you can win the fight

The following N characters represent the order in which you will present your kitten to achieve this result. For example, +PFEF means that you can win the fight, by presenting a Plant kitten, then a Fire kitten, then a water kitten, then a Fire kitten (assuming your team include one Plant, two Fire and one water Kitten.
