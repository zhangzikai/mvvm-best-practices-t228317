Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils.MVVM
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace DxSampleSimpleBehaviors
    Partial Public Class ConfirmationBehaviorFluentAPIUserControl
        Inherits DevExpress.XtraEditors.XtraUserControl
        Public Sub New() 
            InitializeComponent()
            Dim mvvmContext As New MVVMContext()
            mvvmContext.ContainerControl = Me
        
            Dim editor As New CheckEdit()
            editor.Dock = DockStyle.Top
            editor.Text = "Please, try to change checked state of this editor"
            editor.Parent = Me
        

            ' UI binding for the generic ConfirmationBehavior behavior with some specific parameters
            mvvmContext.WithEvent(Of ChangingEventArgs)(editor, "EditValueChanging").
                Confirmation(Sub(behavior)
                                 behavior.Caption = "CheckEdit State changing"
                                 behavior.Text = "This checkEdit's checked-state is about to be changed. Are you sure?"
                             End Sub)
        End Sub
         Private Sub OnDisposing()
            Dim context = MVVMContext.FromControl(Me)
            If context IsNot Nothing Then
                context.Dispose()
            End If
        End Sub
    	End Class
End Namespace