Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Utils.MVVM
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System
Imports System.Drawing
Imports System.Windows.Forms

Namespace DxSampleEventToCommandBehavior
    Partial Public Class UsingCommandsWithThirdPartyControlsUserControl
        Inherits DevExpress.XtraEditors.XtraUserControl
        Public Sub New() 
            InitializeComponent()
            Dim mvvmContext As New MVVMContext()
            mvvmContext.ContainerControl = Me
        
            Dim thirdPartyButton As New Button()
            thirdPartyButton.Dock = DockStyle.Top
            thirdPartyButton.Text = "Execute Command"
            thirdPartyButton.Parent = Me
        

            mvvmContext.ViewModelType = GetType(ViewModel)
            ' UI binding for the ClickToSayHello behavior
            mvvmContext.AttachBehavior(Of ClickToSayHello)(thirdPartyButton)
        End Sub
         Private Sub OnDisposing()
            Dim context = MVVMContext.FromControl(Me)
            If context IsNot Nothing Then
                context.Dispose()
            End If
        End Sub
        Public Class ViewModel
            Protected ReadOnly Property MessageBoxService() As IMessageBoxService
                Get
                    Return Me.GetService(Of IMessageBoxService)()
                End Get
            End Property
            Public Sub SayHello()
                MessageBoxService.ShowMessage("Hello!")
            End Sub
        End Class
        Public Class ClickToSayHello
            Inherits DevExpress.Utils.MVVM.EventToCommandBehavior(Of ViewModel, EventArgs)
            Public Sub New()
                MyBase.New("Click", Sub(x) x.SayHello())
            End Sub
        End Class
    	End Class
End Namespace