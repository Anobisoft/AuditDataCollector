using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Windows.Forms;

namespace AuditDataCollector
{
    public class ClassOptions
    {

        public int DBPort;
        public String DBHost, DBUser, DBPass, DBName;
        RegistryKey rkey;
        byte[] entropy;

        public ClassOptions()
        {
            rkey = Registry.CurrentUser.CreateSubKey("software\\AuditDataCollector");
            entropy = Encoding.UTF8.GetBytes("cegthlegthvtufgegth");
        }

        public Boolean Get()
        {
            //Проверяем количество значений ключа (если их будет не 5(!) то возварщаем fail)
            if (rkey.ValueCount != 5) return false;
            try
            {
                DBHost = rkey.GetValue("DBHost").ToString();
                DBPort = (int)rkey.GetValue("DBPort");
                DBUser = rkey.GetValue("DBUser").ToString();
                DBPass = rkey.GetValue("DBPass").ToString();
                if (DBPass != "")
                {
                    byte[] pp = (byte[])rkey.GetValue("DBPass");
                    DBPass = Encoding.UTF8.GetString(ProtectedData.Unprotect(pp, entropy, DataProtectionScope.CurrentUser));
                }
                DBName = rkey.GetValue("DBName").ToString();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некоторые параметры подключения не могут быть прочитаны:\n" + ex.Message, "Ошибка чтения реестра");
                return false;
            }
        }

        public void Set()
        {
            try
            {
                rkey.SetValue("DBHost", DBHost);
                rkey.SetValue("DBPort", DBPort);
                rkey.SetValue("DBUser", DBUser);
                if (DBPass != "")
                {
                    byte[] up = Encoding.UTF8.GetBytes(DBPass);
                    rkey.SetValue("DBPass", ProtectedData.Protect(up, entropy, DataProtectionScope.CurrentUser));
                }
                rkey.SetValue("DBName", DBName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
