using BLL.DTOS;
using BLL.Services;
using OpenAI_API;
using OpenAI_API.Completions;
using RecipeShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RecipeShare.App_Start
{
    public class RecipeController : ApiController
    {
        [HttpGet]
        [Route("api/recipe/getall")]
        public HttpResponseMessage Recipe()
        {
            try
            {
                var data = RecipeServices.Get();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/recipe/get/{id}")]
        public HttpResponseMessage Recipe(int id)
        {
            try
            {
                var data = RecipeServices.Get(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/recipe/create")]
        public IHttpActionResult CreateRecipe(RecipeDTO recipeDTO)
        {
            try
            {
                var success = RecipeServices.CreateRecipe(recipeDTO);
                if (success)
                    return Created(Request.RequestUri + "/" + recipeDTO.Id, "Recipe created successfully");
                else
                    return InternalServerError(new Exception("Failed to create Recipe"));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        [HttpPut]
        [Route("api/recipe/update/{id}")]
        public HttpResponseMessage UpdateRecipe(int id, RecipeDTO recipeDTO)
        {
            try
            {
                recipeDTO.Id = id;
                var updaterecipe = RecipeServices.UpdateRecipe(recipeDTO);
                return Request.CreateResponse(HttpStatusCode.OK, updaterecipe);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }


        [HttpDelete]
        [Route("api/recipe/delete/{id}")]
        public HttpResponseMessage DeleteRecipe(int id)
        {
            try
            {
                var operation = RecipeServices.DeleteRecipe(id);
                if (operation)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = " Recipe Deleted successfully." });
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Recipe not found." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("api/recipe/{id}/comments")]
        public HttpResponseMessage PostComments(int id)
        {
            try
            {
                var data = RecipeServices.GetwithComments(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        /*
        [HttpPost]
        [Route("api/recipe/getrecipe")]
        public IHttpActionResult GetResult([FromBody] string prompt)
        {
            string answer = string.Empty;
            var openai = new OpenAIAPI();
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = prompt;
            completion.Model = OpenAI_API.Model.DavinciText;
            var result = openai.Completions.CreateCompletionAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
                return Ok(answer);
            }
            else
            {
                return BadRequest("Not found");
            }
        }
        */
        

        
        [HttpPost]
        [Route("api/recipe/getrecipe")]
        public async Task<HttpResponseMessage> GetRecipeSuggestion([FromBody] IngredientModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Ingredients))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Ingredients are required.");
            }

            try
            {
                var suggestion = await GetRecipeSuggestion(input.Ingredients);
                return Request.CreateResponse(HttpStatusCode.OK, new { Suggestion = suggestion });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }


        public async Task<string> GetRecipeSuggestion(string ingredients)
        {
            var openai = new OpenAIAPI();

            string prompt = $"Suggest a recipe using the following ingredients: {ingredients}";

            try
            {
                var completionRequest = new CompletionRequest
                {
                    Prompt = prompt,
                    MaxTokens = 150
                };

                var completionResult = await openai.Completions.CreateCompletionAsync(completionRequest);

                if (completionResult?.Completions == null || !completionResult.Completions.Any())
                {
                    throw new Exception("No suggestions received from OpenAI API.");
                }

                return completionResult.Completions.First().Text.Trim();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error generating recipe suggestion: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating recipe suggestion: {ex.Message}");
            }
        }

    }
}

