Public Class Settings
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BattleTime.Checked = My.Settings.ShowBattleTime
        MP.Checked = My.Settings.ShowMP
        EnemyInfo.Checked = My.Settings.ShowEnemyInfo
        If EnemyInfo.Checked = False Then
            EffectiveHPonly.Enabled = False
            Crush.Enabled = False
            Knockdown.Enabled = False
            Knockout.Enabled = False
            CrushDefense.Enabled = False
        End If
        EffectiveHPonly.Checked = My.Settings.UseEffectiveHPonly
        EnemyMP.Checked = My.Settings.ShowEnemyMP
        Crush.Checked = My.Settings.ShowEnemyCrush
        Knockdown.Checked = My.Settings.ShowEnemyKnockdown
        Knockout.Checked = My.Settings.ShowEnemyKnockout
        CrushDefense.Checked = My.Settings.ShowEnemyCrushDefense
        PartyInfo.Checked = My.Settings.ShowPartyInfo
        PartnerInfo.Checked = My.Settings.ShowPartnerInfo
    End Sub

    Private Sub BattleTime_CheckedChanged(sender As Object, e As EventArgs) Handles BattleTime.CheckedChanged
        If BattleTime.Checked Then
            My.Settings.ShowBattleTime = True
        Else
            My.Settings.ShowBattleTime = False
        End If
    End Sub

    Private Sub MP_CheckedChanged(sender As Object, e As EventArgs) Handles MP.CheckedChanged
        If MP.Checked Then
            My.Settings.ShowMP = True
        Else
            My.Settings.ShowMP = False
        End If
    End Sub

    Private Sub EnemyInfo_CheckedChanged(sender As Object, e As EventArgs) Handles EnemyInfo.CheckedChanged
        If EnemyInfo.Checked Then
            My.Settings.ShowEnemyInfo = True
            EffectiveHPonly.Enabled = True
            Crush.Enabled = True
            Knockdown.Enabled = True
            Knockout.Enabled = True
            CrushDefense.Enabled = True
        Else
            My.Settings.ShowEnemyInfo = False
            EffectiveHPonly.Enabled = False
            Crush.Enabled = False
            Knockdown.Enabled = False
            Knockout.Enabled = False
            CrushDefense.Enabled = False
        End If
    End Sub

    Private Sub EffectiveHPonly_CheckedChanged(sender As Object, e As EventArgs) Handles EffectiveHPonly.CheckedChanged
        If EffectiveHPonly.Checked Then
            My.Settings.UseEffectiveHPonly = True
        Else
            My.Settings.UseEffectiveHPonly = False
        End If
    End Sub

    Private Sub EnemyMP_CheckedChanged(sender As Object, e As EventArgs) Handles EnemyMP.CheckedChanged
        If EnemyMP.Checked Then
            My.Settings.ShowEnemyMP = True
        Else
            My.Settings.ShowEnemyMP = False
        End If
    End Sub

    Private Sub Crush_CheckedChanged(sender As Object, e As EventArgs) Handles Crush.CheckedChanged
        If Crush.Checked Then
            My.Settings.ShowEnemyCrush = True
        Else
            My.Settings.ShowEnemyCrush = False
        End If
    End Sub

    Private Sub Knockdown_CheckedChanged(sender As Object, e As EventArgs) Handles Knockdown.CheckedChanged
        If Knockdown.Checked Then
            My.Settings.ShowEnemyKnockdown = True
        Else
            My.Settings.ShowEnemyKnockdown = False
        End If
    End Sub

    Private Sub Knockout_CheckedChanged(sender As Object, e As EventArgs) Handles Knockout.CheckedChanged
        If Knockout.Checked Then
            My.Settings.ShowEnemyKnockout = True
        Else
            My.Settings.ShowEnemyKnockout = False
        End If
    End Sub

    Private Sub CrushDefense_CheckedChanged(sender As Object, e As EventArgs) Handles CrushDefense.CheckedChanged
        If CrushDefense.Checked Then
            My.Settings.ShowEnemyCrushDefense = True
        Else
            My.Settings.ShowEnemyCrushDefense = False
        End If
    End Sub

    Private Sub PartyInfo_CheckedChanged(sender As Object, e As EventArgs) Handles PartyInfo.CheckedChanged
        If PartyInfo.Checked Then
            My.Settings.ShowPartyInfo = True
        Else
            My.Settings.ShowPartyInfo = False
        End If
    End Sub

    Private Sub PartnerInfo_CheckedChanged(sender As Object, e As EventArgs) Handles PartnerInfo.CheckedChanged
        If PartnerInfo.Checked Then
            My.Settings.ShowPartnerInfo = True
        Else
            My.Settings.ShowPartnerInfo = False
        End If
    End Sub
End Class