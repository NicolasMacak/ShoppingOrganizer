﻿using ShoppingOrganizer.Mobile.Core.Interfaces;
using ShoppingOrganizer.Models.Items;
using ShoppingOrganizer.Database.Entities.Items;

namespace ShoppingOrganizer.Mobile.Domain.Items.Repositories;
public interface IRecipeRepository : IBaseRepository<Recipe, RecipeEntity>
{
}

