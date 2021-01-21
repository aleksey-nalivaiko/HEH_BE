using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class CategoryServiceTests
    {
        private Task<Category> GetCategory()
        {
            Category category = new Category() { };
            return Task.FromResult(category);
        }
    }
}
