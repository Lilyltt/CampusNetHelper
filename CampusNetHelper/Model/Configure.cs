using System.IO;
using CampusNetHelper.ViewModel;
using MadMilkman.Ini;

namespace CampusNetHelper.Model;

public class Configure
{
    public void SaveConfig(MainWindowViewModel viewModel)
    {
        IniFile config = new IniFile();
        IniSection section = config.Sections.Add("BaseConfig");
        section.Serialize(viewModel);
        config.Save($@"{AppDomain.CurrentDomain.BaseDirectory}config.ini");
    }

    public (MainWindowViewModel Result,bool Success) LoadConfig()
    {
        var options = new IniOptions();
        var config = new IniFile(options);
        if(!File.Exists($@"{AppDomain.CurrentDomain.BaseDirectory}config.ini")) return (new MainWindowViewModel(),false);
        config.Load($@"{AppDomain.CurrentDomain.BaseDirectory}config.ini");
        var viewModel = config.Sections[0].Deserialize<ViewModel.MainWindowViewModel>();
        return (viewModel,true);
    }
}