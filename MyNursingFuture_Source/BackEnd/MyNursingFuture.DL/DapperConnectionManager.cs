using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.Util;
using Dapper;
using System.Transactions;
using MyNursingFuture.DL.Models;
using System.Data;

namespace MyNursingFuture.DL
{
    public class QueryEntity
    {
        public object Entity { get; set; }
        public string Query { get; set; }
    }
    public class DapperConnectionManager
    {
        /// <summary>
        /// Used for insert, delete or update queries
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queries"></param>
        /// <returns></returns>
        public Result ExecuteQueries(List<QueryEntity> queries)
        {
            var result = new Result();

            using (var scope = new TransactionScope())
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {
                    foreach (var item in queries)
                    {
                        result.Entity = con.Execute(item.Query, item.Entity);
                    }
                    scope.Complete();
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }
        /// <summary>
        /// Executes a single query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        public Result ExecuteQuery(QueryEntity query)
        {
            var result = new Result();

            using (TransactionScope scope = new TransactionScope())
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {

                    result.Entity = con.Query(query.Query, query.Entity);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// Executes a single query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        public Result ExecuteQuery<T>(QueryEntity query)
        {
            var result = new Result();

            using (TransactionScope scope = new TransactionScope())
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {
                    result.Entity = con.Query<T>(query.Query, query.Entity);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }

        /// <summary>
        /// Insert Single and return Id inserted
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        public Result InsertQuery(QueryEntity query)
        {
            var result = new Result();

            using (TransactionScope scope = new TransactionScope())
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {
                    query.Query = string.Concat(query.Query, ";select SCOPE_IDENTITY();");
                    result.Entity = con.Query<int>(query.Query, query.Entity).First();
                    scope.Complete();
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }

        public Result InsertQueryUnScoped(QueryEntity query)
        {
            var result = new Result();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {
                    query.Query = string.Concat(query.Query, ";select SCOPE_IDENTITY();");
                    result.Entity = con.Query<int>(query.Query, query.Entity).First();
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }

        public Result ExecuteQueryUnScoped(QueryEntity query)
        {
            var result = new Result();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {
                    result.Entity = con.Query(query.Query, query.Entity);
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }


        public Result ExecuteQueryUnScoped<T>(QueryEntity query)
        {
            var result = new Result();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {
                    result.Entity = con.Query<T>(query.Query, query.Entity);
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }

        public Result ExecuteStoredProcedure(QueryEntity query)
        {
            var result = new Result();
            using (TransactionScope scope = new TransactionScope())
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))

                try
                {
                    con.Open();
                    result.Entity = con.Query(query.Query, query.Entity, commandType: CommandType.StoredProcedure);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }

            return result;
        }
        /*        public Result ExecuteTransaction(List<QueryEntity> queries)


                {
                    var result = new Result();
                    using (TransactionScope scope = new TransactionScope())
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))

                        try
                        {
                            con.Open();
                            result.Entity = con.Query(query.Query, query.Entity, commandType: CommandType.StoredProcedure);
                            scope.Complete();
                        }
                        catch (Exception e)
                        {
                            Logger.Log(e);
                            result.Success = false;
                            result.Message = e.Message;
                        }

                    return result;
                }*/

        public Result ExecuteGetOneItemQuery<T>(QueryEntity query)
        {
            var result = new Result();

            using (TransactionScope scope = new TransactionScope())
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNursingFutureConnection"].ConnectionString))
            {
                try
                {
                    result.Entity = con.QuerySingle<T>(query.Query, query.Entity);
                    scope.Complete();
                }
                catch (Exception e)
                {
                    Logger.Log(e);
                    result.Success = false;
                    result.Message = e.Message;
                }
            }
            return result;
        }


    }
}
