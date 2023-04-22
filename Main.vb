Imports System.Drawing.Imaging
Public Class Main
    Inherits Form

    Dim panel As CustomPanel
    Dim magnus(486) As Bitmap
    Dim card(60), magnus_cursor, drop(8) As PictureBox
    Dim table(8, 10), error_message, battle_data(12), time_label, battle, next_card, dummy As Label
    Dim context As ContextMenuStrip
    Dim battle_check, battle_time, id(60), id_buffer(60), hand_size, deck_size, shuffle, cursor_pos, slot, members, partners, enemies, order(60), battle_id, magnus_size, status_effect(9) As Integer
    Dim game, pointer_address, offset, time_address, MP_address, partner_address, pointer, enemy_lookup, magnus_lookup, battle_address, next_(60), first_card, next_draw, first_out, address(9, 8, 5), card_address(60) As UInteger
    Public dolphin As Integer
    Dim hooked, empty, opacity_buffer(60) As Boolean
    Dim hProcess As IntPtr
    Dim emulator As Process()
    Dim timer As Timer
    Public version As String

    ReadOnly character() As String = {"", "Sagi", "Milly", "Guillo", "Empire Grunt", "Imperial Elite", "Dark Serviceman", "Imperial Guard", "Elite Imperial Guard", "Dark Service Peon", "Dark Service Officer", "Fallen Serviceman", "Imperial Swordsman", "Elite Swordsman", "Dark Service Swordsman", "Dark Service Swordmaster", "Imperial Swordguard", "Alpha Paramachina", "Beta Paramachina", "Upgraded Paramachina", "Imperial Battle Machina", "Autonomous Battle Machina", "Masterless Battle Machina", "Cancerite", "Cloud Cancerite", "Mad Cancerite", "Armored Cancerite", "Unuk", "Striper", "Magma Beast", "Shawra", "Blood Leaf", "Badwin", "Filler", "Doomer", "Gormer", "Almer", "Zelmer", "Albireo", "Ray-Moo", "Pul-Puk", "Bar-Mool", "Spell Shellfish", "Magic Shellfish", "Skeleton Warrior", "Undead Swordsman", "Ghoulish Skirmisher", "Devil Claws", "Shadow Claws", "Ghost Claws", "Ceratobus", "Foytow", "Rulug", "Mirabilis", "Lanocaulis", "Acheron", "Maw-Maw-Goo", "Caracal", "Lesser Caracal", "Shadow Caracal", "King Caracal", "Orvata", "Vata", "Balloona", "Fogg", "Armored Balloona", "Slave Balloona", "Alraune", "Queen Alraune", "Ballet Dancer", "Dance King", "Devil's Doll", "Machina Ballerina", "Prima Queen", "Larva Golem", "Cicada Golem", "Ogopogo", "Gigim", "Vodnik", "Juggler", "Master Juggler", "Ahriman", "Mite", "Magician Mite", "Wizard Mite", "Armored Mite", "Goat Chimera", "Phoelix", "Nixie Chimera", "Mobile Turret", "High-Mobility Cannon", "Geryon", "Nebulos", "Monoceros", "Lycaon", "Medium", "Shaman", "Saber Dragon", "Hercules Dragon", "Dragon", "Arma Prototype M", "Hideous Beast 1", "Hideous Beast 2", "Umbra", "Malpercio's Afterling 1", "Malpercio's Afterling 2", "Giacomo 1", "Giacomo 2", "Giacomo 3", "Valara 1", "Valara 2", "Heughes 1", "Heughes 2", "Nasca 1", "Nasca 2", "Machina Arma: Razer 1", "Machina Arma: Razer 2", "Machina Arma: Razer 3", "Promachina Heughes 1", "Promachina Heughes 2", "Machina Arma: Marauder 1", "Machina Arma: Marauder 2", "Cannon", "Cockpit", "Guillo", "Seginus", "Sandfeeder", "Hearteater", "Holoholobird", "Mange-Roches", "Holoholo Chick", "Lord of the Lava Caves", "Rudra", "Promachina Shanath", "Black Dragon", "Wiseman", "Baelheit", "Verus", "Machinanguis A", "Machinanguis B", "Verus-Wiseman", "Holoholo Egg", "Malpercio's Afterling 2 (head)", "Mr. Quintain", "nero_parts", "Machina Arma: Razer"}
    ReadOnly interrupted_battles() As Integer = {5, 29, 30, 42, 43, 62, 74, 93, 103, 108, 149, 151, 147, 135, 145, 252}
    ReadOnly HP_limit() As Integer = {1140 - 571, 2800 - 1401, 1760 - 177, 4560 - 2281, 4270 - 2136, 4000 - 201, 5280 - 397, 5080 - 509, 10320 - 4129, 8220 - 412, 6680 - 335, 9910 - 744, 11280 - 1129, 10690 - 1070, 13140 - 1315, 4270 - 2136}
    'Hideous Beast, Hideous Beast 2, Machina Arma: Razer, Umbra, Lord of the Lava Caves, Machina Arma: Marauder, Promachina Heughes, Machina Arma: Razer 2, Guillo, Promachina Shanath, Machina Arma: Marauder 2, Promachina Heughes 2, Machina Arma: Razer 3, Baelheit, Verus-Wiseman

    Public ReadOnly element_color() As Color = {Color.FromArgb(&H90, &HA8, &H5A, &H2A), Color.FromArgb(&H90, &HFF, &H20, &H0), Color.FromArgb(&H90, &H0, &HB0, &HFF), Color.FromArgb(&H90, &H0, &HB0, &H0), Color.FromArgb(&H90, &HC0, &HC0, &HC0), Color.FromArgb(&H90, &HC0, &H0, &HFF)}
    Public ReadOnly status_color() As Color = {element_color(1), element_color(2), element_color(3), element_color(5)}
    Public ReadOnly bold As New Font("Segoe UI", 9, FontStyle.Bold)

    Private Declare Function OpenProcess Lib "kernel32" (dwDesiredAccess As Integer, bInheritHandle As Integer, dwProcessId As Integer) As Integer
    Private Declare Function ReadProcessMemory Lib "kernel32" Alias "ReadProcessMemory" (hProcess As Integer, lpBaseAddress As Int64, ByRef lpBuffer As Integer, nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (hObject As IntPtr) As Boolean
    Const PROCESS_ALL_ACCESS = &H1F0FF

    Private Sub Open() Handles MyBase.Load
        Hide()
        Application.CurrentCulture = New Globalization.CultureInfo("EN-US")
        Font = New Font("Segoe UI", 9)
        Text = "Baten Kaitos Origins Deck Viewer"
        MaximizeBox = False
        Icon = New Icon(Me.GetType(), "icon.ico")
        MinimumSize = New Size(846, 337)
        MaximumSize = New Size(846, 782)
        DoubleBuffered = True
        KeyPreview = True
        BackColor = Color.DarkGray
        LoadWindowData()

        Dim default_color As Color = Color.FromArgb(&H90, &HFF, &HFF, &HFF)

        context = New ContextMenuStrip()
        With context
            .ShowImageMargin = False
            .Items.Add("")
            AddHandler .Opening, AddressOf OpenContextMenu
            AddHandler .ItemClicked, AddressOf CopyAddress
        End With

        panel = New CustomPanel()
        panel.Size = New Size(Width - 16, Height - 119)
        panel.Location = New Point(0, 79)
        Controls.Add(panel)

        error_message = New Label()
        With error_message
            .Size = New Size(96, 19)
            .Location = New Point(30, 20)
            .AutoSize = True
            .BackColor = Color.DarkRed
            .ForeColor = Color.Transparent
            .Font = New Font("Segoe UI", 10)
        End With
        Controls.Add(error_message)

        Dim variable() As String = {"MP", "Enemy MP", "Partner MP", "EXP", "TP", "Gold"}
        For x = 0 To 11
            battle_data(x) = New Label()
            With battle_data(x)
                .Hide()
                .Size = New Size(68, 21)
                If x < 6 Then
                    .Text = variable(x)
                Else
                    .Font = New Font("Segoe UI", 12, FontStyle.Bold)
                End If
                .Location = New Point(40 + 71 * (x Mod 6), 20 + 18 * Math.Floor(x / 6))
                .BackColor = default_color
                .TextAlign = ContentAlignment.MiddleCenter
                .ContextMenuStrip = context
                .Tag = x Mod 6
            End With
            Controls.Add(battle_data(x))
        Next

        For x = 0 To 7
            drop(x) = New PictureBox()
            With drop(x)
                .Hide()
                .Size = New Size(30, 48)
                .Location = New Point(469 + 30 * x, 16)
                .Tag = x
                .ContextMenuStrip = context
            End With
            Controls.Add(drop(x))
        Next

        time_label = New Label()
        With time_label
            .Hide()
            .Size = New Size(132, 25)
            .Location = New Point(673, 20)
            .AutoSize = True
            .BackColor = default_color
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Segoe UI", 14, FontStyle.Bold)
            .ContextMenuStrip = context
        End With
        Controls.Add(time_label)

        battle = New Label()
        With battle
            .Hide()
            .Size = New Size(61, 25)
            .Location = New Point(729, 50)
            .TextAlign = ContentAlignment.MiddleLeft
            .ForeColor = Color.Blue
            .ContextMenuStrip = context
        End With
        Controls.Add(battle)

        next_card = New Label()
        With next_card
            .Hide()
            .Size = New Size(50, 20)
            .BackColor = default_color
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = bold
            .Text = "NEXT"
        End With
        panel.Controls.Add(next_card)

        empty = True

        Dim column() As String = {"Character", "HP", "Delay", "Down", "Crush", "Poison", "Effect", "Aura"}
        For x = 0 To 7
            For y = 0 To 9
                table(x, y) = New Label()
                With table(x, y)
                    .Hide()
                    If x = 0 Then
                        .Left = 40
                        .Size = New Size(169, 24)
                    Else
                        .Left = 127 + x * 83
                        .Size = New Size(82, 24)
                    End If
                    .Top = 135 + 25 * y
                    .TextAlign = ContentAlignment.MiddleCenter
                    If y = 0 Then
                        .Font = bold
                        .Text = column(x)
                    Else
                        .ContextMenuStrip = context
                    End If
                    .BackColor = default_color
                    .Tag = x
                    .Name = y
                End With
                panel.Controls.Add(table(x, y))
            Next
        Next

        dummy = New Label()
        dummy.Size = New Size(0, 0)
        panel.Controls.Add(dummy)

        For x = 1 To 454
            magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x), New Size(50, 80))
        Next
        For x = 460 To 485
            Try
                magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x), New Size(50, 80))
            Catch ex As Exception
            End Try
        Next

        For x = 0 To 59
            card(x) = New PictureBox()
            With card(x)
                .Hide()
                .Size = New Size(50, 80)
                .Location = New Point(40 + 50 * (x Mod 15), 25 + 90 * Math.Floor(x / 15))
                .Tag = x
                .ContextMenuStrip = context
            End With
            panel.Controls.Add(card(x))
        Next

        magnus_cursor = New PictureBox()
        With magnus_cursor
            .Hide()
            .Size = New Size(20, 20)
            .Top = 1
            .BackColor = Color.Transparent
            .Image = New Bitmap(My.Resources.ResourceManager.GetObject("cursor"), .Size)
        End With
        panel.Controls.Add(magnus_cursor)

        ContextMenuStrip = context
        AddHandler Resize, AddressOf ResizePanel
        Show()
        panel.Focus()

        version = FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion
        Dim i, count As Integer
        For i = 0 To version.Length - 1
            If version.ElementAt(i) = "." Then
                count += 1
            End If
            If count = 2 Then
                Exit For
            End If
        Next
        version = version.Substring(0, i)

        timer = New Timer()
        With timer
            .Interval = 1
            AddHandler .Tick, AddressOf MainLoop
            .Start()
        End With
    End Sub

    Private Sub LoadWindowData()
        Size = My.Settings.WindowSize
        Dim pt As Point = My.Settings.WindowLocation
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

    Private Sub MainLoop()
        Try
            battle_check = 0
            battle_time = 0
            pointer = 0
            emulator = Process.GetProcessesByName("Dolphin")
            If emulator.Length <> 1 Then
                If hooked Then
                    hooked = False
                    CloseHandle(hProcess)
                End If
                If emulator.Length = 0 Then
                    error_message.Text = "Dolphin isn't open."
                Else
                    error_message.Text = "Please use only one instance of Dolphin."
                End If
                Clear()
                Return
            End If
            If Not hooked Then
                Dim title As String = emulator(0).MainWindowTitle
                Dim version As Integer
                Dim dolphin_version() As String = {"Dolphin 4.0", "Dolphin 4.0.1", "Dolphin 4.0.2", "Dolphin 5.0"}
                For x = 0 To 3
                    If title = dolphin_version(x) OrElse title.StartsWith(dolphin_version(x) & " |") Then
                        version = x + 1
                        Exit For
                    End If
                Next
                Select Case version
                    Case 1 To 3
                        dolphin = &H7FFF0000        '4.0, 4.0.1, 4.0.2
                    Case 4
                        dolphin = -&H10000          '5.0
                    Case 0
                        Dim secondary_window() As String = {"TAS Input", "Memory Card Manager", "Cheat Manager", "Cheats Manager", "Dolphin NetPlay Setup", "FIFO Player"}
                        For x = 0 To secondary_window.Length - 1
                            If title.StartsWith(secondary_window(x)) Then
                                error_message.Text = "Please close and reopen " & secondary_window(x) & " to proceed."
                                Return
                            End If
                        Next
                        error_message.Text = "This version of Dolphin is not supported. Please use Dolphin 5.0 or 4.0.x."
                        Return
                End Select
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, emulator(0).Id)
                If hProcess = IntPtr.Zero Then
                    error_message.Text = "Failed to open Dolphin."
                    Return
                End If
                hooked = True
            End If

            game = Read32U(&H80000000L)
            If game = &H474B344A Then           'GK4J (Japanese version)
                pointer_address = &H8086C064L
                time_address = &H802DDBCCL
                MP_address = &H8030B550L
                partner_address = &H802DD3E6L
                enemy_lookup = &H80202238L
                magnus_lookup = &H802339F4L
                magnus_size = 208
                offset = &H1118C
            ElseIf game = &H474B3445 Then       'GK4E (English version)
                pointer_address = &H80857B84L
                time_address = &H802D5ED4L
                MP_address = &H8030BC38L
                partner_address = &H802D56EEL
                enemy_lookup = &H80205068L
                magnus_lookup = &H80228768L
                magnus_size = 188
                offset = &H183A4
            Else
                If game = 0 Then
                    error_message.Text = "No game running."
                Else
                    error_message.Text = "Wrong game."
                End If
                Clear()
                Return
            End If
            error_message.Text = ""

            battle_time = Read32(time_address)
            pointer = Read32U(pointer_address)
            If pointer = 0 Then
                Clear()
                Return
            End If

            battle_check = Read16(pointer + 148)
            If battle_check = -1 Or battle_check = 3 Then           '-1: cutscene / transition to battle; 3: ongoing battle
                battle_id = Read16(time_address - &HA6)
                battle.Text = "Battle: " & battle_id
                battle.Show()
            Else
                battle.Hide()
            End If

            If battle_check <> 3 Then
                Clear()
                Return
            End If
            empty = False
            battle_address = pointer + offset

            'show cursor and next card indicator
            hand_size = Read16(battle_address + &H98A)
            next_card.Left = 40 + 50 * hand_size
            next_card.Show()
            cursor_pos = Read16(battle_address + &H98E)
            magnus_cursor.Left = 55 + 50 * cursor_pos
            magnus_cursor.Show()

            'get cards and order
            next_draw = Read32U(battle_address + &H950)
            first_out = Read32U(battle_address + &H93C)
            deck_size = 60
            For x = 0 To 59
                id(x) = Read16(battle_address + 6 + x * 36)
                If id(x) = 0 Then
                    deck_size = x
                    Exit For
                End If
                next_(x) = Read32U(battle_address + x * 36)
                If next_(x) <> 0 Then
                    next_(x) = (next_(x) - battle_address) / 36
                Else
                    next_(x) = UInteger.MaxValue
                End If
                card_address(x) = battle_address + 4 + order(x) * 36
            Next

            DrawDeck()
            ShowBattleInfo()
            ShowBattleResults()
            If Boost.Visible Then
                ShowBoost()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DrawDeck()
        'check first card
        first_card = Read32U(battle_address + &H984)
        If first_card = 0 Then
            HideCards(0)
            Return
        End If

        'raise card to cursor
        For x = 0 To 6
            If cursor_pos = x Then
                card(x).Top = 21 + panel.AutoScrollPosition.Y
            Else
                card(x).Top = 25 + panel.AutoScrollPosition.Y
            End If
        Next

        'adjust position of remaining cards
        Dim row_pos As Integer
        For x = 7 To deck_size - 1
            row_pos = 25 + 90 * Math.Floor(x / 15) + panel.AutoScrollPosition.Y
            If card(x).Top <> row_pos Then
                card(x).Top = row_pos
            End If
        Next

        'show cards in hand
        slot = (first_card - battle_address) / 36
        ShowCard(0, False)
        For x = 1 To hand_size - 1
            If next_(slot) = UInteger.MaxValue Then
                HideCards(x)
                Return
            End If
            slot = next_(slot)
            ShowCard(x, False)
        Next

        'check card labeled "NEXT"
        If next_draw = 0 Then
            HideCards(hand_size)
            Return
        End If

        'show cards beyond hand
        slot = (next_draw - battle_address) / 36
        ShowCard(hand_size, False)
        For x = hand_size + 1 To deck_size
            If next_(slot) = UInteger.MaxValue Then
                shuffle = x
                Exit For
            End If
            slot = next_(slot)
            ShowCard(x, False)
        Next

        'check first card to be shuffled
        If first_out = 0 Then
            HideCards(shuffle)
            Return
        End If

        'show cards to be shuffled
        slot = (first_out - battle_address) / 36
        ShowCard(shuffle, True)
        For x = shuffle + 1 To deck_size
            If next_(slot) = UInteger.MaxValue Then
                HideCards(x)
                Return
            End If
            slot = next_(slot)
            ShowCard(x, True)
        Next
    End Sub

    Private Sub ShowCard(i As Integer, transparent As Boolean)
        order(i) = Clamp(slot, 0, 59)
        Dim id As Integer = Me.id(slot)
        If id_buffer(i) <> id Then
            id_buffer(i) = id
        ElseIf opacity_buffer(i) = transparent Then
            Return
        End If
        Dim img As Bitmap = magnus(id)
        If Not transparent Then
            opacity_buffer(i) = False
            card(i).Image = img
        Else
            opacity_buffer(i) = True
            card(i).Image = ChangeOpacity(img)
        End If
        If Not card(i).Visible Then
            card(i).Show()
        End If
    End Sub

    Private Sub ShowBattleResults()
        Dim EXP, TP, gold As Integer
        EXP = Read32(time_address + &H34)
        TP = Read32(time_address + &H38)
        gold = Read32(time_address + &H3C)
        battle_data(9).Text = EXP
        battle_data(10).Text = TP
        battle_data(11).Text = gold

        time_label.Text = FormatTime(battle_time, 2)
        time_label.Left = 790 - time_label.Width
        If Not time_label.Visible Then
            time_label.Show()
            For x = 3 To 5
                battle_data(x).Show()
                battle_data(x + 6).Show()
            Next
        End If

        Dim drops As Integer = Read16(time_address + &H42)
        drops = Math.Min(drops, 8)
        Dim id As Integer
        For x = 0 To drops - 1
            id = Read16(time_address + &H46 + 4 * x)
            If id = 0 Then
                Exit For
            End If
            If x < 3 Then
                drop(x).Image = New Bitmap(magnus(id), New Size(30, 48))
            Else
                drop(x).Image = ChangeOpacity(New Bitmap(magnus(id), New Size(30, 48)))
            End If
            drop(x).Show()
        Next
        For x = drops To 7
            drop(x).Hide()
        Next
    End Sub

    Private Sub ShowBattleInfo()
        members = Read16(battle_address - &HE346)
        enemies = Read16(battle_address - &HA2DA)
        Dim partner_id As Integer = Read16(partner_address)
        If partner_id = 0 Then
            partners = 0
        Else
            partners = 1
        End If
        Dim characters As Integer = members + partners + enemies

        'adjust table position
        Dim row_pos As Integer
        For y = 0 To 9
            row_pos = card(deck_size - 1).Top + 100 + 25 * y
            If table(0, 9).Top <> row_pos Then
                For x = 0 To 7
                    table(x, y).Top = row_pos
                Next
            End If
        Next

        'MP
        Dim burst As Boolean = Read32(MP_address + 4)
        If burst = 0 Then
            Dim MP As Double = ReadFloat(MP_address)
            battle_data(0).Text = "MP"
            battle_data(6).Text = Decimals(MP, False)
        Else
            Dim burst_timer As Integer
            If game = &H474B344A Then
                burst_timer = Read32(battle_address + &HA60)    'JP
            Else
                burst_timer = Read32(battle_address + &H3068)   'EN
            End If
            battle_data(0).Text = "MP burst"
            battle_data(6).Text = FormatTime(burst_timer, 0)
        End If
        Dim enemy_MP As Integer = Read16(MP_address + &H1C6)
        battle_data(7).Text = enemy_MP
        For x = 0 To 1
            battle_data(x).Show()
            battle_data(x + 6).Show()
        Next
        If partners = 1 Then
            Dim partner_MP As Integer = Read16(MP_address + &H1C2)
            battle_data(8).Text = partner_MP
            battle_data(2).Show()
            battle_data(8).Show()
        Else
            battle_data(2).Hide()
            battle_data(8).Hide()
        End If

        For x = 0 To 7
            table(x, 0).Show()
        Next
        For y = 1 To 9
            Dim party As Integer
            Select Case y - 1
                Case < members
                    party = 0               'party member
                Case < members + partners
                    party = 1               'partner
                Case < characters
                    party = 2               'enemy
                Case Else
                    For x = 0 To 7
                        table(x, y).Hide()
                    Next
                    Continue For
            End Select

            Dim base, addr, speed_address As UInteger
            Dim i, character As Integer
            Select Case party
                Case 0
                    i = y - 1
                    base = battle_address - &HE2A0 + &H1578 * i
                    addr = battle_address - &HD030 + &H1578 * i
                    character = Read16(addr + 2)
                    speed_address = addr - &HE4
                    address(y, 0, 0) = addr
                    address(y, 0, 1) = 0
                Case 1
                    base = battle_address - &H26B4
                    character = partner_id
                    speed_address = battle_address - &H1528
                    address(y, 0, 0) = partner_address - 2
                    If character <= 3 Then
                        address(y, 0, 1) = partner_address - &H2E6 + (character - 1) * 244
                    Else
                        address(y, 0, 1) = enemy_lookup + (character - 1) * 228
                    End If
                Case 2
                    i = y - 1 - members - partners
                    base = battle_address - &HA230 + &H1494 * i
                    addr = battle_address - &H8F28 + &H1494 * i
                    character = Read16(addr + 2)
                    speed_address = addr - &HE4 - &H98
                    address(y, 0, 0) = addr
                    address(y, 0, 1) = 0
            End Select

            'character
            table(0, y).Text = Me.character(character)

            'HP
            address(y, 1, 0) = base + &H14
            Dim HP As Integer = Read16(base + &H16)
            If party <> 2 Then
                table(1, y).Text = HP
            Else
                Dim temp As Integer = Array.IndexOf(interrupted_battles, battle_id)
                If temp = -1 Then
                    table(1, y).Text = HP
                Else
                    Dim effective_HP As Integer = Math.Max(0, HP - HP_limit(temp))
                    If effective_HP > 0 Then
                        table(1, y).Text = HP & " (" & effective_HP & ")"
                    Else
                        table(1, y).Text = HP
                    End If
                End If
            End If

            'equipment
            Dim armor_speed As Double = 0
            Dim magnus_address As UInteger
            If party = 0 Then
                magnus_address = Read32U(addr - &H1244)
            End If
            If magnus_address <> 0 Then
                Dim magnus_effect As Integer
                For x = 0 To 2
                    magnus_effect = Read16(magnus_address + 66 + x * 8)
                    Select Case magnus_effect
                        Case 55     'increases turnover speed
                            armor_speed = 0.01 * Read16(magnus_address + 70 + x * 8)
                            Exit For
                        Case 61     'decreases turnover speed
                            armor_speed = -0.01 * Read16(magnus_address + 70 + x * 8)
                            Exit For
                    End Select
                Next
            End If

            'aura
            Dim char_speed As Double = 1
            If party = 0 AndAlso Read32U(base + &H1214) <> 0 Then
                Dim aura_timer As Integer
                address(y, 7, 0) = base + &H1360
                aura_timer = Read32(base + &H1360)
                table(7, y).Text = FormatTime(aura_timer, 0)
                table(7, y).ContextMenuStrip = context
                char_speed = ReadFloat(addr + &HFC)
            Else
                table(7, y).Text = ""
                table(7, y).ContextMenuStrip = Nothing
            End If

            'speed boost
            Dim speed1 As Double = ReadFloat(speed_address)
            Dim speed2 As Double = ReadFloat(speed_address + &H50)
            If Boost.Visible Then
                Boost.address(0, y - 1, 0) = speed_address
                Boost.table(0, y - 1, 0).Text = Decimals(speed1, True)
                Boost.address(0, y - 1, 1) = speed_address + &H50
                Boost.table(0, y - 1, 1).Text = Decimals(speed2, True)
            End If
            Dim state As Integer = Read16(base + 2)
            '0 waiting for next turn
            '2 done preparing turn
            '3 waiting in queue
            '4 acting
            '5 leaving queue
            '7 down, waiting to get back into queue
            Dim speed_boost As Double
            If state = 3 Or state = 4 Then
                speed_boost = speed1
            Else
                speed_boost = speed2
            End If
            Dim speed As Double = 0.1 * Math.Round(10 * (char_speed + speed_boost + armor_speed), MidpointRounding.AwayFromZero)

            'delay
            address(y, 2, 0) = base + &H1C
            address(y, 2, 1) = base + 4
            Dim delay As Integer = Read32(base + &H1C)
            If delay = 0 Then
                If state < 4 Or state = 7 Then
                    delay = Read32(base + 4)
                End If
                If party = 0 And state < 3 AndAlso (delay < 0 Or delay > 140) Then
                    delay = 0
                End If
                If delay = 4194444 Then
                    delay = 0
                End If
            Else
                If speed > 0 Then
                    delay = Math.Ceiling(delay / speed)
                    If party > 0 And character <> 142 Then
                        delay += 590                        'enemies and partners always take 1.97 seconds to "select magnus"
                    End If
                Else
                    delay = Integer.MaxValue                'speed <= 0: permanently unable to act
                End If
            End If
            table(2, y).Text = FormatTime(delay, 0)

            'down
            address(y, 3, 0) = base + &H20
            Dim down As Integer = Read32(base + &H20)
            If down > 0 Then
                If speed > 0 Then
                    down = Math.Ceiling(down / speed)
                Else
                    down = Integer.MaxValue
                End If
            End If
            table(3, y).Text = FormatTime(down, 0)

            'crush
            address(y, 4, 0) = base + &H18
            Dim crush As Double = ReadFloat(base + &H18)
            table(4, y).Text = Decimals(crush, True)

            'poison
            address(y, 5, 0) = base + &H1128
            address(y, 5, 1) = base + &H1134
            Dim poison1 As Integer = Read32(base + &H1128)                      'poison timer
            If poison1 > 0 Then
                Dim poison2 As Integer = Read32(base + &H1134)                  'poison damage timer
                table(5, y).Text = FormatTime(poison1, 0) & " (" & FormatTime(poison2, 0) & ")"
            Else
                table(5, y).Text = ""
            End If

            'effect
            address(y, 6, 0) = base + &H1118
            address(y, 6, 1) = base + &H1130
            address(y, 6, 2) = base + &H111C
            address(y, 6, 3) = base + &H1120
            address(y, 6, 4) = base + &H1124
            Dim effect_offset() As Integer = {&H1118, &H111C, &H1120, &H1124}   'flames, frozen, shock, blind
            Dim effect As Integer
            For x = 0 To 3
                effect = Read32(base + effect_offset(x))                        'effect timer
                If effect > 0 Then
                    If x = 0 Then
                        Dim flames As Integer = Read32(base + &H1130)           'flames damage timer
                        table(6, y).Text = FormatTime(effect, 0) & " (" & FormatTime(flames, 0) & ")"
                    Else
                        table(6, y).Text = FormatTime(effect, 0)
                    End If
                    status_effect(y - 1) = x + 1
                    Exit For
                End If
            Next
            If effect = 0 Then
                table(6, y).Text = ""
                status_effect(y - 1) = 0
            End If

            For x = 0 To 7
                table(x, y).Show()
            Next
        Next
        dummy.Top = table(0, characters).Top + 43
    End Sub

    Private Sub ShowBoost()
        Dim value As Double
        Dim characters As Integer = members + partners + enemies
        Dim address As UInteger
        For y = 0 To characters - 1
            Boost.legend(2, y).Text = table(0, y + 1).Text      'names
            Dim base As UInteger
            Select Case y
                Case < members
                    base = Me.address(y + 1, 0, 0) - &H12C              'party member
                Case < members + partners
                    base = battle_address - &H1570                      'partner
                Case Else
                    base = Me.address(y + 1, 0, 0) - &H12C - &H98       'enemy
            End Select
            For x = 1 To 18         '1-6: offense; 7-12: defense; 13-18: resistance
                For z = 0 To 1      '0: turn 1; 1: turn 2
                    address = base + (x - 1) * 4 + z * &H50
                    Boost.address(x, y, z) = address
                    value = ReadFloat(address)
                    Boost.table(x, y, z).Text = Decimals(value, True)
                Next
            Next
        Next

        Boost.AdjustPositions()
        For y = 0 To characters - 1
            Boost.legend(2, y).Show()
            For x = 0 To 18
                Boost.table(x, y, 0).Show()
                Boost.table(x, y, 1).Show()
            Next
        Next
        For y = characters To 8
            Boost.legend(2, y).Hide()
            For x = 0 To 18
                Boost.table(x, y, 0).Hide()
                Boost.table(x, y, 1).Hide()
            Next
        Next
        Boost.dummy.Top = Boost.table(18, characters - 1, 1).Top + 43
    End Sub

    Private Sub Clear()
        If battle_time = 0 Then
            HideBattleResults()
        Else
            ShowBattleResults()
        End If
        If pointer = 0 Or battle_check = 0 Then
            battle.Hide()
        End If

        If empty Then
            Return
        End If
        empty = True

        HideCards(0)
        magnus_cursor.Hide()
        next_card.Hide()

        For x = 0 To 2
            battle_data(x).Hide()
            battle_data(x + 6).Hide()
        Next
        For x = 0 To 7
            For y = 0 To 9
                table(x, y).Hide()
            Next
        Next
        dummy.Top = 0

        If Not Boost.Visible Then
            Return
        End If
        For y = 0 To 8
            Boost.legend(2, y).Hide()
            For x = 0 To 18
                Boost.table(x, y, 0).Hide()
                Boost.table(x, y, 1).Hide()
            Next
        Next
        Boost.dummy.Top = 0
    End Sub

    Private Sub HideBattleResults()
        If Not time_label.Visible Then
            Return
        End If

        time_label.Hide()
        For x = 3 To 5
            battle_data(x).Hide()
            battle_data(x + 6).Hide()
        Next
        For x = 0 To 7
            drop(x).Hide()
        Next
    End Sub

    Private Sub HideCards(start As Integer)
        For x = start To 59
            If card(x).Visible Then
                card(x).Hide()
                id_buffer(x) = 0
                opacity_buffer(x) = False
            End If
        Next
    End Sub

    Private Sub OpenContextMenu()
        Dim source As Control = context.SourceControl
        With context.Items
            .Clear()
            If source Is Me Then
                context.ShowCheckMargin = True
                context.Font = Font
                .Add("Temporary boost")
                Dim offset As New ToolStripMenuItem
                offset.Checked = My.Settings.PhysicalAddresses
                offset.Text = "Show physical addresses"
                .Add(offset)
                .Add("Deck Viewer v" & version)
                .Item(0).Font = bold
                For i = 0 To 2
                    .Item(i).Tag = i + 1
                Next
                Return
            End If

            context.ShowCheckMargin = False
            context.Font = New Font("Consolas", 10)
            Dim dolphin As Integer
            If My.Settings.PhysicalAddresses Then
                dolphin = Me.dolphin
            End If

            'battle id
            If source Is battle Then
                .Add(Hex(dolphin + time_address - &HA8))
                Return
            End If

            'time
            If source Is time_label Then
                .Add(Hex(dolphin + time_address))
                Return
            End If

            Dim x As Integer = source.Tag
            If TypeOf source Is PictureBox Then
                'drop
                If drop.Contains(source) Then
                    .Add(Hex(dolphin + time_address + &H44 + x * 4))
                    Return
                End If
                'deck
                .Add(Hex(dolphin + card_address(x)))
                .Add(Hex(dolphin + magnus_lookup + id(order(x)) * magnus_size))
                Return
            End If

            'MP & results
            If battle_data.Contains(source) Then
                Dim address() As UInteger = {MP_address, MP_address + &H1C4, MP_address + &H1C0, time_address + &H34, time_address + &H38, time_address + &H3C}
                .Add(Hex(dolphin + address(x)))
                If x = 0 And battle_data(0).Text = "MP burst" Then
                    .Add(Hex(dolphin + battle_address + &HA60))
                End If
                Return
            End If

            'table
            Dim y As Integer = source.Name
            For z = 0 To 4
                If address(y, x, z) = 0 Then
                    Exit For
                End If
                .Add(Hex(dolphin + address(y, x, z)))
            Next

            'effect colors
            If x <> 6 Then
                Return
            End If
            .Item(0).ForeColor = status_color(0)
            For i = 1 To 4
                .Item(i).ForeColor = status_color(i - 1)
            Next

            'highlight effect
            Dim highlight As New Font("Consolas", 10, FontStyle.Underline)
            Dim effect As Integer = status_effect(y - 1)
            If effect = 1 Then
                .Item(0).Font = highlight
                .Item(1).Font = highlight
            ElseIf effect > 1 Then
                .Item(effect).Font = highlight
            End If
        End With
    End Sub

    Private Sub CopyAddress(sender As Object, e As ToolStripItemClickedEventArgs)
        Dim tag As String = e.ClickedItem.Tag
        Select Case tag
            Case 1
                OpenBoost()
            Case 2
                My.Settings.PhysicalAddresses = Not My.Settings.PhysicalAddresses
            Case 3
                ViewDocumentation()
            Case Else
                Clipboard.SetText(e.ClickedItem.Text)
        End Select
    End Sub

    Public Function FormatTime(input As Integer, min_length As Integer) As String
        If input = Integer.MaxValue Then
            Return "∞"                      'speed <= 0: permanently unable to act
        End If
        input = Math.Max(input, 0)
        If input = 0 And min_length = 0 Then
            Return ""
        End If
        input = Math.Ceiling(input / 10) * 10       'round up to whole frames (1 second: 300; 1 frame: 10)
        Dim time As TimeSpan = TimeSpan.FromSeconds(Math.Round(input / 300, 2))
        If min_length = 0 And time.Minutes < 1 Then
            Return FormatNumber(time.TotalSeconds, 2, TriState.True, TriState.False, TriState.False)
        End If
        Dim minutes As Integer = Math.Floor(time.TotalMinutes)
        Dim minutes_length As Integer = Math.Max(min_length, minutes.ToString.Length)
        Dim format As String = ""
        For x = 0 To minutes_length - 1
            format &= "0"
        Next
        Return minutes.ToString(format) & time.ToString("\:ss'.'ff")
    End Function

    Public Function Decimals(value As Double, void As Boolean) As String
        If value = 0 And void Then
            Return ""
        End If
        value = Math.Round(value, 2, MidpointRounding.AwayFromZero)
        Dim pre_decimal_length, decimal_places As Integer
        pre_decimal_length = value.ToString.IndexOf(".")
        If pre_decimal_length <> -1 Then
            decimal_places = value.ToString.Length - pre_decimal_length - 1
        End If
        decimal_places = Clamp(decimal_places, 0, 2)
        Return FormatNumber(value, decimal_places, TriState.True, TriState.False, TriState.False)
    End Function

    Public Function Clamp(input As Double, min As Double, max As Double) As Double      'limit input number to a range
        Return Math.Max(min, Math.Min(input, max))
    End Function

    Public Function Read16(address As UInteger) As Integer
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin, buffer, 2, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return BitConverter.ToInt16(bytes, 2)
    End Function

    Public Function Read32(address As UInteger) As Integer
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin, buffer, 4, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return BitConverter.ToInt32(bytes, 0)
    End Function

    Public Function Read32U(address As UInteger) As UInteger
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin, buffer, 4, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return BitConverter.ToUInt32(bytes, 0)
    End Function

    Public Function ReadFloat(address As UInteger) As Double
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin, buffer, 4, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return BitConverter.ToSingle(bytes, 0)
    End Function

    Private Function ChangeOpacity(img As Image) As Bitmap
        Dim bmp As New Bitmap(img.Width, img.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Dim colormatrix As New ColorMatrix
        colormatrix.Matrix33 = 0.5
        Dim imgAttribute As New ImageAttributes
        imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
        g.DrawImage(img, New Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute)
        g.Dispose()
        Return bmp
    End Function

    Private Sub ResizePanel()
        panel.Size = New Size(Width - 16, Height - 119)
    End Sub

    Private Sub OpenBoost()
        If Boost.Visible Then
            Boost.Focus()
            Boost.WindowState = FormWindowState.Normal
        Else
            Boost.Show()
        End If
    End Sub

    Public Sub ViewDocumentation()
        Process.Start("https://github.com/Exchord/Baten-Kaitos-Origins-Deck-Viewer#readme")
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.B
                OpenBoost()
            Case Keys.F1
                ViewDocumentation()
            Case Keys.R
                If Not empty Then
                    Dim card_rows As Integer = Math.Ceiling(deck_size / 15)
                    Dim characters As Integer = members + partners + enemies
                    Height = MaximumSize.Height - 585 + card_rows * 90 + characters * 25
                End If
        End Select
    End Sub

    Private Sub SaveWindowData() Handles Me.FormClosing
        My.Settings.WindowLocation = Location
        My.Settings.WindowSize = Size
    End Sub
End Class