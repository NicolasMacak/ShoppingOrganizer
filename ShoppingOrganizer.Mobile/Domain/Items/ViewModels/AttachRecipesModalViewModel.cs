using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoppingOrganizer.Database.Entities.Items;
using ShoppingOrganizer.Mobile.Domain.Items.Extensions;
using ShoppingOrganizer.Mobile.Domain.Items.Repositories;
using ShoppingOrganizer.Mobile.Shared.Helpers;
using ShoppingOrganizer.Models.Items;
using System.Collections.ObjectModel;
using static ShoppingOrganizer.Mobile.Shared.Constants;
using static ShoppingOrganizer.Models.Items.ItemAttachment;

namespace ShoppingOrganizer.Mobile.Domain.Items.Models.ViewModels;

[QueryProperty(nameof(MainRecipe), PropertyKeys.Recipe)]
public partial class AttachRecipesModalViewModel : ObservableObject
{
    private readonly IRecipeRepository _recipeRepository;
    private readonly IIngredientRepository _ingredientRepository;
    private readonly IRecipePartRepository _recipePartRepository;

    public AttachRecipesModalViewModel() 
    {
        _recipeRepository = ServiceHelper.GetService<IRecipeRepository>(); // injectorService
        _ingredientRepository = ServiceHelper.GetService<IIngredientRepository>();
        _recipePartRepository = ServiceHelper.GetService<IRecipePartRepository>();
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
    ObservableCollection<ItemAttachment> itemsAttachments = [];

    private Dictionary<int, (bool Initially, bool Currently)> _recipesAttachments = new();
    private Dictionary<int, (bool Initially, bool Currently)> _ingredientsAttachments = new();

    [RelayCommand]
    public async Task ToggleItemAttachment(ItemAttachment itemAttachment)
    {
        var alteredItemAttachment = itemAttachment.RecipeId.HasValue ?
            ToggleRecipeAttachment(itemAttachment.RecipeId.Value)
            : ToggleIngredientAttachment(itemAttachment.IngredientId!.Value);

        ItemsAttachments.UpdateItem(alteredItemAttachment);
    }

    /// <summary>
    /// Finds an Recipe in the <see cref="ItemsAttachments"/> and updates its <see cref="AttachmentState"/>
    /// </summary>
    /// <param name="recipeId"></param>
    /// <returns> Updated <see cref="ItemAttachment"/></returns>
    private ItemAttachment ToggleRecipeAttachment(int recipeId)
    {
        _recipesAttachments[recipeId] = (_recipesAttachments[recipeId].Initially, !_recipesAttachments[recipeId].Currently);

        ItemAttachment ItemAttachment = ItemsAttachments.Single(x => x.RecipeId == recipeId);
        ItemAttachment.Attachment = ResolveIngredientAttachmentstate(recipeId);

        return ItemAttachment;
    }

    /// <summary>
    /// Finds an Ingredient in the <see cref="ItemsAttachments"/> and updates its <see cref="AttachmentState"/>
    /// </summary>
    /// <param name="ingredientId"></param>
    /// <returns> Updated <see cref="ItemAttachment"/></returns>
    private ItemAttachment ToggleIngredientAttachment(int ingredientId)
    {
        _ingredientsAttachments[ingredientId] = (_ingredientsAttachments[ingredientId].Initially, _ingredientsAttachments[ingredientId].Currently);

        ItemAttachment ingredientAttachment = ItemsAttachments.Single(x => x.IngredientId == ingredientId);
        ingredientAttachment.Attachment = ResolveRecipeAttachmentState(ingredientId);

        return ingredientAttachment;
    }

    /// <summary>
    /// Returns new state of attachment for the Recipe
    /// </summary>
    private AttachmentState ResolveRecipeAttachmentState(int recipeId) {
        if (!_recipesAttachments.TryGetValue(recipeId, out (bool Initially, bool Currently) recipeAttachment))
        {
            return AttachmentState.Ignored;
        }

        bool AttachmentInitially = recipeAttachment.Initially;
        bool AttachmentCurrently = recipeAttachment.Currently; 

        return (AttachmentInitially, AttachmentCurrently) switch
        {
            (true, true) => AttachmentState.AlreadyAttached,
            (false, true) => AttachmentState.New,
            (true, false) => AttachmentState.Removed,
            _ => AttachmentState.Ignored
        };
    }

    /// <summary>
    /// Returns new state of attachment the Ingredient
    /// </summary>
    private AttachmentState ResolveIngredientAttachmentstate(int ingredientId) {
        if (!_ingredientsAttachments.TryGetValue(ingredientId, out (bool Initially, bool Currently) ingredientAttachment))
        {
            return AttachmentState.Ignored;
        }

        bool Attachmentinitially = ingredientAttachment.Initially;
        bool AttachmentCurrently = ingredientAttachment.Currently; 

        return (Attachmentinitially, AttachmentCurrently) switch
        {
            (true, true) => AttachmentState.AlreadyAttached,
            (false, true) => AttachmentState.New,
            (true, false) => AttachmentState.Removed,
            _ => AttachmentState.Ignored
        };
    }

    public async Task InitializeScreen()
    {
        await SetInitialStateForAttachmentHashsets();
        await SetAttachableItems();
    }

    /// <summary>
    /// All recipes except the main one are mapped to the <see cref="ItemAttachment"/> and added to the <see cref="ItemsAttachments"/>
    /// </summary>
    private async Task SetAttachableItems()
    {
        // ingredients
        (await _ingredientRepository.GetCollection()).ToList()
            .ForEach(x => {
                ItemAttachment itemAttachment = new() { IngredientId = x.Id, Title = x.Title, Attachment = ResolveIngredientAttachmentstate(x.Id) };
                ItemsAttachments.InsertItemAttachment(itemAttachment);
            });

        // recipes
        (await _recipeRepository.GetByFilter(x => x.Id != MainRecipe.Id))
            .ForEach(x => {
                ItemAttachment itemAttachment = new() { RecipeId = x.Id, Title = x.Title, Attachment = ResolveRecipeAttachmentState(x.Id) };
                ItemsAttachments.InsertItemAttachment(itemAttachment);
            });
    }

    private async Task SetInitialStateForAttachmentHashsets()
    {
        List<RecipePart> allMainRecipeRelations = await _recipePartRepository
            .GetByFilter(x => x.OwnerRecipeId == MainRecipe.Id);

        IEnumerable<int> initialIngredientRelations = allMainRecipeRelations
            .Where(x => x.Ingredientid != null)
            .Select(x => x.Ingredientid!.Value);

        IEnumerable<int> initialRecipeRelations = allMainRecipeRelations
            .Where(x => x.RecipeId != null)
            .Select(x => x.RecipeId!.Value);

        _ingredientsAttachments = initialIngredientRelations.ToDictionary(x => x, x => (true, true));
        _recipesAttachments = initialRecipeRelations.ToDictionary(x => x, x => (true, true));
    }

    public async Task SaveAssociations()
    {
        List<RecipePartEntity> recipePartsToAdd = [];

        List<int> recipesToRemove = [];
        List<int> ingredientsToRemove = [];

        foreach (var item in ItemsAttachments)
        {
            if(item.Attachment == AttachmentState.New)
            {
                recipePartsToAdd.Add(new RecipePartEntity { IngredientId = item.IngredientId, RecipeId = item.RecipeId, OwnerRecipeId = MainRecipe.Id, Title = item.Title });
            }
            else if(item.Attachment == AttachmentState.Removed)
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

        // detach recipes and ingredients from the main recipe
        await _recipePartRepository.Delete(x => x.OwnerRecipeId == MainRecipe.Id && (recipesToRemove.Contains(x.RecipeId!.Value) || ingredientsToRemove.Contains(x.IngredientId!.Value)));
        // todo  AddParts
    }

    /// <summary>
    /// Reverts attachments to the initial state
    /// </summary>
    public async Task ResetAssociations()
    {

    }
}
