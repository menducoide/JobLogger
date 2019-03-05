﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobLogger.DataAccessObjects;
using JobLogger.Data;
using JobLogger.IRepositories;
using JobLogger.IServices;

namespace JobLogger.Services
{
    public class TypeService : BaseService<DTOType, Data.Type>, ITypeService
    {
        public TypeService(ITypeRepository typeRepository)
        {
            this.repository = typeRepository;
        }

        public override List<DTOType> List()
        {
            var list = base.List();
            if (list.Count > 0)
                return list;

            list = new List<DTOType>() {
                new DTOType { Description = "Message"},
                new DTOType { Description = "Error"},
                new DTOType { Description = "Warning"},
            };
            var entities = repository.Insert(this.factoryEntity.CreateObject(list));
            list = this.factoryDTO.CreateObject(entities);
            return list;
        }

    }
}
