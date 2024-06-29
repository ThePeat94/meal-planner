using AutoMapper;
using Backend.Database;
using Backend.Dto;
using Backend.Entity;
using Backend.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var foo = "_MyFooCors";

var dbContext = new MealPlannerContext();

builder.Services.AddCors(options =>
{
    options.AddPolicy(foo, policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Meal, MealDto>();
    cfg.CreateMap<MealDto, Meal>();
    cfg.CreateMap<Recipe, RecipeDto>();
    cfg.CreateMap<RecipeDto, Recipe>();
});

var mapper = mapperConfig.CreateMapper();
var mealRepository = new MealRepository(dbContext, dbContext.Meals);
var recipeRepository = new GenericEntityDbRepository<Recipe>(dbContext, dbContext.Recipes);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Meal Planner API", Description = "Creating and managing meals", Version = "V1"});
});

var app = builder.Build();


app.UseCors(foo);
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MealPlanner API V1");
});

app.MapGet("/", () => "Hello World!");

app.MapGet("/meals/{id}", (long id) => mapper.Map<MealDto>(mealRepository.GetById(id)));
app.MapGet("/meals", () => mapper.Map<List<MealDto>>(mealRepository.GetAll()));
app.MapPost("/meals", (MealDto dto) => mapper.Map<MealDto>(mealRepository.Create(mapper.Map<Meal>(dto))));
app.MapDelete("/meals/{id}", (long id) => mapper.Map<MealDto>(mealRepository.Delete(id)));
app.MapPut("/meals", (MealDto update) => mapper.Map<MealDto>(mealRepository.Update(mapper.Map<Meal>(update))));

app.MapGet("/recipes/{id}", (long id) => mapper.Map<RecipeDto>(recipeRepository.GetById(id)));
app.MapGet("/recipes", () => mapper.Map<List<RecipeDto>>(recipeRepository.GetAll()));
app.MapPost("/recipes", (RecipeDto dto) => mapper.Map<RecipeDto>(recipeRepository.Create(mapper.Map<Recipe>(dto))));
app.MapDelete("/recipes/{id}", (long id) => mapper.Map<RecipeDto>(recipeRepository.Delete(id)));
app.MapPut("/recipes", (RecipeDto update) => mapper.Map<RecipeDto>(recipeRepository.Update(mapper.Map<Recipe>(update))));

app.Run();
