using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Mobile.Domain.Items.Extensions;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Mobile.Infrastructure;
using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Mobile.Domain.Items.Constants;
using static ShoppingOrganizer.Models.Items.AttachedItem;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

/* Slovickarenie
 * AttachedItem. Relations
 * 
 */

[QueryProperty(nameof(MainRecipe), PropertyKeys.Recipe)]
public partial class AttachRecipesModalViewModel : ObservableObject
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IRecipePartRepository _recipePartRepository;

    private readonly ILogger<AttachRecipesModalViewModel> _logger;

    #region pragma
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
    public AttachRecipesModalViewModel()
    {
        _recipeRepository = PlatformServiceProvider.GetService<IRecipeRepository>();
        _ingredientRepository = PlatformServiceProvider.GetService<IIngredientRepository>();
        _recipePartRepository = PlatformServiceProvider.GetService<IRecipePartRepository>();
        _logger = PlatformServiceProvider.GetService<ILogger<AttachRecipesModalViewModel>>();
    }

    /// <summary>
    /// Recipe which associations are being modified
    /// </summary>
    [ObservableProperty]
    Recipe mainRecipe; // probably need only title and Id. Zredukovat 

    /// <summary>
    /// Recipes and ingredients that can be attached
    /// </summary>
    [ObservableProperty]
    ObservableCollection<AttachedItem> itemsAttachments = []; // todo. Attached Currently And initially here

    public async Task InitializeScreen()
    {
        await SetAttachableItems();
    }

    /// <summary>
    /// <see cref="AttachedItem.AttachedCurrently"/> of this item will be reverted and <see cref="AttachmentState"/> updated accordingly 
    /// </summary>
    [RelayCommand]
    public void ToggleAttachedItem(AttachedItem attachedItemToToggle)
    {
        attachedItemToToggle.ToggleAttachmentState();

        ItemsAttachments.UpdateItem(attachedItemToToggle);
    }

    [RelayCommand]
    public async Task Save() // todo. optimalization?, managing altered relations in the separate field
    {
        (List<int?> recipesToRemove, List<int?> ingredientsToRemove, List<RecipePartEntity> recipePartsToAdd) = GetAlteredRelations();

        if (recipesToRemove.Count > 0 || ingredientsToRemove.Count > 0)
        {
            await _recipePartRepository.Delete(x => x.OwnerRecipeId == MainRecipe.Id && (recipesToRemove.Contains(x.RecipeId) || ingredientsToRemove.Contains(x.IngredientId))); 
        }

        if(recipePartsToAdd.Count > 0)
        {
            await _recipePartRepository.Update(recipePartsToAdd);
        }
    }

    /// <summary>
    /// Gets relations that were added and removed+
    /// </summary>
    private (List<int?> recipesToRemove, List<int?> ingredientsToRemove, List<RecipePartEntity> recipePartsToAdd) GetAlteredRelations()
    {
        List<RecipePartEntity> recipePartsToAdd = new();
        List<int?> recipesToRemove = new(); // must be nullable. SQLite cannot parse expression 'nullableObject.Value'. Ex: x.RecipeId!.Value
        List<int?> ingredientsToRemove = new();

        foreach (AttachedItem item in ItemsAttachments)
        {
            if (item.State == AttachmentState.New)
            {
                recipePartsToAdd.Add(new RecipePartEntity { OwnerRecipeId = MainRecipe.Id, IngredientId = item.IngredientId, RecipeId = item.RecipeId, Title = item.Title });
            }
            else if (item.State == AttachmentState.Removed)
            {
                if (item.RecipeId.HasValue)
                {
                    recipesToRemove.Add(item.RecipeId.Value);
                }
                else
                {
                    ingredientsToRemove.Add(item.IngredientId!.Value);
                }
            }
        }

        return (recipesToRemove, ingredientsToRemove, recipePartsToAdd);
    }

    /// <summary>
    /// Reverts attachments to the initial state
    /// </summary>
    [RelayCommand]
    public void ResetRelations()
    {
        foreach (AttachedItem item in ItemsAttachments)
        {
            item.ResetToInitialState();
        }

        AttachedItem[] copy = new AttachedItem[ItemsAttachments.Count];
        ItemsAttachments.CopyTo(copy, 0);

        ItemsAttachments.ReplaceItems(copy);
    }

    /// <summary>
    /// All recipes except the main one are mapped to the <see cref="AttachedItem"/> and added to the <see cref="ItemsAttachments"/>
    /// </summary>
    private async Task SetAttachableItems()
    {
        (HashSet<int> initiallyAttachedRecipes, HashSet<int> initiallyAttachedIngredients) = await GetInitiallyAttachedItems();

        List<AttachedItem> initialAttachedItems = new();

        // ingredients
        List<Ingredient> allIngredients = await _ingredientRepository.GetAll();
        allIngredients
            .ForEach(x =>
            {
                AttachedItem itemAttachment = new(attachedInitially: initiallyAttachedIngredients.Contains(x.Id), title: x.Title)
                {
                    IngredientId = x.Id
                };
                initialAttachedItems.Add(itemAttachment);
            });

        // recipes
        List<Recipe> allRecipesExceptMainOne = await _recipeRepository.GetByFilter(x => x.Id != MainRecipe.Id);
        allRecipesExceptMainOne
            .ForEach(x =>
            {
                AttachedItem itemAttachment = new(attachedInitially: initiallyAttachedRecipes.Contains(x.Id), title: x.Title)
                {
                    RecipeId = x.Id
                };
                initialAttachedItems.Add(itemAttachment);
            });

        ItemsAttachments.ReplaceItems(initialAttachedItems);
      }

    /// <summary>
    /// Returns ids of initially attached recipes and ingredients
    /// </summary>
    private async Task<(HashSet<int> initiallyAttachedRecipes, HashSet<int> initiallyAttachedIngredients)> GetInitiallyAttachedItems()
    {
        List<RecipePart> initialyAttachedItems = await _recipePartRepository
            .GetByFilter(x => x.OwnerRecipeId == MainRecipe.Id);

        HashSet<int> initiallyAttachedRecipes = initialyAttachedItems
            .Where(x => x.RecipeId != null)
            .Select(x => x.RecipeId!.Value)
            .ToHashSet();

        HashSet<int> initiallyAttachedIngredients = initialyAttachedItems
            .Where(x => x.Ingredientid != null)
            .Select(x => x.Ingredientid!.Value)
            .ToHashSet();

        return (initiallyAttachedRecipes, initiallyAttachedIngredients);
    }
}