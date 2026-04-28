# Overengineered Tic-Tac-Toe
A WebGL Tic-Tac-Toe game that can also be played on Mobile, both in portrait and landscape screen orientations. 

Video demonstration (mainly for portrait orientation): https://youtu.be/FfnYsl--VdE<br/>
Playable browser version (landscape orientation only): https://aleva147.itch.io/overtic-tac-toe 

***
### Project Description
This game was developed in Unity Game Engine, with SOLID principles in mind and an attempt at maintaining a clean code architecture. Reliance on the Observer pattern, especially on Scriptable Objects for events, makes the written code mostly decoupled, as a lot of classes can function independently and be tested on their own. With some exceptions, code related to logic is properly separated from code related to visuals, and they can be independently modified.

All implemented Tic-Tac-Toe logic works for any NxN grid, not just a 3x3 board. The game over detection algorithm interestingly runs in constant time, unlike the usual linear time implementation. Object pooling was used for VFXs to avoid unnecessary instantiation and destruction of game objects each time a visual effect is used. Adding more themes or removing existing themes can be done entirely in the editor, without the need to change any code. The game also offers data persistence across different sessions, by keeping track of player statistics using PlayerPrefs. 

NOTE: Currently, the UI repositioning is a bit wonky if the screen orientation changes after starting a new round (as can be seen in the video demonstration). Also, if the X-O marks are placed too quickly one after another, the VFX animation acts weird — but this can be simply fixed by killing the existing DOTween before starting a new animation. Apart from these two bugs, the game works as expected, and no other issues have been found.

Unity version used: 6000.0.62f1

***
### Possible Improvements
Below is a list of ideas for future improvements:
+ Animate the drawing of grid lines at the beginning of each round 
+ Change the colors of the grid lines and background based on the selected theme, to make the X-O marks more visible and pleasing
+ Change the style of the strike to match the style of X-O marks of the selected theme
+ Animate the drawing of X and O marks instead of having them pop up
+ Add a more interesting background
+ More different sound effects
+ Add a scrollbar to the ThemeSelectionMenu to be able to fit more than three themes within the same panel dimensions
+ Offer 4x4 and 5x5 grids as playable options
