using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NT106_Q14_DoAnGroup08.ClientCustomer
{
    public static class VietQR
    {
        public static string BuildTLV(string id, string value)
        {
            return id + value.Length.ToString("D2") + value;
        }

        public static string CRC16(string input)
        {
            byte[] data = Encoding.ASCII.GetBytes(input);
            ushort poly = 0x1021;
            ushort crc = 0x0000;

            foreach (byte b in data)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    crc = (ushort)(((crc & 0x8000) != 0) ? (crc << 1) ^ poly : (crc << 1));
                }
            }

            return crc.ToString("X4");
        }

        public static string Generate(string bin, string accountNo, int amount, string info)
        {
            // -------------------------
            // Merchant Account Info (ID 38)
            // -------------------------
            string guid = BuildTLV("00", "A000000727");  // VietQR ID
            string serviceCode = BuildTLV("01", "11");   // Loại dịch vụ
            string bankBin = BuildTLV("02", bin);        // Mã BIN ngân hàng
            string accField = BuildTLV("03", accountNo); // Số tài khoản

            string merchantAccount = BuildTLV("38", guid + serviceCode + bankBin + accField);

            // -------------------------
            // Các trường EMV chuẩn
            // -------------------------
            string payloadFormat = BuildTLV("00", "01");
            string initMethod = BuildTLV("01", "12"); // static QR
            string merchantCat = BuildTLV("52", "0000");
            string currency = BuildTLV("53", "704");
            string amountField = BuildTLV("54", amount.ToString("F0")); // số nguyên
            string countryCode = BuildTLV("58", "VN");
            string addInfo = BuildTLV("62", BuildTLV("01", info));

            // -------------------------
            // Chuỗi raw + CRC placeholder
            // -------------------------
            string raw = payloadFormat
                + initMethod
                + merchantAccount
                + merchantCat
                + currency
                + amountField
                + countryCode
                + addInfo
                + "63" + "04"; // CRC ID + độ dài 4

            // -------------------------
            // Thêm CRC16
            // -------------------------
            string crc = CRC16(raw);

            return raw + crc;
        }
    }
}
