using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static partial class Messages
    {

        public static string MinMaxDoesntExist => "Numune Bilgileri Girilmemiş!, Kalite İle İletişime Geçiniz!";

        public static string ProductMaterialsListError => "Hammadde Listelerken Hata Oluştu!";

        public static string ProductDeleted => "Üretim Silindi";
        public static string RawMaterialError => "Hammadde Kaydı Yapılırken Hata Oluştu!, Kalite İle İletişime Geçiniz!";
        public static string ColorMaterialError => "Boya Kaydı Yapılırken Hata Oluştu!, Kalite İle İletişime Geçiniz!";

        internal static string ProductUpdated => "Güncellendi";

        public static string TareIsGreaterThanZero => "Dara 0'dan Büyük Olmak Zorunda Darayı Kontrol Ediniz!";
        public static string UnitWeightIsGreaterThanZero => "Gramaj 0'dan Büyük Olmak Zorunda Gramaj Bilgilerini Kontrol Ediniz!";
        public static string GrossWeightIsEqualToNetPlusTare => "Brüt Ağırlık Hesaplanan Değerle Uyuşmuyor, Kalite İle İletişime Geçiniz!";
        public static string SerialNoExist => "Kayıt Ederken Hata Oluştu, Biraz Sonra Tekrar Deneyiniz!(SerialNoExist)";
        public static string MaxMinControl => "Kolinin Ağırlığı/Adeti Eksik veya Fazla!";
        public static string SerialNoDoesntExists => "Seri Numarası Bulunamadı!";

        public static string NotEnoughPackageException => "Koli/Poşet Eksik!, Kalite İle İletişime Geçin!";
        public static string NotEnoughRawMaterial => "Hammadde Eksik!, Kalite İle İletişime Geçin!";
        public static string NotEnoughColorMaterial => "Boya Eksik!, Kalite İle İletişime Geçin!";
        public static string ProductAmountIsBiggerThanWorkOrderAmount => "Üretim Miktarı İşemri Miktarını Aştı, Kalite İle İletişime Geçin!";

        public static string RawCodeDoesntMatch => "Bu Serinin Stok Kodu Bu Üretimin Ham Koduyla Eşleşmiyor!";
        public static string NotEnoughAssemblyRawMaterial => "Girmiş Olduğunuz Serinin Miktarı Yetersiz1";

    }
}
