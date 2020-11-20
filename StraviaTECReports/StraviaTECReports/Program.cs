using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace StraviaTECReports
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            ReportController cr = new ReportController();
            Console.WriteLine(cr.Reporte_participantes("Endurance 2020","sam.astua"));
        }
    }
}
