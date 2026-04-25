Imports System
Imports System.Windows.Forms

Namespace RockPaperScissorsLizardSpockVB
    Friend Module Program
        <STAThread()>
        Friend Sub Main()
            ApplicationConfiguration.Initialize()
            Application.Run(New MainForm())
        End Sub
    End Module
End Namespace
