Public Class CustomPanel
    Inherits Panel
    Public Sub New()
        DoubleBuffered = True
        AutoScroll = True
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        UpdateStyles()
    End Sub
End Class