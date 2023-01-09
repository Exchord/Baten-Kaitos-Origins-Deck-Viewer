Public Class Boost
    Inherits Form

    Public legend(3, 18), table(19, 9, 2), dummy As Label
    Public address(19, 9, 2) As Int64
    Dim context As ContextMenuStrip
    Dim panel As CustomPanel
    Dim scroll_pos As Point

    Public ReadOnly cell_width = 59
    ReadOnly column_name() As String = {"Character", "Speed", "Offense", "Defense", "Resistance"}
    ReadOnly column_width() As Integer = {169, cell_width, 359, 359, 359}
    ReadOnly element_name() As String = {"Physical", "Fire", "Ice", "Lightning", "Light", "Darkness"}
    ReadOnly resistance() As String = {"Flames", "Freezing", "Shock", "Blindness", "Poison", "Sleep"}
    ReadOnly element_color() As Color = {Color.FromArgb(&H90, &HA8, &H5A, &H2A), Color.FromArgb(&H90, &HFF, &H20, &H0), Color.FromArgb(&H90, &H0, &HB0, &HFF), Color.FromArgb(&H90, &H0, &HB0, &H0), Color.FromArgb(&H90, &HC0, &HC0, &HC0), Color.FromArgb(&H90, &HC0, &H0, &HFF)}
    ReadOnly secondary_color As Color = Color.FromArgb(&H70, &HFF, &HFF, &HFF)

    Private Sub Open() Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        Text = "Temporary Boost"
        MaximizeBox = False
        Icon = New Icon(Me.GetType(), "icon.ico")
        MinimumSize = New Size(626, 243)
        MaximumSize = New Size(1381, 593)
        DoubleBuffered = True
        KeyPreview = True
        BackColor = Color.DarkGray
        LoadWindowData()

        context = New ContextMenuStrip()
        With context
            .ShowImageMargin = False
            .Items.Add("0")
            AddHandler .Opening, AddressOf OpenContextMenu
            AddHandler .ItemClicked, AddressOf CopyAddress
        End With

        panel = New CustomPanel()
        panel.Size = New Size(Width - 16, Height - 16)
        Controls.Add(panel)

        For y = 0 To 2
            For x = 0 To 17
                legend(y, x) = New Label()
                With legend(y, x)
                    Select Case y
                        Case 0
                            If x >= 5 Then
                                Continue For
                            End If
                            If x < 2 Then
                                .Size = New Size(column_width(x), 49)
                            Else
                                .Size = New Size(column_width(x), 24)
                            End If
                            If x = 0 Then
                                .Location = New Point(20, 20)
                            Else
                                .Location = New Point(legend(0, x - 1).Left + legend(0, x - 1).Width + 1, 20)
                            End If
                            .BackColor = Main.default_color
                            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
                            .Text = column_name(x)
                        Case 1
                            .Size = New Size(cell_width, 24)
                            If x = 0 Then
                                .Location = New Point(legend(0, 1).Left + legend(0, 1).Width + 1, 45)
                            Else
                                .Location = New Point(legend(1, x - 1).Left + legend(1, x - 1).Width + 1, 45)
                            End If
                            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
                            If x < 12 Then
                                .Text = element_name(x Mod 6)
                                .BackColor = element_color(x Mod 6)
                            Else
                                .Text = resistance(x Mod 6)
                                If x < 16 Then
                                    .BackColor = Main.status_color(x Mod 6)
                                Else
                                    .BackColor = Main.default_color
                                End If
                            End If
                        Case 2
                            .Hide()
                            If x >= 9 Then
                                Continue For
                            End If
                            .BackColor = Main.default_color
                            .Size = New Size(column_width(0), 49)
                            .Location = New Point(20, 70 + x * 50)
                    End Select
                    .TextAlign = ContentAlignment.MiddleCenter
                End With
                panel.Controls.Add(legend(y, x))
            Next
        Next

        For x = 0 To 18
            For y = 0 To 8
                For z = 0 To 1
                    table(x, y, z) = New Label()
                    With table(x, y, z)
                        .Hide()
                        .Size = New Size(cell_width, 24)
                        .Location = New Point(190 + x * (cell_width + 1), 70 + y * 50 + z * 25)
                        .TextAlign = ContentAlignment.MiddleCenter
                        If z = 0 Then
                            .BackColor = Main.default_color
                        Else
                            .BackColor = secondary_color
                        End If
                        .Tag = Convert.ToChar(x) & y & z
                        .ContextMenuStrip = context
                    End With
                    panel.Controls.Add(table(x, y, z))
                Next
            Next
        Next

        dummy = New Label()
        With dummy
            .Size = New Size(0, 0)
            .Location = New Point(table(18, 8, 1).Left + cell_width + 19, table(18, 8, 1).Top + 43)
        End With
        panel.Controls.Add(dummy)

        AddHandler Resize, AddressOf ResizePanel
        Show()
        ResizePanel()
        panel.Focus()
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
        context.Items.Clear()
        Dim source As Control = context.SourceControl
        Dim str As String = source.Tag.ToString
        Dim x, y, z As Integer
        x = Asc(str.ElementAt(0))
        y = "&H" & str.ElementAt(1)
        z = "&H" & str.ElementAt(2)
        Dim hex As String = Conversion.Hex(address(x, y, z))
        context.Items.Add(hex)
    End Sub

    Private Sub CopyAddress(sender As Object, e As ToolStripItemClickedEventArgs)
        Clipboard.SetText(e.ClickedItem.Text)
    End Sub

    Private Sub SaveWindowData() Handles Me.FormClosing
        My.Settings.BoostWindowLocation = Location
        My.Settings.BoostWindowSize = Size
    End Sub

    Private Sub ResizePanel()
        panel.Size = New Size(Width - 16, Height - 38)
    End Sub

    Public Sub UpdateRows(rows As Integer)
        If scroll_pos = panel.AutoScrollPosition Then
            Return
        End If
        scroll_pos = panel.AutoScrollPosition
        For y = 0 To 2
            For x = 0 To 17
                Select Case y
                    Case 0
                        If x >= 5 Then
                            Continue For
                        End If
                        If x = 0 Then
                            legend(y, x).Location = New Point(20 + scroll_pos.X, 20 + scroll_pos.Y)
                        Else
                            legend(y, x).Location = New Point(legend(0, x - 1).Left + legend(0, x - 1).Width + 1, 20 + scroll_pos.Y)
                        End If
                    Case 1
                        If x = 0 Then
                            legend(y, x).Location = New Point(legend(0, 1).Left + legend(0, 1).Width + 1, 45 + scroll_pos.Y)
                        Else
                            legend(y, x).Location = New Point(legend(1, x - 1).Left + legend(1, x - 1).Width + 1, 45 + scroll_pos.Y)
                        End If
                    Case 2
                        If x > rows Then
                            Continue For
                        End If
                        legend(y, x).Location = New Point(20 + scroll_pos.X, 70 + x * 50 + scroll_pos.Y)
                End Select
            Next
        Next

        For x = 0 To 18
            For y = 0 To 8
                For z = 0 To 1
                    table(x, y, z).Location = New Point(190 + x * (cell_width + 1) + scroll_pos.X, 70 + y * 50 + z * 25 + scroll_pos.Y)
                Next
            Next
        Next

        dummy.Location = New Point(table(18, rows - 1, 1).Left + cell_width + 19, table(18, rows - 1, 1).Top + 43)
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape, Keys.B
                Close()
            Case Keys.F1
                Main.ViewDocumentation()
        End Select
    End Sub
End Class