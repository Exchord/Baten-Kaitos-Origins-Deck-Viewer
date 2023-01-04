<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Settings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BattleTime = New System.Windows.Forms.CheckBox()
        Me.MP = New System.Windows.Forms.CheckBox()
        Me.EnemyInfo = New System.Windows.Forms.CheckBox()
        Me.Crush = New System.Windows.Forms.CheckBox()
        Me.Knockdown = New System.Windows.Forms.CheckBox()
        Me.Knockout = New System.Windows.Forms.CheckBox()
        Me.CrushDefense = New System.Windows.Forms.CheckBox()
        Me.EnemyMP = New System.Windows.Forms.CheckBox()
        Me.EffectiveHPonly = New System.Windows.Forms.CheckBox()
        Me.PartyInfo = New System.Windows.Forms.CheckBox()
        Me.PartnerInfo = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'BattleTime
        '
        Me.BattleTime.AutoSize = True
        Me.BattleTime.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BattleTime.Location = New System.Drawing.Point(176, 90)
        Me.BattleTime.Name = "BattleTime"
        Me.BattleTime.Size = New System.Drawing.Size(115, 19)
        Me.BattleTime.TabIndex = 0
        Me.BattleTime.Text = "Show battle time"
        Me.BattleTime.UseVisualStyleBackColor = True
        '
        'MP
        '
        Me.MP.AutoSize = True
        Me.MP.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MP.Location = New System.Drawing.Point(176, 44)
        Me.MP.Name = "MP"
        Me.MP.Size = New System.Drawing.Size(76, 19)
        Me.MP.TabIndex = 1
        Me.MP.Text = "Show MP"
        Me.MP.UseVisualStyleBackColor = True
        '
        'EnemyInfo
        '
        Me.EnemyInfo.AutoSize = True
        Me.EnemyInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnemyInfo.Location = New System.Drawing.Point(21, 21)
        Me.EnemyInfo.Name = "EnemyInfo"
        Me.EnemyInfo.Size = New System.Drawing.Size(118, 19)
        Me.EnemyInfo.TabIndex = 2
        Me.EnemyInfo.Text = "Show enemy info"
        Me.EnemyInfo.UseVisualStyleBackColor = True
        '
        'Crush
        '
        Me.Crush.AutoSize = True
        Me.Crush.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Crush.Location = New System.Drawing.Point(21, 67)
        Me.Crush.Name = "Crush"
        Me.Crush.Size = New System.Drawing.Size(57, 19)
        Me.Crush.TabIndex = 4
        Me.Crush.Text = "Crush"
        Me.Crush.UseVisualStyleBackColor = True
        '
        'Knockdown
        '
        Me.Knockdown.AutoSize = True
        Me.Knockdown.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Knockdown.Location = New System.Drawing.Point(21, 90)
        Me.Knockdown.Name = "Knockdown"
        Me.Knockdown.Size = New System.Drawing.Size(142, 19)
        Me.Knockdown.TabIndex = 5
        Me.Knockdown.Text = "Knockdown threshold"
        Me.Knockdown.UseVisualStyleBackColor = True
        '
        'Knockout
        '
        Me.Knockout.AutoSize = True
        Me.Knockout.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Knockout.Location = New System.Drawing.Point(21, 113)
        Me.Knockout.Name = "Knockout"
        Me.Knockout.Size = New System.Drawing.Size(130, 19)
        Me.Knockout.TabIndex = 6
        Me.Knockout.Text = "Knockout threshold"
        Me.Knockout.UseVisualStyleBackColor = True
        '
        'CrushDefense
        '
        Me.CrushDefense.AutoSize = True
        Me.CrushDefense.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CrushDefense.Location = New System.Drawing.Point(21, 136)
        Me.CrushDefense.Name = "CrushDefense"
        Me.CrushDefense.Size = New System.Drawing.Size(101, 19)
        Me.CrushDefense.TabIndex = 7
        Me.CrushDefense.Text = "Crush defense"
        Me.CrushDefense.UseVisualStyleBackColor = True
        '
        'EnemyMP
        '
        Me.EnemyMP.AutoSize = True
        Me.EnemyMP.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnemyMP.Location = New System.Drawing.Point(176, 67)
        Me.EnemyMP.Name = "EnemyMP"
        Me.EnemyMP.Size = New System.Drawing.Size(115, 19)
        Me.EnemyMP.TabIndex = 8
        Me.EnemyMP.Text = "Show enemy MP"
        Me.EnemyMP.UseVisualStyleBackColor = True
        '
        'EffectiveHPonly
        '
        Me.EffectiveHPonly.AutoSize = True
        Me.EffectiveHPonly.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EffectiveHPonly.Location = New System.Drawing.Point(21, 44)
        Me.EffectiveHPonly.Name = "EffectiveHPonly"
        Me.EffectiveHPonly.Size = New System.Drawing.Size(116, 19)
        Me.EffectiveHPonly.TabIndex = 9
        Me.EffectiveHPonly.Text = "Effective HP only"
        Me.EffectiveHPonly.UseVisualStyleBackColor = True
        '
        'PartyInfo
        '
        Me.PartyInfo.AutoSize = True
        Me.PartyInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PartyInfo.Location = New System.Drawing.Point(176, 21)
        Me.PartyInfo.Name = "PartyInfo"
        Me.PartyInfo.Size = New System.Drawing.Size(109, 19)
        Me.PartyInfo.TabIndex = 10
        Me.PartyInfo.Text = "Show party info"
        Me.PartyInfo.UseVisualStyleBackColor = True
        '
        'PartnerInfo
        '
        Me.PartnerInfo.AutoSize = True
        Me.PartnerInfo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PartnerInfo.Location = New System.Drawing.Point(176, 113)
        Me.PartnerInfo.Name = "PartnerInfo"
        Me.PartnerInfo.Size = New System.Drawing.Size(134, 19)
        Me.PartnerInfo.TabIndex = 11
        Me.PartnerInfo.Text = "Show AI partner info"
        Me.PartnerInfo.UseVisualStyleBackColor = True
        '
        'Settings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 187)
        Me.Controls.Add(Me.PartnerInfo)
        Me.Controls.Add(Me.PartyInfo)
        Me.Controls.Add(Me.EffectiveHPonly)
        Me.Controls.Add(Me.EnemyMP)
        Me.Controls.Add(Me.CrushDefense)
        Me.Controls.Add(Me.Knockout)
        Me.Controls.Add(Me.Knockdown)
        Me.Controls.Add(Me.Crush)
        Me.Controls.Add(Me.EnemyInfo)
        Me.Controls.Add(Me.MP)
        Me.Controls.Add(Me.BattleTime)
        Me.Name = "Settings"
        Me.Text = "Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BattleTime As CheckBox
    Friend WithEvents MP As CheckBox
    Friend WithEvents EnemyInfo As CheckBox
    Friend WithEvents Crush As CheckBox
    Friend WithEvents Knockdown As CheckBox
    Friend WithEvents Knockout As CheckBox
    Friend WithEvents CrushDefense As CheckBox
    Friend WithEvents EnemyMP As CheckBox
    Friend WithEvents EffectiveHPonly As CheckBox
    Friend WithEvents PartyInfo As CheckBox
    Friend WithEvents PartnerInfo As CheckBox
End Class
