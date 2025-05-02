using System;
using System.Windows.Forms;

internal static class Program
{
  [STAThread]
  private static void Main()
  {
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    var view = new SyncView();
    var presenter = new SyncPresenter(view);

    Application.Run(view);
  }
}