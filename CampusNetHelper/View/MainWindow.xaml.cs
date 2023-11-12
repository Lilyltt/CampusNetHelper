using System.ComponentModel;
using System.Windows;
using CampusNetHelper.Model;
using CampusNetHelper.ViewModel;
using Panuon.WPF.UI;
using Panuon.WPF.UI.Configurations;

namespace CampusNetHelper.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly Configure Configure;
    public MainWindow()
    {
        InitializeComponent();
        Configure = new Configure();
        var config = Configure.LoadConfig();
        if (config.Success)
        {
            DataContext = config.Result;
        }
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        var viewModel = DataContext as MainWindowViewModel;
        if (viewModel != null)
        {
            Configure.SaveConfig(viewModel);
        }
    }

    private void Login_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Account.Text) || string.IsNullOrEmpty(PassWord.Text))
        {
            MessageBoxX.Show(this,"用户名/密码不能为空!","错误",MessageBoxButton.OK,MessageBoxIcon.Error,DefaultButton.YesOK,2);
            return;
        }
        if (Operator.Login(Account.Text, PassWord.Text))
        {
            if ((bool)AutoExit.IsChecked)
            {
                var result = MessageBoxX.Show(this,
                    "登录成功!即将自动退出",
                    "消息",
                    MessageBoxButton.YesNo,
                    MessageBoxIcon.Info,
                    DefaultButton.YesOK,
                    new MessageBoxXSetting
                    {
                        YesButtonContent = "好的",
                        NoButtonContent = "取消",
                        InverseButtonsSequence = true
                    },
                    3);
                switch(result)
                {
                    case MessageBoxResult.Yes:
                        var viewModel = DataContext as MainWindowViewModel;
                        if (viewModel != null)
                        {
                            Configure.SaveConfig(viewModel);
                        }
                        Environment.Exit(0);
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else
            {
                MessageBoxX.Show(this,"登录成功!","消息",MessageBoxButton.OK,MessageBoxIcon.Info,DefaultButton.YesOK,2);
            }
        }
        else
        {
            MessageBoxX.Show(this,"登录失败!","错误",MessageBoxButton.OK,MessageBoxIcon.Error,DefaultButton.YesOK,2);
        }
    }
    private void LoginOut_OnClick(object sender, RoutedEventArgs e)
    {
        if (Operator.LogOut())
        {
            MessageBoxX.Show(this,"退出成功!","消息",MessageBoxButton.OK,MessageBoxIcon.Info,DefaultButton.YesOK,2);
        }
        else
        {
            MessageBoxX.Show(this,"退出失败!","错误",MessageBoxButton.OK,MessageBoxIcon.Error,DefaultButton.YesOK,2);
        }
    }
    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (AutoLogin.IsChecked != null && (bool)AutoLogin.IsChecked)
        {
            Login_OnClick(null,null);
        }
    }

    private void AutoStart_OnChecked(object sender, RoutedEventArgs e)
    {
        MessageBoxX.Show(this, $"请将本程序的快捷方式放至{Environment.GetFolderPath(Environment.SpecialFolder.Startup)}下","消息",MessageBoxButton.OK,MessageBoxIcon.Info,DefaultButton.YesOK,5);
        AutoStart.IsChecked = false;
    }
}