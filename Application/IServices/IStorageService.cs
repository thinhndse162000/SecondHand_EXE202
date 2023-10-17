﻿using Application.IServices.Base;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IStorageService: IGenericService<Storage>
    {
        Task<Storage> GetStorageByUserId(Guid id);
    }
}
