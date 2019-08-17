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
    class MetadataDesignerValidation
    {
        Dictionary<Type, List<PropertyInfo>> properties;

        
        [SetUp]
        public void Setup()
        {
            properties = new Dictionary<Type, List<PropertyInfo>>();
            Assembly assembly = Assembly.Load("VPC.Entities");

            foreach (Type type in assembly.GetTypes()
                 .Where(t => t.GetInterfaces().Contains(typeof(IItem<Item>))))
            {
                Type qualifiedType = Type.GetType(String.Format("{0},{1}", type.FullName, assembly.FullName));
                properties.Add(type, qualifiedType.GetProperties().ToList());
            }
        }

        [Test]
        public void Metadata_Validate_MissingDisplayname_Field()
        {
            var sb = new StringBuilder();
            foreach(var type in properties)
            {
                foreach (PropertyInfo prop in type.Value)
                {
                    object[] attrs = prop.GetCustomAttributes(true);

                    MemberInfo property = type.Key.GetProperty(prop.Name);
                    if (!Attribute.IsDefined(property, typeof(Metadata.Business.Entity.Configuration.DisplayNameAttribute)))
                    {
                        sb.AppendLine(String.Format("{0}, {1}", type.Key.Name, prop.Name));
                    }
                }
            }

            Assert.That(sb.ToString(), Is.Empty);
        }

        [Test]
        public void Metadata_Validate_FieldAccessibility()
        {
            var sb = new StringBuilder();
            foreach (var type in properties)
            {
                foreach (PropertyInfo prop in type.Value)
                {
                    object[] attrs = prop.GetCustomAttributes(true);

                    MemberInfo property = type.Key.GetProperty(prop.Name);
                    if (!Attribute.IsDefined(property, typeof(Metadata.Business.Entity.Configuration.AccessibleLayoutAttribute)))
                    {
                        sb.AppendLine(String.Format("{0}, {1}", type.Key.Name, prop.Name));
                    }
                }
            }

            Assert.That(sb.ToString(), Is.Empty);
        }

    }
}
