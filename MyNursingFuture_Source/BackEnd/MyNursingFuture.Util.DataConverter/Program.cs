using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.BL.Managers;
using MyNursingFuture.Util;



/*
 * This program is created for recontructing the database to the new format 
 */

namespace MyNursingFuture.Util.DataConverter
{
    class Program

    {

        static void Main(string[] args)
        {

            Console.WriteLine("//Reconstructing database //");
            FrameworkManager FM = new FrameworkManager();
            AspectsManager AM = new AspectsManager();

            var aspect_R = AM.Get();
            ObjectDumper.Dump(aspect_R.Entity);






        }
    }
}
