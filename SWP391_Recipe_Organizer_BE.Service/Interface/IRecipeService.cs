﻿using SWP391_Recipe_Organizer_BE.Repo.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Recipe_Organizer_BE.Service.Interface
{
    public interface IRecipeService
    {
        Recipe Get(string id);
        IEnumerable<Recipe> GetAll();
        bool Add(Recipe item, List<Photo> photos, List<Direction> directions, List<IngredientOfRecipe> lstIngredientOfRecipes, List<NutritionInRecipe> lstNutritionInRecipes);
        bool Delete(string id);
        bool Update(Recipe item, List<Photo> photos, List<Direction> directions, List<IngredientOfRecipe> lstIngredientOfRecipes, List<NutritionInRecipe> lstNutritionInRecipes);
    }
}