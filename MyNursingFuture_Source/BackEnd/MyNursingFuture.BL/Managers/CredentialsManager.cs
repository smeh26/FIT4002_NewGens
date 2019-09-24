using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Util;
using Newtonsoft.Json;

namespace MyNursingFuture.BL.Managers
{
    public interface ICredentialsManager
    {
        Result ValidateAdminToken(string token);
        Result ValidateUserToken(string token, bool recover = false);
    }

    public class CredentialsManager: ICredentialsManager
    {
        private string SecretAdminKey => "Bb3NQPLCEvrTQSPI9To7eOm7A1OKP2ueUb2m6Cr";
        private string SecretUserKey =>  "YS8A0fIf0y4S4yNFyajSUyhNExRaZoVJFcr84kg";
        private string SecretRecoverKey =>  "yNFyajSUyhYS8A0fIf0y4SNEx7eOm7A1OKPkPLg";

        public string GenerateSalt() //length of salt    
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var randNum = new Random();
            var chars = new char[31];
            var allowedCharCount = allowedChars.Length -1;
            for (var i = 0; i <= 31 - 1; i++)
            {
                chars[i] = allowedChars[Convert.ToInt32((allowedCharCount) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public string EncodePassword(string pass, string salt) //encrypt password    
        {
            byte[] bytes = Encoding.Unicode.GetBytes(pass);
            byte[] src = Encoding.Unicode.GetBytes(salt);
            byte[] dst = new byte[src.Length + bytes.Length];
            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return EncodePasswordMd5(Convert.ToBase64String(inArray));
        }
        public string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            MD5 md5 = new MD5CryptoServiceProvider();
            var originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            var encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }
        public string Base64Encode(string sData) // Encode    
        {
            try
            {
                var encDataByte = System.Text.Encoding.UTF8.GetBytes(sData);
                var encodedData = Convert.ToBase64String(encDataByte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public string Base64Decode(string sData) //Decode    
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                var utf8Decode = encoder.GetDecoder();
                var todecodeByte = Convert.FromBase64String(sData);
                var charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                var decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                var result = new string(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }

        public string GenerateAdminToken(AdministratorEntity admin)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddDays(30).Subtract(utc0).TotalSeconds; // Expiration time is up to 1 hour, but lets play on safe side

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var payload = new Dictionary<string, object>
            {
                { "AdministratorId", admin.AdministratorId },
                { "Username", admin.Username },
                { "exp", exp },
                { "iat", iat }
            };

            var token = encoder.Encode(payload, SecretAdminKey);
            return token;
        }

        public string GenerateUserToken(UserEntity user)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddDays(30).Subtract(utc0).TotalSeconds;

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var payload = new Dictionary<string, object>
            {
                { "UserId", user.UserId },
                { "Email", user.Email },
                { "Name", user.Name },
                { "exp", exp },
                { "iat", iat }
            };

            var token = encoder.Encode(payload, SecretUserKey);
            return token;
        }

        public string GenerateEmployerToken(EmployerEntity employer)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddDays(30).Subtract(utc0).TotalSeconds;

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var payload = new Dictionary<string, object>
            {
                { "EmployerID", employer.EmployerID },
                { "Email", employer.Email },
                { "EmployerName", employer.EmployerName },
                { "exp", exp },
                { "iat", iat }
            };

            var token = encoder.Encode(payload, SecretUserKey);
            return token;
        }

        public string GenerateRecoverPasswordToken(UserEntity user)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddDays(1).Subtract(utc0).TotalSeconds;

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var payload = new Dictionary<string, object>
            {
                { "UserId", user.UserId },
                { "exp", exp },
                { "iat", iat }
            };

            var token = encoder.Encode(payload, SecretRecoverKey);
            return token;
        }

        public string GenerateRecoverPasswordToken(EmployerEntity employer)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddDays(1).Subtract(utc0).TotalSeconds;

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var payload = new Dictionary<string, object>
            {
                { "UserId", employer.EmployerID },
                { "exp", exp },
                { "iat", iat }
            };

            var token = encoder.Encode(payload, SecretRecoverKey);
            return token;
        }

        public Result ValidateAdminToken(string token)
        {
            var secret = SecretAdminKey;
            var result = new Result();
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);
                var admin = JsonConvert.DeserializeObject<AdministratorEntity>(json);
                result.Entity = admin;
            }
            catch (Exception)
            {
                result.Success = false;
            }
            return result;
        }

        public Result ValidateUserToken(string token, bool recover = false)
        {
            var secret = recover?SecretRecoverKey:SecretUserKey;
            var result = new Result();
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                var json = decoder.Decode(token, secret, verify: true);
                var user = JsonConvert.DeserializeObject<UserEntity>(json);
                user.Token = token;
                result.Entity = user;
            }
            catch (Exception)
            {
                result.Success = false;
            }
            return result;
        }


    }
}
