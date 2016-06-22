Imports System.IO

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form
    Dim fileName As String
    Dim textContents As String
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TextBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RichTextBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RichTextBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(785, 533)
        Me.SplitContainer1.SplitterDistance = 261
        Me.SplitContainer1.TabIndex = 0
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(218, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(122, 20)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "sdi1.txt"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(136, 12)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Open File "
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Split /Unsplit"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 38)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(782, 225)
        Me.RichTextBox1.TabIndex = 0
        Me.RichTextBox1.Text = ""
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichTextBox2.Location = New System.Drawing.Point(0, 0)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.Size = New System.Drawing.Size(785, 268)
        Me.RichTextBox2.TabIndex = 0
        Me.RichTextBox2.Text = ""
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(347, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 23)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Save Changes"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(785, 533)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents RichTextBox2 As RichTextBox

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

        Dim textContents As String = RichTextBox1.Rtf


        Clipboard.SetText(textContents, TextDataFormat.Rtf)
        If (Clipboard.ContainsText(TextDataFormat.Rtf)) Then
            ' Paste the text contained on the clipboard into a DIFFERENT RichTextBox
            RichTextBox2.Rtf = Clipboard.GetText(TextDataFormat.Rtf)
        End If

    End Sub

    Friend WithEvents Button1 As Button

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If SplitContainer1.Panel2Collapsed = False Then

            SplitContainer1.Panel2Collapsed = True

        Else
            SplitContainer1.Panel2Collapsed = False

        End If
    End Sub

    Private Sub RichTextBox2_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox2.TextChanged
        Dim textContents As String = RichTextBox2.Rtf


        Clipboard.SetText(textContents, TextDataFormat.Rtf)
        If (Clipboard.ContainsText(TextDataFormat.Rtf)) Then
            ' Paste the text contained on the clipboard into a DIFFERENT RichTextBox
            RichTextBox1.Rtf = Clipboard.GetText(TextDataFormat.Rtf)
        End If
    End Sub

    Friend WithEvents Button2 As Button

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim openFile1 As New OpenFileDialog()

        ' Initialize the OpenFileDialog to look for RTF files.
        openFile1.DefaultExt = "*.txt"
        openFile1.Filter = "*.txt"

        ' Determine whether the user selected a file from the OpenFileDialog.
        If (openFile1.ShowDialog() = System.Windows.Forms.DialogResult.OK) _
        And (openFile1.FileName.Length > 0) Then

            fileName = openFile1.FileName

            ' Load the contents of the file into the RichTextBox.
            If openFile1.FileName.Contains(".txt") Then
                ' RichTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText) ' normally filters for rtf documents but if txt selected it also parses it accordingly
                RichTextBox2.LoadFile(fileName, RichTextBoxStreamType.PlainText)
                RichTextBox1.LoadFile(fileName, RichTextBoxStreamType.PlainText)

            Else
                RichTextBox1.LoadFile(openFile1.FileName)
                RichTextBox2.LoadFile(openFile1.FileName)
            End If

        End If
        '  Return 0


        '   Dim myPath = " C:\temp\oscourse\"

        '  fileName = myPath + TextBox1.Text
        ' fileName = "C:\temp\oscourse\sdi1.txt"
        '  Dim openFile1 As New OpenFileDialog()


        Try
            ' Open the file using a stream reader.
            Using sr As New StreamReader(fileName)
                Dim line As String


                ' Read the stream to a string and write the string to the console.
                line = sr.ReadToEnd()




                Console.WriteLine(line)

                '  RichTextBox2.LoadFile(fileName, RichTextBoxStreamType.PlainText)
                '  RichTextBox1.LoadFile(fileName, RichTextBoxStreamType.PlainText)




            End Using
        Catch err As Exception
            Console.WriteLine("The file could not be read:")
            Console.WriteLine(err.Message)
        End Try
    End Sub

    Friend WithEvents TextBox1 As TextBox

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Friend WithEvents Button3 As Button

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        RichTextBox2.SaveFile(fileName, RichTextBoxStreamType.PlainText)
    End Sub
End Class
