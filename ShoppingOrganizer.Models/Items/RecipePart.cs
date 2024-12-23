﻿namespace ShoppingOrganizer.Models.Items;
public class RecipePart
{
    public int Id { get; set; }
    public int OwnerRecipeId {  get; set; }
    public int? RecipeId { get; set; }
    public int? Ingredientid { get; set; }
    public string Title { get; set; } = string.Empty;
    public double Quantity {  get; set; }
    public string? Description { get; set; }
}
