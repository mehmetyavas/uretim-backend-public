using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static partial class Messages
    {
        public static string MachineDoesntExist => "Böyle bir Makine Yok!";
        public static string WorkOrderDoesntExist => "Böyle Bir İşemri Yok!";
        public static string WorkOrderDoesntBelongsToThisMachine => "İşemri Bu Makineye Ait Değil!";
        public static string ThisWorkOrderIsClosed => "İşemri Kapatılmış, Kalite İle İletişime Geçiniz";
        public static string EflowException => "Eflowda Hata Var, Kalite İle İletişime Geçiniz";
    }
}
