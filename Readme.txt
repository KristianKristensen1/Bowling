This program is intended to work as a scoreboard for a bowling game.

The following will give a short introduction to the different classes and the design choices made:

BowlingGame:
The BowlingGame class is responsible for registering the value of rolls and for calculating the score of the game. 
To keep track of the progress of a game the rolls are assigned to a list of frames and the game will be marked as finished when the last frame is complete.

After each roll the score is updated by trying to calculate the value of all the completed frames that has not yet been calculated. This is done
after each roll because the roll might be needed to calculate the value of a previous frame.
When the score of a new frame is calculated, the value of the frame is added to the total value of the game and added to the scoreboard. 
It is assumed that it only makes sense to calculate the score of a frame when it is complete. 
The values of strikes and spares depend on subsequent rolls and will only be calculated if the necessary rolls are available. If necessary rolls are not available 
no value will be added to the scoreboard. To access the subsequent rolls in an easy way all rolls are registered in a flat list and the index of the first roll of
each frame is stored in the frame. 

Frame:
The Frame class is responsible for validating the value of the rolls and for determining if the Frame is a strike or a spare. Dividing the rolls in frames is 
necessary to track the progress of the game and to validate the value of second rolls in the frame.  

LastFrame:
Inherits from Frame class, but overrides logic for validation and frame completion, since special rules applies for the last frame of the game. 
This could be solved in different ways, but this way all the special logic regarding the final frame is located here. 

Program:
Creates a console that can be used to interact with the BowlingGame. Just intended as a way to play around with the game. Uses ConsoleTables to draw the scoreboard.

Test:
A number of test has been added in the BowlingGameTest project. Test are added to validate that the value of normal frames, strikes, and spares 
are calculated correctly and that invalid rolls are rejected. 



