Option Strict On

Public Class Form1


    Declare Function SendMessage Lib "user32.dll" Alias "SendMessageW" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByRef lParam As Point) As Integer

    Const WM_USER As Integer = &H400
    Const EM_GETSCROLLPOS As Integer = WM_USER + 221
    Const EM_SETSCROLLPOS As Integer = WM_USER + 222
    Dim lineCount As Integer
    Dim lineNumber As Integer
    Dim hiddenCount As Integer = 0
    Dim hidden As ArrayList = New ArrayList()
    Dim hidden1 As ArrayList = New ArrayList()
    Dim hiddenArray(88) As ArrayList
    Dim copyBuffer As ArrayList = New ArrayList
    Dim moveBuffer As ArrayList = New ArrayList
    Dim moveIndex As Integer
    Dim MyFileName As String = ""
    Dim isChanged As Boolean = False


    Public Shared Function ReplaceInstances(Input As String, Strings As String(), Replacement As String) As String
        For Each Obj As String In Strings
            Input = Replace(Input, Obj, Replacement)
        Next
        Return Input
    End Function
    Public Function Flush() As Integer

        lineCount = RichTextBox1.Lines().Length
        Dim temp(lineCount) As String
        For index As Integer = 0 To lineCount
            temp(index) = "====="
        Next
        RichTextBox2.Lines = temp
        Return 0

    End Function





    Public Function FindMyText(text As String) As Boolean
        ' Initialize the return value to false by default.
        Dim returnValue As Boolean = False

        ' Ensure a search string has been specified.
        If text.Length > 0 Then
            ' Obtain the location of the search string in richTextBox1.
            Dim indexToText As Integer = RichTextBox1.Find(text)
            ' Determine whether the text was found in richTextBox1.
            If indexToText >= 0 Then
                Label3.Text = ""
                returnValue = True
                RichTextBox1.SelectionStart = RichTextBox1.Find(text)
                RichTextBox1.ScrollToCaret()
            End If
        End If

        Return returnValue
    End Function

    Public Function saveFile() As Integer
        Dim saveFile1 As New SaveFileDialog()

        ' Initialize the SaveFileDialog to specify the RTF extension for the file.
        saveFile1.DefaultExt = "*.rtf"
        saveFile1.Filter = "RTF Files|*.rtf"

        ' Determine if the user selected a file name from the saveFileDialog.
        If MyFileName.Length > 0 Then
            RichTextBox1.SaveFile(MyFileName,
                           RichTextBoxStreamType.RichText)
        Else
            If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
           And (saveFile1.FileName.Length) > 0 Then

                ' Save the contents of the RichTextBox into the file.
                RichTextBox1.SaveFile(saveFile1.FileName,
                    RichTextBoxStreamType.RichText)
            End If

        End If
        isChanged = False
        Return 0
    End Function


    Public Function saveAs() As Integer
        Dim saveFile1 As New SaveFileDialog()

        ' Initialize the SaveFileDialog to specify the RTF extension for the file.
        saveFile1.DefaultExt = "*.rtf"
        saveFile1.Filter = "RTF Files|*.rtf"

        ' Determine if the user selected a file name from the saveFileDialog.
        If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
            And (saveFile1.FileName.Length) > 0 Then

            ' Save the contents of the RichTextBox into the file.
            RichTextBox1.SaveFile(saveFile1.FileName,
                RichTextBoxStreamType.PlainText)
        End If
        isChanged = False
        Return 0
    End Function



    Public Function openFile() As Integer
        Dim openFile1 As New OpenFileDialog()

        ' Initialize the OpenFileDialog to look for RTF files.
        openFile1.DefaultExt = "*.rtf"
        openFile1.Filter = "RTF Files|*.rtf"

        ' Determine whether the user selected a file from the OpenFileDialog.
        If (openFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
        And (openFile1.FileName.Length > 0) Then

            MyFileName = openFile1.FileName

            ' Load the contents of the file into the RichTextBox.
            If openFile1.FileName.Contains(".txt") Then
                RichTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText) ' normally filters for rtf documents but if txt selected it also parses it accordingly


            Else
                RichTextBox1.LoadFile(openFile1.FileName)
            End If

        End If
        Return 0
    End Function
    Public Function GetColumnAndLine() As Integer

        Dim Position As Integer = Me.RichTextBox1.SelectionStart

        Dim Line As Integer = Me.RichTextBox1.GetLineFromCharIndex(Position)


        Dim x As Integer = Line

        '  Label1.Text = "Line : " + CType(x, String)
        Dim y As Integer = Position

        If x = 0 Then

            Label1.Text = "Line : " + CType(x, String)
            lineNumber = x
            Label2.Text = "Column : " + CType(Position + 1, String)

            Return Position + 1

        Else

            Do Until x = Line - 1

                x = Me.RichTextBox1.GetLineFromCharIndex(y) 'x equals line no

                y -= 1 'y is cursor position

            Loop


            Label1.Text = "Line : " + CType(x + 2, String)
            lineNumber = x + 2
            Label2.Text = "Column : " + CType(Position - y - 3, String)
            Return (Position - y - 1) 'i changed the code here, UR CODE WAS -> Return (Position - y + 4)

        End If

    End Function



    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(CInt((Screen.PrimaryScreen.WorkingArea.Width / 2) - (Me.Width / 2)), CInt((Screen.PrimaryScreen.WorkingArea.Height / 2) - (Me.Height / 2)))


        '  RichTextBox1.Text = My.Computer.FileSystem.ReadAllText("C:\Users\John\Desktop\Samsung Disallow Calls.Txt")
        lineCount = RichTextBox1.Lines().Length
        Dim lines(lineCount) As String
        For index As Integer = 0 To lineCount
            lines(index) = "====="
        Next
        RichTextBox2.Lines = lines

    End Sub

    Dim RTB2ScrollPoint As Point



    Private Sub RichTextBox1_VScroll(
ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles RichTextBox1.VScroll

        SendMessage(RichTextBox1.Handle, EM_GETSCROLLPOS, 0, RTB2ScrollPoint)
        SendMessage(RichTextBox2.Handle, EM_SETSCROLLPOS, 0, New Point(RTB2ScrollPoint.X, RTB2ScrollPoint.Y))

        If lineCount <> RichTextBox1.Lines().Length Then

            lineCount = RichTextBox1.Lines().Length
            Dim temp(lineCount) As String
            For index As Integer = 0 To lineCount
                temp(index) = "====="
            Next
            RichTextBox2.Lines = temp
        End If

    End Sub
    Private Sub RichTextBox2_VScroll(
ByVal sender As Object, ByVal e As System.EventArgs) _
  Handles RichTextBox2.VScroll

        SendMessage(RichTextBox2.Handle, EM_GETSCROLLPOS, 0, RTB2ScrollPoint)
        SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, New Point(RTB2ScrollPoint.X, RTB2ScrollPoint.Y))


    End Sub
    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        isChanged = True
        SendMessage(RichTextBox1.Handle, EM_GETSCROLLPOS, 0, RTB2ScrollPoint)
        SendMessage(RichTextBox2.Handle, EM_SETSCROLLPOS, 0, New Point(RTB2ScrollPoint.X, RTB2ScrollPoint.Y))
        ' GetColumnAndLine()

    End Sub


    Private Sub RichTextBox2_TextChanged_1(sender As Object, e As EventArgs) Handles RichTextBox2.TextChanged
        SendMessage(RichTextBox2.Handle, EM_GETSCROLLPOS, 0, RTB2ScrollPoint)
        SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, New Point(RTB2ScrollPoint.X, RTB2ScrollPoint.Y))

    End Sub

    Private Sub RichTextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox1.KeyDown
        GetColumnAndLine()



        If lineCount <> RichTextBox1.Lines().Length Then

            lineCount = RichTextBox1.Lines().Length
            Dim temp(lineCount) As String
            For index As Integer = 0 To lineCount
                temp(index) = "====="
            Next
            RichTextBox2.Lines = temp
        End If


        ' RichTextBox1.Text.LastIndexOf(lastChar)

    End Sub



    Private Sub RichTextBox2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles RichTextBox2.KeyDown




        Dim operation As String
            Dim originalLines() As String
            Dim currentIndex As Integer
            currentIndex = 0
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim lines() As String = RichTextBox2.Lines()
            Dim lines1() As String = RichTextBox1.Lines()
            lineCount = lines.Count



            For index As Integer = 0 To lines.Count - 2
                If Not lines(index) = "=====" Then
                    If Not lines(index) = "" Then
                        Label3.Text = " "
                        currentIndex = index
                        operation = lines(index)
                        operation = Replace(operation, "=", "")
                        If operation.Length > 0 Then



                            If operation.Substring(0, 1) = "c" Then
                                Dim count As Integer = 1

                                If operation.Length > 1 Then
                                    operation = Replace(operation, "c", "")
                                    If operation.Length < 0 Then
                                        count = 1


                                    Else
                                        Try
                                            count = CInt(operation)

                                        Catch ex As Exception
                                            Label3.Text = " Invalid Prompt Enter a valid integer number with c ( like c3 ) "
                                            count = -1

                                            Flush()

                                            Return

                                        End Try
                                    End If
                                    If count > lineCount Then
                                        Label3.Text = " Invalid Prompt Integer cannot be bigger than total line numbers"

                                        Flush()

                                        Return
                                    End If
                                End If

                                originalLines = RichTextBox1.Lines

                                Dim AList As ArrayList = New ArrayList(originalLines)

                                For counter As Integer = 1 To count + 1

                                    copyBuffer.Add(AList(index + counter - 1))


                                Next






                            ElseIf operation.Substring(0, 1) = Chr(34) Then



                                originalLines = RichTextBox1.Lines

                                    Dim AList As ArrayList = New ArrayList(originalLines)

                                AList.Insert(index, AList(index))
                                originalLines = CType(AList.ToArray(GetType(String)), String())
                                RichTextBox1.Lines = originalLines










                            ElseIf operation.Substring(0, 1) = "m" Then
                                Dim count As Integer = 1

                                If operation.Length > 1 Then
                                    operation = Replace(operation, "m", "")



                                    If operation.Length < 0 Then
                                        count = 1


                                    Else
                                        Try
                                            count = CInt(operation)

                                        Catch ex As Exception
                                            Label3.Text = " Invalid Prompt Enter a valid integer number with m ( like m3 ) "
                                            count = -1

                                            Flush()

                                            Return
                                        End Try
                                    End If


                                    If count > lineCount Then
                                        Label3.Text = " Invalid Prompt Integer cannot be bigger than total line numbers"

                                        Flush()

                                        Return
                                    End If
                                End If

                                originalLines = RichTextBox1.Lines
                                moveIndex = index
                                Dim AList As ArrayList = New ArrayList(originalLines)

                                For counter As Integer = 1 To count + 1

                                    moveBuffer.Add(AList(index + counter - 1))


                                Next

                            End If
                        End If

                    End If

                End If


            Next












            For index As Integer = 0 To lines.Count - 2

                If Not lines(index) = "=====" Then

                    If Not lines(index) = "" Then
                        Label3.Text = " "
                        currentIndex = index
                        operation = lines(index)
                        operation = Replace(operation, "=", "")
                        If operation.Length > 0 Then



                            If operation.Substring(0, 1) = "b" Or operation.Substring(0, 1) = "a" Then
                                Dim addition = 0
                                If operation.Substring(0, 1) = "b" Then
                                    addition = +1
                                End If


                                If operation.Length > 1 Then
                                    Label3.Text = " Invalid Prompt :/Above/ Suffix doesnt have additional parameters "
                                End If
                                If copyBuffer.Count > 0 Then

                                    originalLines = RichTextBox1.Lines

                                    Dim AList As ArrayList = New ArrayList(originalLines)
                                    Dim count = copyBuffer.Count
                                    For counter As Integer = 0 To count - 1


                                        AList.Insert(index + counter + addition, copyBuffer(0))
                                        copyBuffer.RemoveAt(0)

                                    Next

                                    originalLines = CType(AList.ToArray(GetType(String)), String())
                                    RichTextBox1.Lines = originalLines


                                    Flush()

                                ElseIf moveBuffer.Count > 0 Then

                                    originalLines = RichTextBox1.Lines

                                    Dim AList As ArrayList = New ArrayList(originalLines)
                                    Dim count = moveBuffer.Count
                                    For counter As Integer = 0 To count - 1

                                        AList.RemoveAt(moveIndex)

                                        AList.Insert(index + addition, moveBuffer(0))
                                        moveBuffer.RemoveAt(0)

                                    Next

                                    originalLines = CType(AList.ToArray(GetType(String)), String())
                                    RichTextBox1.Lines = originalLines


                                    Flush()
                                End If


                            ElseIf operation.Substring(0, 1) = "s" Then
                                Dim count As Integer = 1
                                Dim showCount As Integer

                                If operation.Length > 1 Then
                                    operation = Replace(operation, "s", "")

                                    Try
                                        count = CInt(operation)

                                    Catch ex As Exception
                                        Label3.Text = " Invalid Prompt Enter a valid integer number with s ( like s3 ) "
                                        count = -1

                                    End Try

                                End If

                                originalLines = RichTextBox1.Lines
                                Try
                                    showCount = CInt(originalLines(index).Substring(0, 1)) ' burada @ le falan guvenlik konulabilir 
                                    If count > hiddenArray(showCount).Count Then

                                        Label3.Text = " Invalid Prompt Show Number Cannot Be Bigger than Hidden Lines  "
                                        Return  'sonrasinda patliyo olabilir
                                    End If
                                Catch ex As Exception
                                    Label3.Text = " Unable to receive  "
                                    Return
                                End Try


                                Dim AList As ArrayList = New ArrayList(originalLines)



                                For counter As Integer = 1 To count
                                    If counter = 1 Then
                                        AList.RemoveAt(index)

                                        AList.Insert(index, hiddenArray(showCount)(0))
                                        hiddenArray(showCount).RemoveAt(0)


                                    Else

                                        AList.Insert(index + counter - 1, hiddenArray(showCount)(0))
                                        hiddenArray(showCount).RemoveAt(0)





                                    End If


                                Next

                                If 1 < hiddenArray(showCount).Count Then


                                    Dim PlaceHolder As String = CType(showCount, String) + "  --------" + CType(hiddenArray(showCount).Count, String) + " Line(s) Hidden ----------- " ' one hide available at the moment 
                                    AList.Insert(index + count, PlaceHolder)
                                End If


                                originalLines = CType(AList.ToArray(GetType(String)), String())
                                RichTextBox1.Lines = originalLines


                                Flush()


                            ElseIf operation.Substring(0, 1) = "x" Then
                                Dim count As Integer = 1

                                If operation.Length > 1 Then
                                    operation = Replace(operation, "x", "")

                                    Try
                                        count = CInt(operation)

                                    Catch ex As Exception
                                        Label3.Text = " Invalid Prompt Enter a valid integer number with x ( like x3 ) "
                                        count = -1

                                    End Try

                                    If count > lineCount Then
                                        Label3.Text = " Invalid Prompt Integer cannot be bigger than total line numbers"
                                        Return

                                    End If



                                End If

                                originalLines = RichTextBox1.Lines


                                hiddenArray(hiddenCount) = New ArrayList

                                Dim AList As ArrayList = New ArrayList(originalLines)
                                Dim PlaceHolder As String = CType(hiddenCount, String) + "  --------" + CType(count, String) + " Line(s) Hidden ----------- " ' one hide available at the moment 

                                For counter As Integer = 1 To count + 1
                                    If counter = 1 Then

                                        hiddenArray(hiddenCount).Add(AList(index))
                                        AList.Insert(index, PlaceHolder)

                                    Else

                                        hiddenArray(hiddenCount).Add(AList(index + 2))
                                        AList.RemoveAt(index + 1) ' save to a buffer 

                                    End If


                                Next

                                hiddenCount = hiddenCount + 1
                                originalLines = CType(AList.ToArray(GetType(String)), String())
                                RichTextBox1.Lines = originalLines

                            ElseIf operation.Substring(0, 1) = "i" Then
                                Dim count As Integer = 1

                                If operation.Length > 1 Then
                                    operation = Replace(operation, "i", "")

                                    Try
                                        count = CInt(operation)

                                    Catch ex As Exception
                                        Label3.Text = " Invalid Prompt Enter a valid integer number with i ( like i2 ) "
                                        count = -1

                                    End Try

                                End If

                                originalLines = RichTextBox1.Lines

                                Dim AList As ArrayList = New ArrayList(originalLines)
                                For counter As Integer = 1 To count
                                    AList.Insert(index, " ")
                                Next

                                originalLines = CType(AList.ToArray(GetType(String)), String())
                                RichTextBox1.Lines = originalLines
                            End If

                            If lineCount <> RichTextBox1.Lines().Length Then

                                lineCount = RichTextBox1.Lines().Length
                                Dim temp(lineCount) As String
                                For index1 As Integer = 0 To lineCount
                                    temp(index1) = "====="
                                Next
                                RichTextBox2.Lines = temp
                            End If

                        End If
                    End If
                End If
            Next

            '  lineCount = LineCount + 1


        End If






    End Sub




    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            Try
                Dim setline As Integer = CInt(TextBox1.Text)

                lineNumber = setline
                Label1.Text = "Line : " + CType(lineNumber, String)

                RichTextBox1.SelectionStart = RichTextBox1.GetFirstCharIndexFromLine(lineNumber)
                RichTextBox1.SelectionLength = RichTextBox1.Lines(lineNumber).Length
                RichTextBox1.ScrollToCaret()
                Return
            Catch ex As Exception

            End Try
            If TextBox1.Text.Substring(0, 2) = "up" Then
                Dim word As String = Replace(TextBox1.Text, " ", "")
                word = Replace(word, "up", "")

                Try
                    GetColumnAndLine()

                    Dim upline As Integer = CInt(word)
                    Dim newIndex As Integer
                    If lineNumber < upline Then
                        newIndex = 1 ' may be a problem may need to try 1 
                    Else
                        newIndex = lineNumber - upline + 1
                    End If
                    '  RichTextBox1.Focus()
                    ' RichTextBox1.SelectionStart = newIndex

                    RichTextBox1.SelectionStart = RichTextBox1.GetFirstCharIndexFromLine(newIndex)
                    RichTextBox1.SelectionLength = RichTextBox1.Lines(newIndex).Length


                    'RichTextBox1.SelectionLength = 0

                    RichTextBox1.ScrollToCaret()
                Catch ex As Exception
                    Label3.Text = "Invalid Prompt: Up function needs to be used with small integers (like up 5)"
                End Try


            ElseIf TextBox1.Text.Substring(0, 4) = "quit" Then ' return here 

                If isChanged Then
                    Dim answer = MessageBox.Show("Some files are changed, do you want to exit without saving ? ", "Warning", MessageBoxButtons.YesNoCancel)
                    If answer = DialogResult.Yes Then
                        Environment.Exit(0)
                    End If
                Else
                    Environment.Exit(0)
                End If






            ElseIf TextBox1.Text.Substring(0, 5) = "setcl" Then


                Dim word As String = Replace(TextBox1.Text, " ", "")
                word = Replace(word, "setcl", "")

                Try


                    Dim lineNum As Integer = CInt(word)
                    lineNumber = lineNum
                    Label1.Text = "Line : " + CType(lineNumber, String)

                    RichTextBox1.SelectionStart = RichTextBox1.GetFirstCharIndexFromLine(lineNumber)
                    RichTextBox1.SelectionLength = RichTextBox1.Lines(lineNumber).Length

                Catch ex As Exception
                    Label3.Text = "Invalid Prompt: setcl function needs to be used with small integers (like setcl 5)"
                End Try
                Return
            ElseIf TextBox1.Text.Substring(0, 4) = "left" Then
                Dim pt As Point
                Dim pt2 As Point
                SendMessage(RichTextBox1.Handle, EM_GETSCROLLPOS, 0, pt)
                pt2.X = pt.X - 30
                pt2.Y = pt.Y
                SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, pt2)
                Return
            ElseIf TextBox1.Text.Substring(0, 5) = "right" Then
                Dim pt As Point
                Dim pt2 As Point
                SendMessage(RichTextBox1.Handle, EM_GETSCROLLPOS, 0, pt)
                pt2.X = pt.X + 30
                pt2.Y = pt.Y
                SendMessage(RichTextBox1.Handle, EM_SETSCROLLPOS, 0, pt2)
                Return

            ElseIf TextBox1.Text.Substring(0, 4) = "down" Then
                Dim word As String = Replace(TextBox1.Text, " ", "")
                word = Replace(word, "down", "")

                Try
                    GetColumnAndLine()

                    Dim downline As Integer = CInt(word)
                    Dim newIndex As Integer
                    If lineCount - lineNumber < downline Then
                        newIndex = lineCount - 1  ' may be a problem may need to try 1 
                    Else
                        newIndex = lineNumber + downline
                    End If

                    RichTextBox1.SelectionStart = RichTextBox1.GetFirstCharIndexFromLine(newIndex)
                    RichTextBox1.SelectionLength = RichTextBox1.Lines(newIndex).Length
                    RichTextBox1.ScrollToCaret()
                Catch ex As Exception
                    Label3.Text = "Invalid Prompt: down function needs to be used with small integers (like down 5)"
                End Try

            ElseIf TextBox1.Text.Substring(0, 4) = "find" Then

                Dim word As String
                word = Replace(TextBox1.Text, "find", "")
                Dim args = word.Split(CType("/", Char()))

                If Not FindMyText(args(1)) Then
                    Label3.Text = "Unable to find the word :  " + args(1)
                End If
                Return
            ElseIf TextBox1.Text.Substring(0, 5) = "qquit" Then
                Environment.Exit(0)







            ElseIf TextBox1.Text.Substring(0, 7) = "replace" Then
                Dim word As String
                word = Replace(TextBox1.Text, "replace", "")
                Dim args = word.Split(CType("/", Char()))

                If Not FindMyText(args(1)) Then
                    Label3.Text = "Unable to find the word :  " + args(1)
                Else
                    RichTextBox1.Text = Replace(RichTextBox1.Text, args(1), args(2))
                    '  ReplaceInstances(RichTextBox1.Text, "17" As String() , "asd")
                End If


                Return



            End If

        End If


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object,
   ByVal e As System.EventArgs)
        ' Create an OpenFileDialog to request a file to open.
        openFile()


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Dim saveFile1 As New SaveFileDialog()

        ' Initialize the SaveFileDialog to specify the RTF extension for the file.
        saveFile1.DefaultExt = "*.rtf"
        saveFile1.Filter = "RTF Files|*.rtf"

        ' Determine if the user selected a file name from the saveFileDialog.
        If MyFileName.Length > 0 Then
            RichTextBox1.SaveFile(MyFileName,
                           RichTextBoxStreamType.RichText)
        Else
            If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
           And (saveFile1.FileName.Length) > 0 Then

                ' Save the contents of the RichTextBox into the file.
                RichTextBox1.SaveFile(saveFile1.FileName,
                    RichTextBoxStreamType.RichText)
            End If

        End If

        ' Save the contents of the RichTextBox into the file.


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Dim saveFile1 As New SaveFileDialog()

        ' Initialize the SaveFileDialog to specify the RTF extension for the file.
        saveFile1.DefaultExt = "*.rtf"
        saveFile1.Filter = "RTF Files|*.rtf"

        ' Determine if the user selected a file name from the saveFileDialog.
        If (saveFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
            And (saveFile1.FileName.Length) > 0 Then

            ' Save the contents of the RichTextBox into the file.
            RichTextBox1.SaveFile(saveFile1.FileName,
                RichTextBoxStreamType.PlainText)
        End If
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        openFile()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        saveFile()

    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        saveAs()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        openFile()

    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        saveFile()
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        MessageBox.Show("Welcome to Utku's Text Editor" + vbCrLf + "COMMAND Functions  " + vbCrLf + "Search:  find/keyword  " + vbCrLf + "Replace: replace/word to change / replacement" + vbCrLf + "Up: up number(to scrool up) " + vbCrLf + "Down: down number(to scrool down)" + vbCrLf + "Right/left scrool: right/left" + vbCrLf + "SUFFIX Commands" + vbCrLf + "i : inserts new line" + vbCrLf + "xn: hides lines(Multiple hides available)" + vbCrLf + "sn: Shows hidden lines (multiple shows available)" + vbCrLf + "a/b: puts previously buffered data(copy overrides cut)" + vbCrLf + "cn: copies next n lines" + vbCrLf + "mv cuts next n lines" + vbCrLf + "setcl: Sets the line" + vbCrLf + Chr(34) + ": Duplicates the line ", "Help", MessageBoxButtons.OK)

    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        Dim myValue As String = InputBox("Enter String Here", "Search", "Please Enter the Search Word")


        If Not FindMyText(myValue) Then
            Label3.Text = "Unable to find the word :   " + myValue
        End If

    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        If isChanged Then
            Dim answer = MessageBox.Show("Some files are changed, do you want to exit without saving ? ", "Warning", MessageBoxButtons.YesNoCancel)
            If answer = DialogResult.Yes Then
                Environment.Exit(0)
            End If
        Else
            Environment.Exit(0)
        End If
    End Sub
End Class



