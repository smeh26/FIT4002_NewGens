using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IAdministratorsManager : IManager<AdministratorEntity>
    {
        Result LogIn(AdministratorEntity entity);
        Result Insert(AdministratorEntity entity, bool seal = false);
    }
    public class AdministratorsManager: IAdministratorsManager
    {
        public Result SetPublished(int id, bool published = true)
        {
            throw new InvalidOperationException();
        }
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT * FROM Administrators"
            };
            return con.ExecuteQuery<AdministratorEntity>(query);
        }

        public Result Get(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            query.Query = @"SELECT * FROM Administrators
                            where AdministratorId = @AdministratorId and Sealed = 0";
            query.Entity = new { AdministratorId = id };

            var result = con.ExecuteQuery<AdministratorEntity>(query);

            if (!result.Success)
            {
                result.Message = "Definition not found";
                return result;
            }

            var r = (IEnumerable<AdministratorEntity>)result.Entity;

            result.Entity = r.FirstOrDefault();

            if (result.Entity == null)
                result.Success = false;

            return result;
        }


        public Result Update(AdministratorEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var result = new Result();
            if (!string.IsNullOrEmpty(entity.Password))
            {
                if (entity.Password.Length < 6)
                {
                    result = new Result(false);
                    result.Message = "Password length invalid";
                    return result;
                }
                var credentials = new CredentialsManager();
                var hash = credentials.GenerateSalt();
                entity.Password = credentials.EncodePassword(entity.Password, hash);
                entity.Hash = hash;
                query.Query = @"UPDATE Administrators set Name = @Name, Password = @Password, Hash = @Hash where AdministratorId = @AdministratorId";
            }
            else
            {
                query.Query = @"UPDATE Administrators set Name = @Name where AdministratorId = @AdministratorId";
            }
            
            query.Entity = entity;
            result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The Adminsitrator has been updated" : "An error occurred";
            result.Entity = entity.AdministratorId;
            return result;
        }

       
        public Result LogIn(AdministratorEntity entity)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity();
            var credentials = new CredentialsManager();
            entity.Username = entity.Username.Trim().ToLower();
            query.Query = @"SELECT * FROM Administrators
                            where Username = @Username";
            query.Entity = entity;
            var result = con.ExecuteQuery<AdministratorEntity>(query);

            if (!result.Success)
            {
                result.Message = "Login error";
                return result;
            }

            var r = (IEnumerable<AdministratorEntity>)result.Entity;

            var admin = r.FirstOrDefault();

            if (admin == null)
            {
                result.Message = "User not found";
                result.Success = false;
                return result;
            }
            var password = credentials.EncodePassword(entity.Password, admin.Hash);
            if (password == admin.Password)
            {
                admin.Hash = null;
                admin.Password = null;
                admin.Token = credentials.GenerateAdminToken(admin);
                result.Entity = admin;
                return result;
            }

            result.Message = "Incorrect password";
            result.Success = false;
            return result;
        }

        public Result Insert(AdministratorEntity entity, bool seal = false)
        {
            var result = new Result();
            if (entity.Password.Length < 6)
            {
                result = new Result(false);
                result.Message = "Password length invalid";
                return result;
            }

            var con = new DapperConnectionManager();
            var query = new QueryEntity();

            var credentials =  new CredentialsManager();

            var hash = credentials.GenerateSalt();
            
            entity.Password = credentials.EncodePassword(entity.Password, hash);
            entity.Hash = hash;
            entity.Username = entity.Username.Trim().ToLower();
            entity.Sealed = seal;

            query.Entity = entity;
            query.Query = @"INSERT INTO Administrators (Username, Password, Hash, Sealed, Name) VALUES(@Username, @Password, @Hash, @Sealed, @Name)";

            result = con.InsertQuery(query);
            result.Message = result.Success ? "The administrator has been created" : "An error occurred";
            result.Entity = entity;
            return result;
        }

        public Result Delete(int id)
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity()
            {
                Query = @"DELETE FROM Administrators WHERE AdministratorId = @AdministratorId and Sealed = 0",
                Entity = new { AdministratorId = id }
            };
            var result = con.ExecuteQuery(query);
            result.Message = result.Success ? "The Administrator has been deleted" : "An error occurred";
            return result;
        }

        public Result Insert(AdministratorEntity entity)
        {
            return Insert(entity, false);
        }
    }
}
