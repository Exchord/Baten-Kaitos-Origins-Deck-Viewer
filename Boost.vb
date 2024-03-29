﻿Public Class Boost
    Inherits Form

    Public legend(3, 18), table(19, 9, 2), dummy As Label
    Public address(19, 9, 2) As UInteger
    Dim context As ContextMenuStrip
    Dim scroll_pos As Point

    Private Sub Open() Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9)
        Text = "Temporary Boost"
        MaximizeBox = False
        Icon = New Icon(Me.GetType(), "icon.ico")
        MinimumSize = New Size(626, 243)
        MaximumSize = New Size(1381, 594)
        AutoScroll = True
        DoubleBuffered = True
        KeyPreview = True
        BackColor = Color.DarkGray
        LoadWindowData()

        Dim column_name() As String = {"Character", "Speed", "Offense", "Defense", "Resistance"}
        Dim element_name() As String = {"Physical", "Fire", "Ice", "Lightning", "Light", "Darkness"}
        Dim resistance() As String = {"Flames", "Freezing", "Shock", "Blindness", "Poison", "Sleep"}
        Dim secondary_color As Color = Color.FromArgb(&H70, &HFF, &HFF, &HFF)
        Dim default_color As Color = Color.FromArgb(&H90, &HFF, &HFF, &HFF)

        context = New ContextMenuStrip()
        With context
            .ShowImageMargin = False
            .Items.Add("")
            AddHandler .Opening, AddressOf OpenContextMenu
            AddHandler .ItemClicked, AddressOf CopyAddress
        End With

        For x = 0 To 4
            legend(0, x) = New Label()
            With legend(0, x)
                Select Case x
                    Case 0
                        .Size = New Size(169, 49)
                        .Location = New Point(20, 20)
                    Case 1
                        .Size = New Size(59, 49)
                        .Location = New Point(190, 20)
                    Case Else
                        .Size = New Size(359, 24)
                        .Location = New Point(x * 360 - 470, 20)
                End Select
                .BackColor = default_color
                .TextAlign = ContentAlignment.MiddleCenter
                .Font = Main.bold
                .Text = column_name(x)
            End With
            Controls.Add(legend(0, x))
        Next
        For x = 0 To 17
            legend(1, x) = New Label()
            With legend(1, x)
                .Size = New Size(59, 24)
                If x = 0 Then
                    .Location = New Point(250, 45)
                Else
                    .Location = New Point(250 + x * 60, 45)
                End If
                .TextAlign = ContentAlignment.MiddleCenter
                .Font = Main.bold
                If x < 12 Then
                    .Text = element_name(x Mod 6)
                    .BackColor = Main.element_color(x Mod 6)
                Else
                    .Text = resistance(x Mod 6)
                    If x < 16 Then
                        .BackColor = Main.status_color(x Mod 6)
                    Else
                        .BackColor = default_color
                    End If
                End If
            End With
            Controls.Add(legend(1, x))
        Next
        For x = 0 To 8
            legend(2, x) = New Label()
            With legend(2, x)
                .Hide()
                .BackColor = default_color
                .Size = New Size(169, 49)
                .Location = New Point(20, 70 + x * 50)
                .TextAlign = ContentAlignment.MiddleCenter
            End With
            Controls.Add(legend(2, x))
        Next

        For x = 0 To 18
            For y = 0 To 8
                For z = 0 To 1
                    table(x, y, z) = New Label()
                    With table(x, y, z)
                        .Hide()
                        .Size = New Size(59, 24)
                        .Location = New Point(190 + x * 60, 70 + y * 50 + z * 25)
                        .TextAlign = ContentAlignment.MiddleCenter
                        If z = 0 Then
                            .BackColor = default_color
                        Else
                            .BackColor = secondary_color
                        End If
                        .Tag = Convert.ToChar(x) & y & z
                        .ContextMenuStrip = context
                    End With
                    Controls.Add(table(x, y, z))
                Next
            Next
        Next

        dummy = New Label()
        With dummy
            .Size = New Size(0, 0)
            .Location = New Point(1348, 0)
        End With
        Controls.Add(dummy)

        ContextMenuStrip = context
        Show()
    End Sub

    Private Sub LoadWindowData()
        Size = My.Settings.BoostWindowSize
        Dim pt As Point = My.Settings.BoostWindowLocation
        If pt.X = -1 And pt.Y = -1 Then
            CenterToScreen()
            Return
        End If
        For Each s As Screen In Screen.AllScreens
            If s.Bounds.Contains(pt) Then
                Location = pt
                Return
            End If
        Next
        CenterToScreen()
    End Sub

    Private Sub OpenContextMenu()
        Dim source As Control = context.SourceControl
        With context.Items
            .Clear()
            If source Is Me Then
                context.ShowCheckMargin = True
                context.Font = Font
                Dim offset As New ToolStripMenuItem
                offset.Checked = My.Settings.PhysicalAddresses
                offset.Text = "Show physical addresses"
                .Add(offset)
                .Add("Deck Viewer v" & Main.version)
                .Item(0).Tag = 1
                .Item(1).Tag = 2
                Return
            End If

            context.ShowCheckMargin = False
            context.Font = New Font("Consolas", 10)
            Dim dolphin As Integer
            If My.Settings.PhysicalAddresses Then
                dolphin = Main.dolphin
            End If
            Dim str As String = source.Tag.ToString
            Dim x, y, z As Integer
            x = Asc(str.ElementAt(0))
            y = "&H" & str.ElementAt(1)
            z = "&H" & str.ElementAt(2)
            .Add(Hex(dolphin + address(x, y, z)))
        End With
    End Sub

    Private Sub CopyAddress(sender As Object, e As ToolStripItemClickedEventArgs)
        Dim tag As String = e.ClickedItem.Tag
        Select Case tag
            Case 1
                My.Settings.PhysicalAddresses = Not My.Settings.PhysicalAddresses
            Case 2
                Main.ViewDocumentation()
            Case Else
                Clipboard.SetText(e.ClickedItem.Text)
        End Select
    End Sub

    Private Sub SaveWindowData() Handles Me.FormClosing
        My.Settings.BoostWindowLocation = Location
        My.Settings.BoostWindowSize = Size
    End Sub

    Public Sub AdjustPositions()
        If scroll_pos = AutoScrollPosition Then
            Return
        End If
        scroll_pos = AutoScrollPosition
        For x = 0 To 4
            Select Case x
                Case 0
                    legend(0, x).Location = New Point(20, 20) + scroll_pos
                Case 1
                    legend(0, x).Location = New Point(190, 20) + scroll_pos
                Case Else
                    legend(0, x).Location = New Point(x * 360 - 470, 20) + scroll_pos
            End Select
        Next
        For x = 0 To 17
            If x = 0 Then
                legend(1, x).Location = New Point(250, 45) + scroll_pos
            Else
                legend(1, x).Location = New Point(250 + x * 60, 45) + scroll_pos
            End If
        Next
        For x = 0 To 8
            legend(2, x).Location = New Point(20, 70 + x * 50) + scroll_pos
        Next
        For x = 0 To 18
            For y = 0 To 8
                For z = 0 To 1
                    table(x, y, z).Location = New Point(190 + x * 60, 70 + y * 50 + z * 25) + scroll_pos
                Next
            Next
        Next
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape, Keys.B
                Close()
            Case Keys.F1
                Main.ViewDocumentation()
            Case Keys.R
                Dim characters As Integer
                For x = 0 To 8
                    If Not legend(2, x).Visible Then
                        Exit For
                    End If
                    characters += 1
                Next
                Height = MaximumSize.Height - 450 + characters * 50
        End Select
    End Sub
End Class