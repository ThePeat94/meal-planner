using AutoMapper;
using backend.Dto;
using backend.Entity;
using backend.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Meal, MealDto>();
    cfg.CreateMap<MealDto, Meal>();
    cfg.CreateMap<Recipe, RecipeDto>();
    cfg.CreateMap<RecipeDto, Recipe>();
});

var mapper = mapperConfig.CreateMapper();
var mealRepository = new GenericEntityRepository<Meal>();
var recipeRepository = new GenericEntityRepository<Recipe>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meal Planner API", Description = "Creating and managing meals", Version = "V1"});
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MealPlanner API V1");
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/meals/{id}", (long id) => mapper.Map<MealDto>(mealRepository.GetById(id)));
app.MapGet("/meals", (long id) => mapper.Map<List<MealDto>>(mealRepository.GetAll()));
app.MapPost("/meals", (MealDto dto) => mapper.Map<MealDto>(mealRepository.Create(mapper.Map<Meal>(dto))));
app.MapDelete("/meals/{id}", (long id) => mapper.Map<MealDto>(mealRepository.Delete(id)));
app.MapPut("/meals", (MealDto update) => mapper.Map<MealDto>(mealRepository.Update(mapper.Map<Meal>(update))));

app.MapGet("/recipes/{id}", (long id) => mapper.Map<RecipeDto>(recipeRepository.GetById(id)));
app.MapGet("/recipes", (long id) => mapper.Map<List<RecipeDto>>(recipeRepository.GetAll()));
app.MapPost("/recipes", (RecipeDto dto) => mapper.Map<RecipeDto>(recipeRepository.Create(mapper.Map<Recipe>(dto))));
app.MapDelete("/recipes/{id}", (long id) => mapper.Map<RecipeDto>(recipeRepository.Delete(id)));
app.MapPut("/recipes", (RecipeDto update) => mapper.Map<RecipeDto>(recipeRepository.Update(mapper.Map<Recipe>(update))));

app.Run();
