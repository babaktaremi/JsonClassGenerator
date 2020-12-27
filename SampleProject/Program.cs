using System;
//using JsonClass.Tools;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = JsonClass.Tools.Settings.IdentitySettings.PasswordRequireDigit; //true
            var t2= JsonClass.Tools.Settings.JwtSettings.SecretKey; //secretKey 
            //...
        }
    }
}
