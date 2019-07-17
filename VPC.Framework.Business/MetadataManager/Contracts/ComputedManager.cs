using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using VPC.Entities.EntityCore.Model.Storage;
using VPC.Entities.Role;
using VPC.Framework.Business.MetadataManager.API;
using VPC.Metadata.Business.DataAnnotations;
using VPC.Metadata.Business.DataTypes;
using VPC.Metadata.Business.Entity.Infrastructure;

namespace VPC.Framework.Business.MetadataManager.Contracts {
    public interface IComputedManager {
        dynamic GetComputedField (string methodName, dynamic value);
    }

    public sealed class ComputedManager : IComputedManager {
        public ComputedManager () {

        }

        dynamic IComputedManager.GetComputedField (string methodName, dynamic value) {
            if (methodName.ToLower ().Equals ("agecalculation")) {
                var ageCalculation = new AgeCalculation ();
                return ageCalculation.GetAge (value);
            }
            return null;
        }
    }

}