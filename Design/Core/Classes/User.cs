using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceKazonberriesExpress.Core.Classes
{
    public class User
    {
        [BsonIgnoreIfDefault]
        public ObjectId _id;
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string? ITN { get; set; }
        public string? PassportData { get; set; }

        public User(string fullName, string password, string email, string phoneNumber, string role)
        {
            FullName = fullName;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
        }

        public User(string fullName, string password, string email, string phoneNumber, string role, string? passportData, string? itn)
            : this(fullName, password, email, phoneNumber, role)
        {
            PassportData = passportData;
            ITN = itn;
        }
    }
}
