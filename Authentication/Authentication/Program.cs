using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;


namespace Authentication
{
    public class Program:User
    {
        public static void Create()
        {
            bool check = false;
            User Data = new User();
            do
            {
                Console.Write("First Name = ");
                Data.FirstName = Console.ReadLine();

                Console.Write("Last Name = ");
                Data.LastName = Console.ReadLine();
                if (Data.FirstName == "" || Data.LastName == "")
                {
                    Console.WriteLine("Input tidak boleh kosong");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    check = true;
                }

            } while (check == false);
           
            
            z:
            Console.Write("Password (min. 8 karakter) = ");
            string pass = Console.ReadLine();
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var isValidated = hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasMinimum8Chars.IsMatch(pass);
            if (isValidated)
            {
                Data.Password = BCrypt.Net.BCrypt.HashPassword(pass);
            }
            else
            {
                Console.WriteLine("Password Harus Gabungan Angka, Huruf dan Simbol");
                goto z;
            }

            Random rnd = new Random();
            int rPertama = rnd.Next(1, 99);  
            int rKedua = rnd.Next(100);     
            Data.UserName = Data.FirstName.Substring(0, 2) + Data.LastName.Substring(0, 2) + rPertama + rKedua;
            if (Users.Exists(item => item.UserName == Data.UserName))
            {
               Data.UserName = Data.FirstName.Substring(0, 2) + Data.LastName.Substring(1, 2) + rPertama + rKedua;
            }
            Console.WriteLine($"Username : {Data.UserName}");

            Users.Add(Data);
           
        }

        public static void Remove()
        {
            Console.WriteLine("Masukkan Username dari User yang ingin dihapus.");
            string userName = Console.ReadLine();
            User User = Users.FirstOrDefault(x => x.UserName == userName);
            if (User == null)
            {
                Console.Write("User tersebut tidak dapat ditemukan. Tekan apa saja untuk melanjutkan");
                Console.ReadKey();
                return;
            }

            Console.Write("Yakin akan menghapus User ini? (Y/T)\n");
            PrintUser(User);

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Users.Remove(User);
                Console.Write("User terhapus. Tekan apa saja untuk melanjutkan.");
                Console.ReadKey();
            }
        }

        public static void Update()
        {
            Console.WriteLine("Masukkan Username dari User yang akan diupdate.");
            Console.WriteLine("----------------------------------------------------");
            string userName = Console.ReadLine();
            User User = Users.FirstOrDefault(x => x.UserName == userName);
            if (User == null)
            {
                Console.Write("User tersebut tidak dapat ditemukan. Tekan apa saja untuk melanjutkan");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("----------------------------------------------------");
            Console.Write("Yakin akan mengupdate User ini? (Y/T)\n");
            PrintUser(User);

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                // Define a regular expression for repeated words.
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");

                z:
                Console.Write("\nPassword Terbaru: ");
                string newPass = Console.ReadLine();

                var isValidated = hasNumber.IsMatch(newPass) && hasUpperChar.IsMatch(newPass) && hasMinimum8Chars.IsMatch(newPass);
                if (isValidated)
                {
                    User.Password = BCrypt.Net.BCrypt.HashPassword(newPass);
                    Console.Clear();
                    Console.WriteLine("-----------------------------------------------");
                    PrintUser(User);
                    Console.Write("User sudah terupdate. Tekan apa saja untuk melanjutkan.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Password Harus Gabungan Angka, Huruf dan Simbol");
                    goto z;
                }
                
            }

        }

    }
}
