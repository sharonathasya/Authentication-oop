using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Authentication
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public static List<User> Users = new List<User>();
        public static void Main(string[] args)
        { 

            int command;
            bool exit = false;
            while (exit == false)
            {
                Console.Clear();
                Console.WriteLine("\n============ BASIC AUTHENTICATION ============");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Search User");
                Console.WriteLine("3. Login");
                Console.WriteLine("4. Exit");
                Console.Write("Menu Dipilih : ");
                try
                {
                    command = int.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("\n============ Create User ============");
                            Program.Create();
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("\n============= Search User ============");
                            Search();
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("\n============= Login ============");
                            Login(Users);
                            break;
                        case 4:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Inputan Anda Salah, masukkan angka 1-4");
                            Console.ReadKey();
                            break;
                    }

                }
                catch (FormatException )
                {
                    Console.WriteLine("Input harus berupa angka");
                    Console.Write("Tekan apa saja untuk melanjutkan.");
                    Console.ReadKey();
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Input tidak sesuai");
                    Console.Write("Tekan apa saja untuk melanjutkan.");
                    Console.ReadKey();
                }
            }
        }

        public static void Search()
        {
            Console.WriteLine("Masukkan Username dari User yang akan dicari.");
            Console.WriteLine("----------------------------------------------------");
            string userName = Console.ReadLine();
            User User = Users.FirstOrDefault(x => x.UserName == userName);
            if (User == null)
            {
                Console.Write("User tersebut tidak dapat ditemukan. Tekan apa saja untuk melanjutkan");
                Console.ReadKey();
                return;
            }
            else
            {
                PrintUser(User);
                Console.Write("Tekan apa saja untuk melanjutkan.");
                Console.ReadKey();
            }
        }
       

        public static void PrintUser(User User)
        {
            Console.WriteLine($"First Name: {User.FirstName}");
            Console.WriteLine($"Last Name: {User.LastName}");
            Console.WriteLine($"UserName: {User.UserName}");
            Console.WriteLine($"Password: {User.Password}");
            Console.WriteLine("----------------------------------------------------");
        }

        public static void Show()
        {

            if (Users.Count == 0)
            {
                Console.WriteLine("Tidak ada User terdaftar. Tekan apa saja untuk melanjutkan.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Berikut list User:\n");
            foreach (var User in Users)
            {
                PrintUser(User);
            }
            Console.WriteLine("\nTekan apa saja untuk melanjutkan.");
            Console.ReadKey();
        }

        public  static void Login(List<User> Users)
        {
            Console.Clear();
            Console.Write("Input Username : ");
            string user = Console.ReadLine();

            Console.Write("Input Password : ");
            string pass = Console.ReadLine();

            if (Users.Exists(item => item.UserName == user && BCrypt.Net.BCrypt.Verify(pass, item.Password)))
            {
                Console.WriteLine("Login Berhasil!!");
                //Console.ReadKey();
                Console.Clear();
                int command;
                bool exit = false;
                while (exit == false)
                {
                    Console.Clear();
                    Console.WriteLine("\n============ Menu Login ============");
                    Console.WriteLine("1. Show User");
                    Console.WriteLine("2. Update User");
                    Console.WriteLine("3. Remove User");
                    Console.WriteLine("4. Exit");
                    Console.Write("Menu Dipilih : ");
                    try
                    {
                        command = int.Parse(Console.ReadLine());
                        switch (command)
                        {


                            case 1:
                                Console.Clear();
                                Console.WriteLine("\n============= Show Listed User ============");
                                Console.WriteLine("\n");
                                Show();
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("\n============= Update Password ============");
                                Program.Update();
                                break;
                            case 3:
                                Console.Clear();
                                Program.Remove();
                                break;
                            case 4:
                                exit = true;
                                break;

                            default:
                                Console.WriteLine("Inputan Anda Salah, masukkan angka 1-4");
                                Console.ReadKey();
                                break;
                        }

                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Input harus berupa angka");
                        Console.Write("Tekan apa saja untuk melanjutkan.");
                        Console.ReadKey();
                    }
                }
            }
            else if (Users.Exists(item => item.UserName != user))
            {
                Console.WriteLine("Username Salah");
                Console.ReadKey();
            }
            else if (Users.Exists(item => item.UserName == user && BCrypt.Net.BCrypt.Verify(pass, item.Password) == false))
            {
                Console.WriteLine("Password Salah");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Username Tidak DItemukan");
            }

            
        }

    }
}
