using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using KiCadSnippetManager.Models;
using KiCadSnippetManager.ViewModels;

using SettingsLibrary;

namespace KiCadSnippetManager;

public partial class App : Application
{
   public static readonly MainViewModel MainVM = new();
   public static SettingsModel Settings { get; private set; } = new();
   protected override void OnStartup(StartupEventArgs e)
   {
      Settings = SettingsManager.OnStartup<SettingsModel>(nameof(KiCadSnippetManager)) ?? new();
      base.OnStartup(e);
   }
   protected override void OnExit(ExitEventArgs e)
   {
      SettingsManager.OnExit(Settings, nameof(KiCadSnippetManager));
      base.OnExit(e);
   }

   public static void SaveSettings()
   {
      SettingsManager.OnExit(Settings, nameof(KiCadSnippetManager));
   }
}
