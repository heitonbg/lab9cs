using System;
using System.Windows.Forms;
using FileSync.Views;
using FileSync.Logging;
using FileSync.Presenter;

namespace FileSync
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      var view = new SyncView();
      var logManagerFactory = new Func<bool, ILogManager>(useJson =>
      {
        if (useJson)
        {
          return new JsonLogManager("sync_log.json");
        }
        else
        {
          return new XmlLogManager("sync_log.xml");
        }
      });
      
      var presenter = new SyncPresenter(view, logManagerFactory);

      Application.Run(view);
    }
  }
}