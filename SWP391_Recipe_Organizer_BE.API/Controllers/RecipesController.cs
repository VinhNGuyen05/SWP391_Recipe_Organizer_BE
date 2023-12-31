﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP391_Recipe_Organizer_BE.API.ViewModel;
using SWP391_Recipe_Organizer_BE.Repo;
using SWP391_Recipe_Organizer_BE.Repo.EntityModel;
using SWP391_Recipe_Organizer_BE.Service.Interface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SWP391_Recipe_Organizer_BE.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService recipeService;
        private readonly IReviewService reviewService;
        private readonly IFavoriteRecipeService favoriteRecipeService;
        private readonly IUserAccountService userAccountService;
        private readonly IMapper mapper;
        public RecipesController(IRecipeService recipeService, IMapper mapper, IReviewService reviewService, IFavoriteRecipeService favoriteRecipeService, IUserAccountService userAccountService)
        {
            this.recipeService = recipeService;
            this.mapper = mapper;
            this.reviewService = reviewService;
            this.favoriteRecipeService = favoriteRecipeService;
            this.userAccountService = userAccountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var lst = recipeService.GetAll();
                var recipes = new List<RecipeVM>();
                foreach (var item in lst)
                {
                    var recipe = mapper.Map<RecipeVM>(item);
                    recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                    recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                    recipe.TotalReview = item.Reviews != null ? item.Reviews.Count() : 0;
                    recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                    recipe.TotalFavorite = item.FavoriteRecipes != null ? item.FavoriteRecipes.Count() : 0;
                    recipe.CountryVM = mapper.Map<CountryVM>(item.Country);
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        if (favoriteRecipeService.Get(recipe.RecipeId, userId) != null)
                        {
                            recipe.IsFavorite = true;
                        }
                        else
                        {
                            recipe.IsFavorite = false;
                        }
                    }
                    else
                    {
                        recipe.IsFavorite = false;
                    }
                    recipe.PhotoVMs = new List<PhotoVM>();
                    foreach (var photo in item.Photos)
                    {
                        recipe.PhotoVMs.Add(mapper.Map<PhotoVM>(photo));
                    }
                    recipe.DirectionVMs = new List<DirectionVM>();
                    //foreach (var direction in item.Directions)
                    //{
                    //    recipe.DirectionVMs.Add(mapper.Map<DirectionVM>(direction));
                    //}
                    recipe.ReviewVMs = new List<ReviewVM>();
                    //foreach (var review in item.Reviews)
                    //{
                    //    recipe.ReviewVMs.Add(mapper.Map<ReviewVM>(review));
                    //}
                    recipe.IngredientOfRecipeVMs = new List<IngredientOfRecipeVM>();
                    //foreach (var ingredientOfRecipe in item.IngredientOfRecipes)
                    //{
                    //    var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                    //    var ingredient = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                    //    ingredientOfRecipeVM.IngredientVM = ingredient;
                    //    recipe.IngredientOfRecipeVMs.Add(ingredientOfRecipeVM);
                    //}
                    //foreach (var nutritionInRecipe in item.NutritionInRecipes)
                    //{
                    //    var nutritionInRecipeVM = mapper.Map<NutritionInRecipeVM>(nutritionInRecipe);
                    //    var nutritionVM = mapper.Map<NutritionVM>(nutritionInRecipe.Nutrition);
                    //    nutritionInRecipeVM.NutritionVM = nutritionVM;
                    //    recipe.NutritionInRecipeVMs.Add(nutritionInRecipeVM);
                    //}
                    recipes.Add(recipe);
                }
                return Ok(new
                {
                    Status = 1,
                    Data = recipes
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetBestRecipes()
        {
            try
            {
                var lst = recipeService.GetAll();
                var recipes = new List<RecipeVM>();
                foreach (var item in lst)
                {
                    var recipe = mapper.Map<RecipeVM>(item);
                    recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                    recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                    recipe.TotalReview = item.Reviews != null ? item.Reviews.Count() : 0;
                    recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                    recipe.TotalFavorite = item.FavoriteRecipes != null ? item.FavoriteRecipes.Count() : 0;
                    recipe.CountryVM = mapper.Map<CountryVM>(item.Country);
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        if (favoriteRecipeService.Get(recipe.RecipeId, userId) != null)
                        {
                            recipe.IsFavorite = true;
                        }
                        else
                        {
                            recipe.IsFavorite = false;
                        }
                    }
                    else
                    {
                        recipe.IsFavorite = false;
                    }
                    recipe.PhotoVMs = new List<PhotoVM>();
                    foreach (var photo in item.Photos)
                    {
                        recipe.PhotoVMs.Add(mapper.Map<PhotoVM>(photo));
                    }
                    recipe.DirectionVMs = new List<DirectionVM>();
                    //foreach (var direction in item.Directions)
                    //{
                    //    recipe.DirectionVMs.Add(mapper.Map<DirectionVM>(direction));
                    //}
                    recipe.ReviewVMs = new List<ReviewVM>();
                    //foreach (var review in item.Reviews)
                    //{
                    //    recipe.ReviewVMs.Add(mapper.Map<ReviewVM>(review));
                    //}
                    recipe.IngredientOfRecipeVMs = new List<IngredientOfRecipeVM>();
                    //foreach (var ingredientOfRecipe in item.IngredientOfRecipes)
                    //{
                    //    var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                    //    var ingredient = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                    //    ingredientOfRecipeVM.IngredientVM = ingredient;
                    //    recipe.IngredientOfRecipeVMs.Add(ingredientOfRecipeVM);
                    //}
                    //foreach (var nutritionInRecipe in item.NutritionInRecipes)
                    //{
                    //    var nutritionInRecipeVM = mapper.Map<NutritionInRecipeVM>(nutritionInRecipe);
                    //    var nutritionVM = mapper.Map<NutritionVM>(nutritionInRecipe.Nutrition);
                    //    nutritionInRecipeVM.NutritionVM = nutritionVM;
                    //    recipe.NutritionInRecipeVMs.Add(nutritionInRecipeVM);
                    //}
                    recipes.Add(recipe);
                }
                return Ok(new
                {
                    Status = 1,
                    Data = recipes.OrderByDescending(x => x.AveVote).Take(6).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetMostFavoriteRecipes()
        {
            try
            {
                var lst = recipeService.GetAll();
                var recipes = new List<RecipeVM>();
                foreach (var item in lst)
                {
                    var recipe = mapper.Map<RecipeVM>(item);
                    recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                    recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                    recipe.TotalReview = item.Reviews != null ? item.Reviews.Count() : 0;
                    recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                    recipe.TotalFavorite = item.FavoriteRecipes != null ? item.FavoriteRecipes.Count() : 0;
                    recipe.CountryVM = mapper.Map<CountryVM>(item.Country);
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        if (favoriteRecipeService.Get(recipe.RecipeId, userId) != null)
                        {
                            recipe.IsFavorite = true;
                        }
                        else
                        {
                            recipe.IsFavorite = false;
                        }
                    }
                    else
                    {
                        recipe.IsFavorite = false;
                    }
                    recipe.PhotoVMs = new List<PhotoVM>();
                    foreach (var photo in item.Photos)
                    {
                        recipe.PhotoVMs.Add(mapper.Map<PhotoVM>(photo));
                    }
                    recipe.DirectionVMs = new List<DirectionVM>();
                    //foreach (var direction in item.Directions)
                    //{
                    //    recipe.DirectionVMs.Add(mapper.Map<DirectionVM>(direction));
                    //}
                    recipe.ReviewVMs = new List<ReviewVM>();
                    //foreach (var review in item.Reviews)
                    //{
                    //    recipe.ReviewVMs.Add(mapper.Map<ReviewVM>(review));
                    //}
                    recipe.IngredientOfRecipeVMs = new List<IngredientOfRecipeVM>();
                    //foreach (var ingredientOfRecipe in item.IngredientOfRecipes)
                    //{
                    //    var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                    //    var ingredient = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                    //    ingredientOfRecipeVM.IngredientVM = ingredient;
                    //    recipe.IngredientOfRecipeVMs.Add(ingredientOfRecipeVM);
                    //}
                    //foreach (var nutritionInRecipe in item.NutritionInRecipes)
                    //{
                    //    var nutritionInRecipeVM = mapper.Map<NutritionInRecipeVM>(nutritionInRecipe);
                    //    var nutritionVM = mapper.Map<NutritionVM>(nutritionInRecipe.Nutrition);
                    //    nutritionInRecipeVM.NutritionVM = nutritionVM;
                    //    recipe.NutritionInRecipeVMs.Add(nutritionInRecipeVM);
                    //}
                    recipes.Add(recipe);
                }
                return Ok(new
                {
                    Status = 1,
                    Data = recipes.OrderByDescending(x => x.TotalFavorite).Take(6).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var item = recipeService.Get(id);
                var recipe = mapper.Map<RecipeVM>(item);
                recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                recipe.TotalReview = item.Reviews != null ? item.Reviews.Count() : 0;
                recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                recipe.TotalFavorite = item.FavoriteRecipes != null ? item.FavoriteRecipes.Count() : 0;
                recipe.CountryVM = mapper.Map<CountryVM>(item.Country);
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    if (favoriteRecipeService.Get(recipe.RecipeId, userId) != null)
                    {
                        recipe.IsFavorite = true;
                    }
                    else
                    {
                        recipe.IsFavorite = false;
                    }
                }
                else
                {
                    recipe.IsFavorite = false;
                }

                recipe.PhotoVMs = item.Photos.Select(photo => mapper.Map<PhotoVM>(photo)).ToList();

                recipe.DirectionVMs = item.Directions
                    .Select(direction => mapper.Map<DirectionVM>(direction))
                    .OrderBy(x => x.DirectionsNum)
                    .ToList();

                recipe.ReviewVMs = new List<ReviewVM>();
                foreach (var review in item.Reviews)
                {
                    if (review.UserId == userId)
                    {
                        recipe.UserReview = mapper.Map<ReviewVM>(review);
                        recipe.UserReview.User = mapper.Map<UserAccountVM>(userAccountService.Get(recipe.UserReview.UserId));
                    }
                    else
                    {
                        recipe.ReviewVMs.Add(mapper.Map<ReviewVM>(review));
                        foreach (var reviewVM in recipe.ReviewVMs)
                        {
                            reviewVM.User = mapper.Map<UserAccountVM>(userAccountService.Get(reviewVM.UserId));
                        }
                    }
                }

                recipe.IngredientOfRecipeVMs = item.IngredientOfRecipes.Select(ingredientOfRecipe =>
                {
                    var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                    var ingredientVM = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                    ingredientOfRecipeVM.IngredientVM = ingredientVM;
                    return ingredientOfRecipeVM;
                }).ToList();

                return Ok(new
                {
                    Status = 1,
                    Data = recipe
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetRecipeUpdate(string id)
        {
            try
            {

                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.COOKER)
                    {
                        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var item = recipeService.Get(id);
                        if (item != null)
                        {
                            if (item.UserId == userId)
                            {
                                var recipe = mapper.Map<RecipeVM>(item);
                                recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                                recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                                recipe.TotalReview = item.Reviews != null ? item.Reviews.Count() : 0;
                                recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                                recipe.TotalFavorite = item.FavoriteRecipes != null ? item.FavoriteRecipes.Count() : 0;
                                recipe.CountryVM = mapper.Map<CountryVM>(item.Country);

                                recipe.PhotoVMs = item.Photos.Select(photo => mapper.Map<PhotoVM>(photo)).ToList();

                                recipe.DirectionVMs = item.Directions
                                    .Select(direction => mapper.Map<DirectionVM>(direction))
                                    .OrderBy(x => x.DirectionsNum)
                                    .ToList();

                                recipe.IngredientOfRecipeVMs = item.IngredientOfRecipes.Select(ingredientOfRecipe =>
                                {
                                    var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                                    var ingredientVM = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                                    ingredientOfRecipeVM.IngredientVM = ingredientVM;
                                    return ingredientOfRecipeVM;
                                }).ToList();

                                return Ok(new
                                {
                                    Status = 1,
                                    Message = "Success",
                                    Data = recipe
                                });
                            }
                        }
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Not Found",
                            Data = new { }
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Role Denied",
                            Data = new { }
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetByUser(string id)
        {
            try
            {
                var lst = recipeService.GetByCooker(id);
                var user = userAccountService.GetUserInfo(id);
                if (user == null || user.Role != 2)
                {
                    return NotFound();
                }
                var returnUser = user != null ? mapper.Map<UserAccountVM>(user) : null;
                var data = new List<RecipeVM>();
                foreach (var item in lst)
                {
                    var recipe = mapper.Map<RecipeVM>(item);
                    recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                    recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                    recipe.TotalReview = item.Reviews != null ? item.Reviews.Count() : 0;
                    recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                    recipe.TotalFavorite = item.FavoriteRecipes != null ? item.FavoriteRecipes.Count() : 0;
                    recipe.CountryVM = mapper.Map<CountryVM>(item.Country);
                    var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId != null)
                    {
                        if (favoriteRecipeService.Get(recipe.RecipeId, userId) != null)
                        {
                            recipe.IsFavorite = true;
                        }
                        else
                        {
                            recipe.IsFavorite = false;
                        }
                    }
                    else
                    {
                        recipe.IsFavorite = false;
                    }
                    recipe.PhotoVMs = new List<PhotoVM>();
                    foreach (var photo in item.Photos)
                    {
                        recipe.PhotoVMs.Add(mapper.Map<PhotoVM>(photo));
                    }
                    recipe.DirectionVMs = new List<DirectionVM>();
                    foreach (var direction in item.Directions)
                    {
                        recipe.DirectionVMs.Add(mapper.Map<DirectionVM>(direction));
                    }
                    recipe.ReviewVMs = new List<ReviewVM>();
                    foreach (var review in item.Reviews)
                    {
                        recipe.ReviewVMs.Add(mapper.Map<ReviewVM>(review));
                    }
                    recipe.IngredientOfRecipeVMs = new List<IngredientOfRecipeVM>();
                    foreach (var ingredientOfRecipe in item.IngredientOfRecipes)
                    {
                        var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                        var ingredient = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                        ingredientOfRecipeVM.IngredientVM = ingredient;
                        recipe.IngredientOfRecipeVMs.Add(ingredientOfRecipeVM);
                    }
                    data.Add(recipe);
                }
                return Ok(new
                {
                    Status = 1,
                    Data = data,
                    User = returnUser,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetByCooker()
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.COOKER)
                    {
                        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                        var lst = recipeService.GetByCooker(id);
                        var data = new List<RecipeVM>();
                        foreach (var item in lst)
                        {
                            var recipe = mapper.Map<RecipeVM>(item);
                            recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                            recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                            recipe.TotalReview = item.Reviews != null ? item.Reviews.Count() : 0;
                            recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                            recipe.CountryVM = mapper.Map<CountryVM>(item.Country);
                            recipe.TotalFavorite = item.FavoriteRecipes != null ? item.FavoriteRecipes.Count() : 0;
                            recipe.PhotoVMs = new List<PhotoVM>();
                            foreach (var photo in item.Photos)
                            {
                                recipe.PhotoVMs.Add(mapper.Map<PhotoVM>(photo));
                            }
                            recipe.DirectionVMs = new List<DirectionVM>();
                            foreach (var direction in item.Directions)
                            {
                                recipe.DirectionVMs.Add(mapper.Map<DirectionVM>(direction));
                            }
                            recipe.ReviewVMs = new List<ReviewVM>();
                            foreach (var review in item.Reviews)
                            {
                                recipe.ReviewVMs.Add(mapper.Map<ReviewVM>(review));
                            }
                            recipe.IngredientOfRecipeVMs = new List<IngredientOfRecipeVM>();
                            foreach (var ingredientOfRecipe in item.IngredientOfRecipes)
                            {
                                var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                                var ingredient = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                                ingredientOfRecipeVM.IngredientVM = ingredient;
                                recipe.IngredientOfRecipeVMs.Add(ingredientOfRecipeVM);
                            }
                            data.Add(recipe);
                        }
                        return Ok(new
                        {
                            Status = 1,
                            Data = data
                        });
                    }
                    return Ok(new
                    {
                        Status = 0,
                        Data = new { }
                    });
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeAddUpdateVM recipeVM)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.COOKER)
                    {
                        if (string.IsNullOrEmpty(recipeVM.RecipeName))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Recipe name cannot be empty!!!"
                            });
                        }
                        else
                        {
                            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                            var recipe = mapper.Map<Recipe>(recipeVM);
                            recipe.UserId = userId;
                            recipe.PrepTime = int.Parse(recipeVM.PrepTimeSt);
                            recipe.CookTime = int.Parse(recipeVM.CookTimeSt);
                            recipe.StandTime = int.Parse(recipeVM.StandTimeSt);
                            recipe.Carbohydrate = int.Parse(recipeVM.CarbohydrateSt);
                            recipe.Fat = int.Parse(recipeVM.FatSt);
                            recipe.Protein = int.Parse(recipeVM.ProteinSt);
                            recipe.Servings = int.Parse(recipeVM.ServingsSt);
                            recipe.TotalTime = (recipe.PrepTime != null ? recipe.PrepTime : 0)
                                + (recipe.CookTime != null ? recipe.CookTime : 0)
                                + (recipe.StandTime != null ? recipe.StandTime : 0);
                            recipe.IngredientOfRecipes.Clear();
                            recipe.Photos.Clear();
                            recipe.Directions.Clear();
                            var lstPhoto = new List<Photo>();
                            lstPhoto.Add(new Photo
                            {
                                PhotoName = recipeVM.PhotoVMs.PhotoName
                            });
                            var lstDirection = new List<Direction>();
                            if (recipeVM.DirectionVMs.Any())
                            {
                                int step = 1;
                                foreach (var direction in recipeVM.DirectionVMs)
                                {
                                    if (direction.DirectionsDesc != "")
                                    {
                                        lstDirection.Add(new Direction
                                        {
                                            DirectionsNum = step,
                                            DirectionsDesc = direction.DirectionsDesc
                                        });
                                        step++;
                                    }
                                }
                            }
                            var lstIngredientOfRecipes = new List<IngredientOfRecipe>();
                            if (recipeVM.IngredientOfRecipeVMs.Any())
                            {
                                foreach (var ingredientOfRecipe in recipeVM.IngredientOfRecipeVMs)
                                {
                                    if (ingredientOfRecipe.IngredientName.Contains(" - "))
                                    {
                                        lstIngredientOfRecipes.Add(new IngredientOfRecipe
                                        {
                                            Quantity = ingredientOfRecipe.Quantity,
                                            Ingredient = new Ingredient
                                            {
                                                IngredientName = ingredientOfRecipe.IngredientName.Split(" - ")[0].Trim()
                                            }
                                        });
                                    }
                                    else
                                    {
                                        lstIngredientOfRecipes.Add(new IngredientOfRecipe
                                        {
                                            Quantity = ingredientOfRecipe.Quantity,
                                            Ingredient = new Ingredient
                                            {
                                                IngredientName = ingredientOfRecipe.IngredientName.Trim()
                                            }
                                        });
                                    }
                                }
                            }
                            var check = await recipeService.AddAsync(recipe, lstPhoto, lstDirection, lstIngredientOfRecipes);
                            return check ? Ok(new
                            {
                                Status = 1,
                                Message = "Add Recipe Success"
                            }) : Ok(new
                            {
                                Status = 0,
                                Message = "Add Recipe Fail"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Role Denied",
                            Data = new { }
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRecipe(string id, RecipeAddUpdateVM recipeVM)
        {
            if (id != recipeVM.RecipeId)
            {
                return BadRequest();
            }

            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.COOKER)
                    {
                        if (string.IsNullOrEmpty(recipeVM.RecipeName))
                        {
                            return StatusCode(400, new
                            {
                                Message = "Recipe name cannot be empty!!!"
                            });
                        }
                        else
                        {
                            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                            var recipe = mapper.Map<Recipe>(recipeVM);
                            recipe.PrepTime = int.Parse(recipeVM.PrepTimeSt);
                            recipe.CookTime = int.Parse(recipeVM.CookTimeSt);
                            recipe.StandTime = int.Parse(recipeVM.StandTimeSt);
                            recipe.Carbohydrate = int.Parse(recipeVM.CarbohydrateSt);
                            recipe.Fat = int.Parse(recipeVM.FatSt);
                            recipe.Protein = int.Parse(recipeVM.ProteinSt);
                            recipe.Servings = int.Parse(recipeVM.ServingsSt);
                            recipe.TotalTime = (recipe.PrepTime != null ? recipe.PrepTime : 0)
                                + (recipe.CookTime != null ? recipe.CookTime : 0)
                                + (recipe.StandTime != null ? recipe.StandTime : 0);
                            var lstPhoto = new List<Photo>();
                            if (recipeVM.PhotoVMs != null)
                            {
                                lstPhoto.Add(new Photo
                                {
                                    PhotoName = recipeVM.PhotoVMs.PhotoName
                                });
                            }
                            var lstDirection = new List<Direction>();
                            if (recipeVM.DirectionVMs.Any())
                            {
                                int step = 1;
                                foreach (var direction in recipeVM.DirectionVMs)
                                {
                                    if (direction.DirectionsDesc != "")
                                    {
                                        lstDirection.Add(new Direction
                                        {
                                            DirectionsNum = step,
                                            DirectionsDesc = direction.DirectionsDesc
                                        });
                                        step++;
                                    }
                                }
                            }
                            var lstIngredientOfRecipes = new List<IngredientOfRecipe>();
                            if (recipeVM.IngredientOfRecipeVMs.Any())
                            {
                                foreach (var ingredientOfRecipe in recipeVM.IngredientOfRecipeVMs)
                                {
                                    if (ingredientOfRecipe.IngredientName.Contains(" - "))
                                    {
                                        lstIngredientOfRecipes.Add(new IngredientOfRecipe
                                        {
                                            Quantity = ingredientOfRecipe.Quantity,
                                            Ingredient = new Ingredient
                                            {
                                                IngredientName = ingredientOfRecipe.IngredientName.Split(" - ")[0].Trim()
                                            }
                                        });
                                    }
                                    else
                                    {
                                        lstIngredientOfRecipes.Add(new IngredientOfRecipe
                                        {
                                            Quantity = ingredientOfRecipe.Quantity,
                                            Ingredient = new Ingredient
                                            {
                                                IngredientName = ingredientOfRecipe.IngredientName.Trim()
                                            }
                                        });
                                    }
                                }
                            }
                            var check = recipeService.Update(recipe, lstPhoto, lstDirection, lstIngredientOfRecipes);
                            return check ? Ok(new
                            {
                                Status = 1,
                                Message = "Update Recipe Success"
                            }) : Ok(new
                            {
                                Status = 0,
                                Message = "Update Recipe Fail"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Role Denied",
                            Data = new { }
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            if (recipeService.Get(id) == null)
            {
                return BadRequest();
            }

            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(role))
                {
                    if (role == CommonValues.COOKER)
                    {
                        var check = recipeService.Delete(id);
                        return check ? Ok(new
                        {
                            Status = 1,
                            Message = "Delete Recipe Success"
                        }) : Ok(new
                        {
                            Status = 0,
                            Message = "Delete Recipe Fail"
                        });

                    }
                    else
                    {
                        return Ok(new
                        {
                            Status = 0,
                            Message = "Role Denied",
                            Data = new { }
                        });
                    }
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SearchRecipe([FromBody] RecipeSearch? recipeSearch)
        {
            try
            {
                var result = recipeService.SearchRecipe(
                    recipeSearch.RecipeName,
                    recipeSearch.CountryId,
                    recipeSearch.MealId,
                    recipeSearch.MinTotalTime,
                    recipeSearch.MaxTotalTime,
                    recipeSearch.MinServing,
                    recipeSearch.MaxServing);
                var data = new List<RecipeVM>();
                foreach (var item in result)
                {
                    var recipe = mapper.Map<RecipeVM>(item);
                    recipe.MealVMs = mapper.Map<MealVM>(item.Meal);
                    recipe.UserAccountVMs = mapper.Map<UserAccountVM>(item.User);
                    recipe.TotalReview = item.Reviews.Count();
                    recipe.AveVote = reviewService.GetAveReview(recipe.RecipeId);
                    recipe.TotalFavorite = item.FavoriteRecipes.Count();
                    recipe.CountryVM = mapper.Map<CountryVM>(item.Country);
                    recipe.PhotoVMs = new List<PhotoVM>();
                    foreach (var photo in item.Photos)
                    {
                        recipe.PhotoVMs.Add(mapper.Map<PhotoVM>(photo));
                    }
                    recipe.DirectionVMs = new List<DirectionVM>();
                    //foreach (var direction in item.Directions)
                    //{
                    //    recipe.DirectionVMs.Add(mapper.Map<DirectionVM>(direction));
                    //}
                    recipe.ReviewVMs = new List<ReviewVM>();
                    //foreach (var review in item.Reviews)
                    //{
                    //    recipe.ReviewVMs.Add(mapper.Map<ReviewVM>(review));
                    //}
                    recipe.IngredientOfRecipeVMs = new List<IngredientOfRecipeVM>();
                    //foreach (var ingredientOfRecipe in item.IngredientOfRecipes)
                    //{
                    //    var ingredientOfRecipeVM = mapper.Map<IngredientOfRecipeVM>(ingredientOfRecipe);
                    //    var ingredient = mapper.Map<IngredientVM>(ingredientOfRecipe.Ingredient);
                    //    ingredientOfRecipeVM.IngredientVM = ingredient;
                    //    recipe.IngredientOfRecipeVMs.Add(ingredientOfRecipeVM);
                    //}
                    data.Add(recipe);
                }
                return Ok(new
                {
                    Status = 1,
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SuggestRecipe([Required][MinLength(3)] string search)
        {
            try
            {
                if (!string.IsNullOrEmpty(search) && search.Length >= 3)
                {
                    var suggest = recipeService.GetSuggest(search);
                    return Ok(new
                    {
                        Status = 1,
                        Message = "Success",
                        Data = suggest
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Status = 0,
                        Message = "Fail",
                        Data = new { }
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
