﻿using System.Collections.Generic;
using CsvLoader.Data.Repositories.Interfaces;

namespace CsvLoader.Data.Factories.Interfaces
{
    public interface IProductRepositoryFactory
    {
        IProductRepository CreateInstance();
        IEnumerable<IProductRepository> CreateInstances();
    }
}