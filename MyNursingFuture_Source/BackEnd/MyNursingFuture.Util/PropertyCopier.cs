﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.Util
{
    /*
     * PropertyCopier code by Murat Aykanat @ PluralSight
     * https://www.pluralsight.com/guides/property-copying-between-two-objects-using-reflection
     */
    public class PropertyCopier<TParent, TChild> where TParent : class
                                            where TChild : class
    {
        public static void Copy(TParent parent, TChild child)
        {
            if (parent == null )
                return;
            var parentProperties = parent.GetType().GetProperties();
            var childProperties = child.GetType().GetProperties();

            foreach (var parentProperty in parentProperties)
            {
                foreach (var childProperty in childProperties)
                {
                    if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                    {
                        childProperty.SetValue(child, parentProperty.GetValue(parent));
                        break;
                    }
                }
            }
        }
    }
}
