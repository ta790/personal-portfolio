Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq
Imports System.Media
Imports System.Windows.Forms

Namespace RockPaperScissorsLizardSpockVB
    Public Class MainForm
        Inherits Form

        Private ReadOnly rng As New Random()
        Private ReadOnly moveButtons As New List(Of Button)()
        Private ReadOnly moveWinsAgainst As New Dictionary(Of String, String()) From {
            {"Rock", New String() {"Scissors", "Lizard"}},
            {"Paper", New String() {"Rock", "Spock"}},
            {"Scissors", New String() {"Paper", "Lizard"}},
            {"Lizard", New String() {"Paper", "Spock"}},
            {"Spock", New String() {"Rock", "Scissors"}}
        }
        Private ReadOnly moveCounts As New Dictionary(Of String, Integer) From {
            {"Rock", 0},
            {"Paper", 0},
            {"Scissors", 0},
            {"Lizard", 0},
            {"Spock", 0}
        }

        Private playerScore As Integer
        Private computerScore As Integer
        Private drawScore As Integer
        Private totalRounds As Integer

        Private titleLabel As Label = Nothing!
        Private descriptionLabel As Label = Nothing!
        Private playerMoveLabel As Label = Nothing!
        Private computerMoveLabel As Label = Nothing!
        Private resultLabel As Label = Nothing!
        Private scoreLabel As Label = Nothing!
        Private roundLabel As Label = Nothing!
        Private mostChosenLabel As Label = Nothing!
        Private historyListBox As ListBox = Nothing!
        Private clearHistoryButton As Button = Nothing!
        Private quitButton As Button = Nothing!

        Public Sub New()
            InitializeGameLayout()
            ResetStatusLabels()
        End Sub

        Private Sub InitializeGameLayout()
            Text = "Rock, Paper, Scissors, Lizard, Spock"
            StartPosition = FormStartPosition.CenterScreen
            ClientSize = New Size(1160, 720)
            MinimumSize = New Size(1160, 720)
            BackColor = Color.FromArgb(18, 32, 47)
            Font = New Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point)
            DoubleBuffered = True

            titleLabel = New Label() With {
                .Text = "Rock, Paper, Scissors, Lizard, Spock",
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 24.0F, FontStyle.Bold, GraphicsUnit.Point),
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(20, 18),
                .Size = New Size(1110, 50)
            }

            descriptionLabel = New Label() With {
                .Text = "Choose one move each round. The game continues until you press Quit Game.",
                .ForeColor = Color.FromArgb(220, 235, 245),
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Location = New Point(20, 70),
                .Size = New Size(1110, 26)
            }

            Dim choicesGroup As New GroupBox() With {
                .Text = "Choose Your Move",
                .ForeColor = Color.White,
                .Location = New Point(20, 110),
                .Size = New Size(1110, 130),
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold, GraphicsUnit.Point)
            }

            Dim moveNames As String() = {"Rock", "Paper", "Scissors", "Lizard", "Spock"}
            For i As Integer = 0 To moveNames.Length - 1
                Dim moveButton As New Button() With {
                    .Name = "btn" & moveNames(i),
                    .Text = moveNames(i),
                    .Size = New Size(190, 55),
                    .Location = New Point(18 + (i * 214), 42),
                    .BackColor = Color.FromArgb(239, 184, 16),
                    .ForeColor = Color.FromArgb(35, 35, 35),
                    .FlatStyle = FlatStyle.Flat,
                    .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold, GraphicsUnit.Point),
                    .Tag = moveNames(i)
                }
                moveButton.FlatAppearance.BorderSize = 0
                AddHandler moveButton.Click, AddressOf MoveButton_Click
                moveButtons.Add(moveButton)
                choicesGroup.Controls.Add(moveButton)
            Next

            Dim statusGroup As New GroupBox() With {
                .Text = "Current Round",
                .ForeColor = Color.White,
                .Location = New Point(20, 255),
                .Size = New Size(540, 215),
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold, GraphicsUnit.Point)
            }

            playerMoveLabel = CreateInfoLabel(New Point(20, 38), New Size(490, 28), "Your move: not chosen yet")
            computerMoveLabel = CreateInfoLabel(New Point(20, 78), New Size(490, 28), "Computer move: waiting")
            resultLabel = CreateInfoLabel(New Point(20, 120), New Size(490, 60), "Result: start the game by clicking any move")
            resultLabel.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point)

            statusGroup.Controls.Add(playerMoveLabel)
            statusGroup.Controls.Add(computerMoveLabel)
            statusGroup.Controls.Add(resultLabel)

            Dim scoreGroup As New GroupBox() With {
                .Text = "Scoreboard And Statistics",
                .ForeColor = Color.White,
                .Location = New Point(590, 255),
                .Size = New Size(540, 215),
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold, GraphicsUnit.Point)
            }

            scoreLabel = CreateInfoLabel(New Point(20, 38), New Size(490, 34), "")
            scoreLabel.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold, GraphicsUnit.Point)

            roundLabel = CreateInfoLabel(New Point(20, 88), New Size(490, 28), "")
            mostChosenLabel = CreateInfoLabel(New Point(20, 128), New Size(490, 50), "")

            scoreGroup.Controls.Add(scoreLabel)
            scoreGroup.Controls.Add(roundLabel)
            scoreGroup.Controls.Add(mostChosenLabel)

            Dim historyGroup As New GroupBox() With {
                .Text = "Previous History",
                .ForeColor = Color.White,
                .Location = New Point(20, 485),
                .Size = New Size(1110, 195),
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold, GraphicsUnit.Point)
            }

            historyListBox = New ListBox() With {
                .Location = New Point(20, 35),
                .Size = New Size(830, 135),
                .Font = New Font("Consolas", 10.5F, FontStyle.Regular, GraphicsUnit.Point)
            }

            clearHistoryButton = New Button() With {
                .Text = "Clear History",
                .Size = New Size(210, 50),
                .Location = New Point(875, 45),
                .BackColor = Color.FromArgb(60, 145, 230),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold, GraphicsUnit.Point)
            }
            clearHistoryButton.FlatAppearance.BorderSize = 0
            AddHandler clearHistoryButton.Click, AddressOf ClearHistoryButton_Click

            quitButton = New Button() With {
                .Text = "Quit Game",
                .Size = New Size(210, 50),
                .Location = New Point(875, 110),
                .BackColor = Color.FromArgb(208, 61, 73),
                .ForeColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold, GraphicsUnit.Point)
            }
            quitButton.FlatAppearance.BorderSize = 0
            AddHandler quitButton.Click, AddressOf QuitButton_Click

            historyGroup.Controls.Add(historyListBox)
            historyGroup.Controls.Add(clearHistoryButton)
            historyGroup.Controls.Add(quitButton)

            Controls.Add(titleLabel)
            Controls.Add(descriptionLabel)
            Controls.Add(choicesGroup)
            Controls.Add(statusGroup)
            Controls.Add(scoreGroup)
            Controls.Add(historyGroup)
        End Sub

        Private Function CreateInfoLabel(location As Point, size As Size, text As String) As Label
            Return New Label() With {
                .Location = location,
                .Size = size,
                .Text = text,
                .ForeColor = Color.FromArgb(226, 238, 248),
                .BackColor = Color.Transparent
            }
        End Function

        Private Sub MoveButton_Click(sender As Object, e As EventArgs)
            Dim clickedButton As Button = DirectCast(sender, Button)
            Dim playerMove As String = CStr(clickedButton.Tag)
            Dim computerMove As String = GetComputerMove()
            Dim roundResult As String = DecideWinner(playerMove, computerMove)

            totalRounds += 1
            moveCounts(playerMove) += 1

            Select Case roundResult
                Case "Win"
                    playerScore += 1
                    resultLabel.Text = "Result: You win this round."
                    resultLabel.ForeColor = Color.FromArgb(105, 214, 140)
                    SystemSounds.Asterisk.Play()
                Case "Lose"
                    computerScore += 1
                    resultLabel.Text = "Result: Computer wins this round."
                    resultLabel.ForeColor = Color.FromArgb(255, 120, 120)
                    SystemSounds.Hand.Play()
                Case Else
                    drawScore += 1
                    resultLabel.Text = "Result: It is a draw."
                    resultLabel.ForeColor = Color.FromArgb(255, 214, 102)
                    SystemSounds.Exclamation.Play()
            End Select

            playerMoveLabel.Text = "Your move: " & playerMove
            computerMoveLabel.Text = "Computer move: " & computerMove

            Dim historyLine As String =
                $"Round {totalRounds}: You chose {playerMove}, Computer chose {computerMove}, Result = {roundResult}"
            historyListBox.Items.Insert(0, historyLine)

            UpdateScoreboard()
        End Sub

        Private Function GetComputerMove() As String
            Dim moves As String() = {"Rock", "Paper", "Scissors", "Lizard", "Spock"}
            Dim randomIndex As Integer = rng.Next(moves.Length)
            Return moves(randomIndex)
        End Function

        Private Function DecideWinner(playerMove As String, computerMove As String) As String
            If playerMove = computerMove Then
                Return "Draw"
            End If

            If moveWinsAgainst(playerMove).Contains(computerMove) Then
                Return "Win"
            End If

            Return "Lose"
        End Function

        Private Sub UpdateScoreboard()
            scoreLabel.Text = $"Player: {playerScore}    Computer: {computerScore}    Draws: {drawScore}"
            roundLabel.Text = $"Total rounds played: {totalRounds}"
            mostChosenLabel.Text = GetMostChosenMoveMessage()
        End Sub

        Private Function GetMostChosenMoveMessage() As String
            Dim highestCount As Integer = moveCounts.Values.Max()

            If highestCount = 0 Then
                Return "Most chosen move: no move has been selected yet."
            End If

            Dim topMoves As List(Of String) =
                moveCounts.Where(Function(item) item.Value = highestCount).
                    Select(Function(item) item.Key).
                    ToList()

            Return "Most chosen move: " & String.Join(", ", topMoves) & $" ({highestCount} time(s))"
        End Function

        Private Sub ClearHistoryButton_Click(sender As Object, e As EventArgs)
            historyListBox.Items.Clear()
            historyListBox.Items.Add("History cleared. Scores and statistics are still kept.")
        End Sub

        Private Sub QuitButton_Click(sender As Object, e As EventArgs)
            Dim summaryMessage As String =
                "Final score:" & Environment.NewLine &
                $"Player: {playerScore}" & Environment.NewLine &
                $"Computer: {computerScore}" & Environment.NewLine &
                $"Draws: {drawScore}" & Environment.NewLine &
                $"Rounds played: {totalRounds}" & Environment.NewLine &
                GetMostChosenMoveMessage()

            MessageBox.Show(summaryMessage, "Game Summary", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End Sub

        Private Sub ResetStatusLabels()
            scoreLabel.Text = "Player: 0    Computer: 0    Draws: 0"
            roundLabel.Text = "Total rounds played: 0"
            mostChosenLabel.Text = "Most chosen move: no move has been selected yet."
        End Sub
    End Class
End Namespace
