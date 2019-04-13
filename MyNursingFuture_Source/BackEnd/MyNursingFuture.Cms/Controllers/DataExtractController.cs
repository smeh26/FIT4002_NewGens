using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyNursingFuture.Cms.Models;
using MyNursingFuture.BL.Managers;
using AutoMapper;
using System.Collections;
using System.Text;
using MyNursingFuture.Util;
using MyNursingFuture.DL;

namespace MyNursingFuture.Cms.Controllers
{
    public class DataExtractController : BaseController
    {
        private readonly IDataExtractManager _dataExtractManager;
        private readonly IMapper _mapper;

        public DataExtractController(IDataExtractManager dataExtractManager, IMapper mapper, ILogChangesManager logChangesManager) : base(logChangesManager)
        {
            _dataExtractManager = dataExtractManager;
            _mapper = mapper;
        }

        // GET: DataExtract
        public ActionResult Index()
        {
            ViewBag.DataExtractTypes = TypeHelper.GetAll<DataExtractType>().Select(x => new SelectListItem() { Text = x.Description, Value = x.Value.ToString() }).ToArray();

            return View("Index");
        }

        //Export to CSV file
        [HttpPost]
        public void ExportDataExtractToCSV(DataExtractModel model)
        {           
            StringWriter sw = new Utf8StringWriter();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Exported_Data.csv");
            Response.ContentType = "text/csv; charset=utf-8";

            string spName = "GetExtract";

            if (int.Parse(model.ExtractType) > 3)
            {
                spName = "GetAnonExtract";
                model.ExtractType = (int.Parse(model.ExtractType) - 3).ToString();
            }

            var result = _dataExtractManager.Get(spName, model.ExtractType, model.DateStart, model.DateEnd);
            var rows = (IEnumerable<IDictionary<string, object>>)result.Entity;
            
            if (rows != null)
            {
                foreach (var row in rows)
                {
                    foreach (var column in row)
                    {
                        sw.Write(string.Format("\"{0}\",", column.Key));
                    }
                    sw.WriteLine();
                    break;
                }

                foreach (var row in rows)
                {
                    foreach (var column in row)
                    {
                        if (column.Value != null && typeof(String) == column.Value.GetType())
                        {
                            sw.Write(string.Format("\"{0}\",", ((string)column.Value).Replace('’', '\'')));
                        } else
                        {
                            sw.Write(string.Format("\"{0}\",", column.Value));
                        }
                    }
                    sw.WriteLine();
                }
            }
            else
            {
                sw.WriteLine("Empty " + result.Success + " " + result.Message);
            }
            Response.Write(sw.ToString());

            Response.End();
        }
    }
}
