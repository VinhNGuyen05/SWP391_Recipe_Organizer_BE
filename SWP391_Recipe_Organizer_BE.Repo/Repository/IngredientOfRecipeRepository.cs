﻿using SWP391_Recipe_Organizer_BE.Repo.DataAccess;
using SWP391_Recipe_Organizer_BE.Repo.EntityModel;
using SWP391_Recipe_Organizer_BE.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391_Recipe_Organizer_BE.Repo.Repository
{
    public class IngredientOfRecipeRepository : GenericRepository<IngredientOfRecipe>, IIngredientOfRecipeRepository
    {
        private readonly IIngredientRepository ingredientRepository;
        public IngredientOfRecipeRepository(RecipeOrganizerDBContext dBContext, IIngredientRepository ingredientRepository) : base(dBContext)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public bool Remove(string recipeId, string ingredientId)
        {
            try
            {
                var item = dbSet.Where(x => x.IngredientId == ingredientId && x.RecipeId == recipeId).FirstOrDefault();
                if (item != null)
                {
                    dbSet.Remove(item);
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveAll(string recipeId)
        {
            try
            {
                var items = dbSet.Where(x => x.RecipeId == recipeId).ToList();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        dbSet.Remove(item);
                    }
                    dBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddRange(List<IngredientOfRecipe> ingredientOfRecipes, string recipeId)
        {
            try
            {
                bool check = true;
                foreach (var ingredientOfRecipe in ingredientOfRecipes)
                {
                    var ingredient = ingredientRepository.Get(x => x.IngredientName == ingredientOfRecipe.Ingredient.IngredientName && x.IsDelete == false);
                    if (ingredient != null)
                    {
                        ingredientOfRecipe.RecipeId = recipeId;
                        ingredientOfRecipe.IngredientId = ingredient.IngredientId;
                        ingredientOfRecipe.Ingredient = ingredient;
                        var item = dbSet.Where(x => x.IngredientId == ingredientOfRecipe.IngredientId && x.RecipeId == recipeId).FirstOrDefault();
                        if (item == null)
                        {
                            dbSet.Add(ingredientOfRecipe);
                        }
                        else
                        {
                            dBContext.Attach(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                            dbSet.Update(ingredientOfRecipe);
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                dBContext.SaveChanges();
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
