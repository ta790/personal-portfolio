# Rock, Paper, Scissors, Lizard, Spock VB Windows Forms Guide

This project is a complete beginner-friendly Windows Forms game written in Visual Basic.

It includes:

- play until the player quits
- score recording
- previous history recording
- most chosen move tracking
- a positive sound when the player wins
- a different sound when the player loses

## 1. Project Folder

Inside this folder you now have:

- `RockPaperScissorsLizardSpockVB.vbproj`
- `Program.vb`
- `MainForm.vb`

## 2. What Each File Does

### `RockPaperScissorsLizardSpockVB.vbproj`

This is the project file.

It tells Visual Studio:

- this is a VB project
- this is a Windows Forms app
- start the program from `Program.vb`

### `Program.vb`

This is the startup file.

Its job is:

- start Windows Forms
- open `MainForm`

### `MainForm.vb`

This is the main game screen.

It contains:

- the form layout
- the move buttons
- the score logic
- the history logic
- the most chosen move logic
- the quit button
- the sound behavior

## 3. How The Game Works

When the player clicks a move button:

1. the program reads the selected move
2. the computer randomly chooses one move
3. the program compares both moves
4. the result is decided as win, lose, or draw
5. the score is updated
6. the round is added to history
7. the most chosen move is recalculated
8. a sound is played

## 4. Game Rules Used In The Code

The rules are:

- Rock beats Scissors and Lizard
- Paper beats Rock and Spock
- Scissors beats Paper and Lizard
- Lizard beats Paper and Spock
- Spock beats Rock and Scissors

## 5. Form Layout In Detail

The form is built fully in code, so you do not need images or drag-and-drop controls.

### Main window

- title: `Rock, Paper, Scissors, Lizard, Spock`
- width: `1160`
- height: `720`
- dark blue background

### Top section

At the top there are 2 labels:

- a large title label
- a short description label

### Group 1: Choose Your Move

This group box is near the top.

It contains 5 buttons:

- Rock
- Paper
- Scissors
- Lizard
- Spock

Each button:

- has the same size
- is placed in one row
- calls the same click event
- stores its move name in the `Tag` property

### Group 2: Current Round

This section shows:

- your selected move
- the computer move
- the result of the round

### Group 3: Scoreboard And Statistics

This section shows:

- player score
- computer score
- draw count
- total rounds played
- most chosen move

### Group 4: Previous History

This section shows:

- a `ListBox` for all previous rounds
- a `Clear History` button
- a `Quit Game` button

## 6. Visual Studio Steps From Beginning

If you already have Visual Studio installed:

1. Open Visual Studio.
2. Click `Open a project or solution`.
3. Browse to this folder.
4. Open `RockPaperScissorsLizardSpockVB.vbproj`.
5. Wait for Visual Studio to load the project.
6. Press `F5` to run.

If you do not have Visual Studio installed yet:

1. Download Visual Studio Community.
2. During installation, select the workload `Desktop development with .NET`.
3. Finish the installation.
4. Open the project file `RockPaperScissorsLizardSpockVB.vbproj`.

## 7. If You Want To Build The Project Manually In Visual Studio

If your teacher wants you to create it yourself step by step, follow this exact process.

### Step A: Create the project

1. Open Visual Studio.
2. Click `Create a new project`.
3. Search for `Windows Forms App`.
4. Choose the Visual Basic version.
5. Click `Next`.
6. Name the project `RockPaperScissorsLizardSpockVB`.
7. Choose a folder.
8. Click `Create`.

### Step B: Replace the generated files

1. In `Solution Explorer`, open the project files.
2. Replace the content of `Program.vb` with the content from this project.
3. Replace the content of `Form1.vb` with the content from `MainForm.vb`.
4. Rename `Form1.vb` to `MainForm.vb` if needed.
5. Make sure the startup form is `MainForm`.

## 8. Detailed Explanation Of The Main Parts Of The Code

### A. Variables

At the top of `MainForm.vb` there are variables such as:

- `playerScore`
- `computerScore`
- `drawScore`
- `totalRounds`
- `moveCounts`

These are used to store data while the game is running.

### B. `moveWinsAgainst`

This dictionary stores the rules of the game.

Example:

- Rock wins against Scissors and Lizard
- Paper wins against Rock and Spock

This makes the winner check simple and clean.

### C. `InitializeGameLayout`

This procedure creates:

- labels
- buttons
- group boxes
- list box

It also sets:

- colors
- font sizes
- positions
- widths and heights

### D. `MoveButton_Click`

This is the most important event.

When the player clicks any move button:

1. the button is converted into a `Button`
2. the move is read from `Tag`
3. the computer move is generated
4. the result is calculated
5. score values are updated
6. history is added
7. labels are refreshed
8. a sound is played

### E. `GetComputerMove`

This function randomly returns one move from:

- Rock
- Paper
- Scissors
- Lizard
- Spock

### F. `DecideWinner`

This function compares the player's move and the computer's move.

It returns one of these strings:

- `Win`
- `Lose`
- `Draw`

### G. `UpdateScoreboard`

This procedure updates the labels after every round.

It shows:

- the scores
- the total rounds
- the most chosen move

### H. `GetMostChosenMoveMessage`

This function checks all player move counts and returns the move chosen most often.

If there is a tie, it shows all top moves.

### I. `ClearHistoryButton_Click`

This clears the history list only.

Important:

- scores are not reset
- statistics are not reset

### J. `QuitButton_Click`

This shows a final summary message and then closes the form.

That is how the player can keep playing until they choose to stop.

## 9. Sound Behavior

The code uses built-in Windows system sounds.

That means:

- you do not need to add any sound files
- you do not need to add images

Used sounds:

- win: `SystemSounds.Asterisk.Play()`
- lose: `SystemSounds.Hand.Play()`
- draw: `SystemSounds.Exclamation.Play()`

If you later want a custom cheering sound:

1. add a `.wav` file to the project
2. use `SoundPlayer`
3. play the file when the player wins

## 10. How To Change The Form Layout Later

If you want bigger buttons:

- change `.Size = New Size(190, 55)`

If you want to move the buttons:

- change `.Location = New Point(...)`

If you want a different form size:

- change `ClientSize = New Size(1160, 720)`

If you want different colors:

- change the `BackColor` and `ForeColor` values

## 11. Simple Practice Tasks For You

After you run the project, you can try these beginner exercises:

1. change the background color
2. change button text color
3. add a reset scores button
4. show a message when the player reaches 5 wins
5. replace system sounds with your own `.wav` files

## 12. Important Note

I could create the full project files here, but I could not run the project in this environment because the .NET SDK is not installed on this machine right now.

So the code is ready for Visual Studio, but you should open it there and run it with `F5`.
