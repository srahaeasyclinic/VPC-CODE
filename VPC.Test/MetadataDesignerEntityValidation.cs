using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using NUnit.Framework;
using VPC.Metadata.Business.Entity.Infrastructure;
using VPC.Entities.EntityCore.Metadata.Runtime;
using System.Linq;

namespace VPC.Test
{
    [Category("Metadata Designer Validation")]
    class MetadataDesignerEntityValidation
    {
        List<Type> types;

        
        [SetUp]
        public void Setup()
        {
            types = new List<Type>();
            Assembly assembly = Assembly.Load("VPC.Entities");

            foreach (Type type in assembly.GetTypes()
                 .Where(t => t.GetInterfaces().Contains(typeof(IItem<Item>))))
            {
                types.Add(type);
            }
        }

        [Test]
        public void Metadata_Validate_MissingPluralname_Entity()
        {
            var sb = new StringBuilder();
            foreach(var type in types)
            {
               
                object[] attrs = type.GetCustomAttributes(true);
                if (!Attribute.IsDefined(type, typeof(Metadata.Business.Entity.Configuration.PluralNameAttribute)))
                {
                    sb.AppendLine(String.Format("{0}", type.Name));
                }
            }

            Assert.That(sb.ToString(), Is.Empty);
        }
    }
}
