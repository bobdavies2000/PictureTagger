Imports cv = OpenCvSharp
Imports cvext = OpenCvSharp.Extensions
Imports System.IO
Public Class Form1
    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Dim msRNG As New System.Random
        Dim picDir As New DirectoryInfo("../../../InputPictures")
        Dim textHeight = 50
        For Each fn In picDir.GetFiles("*.jpg")
            Dim pic = cv.Cv2.ImRead(fn.FullName)
            Dim output As New cv.Mat(pic.Height + textHeight, Math.Max(pic.Width, 800), cv.MatType.CV_8UC3, 0)
            Dim r = New cv.Rect(0, textHeight, pic.Width, pic.Height)
            pic.CopyTo(output(r))

            Dim txt = Mid(fn.Name, 1, Len(fn.Name) - 4)
            cv.Cv2.PutText(output, txt, New cv.Point(0, textHeight - 10), cv.HersheyFonts.HersheyTriplex, 1.0, cv.Scalar.White, 2, cv.LineTypes.AntiAlias)

            Dim randomizedName = "../../../SlideShow/r" + Format(CInt(msRNG.Next(0, 9999)), "0000") + " "
            cv.Cv2.ImWrite(randomizedName + "1" + fn.Name, output)
            cv.Cv2.ImWrite(randomizedName + "2" + fn.Name, output) ' doubles the time to see the picture...
            'cv.Cv2.ImShow("next pic", output)
            'cv.Cv2.WaitKey(1000)
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()
        Application.DoEvents()
        ToolStripButton2_Click(sender, e)
    End Sub
End Class
